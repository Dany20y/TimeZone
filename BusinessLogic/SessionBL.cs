using eUseControl.BusinessLogic.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Time_Zone.BussinesLogic;
using Time_Zone.BussinessLogic.Core;
using Time_Zone.BussinessLogic.DBModel;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.Domain.Entities.User;

namespace Time_Zone.BussinesLogic
{

    public class SessionBL : UserApi, ISession
    {

        private readonly AdminApi _adminApi = new AdminApi();

        public ULoginResp UserLogin(UserLogin data)
        {
            return UserLoginAction(data);
        }
        public ULoginResp UserRegister(URegister data)
        {
            return UserRegisterAction(data);
        }


        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public Session GetSessionByCookie(string token)
        {
            return GetSession(token);
        }

        public void LogoutUserByCookie(string token, HttpContextBase httpContext)
        {
            LogoutUser(token, httpContext);
        }

        public bool ChangeUserPassword(string username, string oldPassword, string newPassword)
        {
            return UpdateUserPassword(username, oldPassword, newPassword);
        }

        public bool UpdateUser(ULoginData user)
        {
            var adminApi = new AdminApi();
            return adminApi.UpdateUserDetails(user);
        }

        public void DeleteUser(int userId)
        {
            var adminApi = new AdminApi();
            adminApi.DeleteUser(userId);
        }
        public List<ULoginData> GetAllUsers()
        {
            var adminApi = new AdminApi();
            return adminApi.FetchAllUsers();
        }

/*
        public List<Product> GetAllProducts()
        {
            return _adminApi.FetchAllProducts();
        }*/
        public List<Product> GetAllProducts()
        {
            using (var context = new ProductContext()) // Asigură-te că folosești contextul corect
            {
                return context.Products
                    .Include(p => p.Category) // Include categoria pentru fiecare produs
                    .ToList();
            }
        }


        public Product GetProductById(int productId)
        {
            return _adminApi.GetProductById(productId);
        }

        public bool CreateProduct(Product product)
        {
            return _adminApi.AddProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            return _adminApi.UpdateProductDetails(product);
        }

        public bool DeleteProduct(int productId)
        {
            return _adminApi.DeleteProduct(productId);
        }

        public List<Category> GetAllCategories()
        {
            return _adminApi.FetchAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _adminApi.GetCategoryById(categoryId);
        }

        public bool CreateCategory(Category category)
        {
            return _adminApi.AddCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            return _adminApi.UpdateCategoryDetails(category);
        }

        public bool DeleteCategory(int categoryId)
        {
            return _adminApi.DeleteCategory(categoryId);
        }

        public List<Product> GetAllProductsIncludingCategories()
        {
            return _adminApi.GetAllProductsWithCategories();
        }
    }
}

