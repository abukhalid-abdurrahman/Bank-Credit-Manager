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
                    this.UserOutput(_login);
                    
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
                    _reader.Close();
                }
                else if(_cmd == "2")
                {
                    this.Output("Список всех клиентов: ");
                    SQLManager _sqlManger = new SQLManager();
                    SqlDataReader _reader = _sqlManger.Select("select * from users_list_table");
                    while(_reader.Read())
                    {
                        this.Output("ID: " + _reader.GetValue(0));
                        this.Output("Имя: " + _reader.GetValue(1));
                        this.Output("Логин (номер телнфона): " + _reader.GetValue(2));
                        this.Output("Пароль: " + _reader.GetValue(3));
                        this.Output("ДР: " + _reader.GetValue(4));
                        this.Output("Прописка: " + _reader.GetValue(5));
                        this.Output("Серия паспорта: " + _reader.GetValue(6));
                    }
                    _reader.Close();
                }
            }
        }
        public void UserOutput(string _name)
        {
            this.Output("Панель пользователя(клиента):\t1.Просмотерть заявок\t2.Остаток кредитов\t3.Детали кредита в виде графика погашения\t0.Выход");
            string _cmd = string.Empty;
            while(_cmd == "0")
            {
                _cmd = this.Input("Выберите действие(1,2,3,0): ");
                SQLManager _sqlManger = new SQLManager();
                SqlDataReader _reader;
                if(_cmd == "1")
                {
                    _reader = _sqlManger.Select($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term, _results from users_application where _login='{_name}'");
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
                    _reader = _sqlManger.Select($"select _credit_summ_from_general_revenue, _credit_aim, _credit_term where _is_payed=0");
                    this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(0).ToString().Trim());
                    this.Output("Цель кредита: " + _reader.GetValue(1).ToString().Trim());
                    this.Output("Срок кредита: " + _reader.GetValue(2).ToString().Trim());
                }
                else if(_cmd == "3")
                {
                    _reader = _sqlManger.Select($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term from users_application where _login='{_name}' and _status='OK'");
                    this.Output("Пол: " + _reader.GetValue(0).ToString().Trim());
                    this.Output("Возраст: " + _reader.GetValue(1).ToString().Trim());
                    this.Output("Семейное положение: " + _reader.GetValue(2).ToString().Trim());
                    this.Output("Гражданство: " + _reader.GetValue(3).ToString().Trim());
                    this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(4).ToString().Trim());
                    this.Output("Цель кредита: " + _reader.GetValue(5).ToString().Trim());
                    this.Output("Срок кредита: " + _reader.GetValue(6).ToString().Trim());
                    _reader = _sqlManger.Select($"select _date, _summ from payment_list where _login={_name}");
                    this.Output("Дата: " + _reader.GetValue(0).ToString().Trim());
                    this.Output("Сумма: " + _reader.GetValue(1).ToString().Trim());
                }
                _reader.Close();
            }
        }
        public void RegistrateOutput()
        {
            string _login = Input("Введите свой номер телефона: ");
            string _password = Input("Придумайте пароль: ");
            string _password_again = Input("Повторите пароль: ");
            if(_password == _password_again)    
            {
                string _name = Input("Введите своё имя: ");
                string _date_of_birth = Input("Введите дату рождения, формат(dd-mm-yyyy): ");
                string _home_path = Input("Введите прописку: ");
                string _seria = Input("Введите серию паспорта: ");
                SQLManager _sqlManger = new SQLManager();
                _sqlManger.InsertData("users_list_table", "_name, _login, _password, _date_of_birth, _home_path, _seria", $"'{_name}', {_login}, '{_password}', '{_date_of_birth}', '{_home_path}', '{_seria}'");
                this.Output("Вы успешно зарегистрировались!");
                string _agree = Input("Хотите подать заявку? (Д/Н): ");
                if(_agree == "Д")
                    ApplicationInput(_login);
                else
                    return;
            }
            else
                RegistrateOutput();
        }
        public void ApplicationInput(string _login)
        {
            string _user_gender = Input("Пол(муж/жен): ");
            int _user_age = Convert.ToInt32(Input("Возраст: "));
            string _married = Input("Семейное положение(холост/семеянин/вразводе/вдовец/вдова): ");
            string _nationality = Input("Гражданство (Таджикистан/Зарубеж): ");
            int _credit_summ_from_general_revenue = Convert.ToInt32(Input("Cумма кредита от общего дохода: "));
            string _credit_aim = Input("Цель кредита(бытовая техника/ремонт/телефон/прочее): ");
            int _credit_term = Convert.ToInt32(Input("Срок кредита: "));
            ClientApplication _client = new ClientApplication(_login);
            _client.CreateApplication(_user_gender, _married, _user_age, _nationality, _credit_summ_from_general_revenue, _credit_aim, _creditTerm);
            bool _isAccepted = _client.AcceptedToCredit();
            string _status = "NONE";
            if(_isAccepted)
                _status = "OK";
            else
                _status = "DISOK";

        }
    }
}