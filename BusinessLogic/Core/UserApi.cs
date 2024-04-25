﻿using System;
using System.Linq;
using Domain.Entities;
using System.ComponentModel;
using System.Data.Entity;
using Helpers;
using System.Web;
using AutoMapper;
using Time_Zone.BusinessLogic.DBModel.Seed;
using Time_Zone.Domain.Entities;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User.Global;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Enums;
using Time_Zone.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;
using Time_Zone.Domain.User;
using Domain.Entities.Product;


namespace BusinessLogic.Core
{
    public class UserApi
    {
        internal ActionStatus UserLogData(ULoginData login)
        {
            UDbTable result;
            var pass = LoginHelper.HashGen(login.Password);
            var validate = new EmailAddressAttribute();
            var isValidEmail = validate.IsValid(login.Username);

            using (var db = new UserContext())
            {
                result = isValidEmail
                    ? db.Users.FirstOrDefault(u => u.Email == login.Email)
                    : db.Users.FirstOrDefault(u => u.Credentials == login.Username && u.Password == pass);
            }

            if (result == null)
            {
                return new ActionStatus
                {
                    Status = false,
                    StatusMessage = "The username or password is incorrect"
                };
            }

            if (result != null && result.Password == pass)
            {
                using (var todo = new UserContext())
                {

                    /*  result. = login.LoginIp;*/
                    result.LastLogin = login.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }
                if (result.level == LevelAcces.Admin)
                    return new ActionStatus { Status = true, StatusMessage = "Admin" };
                else
                    return new ActionStatus { Status = true, StatusMessage = "User" };
            }

            return new ActionStatus { Status = true };
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
                    Password = hashedPassword,
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

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Credentials == session.Username);
                }
            }

            if (curentUser == null) return null;
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }

        internal LevelStatus CheckLevelLogic(string keySession)
        {
            return new LevelStatus();
        }

        internal ProductDetail GetProdUser(int id)
        {
            return new ProductDetail();
        }

        public bool UserSessionStatus()
        {
            return true;
        }


    }
}
