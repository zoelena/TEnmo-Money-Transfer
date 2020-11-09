﻿using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.DAO;
using TenmoClient.Data;

namespace TenmoClient.Views
{

    public class LoginRegisterMenu : ConsoleMenu
    {
        private readonly AuthService authService;

        public LoginRegisterMenu(AuthService authService)
        {
            this.authService = authService;

            AddOption("Login", Login)
                .AddOption("Register", Register)
                .AddOption("Exit", Exit);

            Configure(cfg =>
            {
                cfg.Title = "**** TEnmo Login Menu ****";
                cfg.ItemForegroundColor = ConsoleColor.DarkYellow;
                cfg.SelectedItemForegroundColor = ConsoleColor.DarkYellow;
                cfg.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                cfg.Selector = ">> ";
                cfg.BeepOnError = true;
            });
        }

        private MenuOptionResult Login()
        {
            User user = null;
            while (user == null)
            {
                LoginUser loginUser = new LoginUser();
                loginUser.Username = GetString("Username: ", true);
                if (loginUser.Username.Trim().Length == 0)
                {
                    Console.WriteLine("Login cancelled.");
                    return MenuOptionResult.WaitAfterMenuSelection;
                }

                loginUser.Password = GetPasswordFromConsole("Password: ");
                user = authService.Login(loginUser);
                if (user == null)
                {
                    Console.WriteLine("Username or password is not valid.");
                }
            }
            UserService.SetLogin(user);

            // User is logged in, show the main menu now.
            AccountApiDAO accountApiDao = new AccountApiDAO();
            TransferApiDAO transferApiDao = new TransferApiDAO();
            UserApiDAO userApiDao = new UserApiDAO();
            return new MainMenu(accountApiDao, transferApiDao, userApiDao).Show();
        }

        private MenuOptionResult Register()
        {
            bool isRegistered = false;
            while (!isRegistered)
            {
                LoginUser registerUser = new LoginUser();
                registerUser.Username = GetString("Username: ", true);
                if (registerUser.Username.Trim().Length == 0)
                {
                    Console.WriteLine("Registration cancelled.");
                    return MenuOptionResult.WaitAfterMenuSelection;
                }
                registerUser.Password = GetPasswordFromConsole("Password: ");
                isRegistered = authService.Register(registerUser);
                if (!isRegistered)
                {
                    Console.WriteLine("Registration failed.");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Registration successful. You can now log in.");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"{new string('_', 26)}");
            Console.WriteLine();
            Console.WriteLine("****Welcome to TEnmo!****");
            Console.WriteLine();
            Console.WriteLine($"{new string('_', 26)}");
        }

        #region Console Helper Functions
        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }
        #endregion
    }
}
