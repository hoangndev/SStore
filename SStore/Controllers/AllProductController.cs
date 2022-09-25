using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

namespace SStore.Controllers
{
    public class AllProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AllProduct
        public ActionResult Index(string searchProduct)
        {
            var allProduct = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory);
            if (!String.IsNullOrEmpty(searchProduct))
            {
                allProduct = allProduct.Where(p => p.ProductName.Contains(searchProduct));
                if (allProduct.Count() > 0)
                {
                    return View(allProduct.ToList());
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            return View(allProduct.ToList());
        }
        public ActionResult Shoes()
        {
            var Shoes = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Shoes")).ToList();
            return View(Shoes);
        }
        public ActionResult Shirt()
        {
            var Shirt = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Shirt")).ToList();
            return View(Shirt);
        }
        public ActionResult Hat()
        {
            var Hat = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Hat")).ToList();
            return View(Hat);
        }
        public ActionResult Accessories()
        {
            var Accessories = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Accessories")).ToList();
            return View(Accessories);
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}