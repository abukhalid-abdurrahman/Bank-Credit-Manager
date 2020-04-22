namespace Bank_Credit_Manager
{
    public interface IAuthentication
    {
        bool Login(string _table_name);
        bool PasswordVerification();
        bool isPreviouslyCreated(bool _admin);
        bool RegistrateAccount(string _table_name);
    }
}