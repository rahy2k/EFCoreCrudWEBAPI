﻿using AutoMapper;
using DbFirst;
using DbFirst.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductDataController : Controller
    {
        AppDBContext _db;
        IMapper _mapper;
        HttpClient client;
        IConfiguration configuration;
        public ProductDataController(IConfiguration configuration, AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            this.configuration = configuration;

            this.client = new HttpClient();
            client.BaseAddress = new Uri(configuration["ApiAddress"]);
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<ProductModel>data= new List<ProductModel>();
        //    var response = client.GetAsync(client.BaseAddress+"/product/getall").Result; //sync
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string strData = response.Content.ReadAsStringAsync().Result; //sync
        //        data = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(strData);
        //    }
        //    return View(data);
        //}

        IEnumerable<CategoryModel> GetCategories()
        {
            IEnumerable<CategoryModel> data = new List<CategoryModel>();
            var response = client.GetAsync(client.BaseAddress + "/category/getall").Result; //async
            if (response.IsSuccessStatusCode)
            {
                string strData = response.Content.ReadAsStringAsync().Result; //async
                data = JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(strData);
            }
            return data;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductModel> data = new List<ProductModel>();
            var response = await client.GetAsync(client.BaseAddress + "/product"); //async
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync(); //async
                data = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(strData);
            }
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");
            var response = client.PostAsync(client.BaseAddress + "/product", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories();
            return View();
        }
        public IActionResult Edit(int id)
        {
            ProductModel product = new ProductModel();
            var model = _db.Products.Find(id);
            string strData = JsonSerializer.Serialize(id);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");
            var response = client.PostAsync(client.BaseAddress + "/product/{id?}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories();
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
                string strData = JsonSerializer.Serialize(product);
                StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/product", content).Result;
                TempData["Message"] = "Record Has been updated!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = GetCategories();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
           
                string strData = JsonSerializer.Serialize(id);
                StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/product/{id?}", content).Result;
         
            return RedirectToAction("Index");
        }

    }
}
