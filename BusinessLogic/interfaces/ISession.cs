using Time_Zone.Domain.Entities.User;
using System.Web;
using System.Collections.Generic;
using Time_Zone.Domain.Entities.Product;

namespace Time_Zone.BussinesLogic
{
    public interface ISession
    {
        ULoginResp UserLogin(UserLogin data);
        ULoginResp UserRegister(URegister data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
        Session GetSessionByCookie(string token);
        void LogoutUserByCookie(string token, HttpContextBase httpContext);
        bool ChangeUserPassword(string username, string oldPassword, string newPassword);
        bool UpdateUser(ULoginData user);
        void DeleteUser(int userId);
        List<ULoginData> GetAllUsers();

        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        List<Product> GetAllProductsIncludingCategories();

        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);
    }
}
