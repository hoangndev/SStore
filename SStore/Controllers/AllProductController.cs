using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace SStore.Controllers
{
    public class AllProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AllProduct
        public ActionResult Index(int? page, string searchString, string BrandFilter, string CategoryFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var allProduct = db.Products.Include(p => p.productBrand).OrderBy(p => p.ProductName).Include(p => p.ProductCategory);

            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;
            var categories = db.ProductCategories;
            ViewBag.ListCategory = categories;
            if (!String.IsNullOrEmpty(searchString))
            {
                allProduct = allProduct.Where(p => p.ProductName.Contains(searchString));
                if (allProduct.Count() > 0)
                {
                    return View(allProduct.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if (!String.IsNullOrEmpty(BrandFilter))
            {
                allProduct = allProduct.Where(p => p.productBrand.BrandName.Equals(BrandFilter));
                return View(allProduct.ToPagedList(pageNumber, pageSize));
            }
            else if (!String.IsNullOrEmpty(CategoryFilter))
            {
                allProduct = allProduct.Where(p => p.ProductCategory.CategoryName.Equals(CategoryFilter));
                return View(allProduct.ToPagedList(pageNumber, pageSize));
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                allProduct = allProduct.Where(p => p.Price >= LowPrice && p.Price <= HighPrice);
                return View(allProduct.ToPagedList(pageNumber, pageSize));
            }
            return View(allProduct.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Shoes()
        {
            var Shoes = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Shoes")).OrderBy(p => p.ProductName).ToList();
            return View(Shoes);
        }
        public ActionResult Shirt()
        {
            var Shirt = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Shirt")).OrderBy(p => p.ProductName).ToList();
            return View(Shirt);
        }
        public ActionResult Hat()
        {
            var Hat = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Hat")).OrderBy(p => p.ProductName).ToList();
            return View(Hat);
        }
        public ActionResult Accessories()
        {
            var Accessories = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Accessories")).OrderBy(p => p.ProductName).ToList();
            return View(Accessories);
        }
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).SingleOrDefault(p => p.Id == id);
            product.View++;
            db.SaveChanges();
            if (product == null)
            {
                return RedirectToAction("NotFound");
            }
            return View(product);
        }
    }
}