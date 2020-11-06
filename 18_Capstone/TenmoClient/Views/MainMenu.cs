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
        private UserApiDAO userApiDao;
        public MainMenu(AccountApiDAO accountApiDao, TransferApiDAO transferApiDao)
        {
            this.transferApiDao = transferApiDao;
            this.accountApiDao = accountApiDao;
            this.userApiDao = userApiDao;

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
            try
            {
                Console.WriteLine($"Current Balance for User: {UserService.GetUserName()}");
                Console.WriteLine();
                int userId = UserService.GetUserId();
                decimal returnedBalance = accountApiDao.GetAccountBalance(); ///remove userId
                Console.WriteLine($"Your current account balance is: {returnedBalance}");
                return MenuOptionResult.WaitAfterMenuSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return MenuOptionResult.WaitAfterMenuSelection;
            }
        }

        private MenuOptionResult ViewTransfers()
        {
            try
            {
                string comboTypeName = "";
                List<Transfer> transfers = transferApiDao.GetTransfers();
                Console.WriteLine($"{new string('_', 50)}");
                string[] headings = { "Transfers" };
                Console.WriteLine($"{headings[0],-14}");
                string[] headingsTwo = { "ID", "From/To", "Amount" };
                Console.WriteLine($"{headingsTwo[0],0} {headingsTwo[1],10} {headingsTwo[2],20}");
                Console.WriteLine($"{new string('_', 50)}");
                Console.WriteLine();

                foreach (Transfer toPrint in transfers)
                {
                    if (toPrint.ToId == UserService.GetUserId())
                    {
                        comboTypeName = $"From: {toPrint.FromName}";
                    }
                    else
                    {
                        comboTypeName = $"To: {toPrint.ToName}";
                    }

                    Console.WriteLine($"{toPrint.TransferID,0} {comboTypeName,12} {toPrint.Amount,16}");
                }

                Console.WriteLine($"{new string('_', 50)}");
                int inputId = GetInteger("Please enter transfer ID to view details (0 to cancel): ");
                //MAKE SURE 0 DOES NOT PASS, INSTEAD EXIT, CURRENTLY NOT DOING THAT
                Transfer requestedTransfer = transfers.Find(x => x.TransferID.Equals(inputId));
                Console.WriteLine($"{new string('_', 50)}");
                string[] headingsThree = { "Transfer Details" };
                Console.WriteLine($"{headingsThree[0],25}");
                Console.WriteLine($"{new string('_', 50)}");
                Console.WriteLine($"ID: {requestedTransfer.TransferID}");
                Console.WriteLine($"From: {requestedTransfer.FromName}");
                Console.WriteLine($"To: {requestedTransfer.ToName}");
                Console.WriteLine($"Type: {requestedTransfer.TypeName}");
                Console.WriteLine($"Status: {requestedTransfer.StatusName}");
                Console.WriteLine($"Amount: {requestedTransfer.Amount}");
                // option to select a different transfer number
                return MenuOptionResult.WaitAfterMenuSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return MenuOptionResult.WaitAfterMenuSelection;
            }

        }

        private MenuOptionResult ViewRequests()
        {
            try
            {
                Console.WriteLine("Not yet implemented!");
                return MenuOptionResult.WaitAfterMenuSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return MenuOptionResult.WaitAfterMenuSelection;
            }
        }

        private MenuOptionResult SendTEBucks()
        {
            try
            {
                Console.WriteLine($"{new string('_', 50)}");
                string[] headings = { "Users" };
                Console.WriteLine($"{headings[0],0}");
                string[] headingsTwo = { "ID", "Name" };
                Console.WriteLine($"{headingsTwo[0],0} {headingsTwo[1],10}");
                Console.WriteLine($"{new string('_', 50)}");
                Console.WriteLine();
                List<User> users = userApiDao.GetUsers();
                //need list of users need UserApiDAO?
                Console.WriteLine();
                Console.WriteLine($"{new string('_', 50)}");
                //int accountFrom = GetInteger("Please re-enter your ID: "); //do we need this
                int userIdTo = GetInteger("Enter ID of user you are sending to (0 to cancel): ");
                decimal amount = GetDecimal("Enter amount:");

                transferApiDao.NewTransfer(userIdTo, amount);
                return MenuOptionResult.WaitAfterMenuSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return MenuOptionResult.WaitAfterMenuSelection;
            }
        }

        private MenuOptionResult RequestTEBucks()
        {
            try
            {
                Console.WriteLine("Not yet implemented!");
                return MenuOptionResult.WaitAfterMenuSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return MenuOptionResult.WaitAfterMenuSelection;
            }
        }

        private MenuOptionResult Logout()
        {
            try
            {
                UserService.SetLogin(new User()); //wipe out previous login info
                return MenuOptionResult.CloseMenuAfterSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return MenuOptionResult.WaitAfterMenuSelection;
            }
        }

    }
}
