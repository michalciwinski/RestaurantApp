using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OpenAI;
using OpenAI.Images;
using RestaurantApp.Controllers;
using RestaurantApp.Entities;
using RestaurantApp.Exceptions;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;
using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO.Pipes;


namespace RestaurantApp.Services.Implementation
{
    public class MenuService : ControllerBase, IMenuService
    {
        private RestaurantDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
        public MenuService(RestaurantDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult GetDishes()
        {
            List<ModelMenu> dishes = new List<ModelMenu>();
            try
            {
                var dishesList = _context.Tmenus.Include(e => e.TdishType)
                                               .OrderBy(e => e.TdishTypeId)
                                               .ToList();
                dishesList.ForEach(row => dishes.Add(new ModelMenu()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Description = row.Description,
                    Price = row.Price,
                    DishType = row.TdishType.Name,
                    SrcPic = row.SrcPict
                }));
                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return BadRequest("Can't download information from database");
            }
        }

        public IActionResult GetDish(int id)
        {
            try
            {
                var record = _context.Tmenus.Include(e => e.TdishType)
                                        .FirstOrDefault(e => e.Id == id);
                ModelMenu dish = new ModelMenu
                {
                    Id = record.Id,
                    Name = record.Name,
                    Description = record.Description,
                    Price = record.Price,
                    DishType = record.TdishType.Name,
                    SrcPic = record.SrcPict
                };
                return Ok(dish);
            }
            catch (Exception ex)
            {
                return BadRequest("Can't download information from database");
            }

        }

        public IActionResult GetIngredientsOfDish(int id)
        {
            try
            {
                var dishesList = _context.TvMenuIngredients.Where(c => c.Id == id).Select(c => c.NameOfIngredient).ToList();
                return Ok(dishesList);
            }
            catch (Exception ex)
            {
                return BadRequest("Can't download information from database");
            }

        }

        public IActionResult DeleteDish(int id)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                        var menuToDelete = _context.Tmenus
                                                   .Include(e => e.TcompositionPositions) 
                                                   .FirstOrDefault(e => e.Id == id);
                        if (menuToDelete != null)
                        {
                            _context.Tmenus.Remove(menuToDelete);
                            _context.SaveChanges();
                        }

                        var compositionPositionsToDelete = _context.TcompositionPositions
                                                                   .Where(e => e.TmenuId == id)
                                                                   .ToList();
                        _context.TcompositionPositions.RemoveRange(compositionPositionsToDelete);
                        _context.SaveChanges();

                        string filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", menuToDelete.Name + ".png");
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        transaction.Commit();
                    }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Can't delete dish from database.");
            }
        }



        public IActionResult AddDish(ModelMenuWithPicture Dish, ModelListOfIngredients Ingredients) //Task because of await in stream
        {
        try
        {
            var checkDb = _context.Tmenus.Where(d => d.Name.Equals(Dish.Name) &&
                                d.Description.Equals(Dish.Description) &&
                                d.Price.Equals(Dish.Price)).FirstOrDefault();
            if (checkDb != null) 
            {
                throw new BadRequestException("Record exist in database");
            }

            //prepare path,name
            string imageName = Dish.Name + ".png";
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            string forDbPath = "https://localhost:7197/Images/" + imageName;

            //MENU TABLE
            Tmenu DishTableDB = new Tmenu
            {
                Id = Dish.Id,
                Name = Dish.Name,
                Description = Dish.Description,
                Price = Dish.Price,
                TdishTypeId = Dish.DishType,
                SrcPict = forDbPath
            };
            _context.Tmenus.Add(DishTableDB);
            _context.SaveChanges();

            //PICTURE DISH
            Task.Run(async () => //parallel task, to not block main thread
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await Dish.ImageFile.CopyToAsync(fileStream);
                } 
            });

            //TAKE ID OF NEW CREATED DISH
            var idMenuNew = _context.Tmenus.Where(d => d.Name.Equals(Dish.Name)).Select(c => c.Id).FirstOrDefault();

            //INGREDIENTS TABLE
            //check if we have got new ingredients - if yes, add them to table Tingredients
            var newIngredients = new List<string>();
            foreach (var el in Ingredients.Namee)
            {
                var tmp = _context.Tingredients.Where(d => d.NameOfIngredient.Equals(el)).Select(c => c.NameOfIngredient).ToList();
                if (tmp.Count == 0)
                {
                    newIngredients.Add(el);
                }

            }

            //ADD TO TABLE NEW INGREDIENTS - if exists new
            foreach (var el in newIngredients)
            {
                Tingredient TingredientTable = new Tingredient
                {
                    Id = 0,//autoincrement
                    NameOfIngredient = el
                };
                _context.Tingredients.Add(TingredientTable);
            }
            _context.SaveChanges();

            //ADD TO TABLE TCOMPOSITIONPOSITION NEW RECORDS
            foreach (var el in Ingredients.Namee)
            {
                var idIngredient = _context.Tingredients.Where(d => d.NameOfIngredient.Equals(el)).Select(c => c.Id).FirstOrDefault();
                TcompositionPosition tcompositionPosition = new TcompositionPosition
                {
                    Id = 0,//autoincrement
                    TmenuId = idMenuNew,
                    TingredientsId = idIngredient
                };
                _context.TcompositionPositions.Add(tcompositionPosition);
            }
            _context.SaveChanges();
            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest("Can't add dish to database");
        }
    }




        public IActionResult UpdateDish(ModelMenuToUpdate Dish)
        {
        var recordToUpdate = _context.Tmenus.Where(d => d.Name.Equals(Dish.Name) &&
                                d.Description.Equals(Dish.Description) &&
                                d.Price.Equals(Dish.Price)).FirstOrDefault();

        try
        {
            
            /*if (Dish.NewName != "")
            {
                recordToUpdate.Name = Dish.NewName;
            }
            if (Dish.NewDescription != "")
            {
                recordToUpdate.Description = Dish.NewDescription;
            }
            if (Dish.NewPrice != 0)
            {
                recordToUpdate.Price = Dish.NewPrice;
            }
            if (Dish.NewType != "")
            {
                int DishID;
                if (Dish.NewType == "Starter")
                    DishID = 1;
                else if (Dish.NewType == "Soup")
                    DishID = 2;
                else if (Dish.NewType == "Main course")
                    DishID = 3;
                else if (Dish.NewType == "Dessert")
                    DishID = 4;
                else if (Dish.NewType == "Drink")
                    DishID = 5;
                else
                    DishID = 0;
                recordToUpdate.TdishTypeId = DishID;
            }*/
            _context.SaveChanges();
            return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Can't add dish to database");
            }



        }





    }
}
