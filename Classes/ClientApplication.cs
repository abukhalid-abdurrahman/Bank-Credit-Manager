/*
    Разработчик: _faha_
    Дата: 22.04.2020
    Класс: ClientApplication
    Файл: ClientApplication.cs
    Описание: Осуществление регистрации заявок от клиентов
*/
using System.Data;
using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    public class ClientApplication : IClientApplication
    {
        private string _login;
        public ClientApplication(string clientLogin)
        {
            _login = clientLogin;
        }

        ///<summary>
        ///Регистрация заявки пользователя для получения кредита
        ///</summary>
        public void CreateApplication(string _gender, string _isMarried, int _age, string _nation, int _creditSumm, string _creditAim, int _creditTerm)
        {
            SQLManager _sqlManager = new SQLManager();
            _sqlManager.InsertData("users_application", "_login, _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, _arrearage_in_credit_history, _credit_aim, _credit_term, _status, _balls, _results, _is_payed",
            $"{_login}, '{_gender}', {_age}, '{_isMarried}', '{_nation}', {_creditSumm}, {this.CreditsCount()}, {this.CreditArrearage()}, '{_creditAim}', {_creditTerm}, 'NONE', 0, 0, 0");
        }

        ///<summary>
        ///Возвращает кол-во не оплаченных кредитов
        ///</summary>
        public int CreditArrearage()
        {
            int _results = 0;
            SQLManager _sqlManager = new SQLManager();
            SqlConnection _sqlConn = new SqlConnection(_sqlManager.ConnectionString());
            _sqlConn.Open();
            if (_sqlConn.State == ConnectionState.Open)
            {
                SqlCommand _sqlCmd = new SqlCommand("select _summ from [Faridun].[dbo].[payment_list] where _summ != 0", _sqlConn);
                SqlDataReader _reader = _sqlCmd.ExecuteReader();
                while (_reader.Read())
                {
                    _results++;
                }
                _reader.Close();
                _sqlConn.Close();
            }
            return _results;
        }

        ///<summary>
        ///Возвращает кол-во кредитов
        ///</summary>
        public int CreditsCount()
        {
            int _results = 0;
            SQLManager _sqlManager = new SQLManager();
            SqlConnection _sqlConn = new SqlConnection(_sqlManager.ConnectionString());
            _sqlConn.Open();
            if (_sqlConn.State == ConnectionState.Open)
            {
                SqlCommand _sqlCmd = new SqlCommand("select _status from [Faridun].[dbo].[users_application] where _status = 'OK'", _sqlConn);
                SqlDataReader _reader = _sqlCmd.ExecuteReader();
                while (_reader.Read())
                {
                    _results++;
                }
                _reader.Close();
                _sqlConn.Close();
            }
            return _results;
        }

        ///<summary>
        ///Проверка заявки клиента на получение кредита, True если одобрен, иначе Else
        ///</summary>
        public bool AcceptedToCredit()
        {
            int _balls = 0;
            SQLManager _sqlManager = new SQLManager();
            SqlConnection _sqlConn = new SqlConnection(_sqlManager.ConnectionString());
            _sqlConn.Open();
            if (_sqlConn.State == ConnectionState.Open)
            {
                SqlCommand _sqlCmd = new SqlCommand($"select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, _arrearage_in_credit_history, _credit_aim, _credit_term from [Faridun].[dbo].[users_application] where _login={_login}", _sqlConn);
                SqlDataReader _sqlReader = _sqlCmd.ExecuteReader();
                string _user_gender = string.Empty;
                int _user_age = 0;
                string _married = string.Empty;
                string _nationality = string.Empty;
                int _credit_summ_from_general_revenue = 0;
                int _credit_history = 0;
                int _arrearage_in_credit_history = 0;
                string _credit_aim = string.Empty;
                int _credit_term = 0;
                while (_sqlReader.Read())
                {
                    _user_gender = _sqlReader.GetValue(0).ToString().Trim();
                    _user_age = int.Parse(_sqlReader.GetValue(1).ToString());
                    _married = _sqlReader.GetValue(2).ToString().Trim();
                    _nationality = _sqlReader.GetValue(3).ToString().Trim();
                    _credit_summ_from_general_revenue = int.Parse(_sqlReader.GetValue(4).ToString());
                    _credit_history = int.Parse(_sqlReader.GetValue(5).ToString());
                    _arrearage_in_credit_history = int.Parse(_sqlReader.GetValue(6).ToString());
                    _credit_aim = _sqlReader.GetValue(7).ToString().Trim();
                    _credit_term = int.Parse(_sqlReader.GetValue(8).ToString());
                }
                _sqlReader.Close();
                _sqlConn.Close();
                if (_user_gender == "муж")
                    _balls++;
                else if (_user_gender == "жен")
                    _balls += 2;

                if (_married == "холост")
                    _balls++;
                else if (_married == "семеянин")
                    _balls += 2;
                else if (_married == "вразводе")
                    _balls++;
                else if (_married == "вдовец/вдова")
                    _balls += 2;


                if (_user_age < 25)
                    _balls += 0;
                else if (_user_age >= 25 || _user_age <= 35)
                    _balls++;
                else if (_user_age >= 36 || _user_age <= 62)
                    _balls += 2;
                else if (_user_age >= 63)
                    _balls++;

                if (_nationality == "Таджикистан")
                    _balls++;
                else if (_nationality == "Зарубеж")
                    _balls += 0;

                if (_credit_summ_from_general_revenue < 80)
                    _balls += 4;
                else if (_credit_summ_from_general_revenue >= 80 || _credit_summ_from_general_revenue <= 150)
                    _balls += 3;
                else if (_credit_summ_from_general_revenue > 150 || _credit_summ_from_general_revenue <= 250)
                    _balls += 2;
                else if (_credit_summ_from_general_revenue > 250)
                    _balls += 1;

                if (_credit_history > 3)
                    _balls += 2;
                else if (_credit_history == 1 || _credit_history == 2)
                    _balls++;
                else if (_credit_history == 0)
                    _balls -= 1;

                if (_arrearage_in_credit_history > 7)
                    _balls -= 3;
                else if (_arrearage_in_credit_history >= 5 || _arrearage_in_credit_history <= 7)
                    _balls -= 2;
                else if (_arrearage_in_credit_history == 4)
                    _balls -= 1;
                else if (_arrearage_in_credit_history < 3)
                    _balls += 0;


                if (_credit_aim == "бытовая техника")
                    _balls += 2;
                else if (_credit_aim == "ремонт")
                    _balls++;
                else if (_credit_aim == "телефон")
                    _balls += 0;
                else if (_credit_aim == "прочее")
                    _balls -= 1;

                if (_credit_term > 12 || _credit_term <= 12)
                    _balls++;
            }

            if (_balls > 11)
            {
                _sqlManager.UpdateData("users_application", $"_balls={_balls}, _status='OK'", $"_login={_login}");
                return true;
            }
            else
            {
                _sqlManager.UpdateData("users_application", $"_balls={_balls}, _status='DISOK'", $"_login={_login}");
                return false;
            }
        }
    }
}