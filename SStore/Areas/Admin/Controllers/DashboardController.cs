using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace SStore.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Dashboard
        public ActionResult Index()
        {

            var products = db.Products.Take(5).OrderByDescending(p => p.View);
            var listProducts = new List<Product>();
            foreach (var product in products)
            {
                listProducts.Add(product);
            }

            return View(listProducts);
        }

        public PartialViewResult GetProductView()
        {
            var products = db.Products.Take(6).OrderByDescending(p => p.View);
            var listProducts = new List<Product>();
            foreach (var product in products)
            {
                listProducts.Add(product);
            }
            return PartialView(listProducts);
        }

    }
}