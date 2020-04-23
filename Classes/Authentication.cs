/*
    Разработчик: _faha_
    Дата: 21.04.2020
    Класс: Authentication
    Файл: Authentication.cs
    Описание: Осуществление регистрацию и авторизацию пользователя
*/
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Bank_Credit_Manager
{
    public class Authentication : IAuthentication
    {
        private string username = string.Empty;
        private string userpassword = string.Empty;
        public string dateOfBirth = string.Empty;
        public string homePath = string.Empty;
        public string seria = string.Empty;
        public Authentication(string _name, string _password)
        {
            username = _name;
            userpassword = _password;
        }

        ///<summary>
        ///Регистрация аккаунта
        ///</summary>
        public bool RegistrateAccount(string _table_name)
        {
            if(this.isPreviouslyCreated(true))
                return false;
            else
            {
                try
                {
                    SQLManager _sqlManager = new SQLManager();
                    if(_table_name == "users_list_table")
                        _sqlManager.InsertData("users_list_table", "_name, _password, _date_of_birth, _home_path, _seria", $"'{username}', '{userpassword}', '{dateOfBirth}', '{homePath}', '{seria}'");
                    else
                        _sqlManager.InsertData($"{_table_name}", "_name, _password", "'{username}', '{userpassword}'");
                    return true;
                }
                catch(Exception ex)
                {
                    Log.Error(ex.Message);
                    return false;
                }
            }
        }

        ///<summary>
        ///Возвращает True если пользователь с заданым логином существует, иначе Else
        ///</summary>
        public bool isPreviouslyCreated(bool _admin)
        {
            string _query = string.Empty;
            if(_admin)
                _query = $"select _name from admin_list_table where _name={username}";
            else
                _query = $"select _name from users_list_table where _name={username}";
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select(_query);
            if(_reader.FieldCount > 0)
                return true;
            else
                return false;
        }

        ///<summary>
        ///Авторизация
        ///</summary>
        public bool Login(string _table_name)
        {
            string _query = string.Empty;
            SQLManager _sqlManager = new SQLManager();
            SqlDataReader _reader = _sqlManager.Select($"select _name, _password from {_table_name} where _name={username}, _password={userpassword}");
            if(_reader.FieldCount > 0)   
                return true;
            else
                return false;
        }

        ///<summary>
        ///Проверка правильности пароля
        ///</summary>
        public bool PasswordVerification()
        {
            return Regex.IsMatch(userpassword, "^[a-zA-Z0-9]+$");
        }
    }
}