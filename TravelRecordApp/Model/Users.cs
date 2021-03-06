﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static async Task<bool> Login(string email, string password)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;

            }
            else
            {
                var user = (await App.MobileService.GetTable<Users>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    App.user = user; // Save the user info in the app

                    if (user.Password == password)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }


        public static async void Register(Users user)
        {
            await App.MobileService.GetTable<Users>().InsertAsync(user);
        }

    }
}
