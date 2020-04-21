/*
    Разработчик: _faha_
    Дата: 21.04.2020
    Класс: HashCode
    Файл: HashCode.cs
    Описание: Генерация хеш-кода, по алгоритму MD5
*/
using System;
using System.Security.Cryptography;
using System.Text;

namespace Bank_Credit_Manager
{
    class HashCode : IHashCode
    {
        private string inputedData = string.Empty;
        public HashCode(string _data)
        {
            inputedData = _data;
        }

        ///<summary>
        ///Возвращает хеш-код от задоного значения
        ///</summary>
        public string Generate()
        {
            string _results = string.Empty;
            var _md5 = MD5.Create();
            var _hashCode = _md5.ComputeHash(Encoding.UTF8.GetBytes(inputedData));
            _results = Convert.ToBase64String(_hashCode);
            return _results;
        }
    }
}
