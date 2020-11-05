using TenmoClient.Data;

namespace TenmoClient
{
    public static class UserService
    {
        private static User user = new User();
        private static Account account = new Account();

        public static void SetLogin(User u)
        {
            user = u;
        }
        public static void SetAccount(Account a)
        {
            account = a;
        }
        public static decimal GetBalance()
        {
            return account.Balance;
        }
        public static string GetUserName()
        {
            return user.Username;
        }

        public static int GetUserId()
        {
            return user.UserId;
        }

        public static bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(user.Token);
        }

        public static string GetToken()
        {
            return user?.Token ?? string.Empty;
        }
    }
}
