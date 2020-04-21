/*
    Разработчик: _faha_
    Дата: 21.04.2020
    Класс: Authentication
    Файл: Authentication.cs
    Описание: Осуществление регистрацию и авторизацию пользователя
*/
using System;

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
            throw new NotImplementedException();
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
        public bool isPreviouslyCreated()
        {
            throw new NotImplementedException();
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