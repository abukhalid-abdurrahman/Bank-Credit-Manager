namespace Bank_Credit_Manager
{
    public interface IAuthentication
    {
        bool Login();
        bool Registrate();
        bool PasswordVerification();
        bool isPreviouslyCreated();
        bool CreateClientAccount();
        bool CreateAdminAccount();
    }
}