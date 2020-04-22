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
        public void CreateApplication(string _name, string _gender, string _isMarried, int _age, string _nation, int _creditSumm, string _creditAim, string _creditTerm)
        {
            SQLManager _sqlManager = new SQLManager();
            _sqlManager.InsertData("users_application", "_name, _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, _arrearage_in_credit_history, _credit_aim, _credit_term, _status, _ball, _results, _is_payed", 
            $"'{_name}', '{_gender}', {_age}, '{_isMarried}', '{_nation}', {_creditSumm}, {this.CreditsCount(_name)}, {this.CreditArrearage(_name)}, '{_creditAim}', {_creditTerm}, 'NONE', 0, 0, 0");
        }

        public int CreditArrearage(string _name)
        {
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select($"select _name from users_application where _name='{_name}'");
            if(_reader.FieldCount > 0)
            {
                _reader = _sqlManager.Select("select _arrearage_in_credit_history from users_application where _name='{_name}'");
                return Convert.ToInt32(_reader.GetValue(0));
            }
            else
                return 0;
        }

        public int CreditsCount(string _name)
        {
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select($"select _name from users_application where _name='{_name}'");
            if(_reader.FieldCount > 0)
            {
                _reader = _sqlManager.Select("select _credit_history from users_application where _name='{_name}'");
                return Convert.ToInt32(_reader.GetValue(0));
            }
            else
                return 0;
        }
    }
}