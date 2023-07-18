using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Controllers;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RestaurantApp.Services
{
    public class MenuService 
    {
        private RestaurantDbContext _context;
        public MenuService(RestaurantDbContext context)
        {
            _context = context;
        }

        public List<MenuModel> GetDishes()
        {
            List <MenuModel> dishes = new List<MenuModel>();
            var dishesList = _context.TMenu.ToList();
            dishesList.ForEach(row => dishes.Add(new MenuModel()
            {
                Id = row.Id,
                Name = row.Name,
                Description = row.Description,
                Price = row.Price,
            })) ; 
            return dishes;
        }

        public MenuModel GetDish(int id)
        {
            var record = _context.TMenu.Find(id);
            MenuModel dish = new MenuModel
            {
                Id = record.Id,
                Name = record.Name,
                Description = record.Description,
                Price = record.Price
            };
            return dish;
        }

        public int DeleteDish(MenuModel Dish) 
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

        public int AddDish(MenuModel Dish)
        {
            var recordToAdd = _context.TMenu.Where(d => d.Name.Equals(Dish.Name) &&
                                    d.Description.Equals(Dish.Description) &&
                                    d.Price.Equals(Dish.Price)).FirstOrDefault();
            if (recordToAdd != null)
            {
                return 409;// already exist in db
            }
            else if (recordToAdd == null)
            {
                TMenu DishTableDB = new TMenu
                {
                    Name = Dish.Name,
                    Description = Dish.Description,
                    Price = Dish.Price
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

        public int UpdateDish(MenuModelToUpdate Dish)
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
