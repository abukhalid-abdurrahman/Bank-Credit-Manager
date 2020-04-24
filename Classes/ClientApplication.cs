/*
    Разработчик: _faha_
    Дата: 22.04.2020
    Класс: ClientApplication
    Файл: ClientApplication.cs
    Описание: Осуществление регистрации заявок от клиентов
*/
using System;
using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    public class ClientApplication : IClientApplication
    {
        private stringc _name;
        public ClientApplication(string clientName)
        {
            _name = clientName;
        }
        public void CreateApplication(string _gender, string _isMarried, int _age, string _nation, int _creditSumm, string _creditAim, string _creditTerm)
        {
            SQLManager _sqlManager = new SQLManager();
            _sqlManager.InsertData("users_application", "_login, _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, _arrearage_in_credit_history, _credit_aim, _credit_term, _status, _ball, _results, _is_payed", 
            $"{_name}, '{_gender}', {_age}, '{_isMarried}', '{_nation}', {_creditSumm}, {this.CreditsCount(_name)}, {this.CreditArrearage(_name)}, '{_creditAim}', {_creditTerm}, 'NONE', 0, 0, 0");
        }

        public int CreditArrearage()
        {
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select($"select _login from users_application where _login='{_name}'");
            if(_reader.FieldCount > 0)
            {
                _reader = _sqlManager.Select("select _arrearage_in_credit_history from users_application where _name='{_name}'");
                _reader.Close();
                return Convert.ToInt32(_reader.GetValue(0));
            }
            else
                return 0;
        }

        public int CreditsCount()
        {
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select($"select _login from users_application where _login='{_name}'");
            if(_reader.FieldCount > 0)
            {
                _reader = _sqlManager.Select("select _credit_history from users_application where _name='{_name}'");
                _reader.Close();
                return Convert.ToInt32(_reader.GetValue(0));
            }
            else
                return 0;
        }

        public bool AcceptedToCredit()
        {
            int _balls = 0;
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select("select _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, _arrearage_in_credit_history, _credit_aim, _credit_term from users_application where _name='{_name}'");
            string _user_gender = _reader.GetValue(0).ToString().Trim();
            int _user_age = _reader.GetValue(1);
            string _married = _reader.GetValue(2).ToString().Trim();
            string _nationality = _reader.GetValue(3).ToString().Trim();
            int _credit_summ_from_general_revenue = _reader.GetValue(4);
            int _credit_history = _reader.GetValue(5);
            int _arrearage_in_credit_history = _reader.GetValue(6);
            string _credit_aim = _reader.GetValue(7).ToString().Trim();
            int _credit_term = _reader.GetValue(8);

            if(_user_gender == "муж")
                _balls++;
            else if(_user_gender == "жен")
                _balls += 2;

            if(_marrieda == "холост")
                _balls++;
            else if(_married == "семеянин")
                _balls += 2;
            else if(_married == "вразводе")
                _balls++;
            else if(_married == "вдовец/вдова")
                _balls += 2;
            

            if(_user_age < 25)
                _balls += 0;
            else if(_user_age => 25 || _user_age <= 35)
                _balls++;
            else if(_user_age => 36 || _user_age <= 62)
                _balls += 2;
            else if(_user_age => 63)
                _balls++;

            if(_nationality = "Таджикистан")
                _balls++;
            else if(_nationality = "Зарубеж")
                _balls += 0;

            if(_credit_summ_from_general_revenue < 80)
                _balls += 4;
            else if(_credit_summ_from_general_revenue => 80 || _credit_summ_from_general_revenue <= 150)
                _balls += 3;
            else if(_credit_summ_from_general_revenue > 150 || _credit_summ_from_general_revenue <= 250)
                _balls += 2;
            else if(_credit_summ_from_general_revenue > 250)
                _balls += 1;

            if(_credit_history > 3)
                _balls += 2;
            else if(_credit_history == 1 || _credit_history == 2)
                _balls++;
            else if(_credit_history == 0)
                _balls -= 1;

            if(_arrearage_in_credit_history > 7)
                _balls -= 3;
            else if(_arrearage_in_credit_history => 5 || _arrearage_in_credit_history <= 7)
                _balls -= 2;
            else if(_arrearage_in_credit_history == 4)
                _balls -= 1;
            else if(_arrearage_in_credit_history < 3)
                _balls += 0;


            if(_credit_aim == "бытовая техника")
                _balls += 2;
            else if(_credit_aim == "ремонт")
                _balls++;
            else if(_credit_aim == "телефон")
                _balls += 0;
            else if(_credit_aim == "прочее")
                _balls -= 1;

            if(_credit_term > 12 || _credit_term <= 12)
                _balls++;

            if(_balls > 11)
                return true;
            else if (_balls <= 11)
                return false;
        }
    }
}