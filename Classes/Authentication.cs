/*
    Разработчик: _faha_
    Дата: 21.04.2020
    Класс: Authentication
    Файл: Authentication.cs
    Описание: Осуществление регистрацию и авторизацию пользователя
*/
using System;
using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    public class Authentication : IAuthentication
    {
        private string username = string.Empty;
        private string userpassword = string.Empty;
        public Authentication(string _name, string _password)
        {
            username = _name;
            userpassword = _password;
        }

        ///<summary>
        ///Создание аккаунта администратора
        ///</summary>
        public bool CreateAdminAccount()
        {
            
        }

        ///<summary>
        ///Создание аккаунта для клиента
        ///</summary>
        public bool CreateClientAccount()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Возвращает True если пользователь с заданым логином существует, иначе Else
        ///</summary>
        public bool isPreviouslyCreated(bool _admin)
        {
            string _query = string.Empty;
            if(_admin)
                _query = "select _admin_name from admin_list_table";
            else
                _query = "select _user_name from users_list_table";
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
        public bool Login()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Проверка правильности пароля
        ///</summary>
        public bool PasswordVerification()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Регистрация
        ///</summary>
        public bool Registrate()
        {
            throw new NotImplementedException();
        }
    }
}