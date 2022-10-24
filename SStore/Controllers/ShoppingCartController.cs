using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string Cart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[Cart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Products.Find(id),1)
                };
                Session[Cart] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[Cart];
                int check = isExisting(id);
                if (check == -1)
                    lsCart.Add(new Cart(db.Products.Find(id), 1));
                else
                    lsCart[check].Quantity++;
                Session[Cart] = lsCart;
            }
            return View("Index");
        }
        private int isExisting(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Product.Id == id) return i;
            }
            return -1;
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = isExisting((id));
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            lsCart.RemoveAt(check);
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCart(FormCollection formCollection)
        {
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            int productId = int.Parse(formCollection["productId"]);
            int quantity = int.Parse(formCollection["Quantity"]);
            var item = lsCart.Find(s => s.Product.Id == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            };

            Session[Cart] = lsCart;
            return RedirectToAction("Index");
        }
    }
}
