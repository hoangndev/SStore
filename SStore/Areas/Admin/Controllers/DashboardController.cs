using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SStore.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> _userManager;

        public DashboardController()
        {
            db = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
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
        public PartialViewResult GetOrderByMonth()
        {
            /* Get 6 near months */
            DateTime thisMonth = DateTime.Now.AddMonths(-1);
            DateTime secondMonth = thisMonth.AddMonths(-1);
            DateTime thirtMonth = secondMonth.AddMonths(-1);
            DateTime fourthMonth = thirtMonth.AddMonths(-1);
            DateTime fifthMonth = thirtMonth.AddMonths(-1);
            DateTime SixthMonth = fifthMonth.AddMonths(-1);

            /* Get 6 months name */
            var ThisMonthName = DateTime.Now.ToString("MMMM");
            var secondMonthName = thisMonth.ToString("MMMM");
            var thirtMonthName = secondMonth.ToString("MMMM");
            var fourthMonthName = thirtMonth.ToString("MMMM");
            var fifthMonthName = fourthMonth.ToString("MMMM");
            var SixthMonthName = fifthMonth.ToString("MMMM");
            /* Pass months name to view */
            ViewBag.ThisMonthName = ThisMonthName;
            ViewBag.SecondMonthName = secondMonthName;
            ViewBag.ThirtMonthName = thirtMonthName;
            ViewBag.FourthMonthName = fourthMonthName;
            ViewBag.FifthMonthName = fifthMonthName;
            ViewBag.SixthMonthName = SixthMonthName;
            /* Get orders total value of each month */
            var thisMonthOrders = db.Orders.Where(u => u.OrderDate <= DateTime.Now && u.OrderDate >= thisMonth).ToList();
            var secondMonthOrders = db.Orders.Where(u => u.OrderDate <= thisMonth && u.OrderDate >= secondMonth).ToList();
            var thirtMonthOrders = db.Orders.Where(u => u.OrderDate <= secondMonth && u.OrderDate >= thirtMonth).ToList();
            var fourthMonthOrders = db.Orders.Where(u => u.OrderDate <= thirtMonth && u.OrderDate >= fourthMonth).ToList();
            var fifthMonthOrders = db.Orders.Where(u => u.OrderDate <= fourthMonth && u.OrderDate >= fifthMonth).ToList();
            var SixthMonthOrders = db.Orders.Where(u => u.OrderDate <= fifthMonth && u.OrderDate >= SixthMonth).ToList();
            /* Pass orders total value to view */
            ViewBag.ThisMonth = thisMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.SecondMonth = secondMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.ThirtMonth = thirtMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.FourthMonth = fourthMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.FifthMonth = fifthMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.SixthMonth = SixthMonthOrders.Sum(v => v.TotalPrice);
            return PartialView();
        }

        public PartialViewResult GetCounts()
        {
            DateTime MonthEndDate = DateTime.Now.AddMonths(-1);
            DateTime LastMonthEndDate = MonthEndDate.AddMonths(-1);

            var users = db.Users.ToList();
            var Customers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Customer"))
                {
                    Customers.Add(user);
                }
            }
            var thisMonthUser = db.UserInfos.Where(u => u.RegisterDate <= DateTime.Now && u.RegisterDate >= MonthEndDate).ToList();
            var lastMonthUser = db.UserInfos.Where(u => u.RegisterDate <= MonthEndDate && u.RegisterDate >= LastMonthEndDate).ToList();
            ViewBag.Users = Customers.Count();
            ViewBag.ThisMonthUsers = thisMonthUser.Count();
            ViewBag.LastMonthUsers = lastMonthUser.Count();
            var thisMonthOrders = db.Orders.Where(c => c.OrderDate <= DateTime.Now && c.OrderDate >= MonthEndDate).ToList();
            var lastMonthOrders = db.Orders.Where(c => c.OrderDate <= MonthEndDate && c.OrderDate >= LastMonthEndDate).ToList();
            ViewBag.ThisMonthOrders = thisMonthOrders.Count();
            ViewBag.LastMonthOrders = lastMonthOrders.Count();
            var thisMonthcurrency = db.Orders.Where(c => c.OrderDate <= DateTime.Now && c.OrderDate >= MonthEndDate).ToList();
            var lastMonthcurrency = db.Orders.Where(c => c.OrderDate <= MonthEndDate && c.OrderDate >= LastMonthEndDate).ToList();
            ViewBag.ThisMonthCurrency = thisMonthcurrency.Sum(c => c.TotalPrice);
            ViewBag.LastMonthCurrency = lastMonthcurrency.Sum(c => c.TotalPrice);
            var thisMonthProduct = db.Products.Where(c => c.CreatedDate <= DateTime.Now && c.CreatedDate >= MonthEndDate).ToList();
            var lastMonthProduct = db.Products.Where(c => c.CreatedDate <= MonthEndDate && c.CreatedDate >= LastMonthEndDate).ToList();
            ViewBag.Products = db.Products.Count();
            ViewBag.ThisMonthProduct = thisMonthProduct.Count();
            ViewBag.LastMonthProduct = lastMonthProduct.Count();
            return PartialView();
        }
    }
}