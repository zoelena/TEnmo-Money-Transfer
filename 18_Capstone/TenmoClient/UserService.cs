using TenmoClient.Data;

namespace TenmoClient
{
    public static class UserService
    {
        private static API_User user = new API_User();
        private static API_Account account = new API_Account();

        public static void SetLogin(API_User u)
        {
            user = u;
        }
        public static void SetAccount(API_Account a)
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
