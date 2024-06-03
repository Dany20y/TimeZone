using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Time_Zome.BussinesLogic;
using Time_Zone.BussinessLogic;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Enums;

namespace Time_Zome.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISession _session;

        public AdminController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Admin
        public ActionResult Admin()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult ManageUsers()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
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
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(ULoginData user)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    if (!ModelState.IsValid)
                    {
                        foreach (var entry in ModelState)
                        {
                            if (entry.Value.Errors.Any())
                            {
                                foreach (var error in entry.Value.Errors)
                                {
                                    System.Diagnostics.Debug.WriteLine($"{entry.Key}: {error.ErrorMessage}");
                                }
                            }
                        }
                    }

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

                    var errorMessages = new List<string>();
                    foreach (var entry in ModelState)
                    {
                        foreach (var error in entry.Value.Errors)
                        {
                            errorMessages.Add($"{entry.Key}: {error.ErrorMessage}");
                        }
                    }

                    TempData["ErrorMessages"] = errorMessages;

                    var users = _session.GetAllUsers();
                    ViewBag.Users = users ?? new List<ULoginData>();
                    return View("ManageUsers");
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    _session.DeleteUser(id);
                    return RedirectToAction("ManageUsers");

                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public AdminController(ISession session)
        {
            _session = session;
        }

        public ActionResult Products()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    var products = _session.GetAllProductsIncludingCategories();
                    return View(products);
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CreateProduct()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    ViewBag.Categories = _session.GetAllCategories()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
                    return View();
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product, HttpPostedFileBase image)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var userSession = _session.GetSessionByCookie(token);
                var user = _session.GetUserByCookie(token);

                if (userSession != null && userSession.ExpireTime > DateTime.Now && user.Level == LevelAcces.Admin)
                {
                    if (ModelState.IsValid)
                    {
                        if (image != null && image.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                            image.SaveAs(path);
                            product.ImagePath = "http://localhost:56271/Images/Products/" + fileName; // Save the path in your product object
                        }

                        _session.CreateProduct(product);
                        return RedirectToAction("Products");
                    }
                    ViewBag.Categories = new SelectList(_session.GetAllCategories(), "Id", "Name");
                    return View(product);
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult EditProduct(int id)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
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
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(int id, Product product, HttpPostedFileBase image)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var userSession = _session.GetSessionByCookie(token);
                var user = _session.GetUserByCookie(token);

                if (userSession != null && userSession.ExpireTime > DateTime.Now && user.Level == LevelAcces.Admin)
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
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    _session.DeleteProduct(id);
                    return RedirectToAction("Products");
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult Categories()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    var categories = _session.GetAllCategories();
                    return View(categories);
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // Create Category - GET
        public ActionResult CreateCategory()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    return View();
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // Create Category - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    if (ModelState.IsValid)
                    {
                        _session.CreateCategory(category);
                        return RedirectToAction("Categories");
                    }
                    return View(category);
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // Edit Category - GET
        public ActionResult EditCategory(int id)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    var category = _session.GetCategoryById(id);
                    if (category == null)
                    {
                        return HttpNotFound();
                    }
                    return View(category);
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // Edit Category - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    if (ModelState.IsValid)
                    {
                        _session.UpdateCategory(category);
                        return RedirectToAction("Categories");
                    }
                    return View(category);
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // Delete Category
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var token = Request.Cookies["X-KEY"].Value;
                var UserSession = _session.GetSessionByCookie(token);
                var User = _session.GetUserByCookie(token);

                if (UserSession != null && UserSession.ExpireTime > DateTime.Now && User.Level == LevelAcces.Admin)
                {
                    _session.DeleteCategory(id);
                    return RedirectToAction("Categories");
                }
                else return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}