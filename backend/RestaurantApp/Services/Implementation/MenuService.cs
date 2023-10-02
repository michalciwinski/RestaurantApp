using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Controllers;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RestaurantApp.Services.Implementation
{
    public class MenuService : IMenuService
    {
        private RestaurantDbContext _context;
        public MenuService(RestaurantDbContext context)
        {
            _context = context;
        }

        public List<ModelMenu> GetDishes()
        {
            List<ModelMenu> dishes = new List<ModelMenu>();
            var dishesList = _context.TMenu.Include(e => e.TDishType)
                                           .OrderBy(e => e.TDishTypeId)
                                           .ToList();
            dishesList.ForEach(row => dishes.Add(new ModelMenu()
            {
                Id = row.Id,
                Name = row.Name,
                Description = row.Description,
                Price = row.Price,
                DishType = row.TDishType.Name,
            }));
            return dishes;
        }
        //TO DO - else..
        public ModelMenu GetDish(int id)
        {
            var record = _context.TMenu.Include(e => e.TDishType)
                                        .FirstOrDefault(e => e.Id == id);
            if (record != null)
            {
                ModelMenu dish = new ModelMenu
                {
                    Id = record.Id,
                    Name = record.Name,
                    Description = record.Description,
                    Price = record.Price,
                    DishType = record.TDishType.Name
                };
                return dish;
            }
            else
            {
                ModelMenu dish = new ModelMenu
                {
                    Id = 0,
                    Name = "empty",
                    Description = "empty",
                    Price = 0,
                    DishType = "empty",
                };
                return dish;
            }


        }

        public int DeleteDish(ModelMenu Dish)
        {
            var recordToDelete = _context.TMenu.Where(d => d.Name.Equals(Dish.Name) &&
                                    d.Description.Equals(Dish.Description) &&
                                    d.Price.Equals(Dish.Price)).FirstOrDefault();
            if (recordToDelete != null)
            {
                _context.TMenu.Remove(recordToDelete);
                _context.SaveChanges();
                return 200;
            }
            else
            {
                return 404;//not found in db
            }
        }

        public int AddDish(ModelMenu Dish)
        {
            var checkDb = _context.TMenu.Where(d => d.Name.Equals(Dish.Name) &&
                                    d.Description.Equals(Dish.Description) &&
                                    d.Price.Equals(Dish.Price)).FirstOrDefault();
            if (checkDb != null)
            {
                return 409;// already exist in db
            }
            else if (checkDb == null)
            {
                int DishID;
                if (Dish.DishType == "Starter")
                    DishID = 1;
                else if (Dish.DishType == "Soup")
                    DishID = 2;
                else if (Dish.DishType == "Main course")
                    DishID = 3;
                else if (Dish.DishType == "Dessert")
                    DishID = 4;
                else if (Dish.DishType == "Drink")
                    DishID = 5;
                else
                    DishID = 0;
                TMenu DishTableDB = new TMenu
                {
                    Name = Dish.Name,
                    Description = Dish.Description,
                    Price = Dish.Price,
                    TDishTypeId = DishID
                };
                _context.TMenu.Add(DishTableDB);
                _context.SaveChanges();
                return 200;
            }
            else
            {
                return 404;//other problem
            }

        }

        public int UpdateDish(ModelMenuToUpdate Dish)
        {
            var recordToUpdate = _context.TMenu.Where(d => d.Name.Equals(Dish.Name) &&
                                    d.Description.Equals(Dish.Description) &&
                                    d.Price.Equals(Dish.Price)).FirstOrDefault();

            try
            {
                if (Dish.NewName != "")
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
                    recordToUpdate.TDishTypeId = DishID;
                }
                _context.SaveChanges();
                return 200;
            }
            catch
            {


                return 409;
            }



        }





    }
}
