﻿using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SStore.Models;

namespace SStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        // GET: /Product
        public PartialViewResult NewProductListPartial()
        {
            var newProductList = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.BrandId.Equals(1)).ToList();
            return PartialView(newProductList);
        }

        public PartialViewResult HotProductListPartial()
        {
            var hotProductList = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.BrandId.Equals(2)).ToList();
            return PartialView(hotProductList);
        }
        public PartialViewResult FashionProductListPartial()
        {
            var fashionProductList = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.BrandId.Equals(1)).OrderByDescending(p => p.Id).ToList();
            return PartialView(fashionProductList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}