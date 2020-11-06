using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.DAO;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private AccountApiDAO accountApiDao;
        private TransferApiDAO transferApiDao;
        public MainMenu(AccountApiDAO accountApiDao, TransferApiDAO transferApiDao)
        {
            this.transferApiDao = transferApiDao;
            this.accountApiDao = accountApiDao;
            AddOption("View your current balance", ViewBalance)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View your pending requests", ViewRequests)
                .AddOption("Send TE bucks", SendTEBucks)
                .AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);

            Configure(cfg =>
            {
                cfg.Title = "**** Welcome to TEnmo! ****";
                cfg.ItemForegroundColor = ConsoleColor.Blue;
                cfg.SelectedItemForegroundColor = ConsoleColor.White;
                cfg.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                cfg.Selector = ">> ";
                cfg.BeepOnError = true;
            });
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            Console.WriteLine($"Current Balance for User: {UserService.GetUserName()}");
            Console.WriteLine();
            int userId = UserService.GetUserId();
            decimal returnedBalance = accountApiDao.GetAccountBalance(userId);
            Console.WriteLine($"Your current account balance is: {returnedBalance}");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            
            List<Transfer> transfers = transferApiDao.GetTransfers(UserService.GetUserId());
            Console.WriteLine($"{new string('_', 50)}");
            string[] headings = { "Transfers" };
            Console.WriteLine($"{headings[0],-14}");
            string[] headingsTwo = { "ID", "From", "To", "Amount" };
            Console.WriteLine($"{headingsTwo[0],0} {headingsTwo[1],10} {headingsTwo[2],10} {headingsTwo[3],20}");
            Console.WriteLine($"{new string('_', 50)}");
            Console.WriteLine();

            foreach (Transfer toPrint in transfers)
            {
                Console.WriteLine($"{toPrint.TransferID, 0} {toPrint.FromName, 12} {toPrint.ToName, 12} {toPrint.Amount, 16}");
            }

            Console.WriteLine($"{new string('_', 50)}");
            Console.WriteLine("Please enter transfer ID to view details (0 to cancel): "); //need to implement

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            Console.WriteLine($"{new string('_', 50)}");
            string[] headings = { "Users" };
            Console.WriteLine($"{headings[0],0}");
            string[] headingsTwo = { "ID", "Name" };
            Console.WriteLine($"{headingsTwo[0],0} {headingsTwo[1],10}");
            Console.WriteLine($"{new string('_', 50)}");
            Console.WriteLine();
            //need list of users
            Console.WriteLine();
            Console.WriteLine($"{new string('_', 50)}");
            int accountFrom = GetInteger("Please re-enter your ID: "); //do we need this
            int accountTo = GetInteger("Enter ID of user you are sending to (0 to cancel): ");
            decimal amount = GetDecimal("Enter amount:");

            transferApiDao.NewTransfer(accountFrom, accountTo, amount);
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult RequestTEBucks()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }

    }
}
