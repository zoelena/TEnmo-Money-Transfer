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
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            int userId = UserService.GetUserId();
            decimal returnedBalance = accountApiDao.GetAccountBalance(userId);
            Console.WriteLine($"Balance: {returnedBalance}");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            List<Transfer> transfers = transferApiDao.GetTransfers(UserService.GetUserId());
            foreach(Transfer toPrint in transfers)
            {
                Console.WriteLine($"{toPrint.TransferID}        {toPrint.TypeName}   {toPrint.Amount}");
            }

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            Console.WriteLine("Not yet implemented!");
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
