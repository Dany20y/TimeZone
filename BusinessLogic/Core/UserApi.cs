using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User.Global;
using Time_Zone.Domain.Entities.Product;
using System.Net.Http.Headers;
using Time_Zone.Domain.User;
using BusinessLogic.DBModel.Seed;
using Time_Zone.Domain.Enums;
using Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;

namespace Time_Zone.BusinessLogic.Core
{
    public class UserApi
    {
        internal ActionStatus UserLogData(ULoginData login)
        {
            UDbTable result;
            var pass = LoginHelper.HashGen(login.Password);
            var validate = new EmailAddressAttribute();
            var isValidEmail = validate.IsValid(login.Credential);

            using (var db = new UserContext())
            {
                result = isValidEmail
                    ? db.Users.FirstOrDefault(u => u.Email == login.Credential)
                    : db.Users.FirstOrDefault(u => u.Credentials == login.Credential && u.Password == pass);
            }

            if (result == null)
            {
                return new ActionStatus
                {
                    Status = false,
                    StatusMessage = "The username or password is incorrect"
                };
            }

            using (var db = new UserContext())
            {
                result.LastLogin = DateTime.Now;
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
            }

            return new ActionStatus {Status = true};
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                var isValidEmail = new EmailAddressAttribute().IsValid(loginCredential);
                var curent = db.Sessions.FirstOrDefault(e => e.Username == loginCredential);

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(1);
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(1)
                    });
                }

                db.SaveChanges();
            }

            return apiCookie;
        }

        public ActionStatus RegisterUserAction(URegisterData data)
        {
            try
            {
                using (var db = new UserContext())
                {
                    bool emailExists = db.Users.Any(u => u.Email == data.Email);
                    bool usernameExists = db.Users.Any(u => u.Credentials == data.Username);

                    if (emailExists || usernameExists)
                    {
                        return new ActionStatus { Status = false, StatusMessage = "User with the provided email or username already exists" };
                    }
                }

                var hashedPassword = LoginHelper.HashGen(data.Password);
                var newUser = new UDbTable
                {

                    Credentials = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    LastLogin = DateTime.Now,
                    level = LevelAcces.User
                };

                using (var db = new UserContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }

                return new ActionStatus { Status = true, StatusMessage = "User registered successfully" };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in an appropriate way
                return new ActionStatus { Status = false, StatusMessage = $"An error occurred: {ex.Message}" };
            }
        }


        internal LevelStatus CheckLevelLogic(string keySession)
        {
            return new LevelStatus();
        }

        internal ProductDetail GetProductUser(int id)
        {
            return new ProductDetail();
        }
        public bool UserSessionStatus()
        {
            return true;
        }

    }
}
