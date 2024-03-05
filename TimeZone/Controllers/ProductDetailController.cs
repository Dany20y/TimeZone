using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeZone.BusinessLogic.Interfaces;
using TimeZone.BusinessLogic;
using TimeZone.Domain.Entities.Product;

namespace TimeZone.Controllers
{
    public class ProductDetailController : Controller
    {
        private IProduct _product;
        // GET: ProductDetail
        ProductDetailController() 
        { 
            BussinesLogic bussinesl = new BussinesLogic();

            _product = bussinesl.GetProductBL();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetProduct(int id)
        {
            ProductDetail productDetail = _product.GetProductDetail(id);
            return View();
        }
    }
}