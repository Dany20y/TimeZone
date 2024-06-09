using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Time_Zone.BussinesLogic;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Models;

public class ProductsController : Controller
{
    private readonly ISession _session;

    public ProductsController()
    {
        _session = new SessionBL(); // Instanțiază SessionBL care folosește baza de date
    }

    public ActionResult Shop()
    {
        var products = _session.GetAllProducts();

        // Construiește modelul pentru vizualizare
        var model = new ProductsViewModel
        {
            Products = products,
            Categories = products.Select(p => p.Category?.Name ?? "Unknown").Distinct().ToList()
        };

        // Returnează vizualizarea cu modelul
        return View("~/Views/Home/Shop.cshtml", model); // Asigură-te că locația vizualizării este corectă
    }

}
