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
            _sqlManager.InsertData("users_application", "_name, _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, _arrearage_in_credit_history, _credit_aim, _credit_term, _status, _ball, _results, _is_payed", $"_name, _user_gender, _user_age, _married, _nationality, _credit_summ_from_general_revenue, _credit_history, {this.CreditArrearage()}, {_creditAim}, {_creditTerm}, 'NONE', 0, 0, 0");
            
        }

        public int CreditArrearage()
        {
            throw new NotImplementedException();
        }

        public int CreditsCount()
        {
            throw new NotImplementedException();
        }
    }
}