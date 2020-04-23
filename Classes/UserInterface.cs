/*
    Разработчик: _faha_
    Дата: 23.04.2020
    Класс: UserInterface
    Файл: UserInterface.cs
    Описание: Пользовательский интерфейс
*/
using System;

namespace Bank_Credit_Manager
{
    public class UserInterface : IUserInterface
    {
        public string Input()
        {
            return Console.ReadLine();
        }
        public public string Input(string _text)
        {
            Console.Write(_text);
            return Console.ReadLine();
        }
        public string Output(string _text)
        {
            Console.WriteLine(_text);
        }
        public void LoginOutput()
        {
            bool _isAdmin;
            string _login = Input("Логин (Номер телефона): ");
            string _password = string.Empty;
            if(_login = "/admin/")
                _isAdmin = true;
            else
                _password = Input("Пароль: ");
            
            if(_isAdmin)
            {
                this.Output("Вы вошли в панель администратора!");
                string _adminLogin = Input("Введите имя: ");
                string _adminPassword = Input("Введите пароль: ");
                Authentication _auth = new Authentication(_adminLogin, _adminPassword);
                bool _isLogged = _auth.Login("admin_list_table");
                if(_isLogged)
                {
                    this.Output("Приветствую вас: " + _adminLogin);
                    this.AdminOutput();
                }
                else
                {
                    this.Output("Не правильный логин или пароль!");
                    this.LoginOutput();
                }
            }
            else
            {
                Authentication _auth = new Authentication(_login, _password);
                bool _isLogged = _auth.Login("users_list_table");
                if(_isLogged)
                {
                    SQLManager _sqlManger = new SQLManager();
                    string _clientName = _sqlManger.Select().GetValue(0).ToString().Trim();
                    this.Output("Приветствую вас: " + _login);
                    this.UserOutput();
                    
                }
                else
                {
                    this.Output("Не правильный логин или пароль!");
                    this.LoginOutput();
                }
            }

        }

        public void AdminOutput()
        {

        }
        public void UserOutput()
        {

        }
        public void RegistrateOutput()
        {

        }
        public void ApplicationInput()
        {

        }
    }
}