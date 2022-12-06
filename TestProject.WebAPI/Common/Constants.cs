using TestProject.WebAPI.Data;

namespace TestProject.WebAPI.Common
{
    public static class Constants
    {
        //User Constants
        public static readonly string EmailAlreadyExists = $"Email address is already in used.";
        public static readonly string InvalidUserObject = $"Invalid User object.";

        //Account Constants
        public static readonly string NoUserExist = "No user exists with the user id :{0}";
        public static readonly string AccountNotCreated = "User is not elegible to create an Account, as available balance is less than :${0}";

        public static readonly int BalanceAmount = 1000;
    }
}
