namespace Bank_Credit_Manager
{
    public interface IUserInterface
    {
        string Input();
        string Input(string _text);
        string Output(string _text);
        void LoginOutput();
        void RegistrateOutput();
        void ApplicationInput(string _login);
        void AdminOutput();
        void UserOutput(string _name);
        void PaymentStory(float _summ);
    }
}