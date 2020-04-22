/*
    Разработчик: _faha_
    Дата: 21.04.2020
    Класс: SQLManager
    Файл: SQLManager.cs
    Описание: Осуществление связи с базой данных и отправкой запросов
*/
using System;
using System.Data;
using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    class SQLManager : ISQLManager
    {
        ///<summary>
        ///Строка соединения
        ///</summary>
        public string ConnectionString()
        {
            return "Data Source=localhost;Initial catalog=Faridun;Integrated Security=True";
        }

        ///<summary>
        ///Вводит новые данные в таблицу
        ///</summary>
        public void InsertData(string _tableName, string _query)
        {
               
        }

        ///<summary>
        ///Возвращает True если соединен с базой данных, иначе Else
        ///</summary>
        public bool isConnected()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Возвращает данные по заданому запросу
        ///</summary>
        public SqlDataReader Select(string _query)
        {
            throw new NotImplementedException();
        }
    }
}
