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
            this.Output("Панель администратора:\t1.Просмотр заявок\t2.Просмотр клиентов\t0.Выход");
            string _cmd = string.Empty;
            while(_cmd == "0")
            {
                _cmd = this.Input("Выберите действие(1,2,0): ");
                if(_cmd == "1")
                {
                    string _name = this.Input("Введите логин(номер телефона) пользователя: ");
                    SQLManager _sqlManger = new SQLManager();
                    SqlDataReader _reader = _sqlManger.Select($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term, _results from users_application where _login='{_name}'"); 
                    this.Output("Данные заявки: " + _sqlManger.Select($"select _name from users_list_table where _login={_name}").GetValue(0).ToString().Trim());
                    this.Output("Пол: " + _reader.GetValue(0).ToString().Trim());
                    this.Output("Возраст: " + _reader.GetValue(1).ToString().Trim());
                    this.Output("Семейное положение: " + _reader.GetValue(2).ToString().Trim());
                    this.Output("Гражданство: " + _reader.GetValue(3).ToString().Trim());
                    this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(4).ToString().Trim());
                    this.Output("Цель кредита: " + _reader.GetValue(5).ToString().Trim());
                    this.Output("Срок кредита: " + _reader.GetValue(6).ToString().Trim());
                    this.Output("Результат: " + _reader.GetValue(7).ToString().Trim());
                }
                else if(_cmd == "2")
                {

                }
            }
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