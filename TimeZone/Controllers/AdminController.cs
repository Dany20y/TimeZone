using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Time_Zone.Controllers;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Enums;
using Time_Zone.BussinesLogic;
using Time_Zone.Models;

namespace Time_Zone.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ISession _session;

        public AdminController() : base()
        {
            var bl = new Time_Zone.BusinessLogic.BusinessLogicService();
            _session = bl.GetSessionBL();
        }

        public ActionResult Admin()
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ManageUsers()
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                var model = _session.GetAllUsers();
                ViewBag.Users = model ?? new List<ULoginData>();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(ULoginData user)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                if (ModelState.IsValid)
                {
                    bool updateResult = _session.UpdateUser(user);
                    if (updateResult)
                    {
                        TempData["Message"] = "User updated successfully.";
                        return RedirectToAction("ManageUsers");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update user.");
                    }
                }
                ViewBag.Users = _session.GetAllUsers() ?? new List<ULoginData>();
                return View("ManageUsers");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                _session.DeleteUser(id);
                return RedirectToAction("ManageUsers");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Products()
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                var products = _session.GetAllProductsIncludingCategories();
                return View(products);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CreateProduct()
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                ViewBag.Categories = _session.GetAllCategories()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product, HttpPostedFileBase image)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        image.SaveAs(path);
                        product.ImagePath = "http://localhost:56271/Images/Products/" + fileName;
                    }

                    _session.CreateProduct(product);
                    return RedirectToAction("Products");
                }
                ViewBag.Categories = new SelectList(_session.GetAllCategories(), "Id", "Name");
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult EditProduct(int id)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                var product = _session.GetProductById(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Categories = _session.GetAllCategories()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(int id, Product product, HttpPostedFileBase image)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        image.SaveAs(path);
                        product.ImagePath = "~/Images/" + fileName;
                    }

                    _session.UpdateProduct(product);
                    return RedirectToAction("Products");
                }
                ViewBag.Categories = new SelectList(_session.GetAllCategories(), "Id", "Name", product.CategoryId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                _session.DeleteProduct(id);
                return RedirectToAction("Products");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Categories()
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                var categories = _session.GetAllCategories();
                return View(categories);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CreateCategory()
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                if (ModelState.IsValid)
                {
                    _session.CreateCategory(category);
                    return RedirectToAction("Categories");
                }
                return View(category);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult EditCategory(int id)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                var category = _session.GetCategoryById(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                if (ModelState.IsValid)
                {
                    _session.UpdateCategory(category);
                    return RedirectToAction("Categories");
                }
                return View(category);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            string sessionStatus = SessionStatus();
            if (sessionStatus == "Admin")
            {
                _session.DeleteCategory(id);
                return RedirectToAction("Categories");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
