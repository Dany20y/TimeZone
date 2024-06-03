using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.BussinessLogic;
using Time_Zome.BussinesLogic;

namespace Time_Zome.Controllers
{
    public class CartController : Controller
    {
        private ISession _session;

        public CartController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }



    [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = _session.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
            }

            cart.AddItem(product, quantity); // Use the specified quantity
            Session["Cart"] = cart;

            return RedirectToAction("Index", "Cart"); // Redirect to the Cart index action
        }

        public ActionResult Index()
        {
            var cart = Session["Cart"] as Cart;
            ViewBag.CartCount = cart?.Lines.Sum(item => item.Quantity) ?? 0;
            return View(cart);
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            cart.RemoveItem(id);
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProductCheckout(decimal? subtotal)
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            ViewBag.Subtotal = subtotal ?? 0m;
            return View(cart);
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            string firstName = form["first_name"];
            string lastName = form["last_name"];
            string company = form["company"];
            string phone = form["phone"];
            string email = form["email"];
            string country = form["country"];
            string address1 = form["address1"];
            string address2 = form["address2"];
            string city = form["city"];
            string district = form["district"];
            string zip = form["zip"];
            bool createAccount = form["create_account"] == "on";
            bool shipToDifferent = form["ship_to_different"] == "on";
            string orderNotes = form["order_notes"];
            string paymentMethod = form["payment_method"];
            bool termsAccepted = form["terms"] == "on";

            decimal subtotalValue = Convert.ToDecimal(form["subtotal"] ?? "0");

            return RedirectToAction("SuccessPayment");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            ViewBag.CartCount = cart.Lines.Sum(item => item.Quantity);
            return PartialView("_CartSummary");
        }

        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            var line = cart.Lines.FirstOrDefault(x => x.Product.Id == id);
            if (line != null)
            {
                line.Quantity = quantity;
            }
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
