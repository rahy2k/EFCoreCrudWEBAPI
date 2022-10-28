using AutoMapper;
using DbFirst;
using DbFirst.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        AppDBContext _db;
        IMapper _mapper;
        public ProductController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // LINQ Syntax
            //Method based syntax
            //var products = _db.Products.Where(p => p.ProductId > 0).ToList();

            //Query based syntax
            //var products = (from prd in _db.Products
            //                where prd.ProductId > 0
            //                select prd).ToList();

            var products = await (from prd in _db.Products
                                  join cat in _db.Categories
                                  on prd.CategoryId equals cat.CategoryId //inner join condition
                                  select new ProductModel
                                  {
                                      ProductId = prd.ProductId,
                                      Name = prd.Name,
                                      Description = prd.Description,
                                      UnitPrice = prd.UnitPrice,
                                      CategoryId = prd.CategoryId,
                                      CategoryName = cat.Name
                                  }).ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            ModelState.Remove("ProductId");

            if (ModelState.IsValid)
            {
                //Product product = new Product
                //{
                //    Name = model.Name,
                //    Description = model.Description,
                //    UnitPrice = model.UnitPrice,
                //    CategoryId = model.CategoryId
                //};

                Product product = _mapper.Map<Product>(model);
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _db.Categories.ToList();
            ProductModel product = new ProductModel();
            var model = _db.Products.Find(id);
            if (model != null)
            {
                product = _mapper.Map<ProductModel>(model);

                //product.ProductId = model.ProductId;
                //product.Name = model.Name;
                //product.Description = model.Description;
                //product.UnitPrice = model.UnitPrice;
                //product.CategoryId = model.CategoryId;
            }
            return View("Create", product);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    ProductId = model.ProductId,
                    Name = model.Name,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId
                };
                _db.Products.Update(product);
                _db.SaveChanges();
                TempData["Message"] = "Record Has been updated!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _db.Categories.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            var model = _db.Products.Find(id);
            if (model != null)
            {
                _db.Products.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
