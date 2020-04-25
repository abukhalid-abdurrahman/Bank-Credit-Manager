/*
    Разработчик: _faha_
    Дата: 23.04.2020
    Класс: UserInterface
    Файл: UserInterface.cs
    Описание: Пользовательский интерфейс
*/
using System;
using System.Data;
using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    public class UserInterface : IUserInterface
    {
        ///<summary>
        ///Возвращает строку с символами которые ввел пользователь
        ///</summary>
        public string Input()
        {
            return Console.ReadLine();
        }

        ///<summary>
        ///Выводит параметр _text в консоль и возвращает строку с символами которые ввел пользователь
        ///</summary>
        public string Input(string _text)
        {
            Console.Write(_text);
            return Console.ReadLine();
        }

        ///<summary>
        ///Вывод текста в консоль
        ///</summary>
        public void Output(string _text)
        {
            Console.WriteLine(_text);
        }

        ///<summary>
        ///Авторизация пользователя
        ///</summary>
        public void LoginOutput()
        {
            bool _isAdmin = false;
            string _login = string.Empty;
            while (_login != "/Назад/")
            {
                _login = Input("Логин (Номер телефона): ");
                string _password = string.Empty;
                if (_login == "/admin/")
                    _isAdmin = true;
                else
                    _password = Input("Пароль: ");

                if (_isAdmin)
                {
                    this.Output("Вы вошли в панель администратора!");
                    string _adminLogin = Input("Введите имя: ");
                    string _adminPassword = Input("Введите пароль: ");
                    Authentication _auth = new Authentication(_adminLogin, _adminPassword);
                    bool _isLogged = _auth.Login("admin_list_table");
                    if (_isLogged)
                    {
                        this.Output("Приветствую вас: " + _adminLogin);
                        this.AdminOutput();
                    }
                    else
                    {
                        this.Output("Не правильный логин или пароль!");
                    }
                }
                else
                {
                    Authentication _auth = new Authentication(_login, _password);
                    _auth.loginUser = int.Parse(_login);
                    bool _isLogged = _auth.Login("users_list_table");
                    if (_isLogged)
                    {
                        SQLManager _sqlManger = new SQLManager();
                        SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                        _sqlConn.Open();
                        if (_sqlConn.State == ConnectionState.Open)
                        {
                            SqlCommand _sqlCmd = new SqlCommand($"select _name from users_list_table where _login={_login}", _sqlConn);
                            SqlDataReader _reader = _sqlCmd.ExecuteReader();
                            string _loggedUserName = string.Empty;
                            while (_reader.Read())
                            {
                                _loggedUserName = _reader.GetValue(0).ToString().Trim();
                            }
                            this.Output("Приветствую вас: " + _loggedUserName);
                            this.UserOutput(_login);
                            _reader.Close();
                            _sqlConn.Close();
                        }
                    }
                    else
                    {
                        this.Output("Не правильный логин или пароль!");
                    }
                }
            }
        }

        ///<summary>
        ///Панель администратора
        ///</summary>
        public void AdminOutput()
        {
            this.Output("Панель администратора:\t1.Просмотр заявок\t2.Просмотр клиентов\t0.Выход");
            string _cmd = string.Empty;
            while (_cmd != "0")
            {
                _cmd = this.Input("Выберите действие(1,2,0): ");
                if (_cmd == "1")
                {
                    string _name = this.Input("Введите логин(номер телефона) пользователя: ");
                    SQLManager _sqlManger = new SQLManager();
                    SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                    _sqlConn.Open();
                    if (_sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand _sqlCmd = new SqlCommand($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term, _results from [Faridun].[dbo].[users_application] where _login={_name}", _sqlConn);
                        SqlDataReader _reader = _sqlCmd.ExecuteReader();

                        while (_reader.Read())
                        {
                            this.Output("Пол: " + _reader.GetValue(0).ToString().Trim());
                            this.Output("Возраст: " + _reader.GetValue(1).ToString().Trim());
                            this.Output("Семейное положение: " + _reader.GetValue(2).ToString().Trim());
                            this.Output("Гражданство: " + _reader.GetValue(3).ToString().Trim());
                            this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(4).ToString().Trim());
                            this.Output("Цель кредита: " + _reader.GetValue(5).ToString().Trim());
                            this.Output("Срок кредита: " + _reader.GetValue(6).ToString().Trim());
                            this.Output("Результат: " + _reader.GetValue(7).ToString().Trim());
                        }

                        _reader.Close();
                        _sqlConn.Close();
                    }
                }
                else if (_cmd == "2")
                {
                    this.Output("Список всех клиентов: ");
                    SQLManager _sqlManger = new SQLManager();
                    SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                    _sqlConn.Open();
                    if (_sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand _sqlCmd = new SqlCommand($"select * from [Faridun].[dbo].[users_list_table]", _sqlConn);
                        SqlDataReader _reader = _sqlCmd.ExecuteReader();

                        while (_reader.Read())
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
                        _sqlConn.Close();
                    }
                }
            }
        }

        ///<summary>
        ///Личный кабинет пользователя
        ///</summary>
        public void UserOutput(string _name)
        {
            this.Output("Панель пользователя(клиента):\t1.Просмотерть заявок\t2.Остаток кредитов\t3.Детали кредита в виде графика погашения\t4.Подать заявку\t0.Выход");
            string _cmd = string.Empty;
            while (_cmd != "0")
            {
                _cmd = this.Input("Выберите действие(1,2,3,0): ");
                SQLManager _sqlManger = new SQLManager();
                if (_cmd == "1")
                {
                    SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                    _sqlConn.Open();
                    if (_sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand _sqlCmd = new SqlCommand($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term, _results from [Faridun].[dbo].[users_application] where _login={_name}", _sqlConn);
                        SqlDataReader _reader = _sqlCmd.ExecuteReader();
                        while (_reader.Read())
                        {
                            this.Output("Пол: " + _reader.GetValue(0).ToString().Trim());
                            this.Output("Возраст: " + _reader.GetValue(1).ToString().Trim());
                            this.Output("Семейное положение: " + _reader.GetValue(2).ToString().Trim());
                            this.Output("Гражданство: " + _reader.GetValue(3).ToString().Trim());
                            this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(4).ToString().Trim());
                            this.Output("Цель кредита: " + _reader.GetValue(5).ToString().Trim());
                            this.Output("Срок кредита: " + _reader.GetValue(6).ToString().Trim());
                            this.Output("Результат: " + _reader.GetValue(7).ToString().Trim());
                        }
                        _reader.Close();
                        _sqlConn.Close();
                    }
                }
                else if (_cmd == "2")
                {
                    SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                    _sqlConn.Open();
                    if (_sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand _sqlCmd = new SqlCommand($"select _credit_summ_from_general_revenue, _credit_aim, _credit_term from [Faridun].[dbo].[users_application] where _is_payed = 0", _sqlConn);
                        SqlDataReader _reader = _sqlCmd.ExecuteReader();
                        while (_reader.Read())
                        {
                            this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(0).ToString().Trim());
                            this.Output("Цель кредита: " + _reader.GetValue(1).ToString().Trim());
                            this.Output("Срок кредита: " + _reader.GetValue(2).ToString().Trim());
                        }
                        _reader.Close();
                        _sqlConn.Close();
                    }
                }
                else if (_cmd == "3")
                {
                    SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                    _sqlConn.Open();
                    if (_sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand _sqlCmd = new SqlCommand($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term from [Faridun].[dbo].[users_application] where _login={_name} and _status='OK'", _sqlConn);
                        SqlDataReader _reader = _sqlCmd.ExecuteReader();
                        while (_reader.Read())
                        {
                            this.Output("Пол: " + _reader.GetValue(0).ToString().Trim());
                            this.Output("Возраст: " + _reader.GetValue(1).ToString().Trim());
                            this.Output("Семейное положение: " + _reader.GetValue(2).ToString().Trim());
                            this.Output("Гражданство: " + _reader.GetValue(3).ToString().Trim());
                            this.Output("Cумма кредита от общего дохода: " + _reader.GetValue(4).ToString().Trim());
                            this.Output("Цель кредита: " + _reader.GetValue(5).ToString().Trim());
                            this.Output("Срок кредита: " + _reader.GetValue(6).ToString().Trim());
                        }
                        _reader.Close();
                        _sqlConn.Close();
                    }

                    _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
                    _sqlConn.Open();
                    if (_sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand _sqlCmd = new SqlCommand($"select _date, _summ from [Faridun].[dbo].[payment_list] where _login={_name}", _sqlConn);
                        SqlDataReader _reader = _sqlCmd.ExecuteReader();
                        while (_reader.Read())
                        {
                            this.Output("Дата: " + _reader.GetValue(0).ToString().Trim());
                            this.Output("Сумма: " + _reader.GetValue(1).ToString().Trim());
                        }
                        _reader.Close();
                        _sqlConn.Close();
                    }
                }
                else if (_cmd == "4")
                {
                    ApplicationInput(_name);
                }
            }
        }

        ///<summary>
        ///Регистрация пользователя
        ///</summary>
        public void RegistrateOutput()
        {
            string _login = string.Empty;
            while (_login != "/Назад/")
            {
                _login = Input("Введите свой номер телефона: ");
                string _password = Input("Придумайте пароль: ");
                string _password_again = Input("Повторите пароль: ");
                if (_password == _password_again)
                {
                    string _name = Input("Введите своё имя: ");
                    string _date_of_birth = Input("Введите дату рождения, формат(dd-mm-yyyy): ");
                    string _home_path = Input("Введите прописку: ");
                    string _seria = Input("Введите серию паспорта: ");
                    Authentication authentication = new Authentication(_name, _password);
                    authentication.dateOfBirth = _date_of_birth;
                    authentication.homePath = _home_path;
                    authentication.loginUser = int.Parse(_login);
                    bool _reged = authentication.RegistrateAccount("users_list_table");
                    if (_reged)
                    {
                        this.Output("Вы успешно зарегистрировались!");
                        string _agree = Input("Хотите подать заявку? (Д/Н): ");
                        if (_agree == "Д")
                            ApplicationInput(_login);
                        else
                            return;
                    }
                }
            }
        }

        ///<summary>
        ///Регистрация заявки пользователя на кредит
        ///</summary>
        public void ApplicationInput(string _login)
        {
            string _user_gender = Input("Пол(муж/жен): ");
            int _user_age = Convert.ToInt32(Input("Возраст: "));
            string _married = Input("Семейное положение(холост/семеянин/вразводе/вдовец/вдова): ");
            string _nationality = Input("Гражданство (Таджикистан/Зарубеж): ");
            int _credit_summ_from_general_revenue = Convert.ToInt32(Input("Cумма кредита от общего дохода: "));
            string _credit_aim = Input("Цель кредита(бытовая техника/ремонт/телефон/прочее): ");
            int _credit_term = Convert.ToInt32(Input("Срок кредита: "));
            int _credit_summ = Convert.ToInt32(Input("Сумма кредита: "));
            ClientApplication _client = new ClientApplication(_login);
            _client.CreateApplication(_user_gender, _married, _user_age, _nationality, _credit_summ_from_general_revenue, _credit_aim, _credit_term);
            bool _isAccepted = _client.AcceptedToCredit();
            string _status = "NONE";

            if (_isAccepted)
                _status = "OK";
            else
                _status = "DISOK";

            SQLManager _sqlManger = new SQLManager();
            _sqlManger.UpdateData("users_application", $"_status='{_status}'", $"_login={_login}");
            string date = $"{DateTime.Now.Day.ToString()}.{DateTime.Now.Month.ToString()}.{DateTime.Now.Year.ToString()}";
            _sqlManger.InsertData("payment_list", "_login, _date, _summ", $"'{_login}', '{date}', {_credit_summ}");
        }

        ///<summary>
        ///Внесение денег
        ///</summary>
        public void PaymentStory(float _summ, string _login)
        {
            SQLManager _sqlManger = new SQLManager();
            string date = $"{DateTime.Now.Day.ToString()}.{DateTime.Now.Month.ToString()}.{DateTime.Now.Year.ToString()}";
            float _toPlay = 0.0f;
            SqlConnection _sqlConn = new SqlConnection(_sqlManger.ConnectionString());
            _sqlConn.Open();
            if (_sqlConn.State == ConnectionState.Open)
            {
                SqlCommand _sqlCmd = new SqlCommand($"select _summ from [Faridun].[dbo].[payment_list] where _login={_login}", _sqlConn);
                SqlDataReader _reader = _sqlCmd.ExecuteReader();
                while (_reader.Read())
                {
                    _toPlay = float.Parse(_reader.GetValue(0).ToString().Trim().Replace('.', ','));
                }
                _reader.Close();
                _sqlConn.Close();
            }

            _summ = _toPlay - _summ;
            _sqlManger.InsertData("payment_list", "_login, _date, _summ", $"{_login}, '{date}', {_summ}");
            if (_summ <= 0)
            {
                _sqlManger.UpdateData("users_application", "_is_payed=1", $"_login={_login}");
            }
        }
    }
}