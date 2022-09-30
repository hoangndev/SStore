using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SStore.Models;

namespace SStore.Controllers
{
    public class FeedbackClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Feedback/Create
        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                feedback.CreateDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(feedback);
        }
    }
}