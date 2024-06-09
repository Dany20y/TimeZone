using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Time_Zone.BussinesLogic;
using Time_Zone.BussinessLogic;
using Time_Zone.BussinessLogic.DBModel;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.Models;
using Time_Zone.BusinessLogic;

namespace Time_Zone.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ISession _session;

        public CartController()
        {
            var bl = new BusinessLogicService();
            _session = bl.GetSessionBL();
            _cartService = new CartService(new ProductContext());
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int id, int quantity)
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "Please log in to add products to the cart.";
                return RedirectToAction("Login", "Login");
            }

            var product = _session.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            string userId = Session["Username"].ToString();
            await _cartService.AddToCart(id, userId, quantity);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Index()
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "Please log in to view your cart.";
                return RedirectToAction("Login", "Login");
            }

            string userId = Session["Username"].ToString();
            var cartLines = await _cartService.GetUserCart(userId);

            ViewBag.CartCount = cartLines.Sum(item => item.Quantity);

            var cartViewModel = new Cart
            {
                Lines = cartLines
            };

            return View(cartViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "Please log in to remove products from the cart.";
                return RedirectToAction("Login", "Login");
            }

            string userId = Session["Username"].ToString();
            await _cartService.RemoveFromCart(id, userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateQuantity(int id, int quantity)
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "Please log in to update the quantity of products in the cart.";
                return RedirectToAction("Login", "Login");
            }

            string userId = Session["Username"].ToString();
            var cartLines = await _cartService.GetUserCart(userId);
            var lineToUpdate = cartLines.FirstOrDefault(cl => cl.ProductId == id);

            if (lineToUpdate != null)
            {
                lineToUpdate.Quantity = quantity;
                await _cartService.UpdateCart(lineToUpdate);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProductCheckout(decimal? subtotal)
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            decimal validSubtotal = subtotal ?? 0m;

            // Verifică valoarea primitului subtotal
            System.Diagnostics.Debug.WriteLine($"Subtotal primit: {validSubtotal}");

            // Setează subtotalul în ViewBag
            ViewBag.Subtotal = validSubtotal;
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

            // Procesare checkout

            return RedirectToAction("SuccessPayment");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            if (Session["Username"] == null)
            {
                ViewBag.CartCount = 0;
                return PartialView("_CartSummary");
            }

            string userId = Session["Username"].ToString();
            var cartLines = _cartService.GetUserCart(userId).Result;
            ViewBag.CartCount = cartLines.Sum(item => item.Quantity);
            return PartialView("_CartSummary");
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
