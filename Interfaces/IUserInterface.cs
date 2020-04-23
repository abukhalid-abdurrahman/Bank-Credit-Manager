namespace Bank_Credit_Manager
{
    public interface IUserInterface
    {
        string Input();
        string Input(string _text);
        string Output(string _text);
        void LoginOutput();
        void RegistrateOutput();
        void ApplicationInput();
        void AdminOutput();
        void UserOutput();
    }
}