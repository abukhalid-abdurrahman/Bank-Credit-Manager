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
        public SqlConnection _sqlConn;
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
        public void InsertData(string _tableName, string _columns, string _values)
        {
            try
            {
                _sqlConn = new SqlConnection(this.ConnectionString());
                _sqlConn.Open();
                if(this.isConnected(_sqlConn))
                {
                    SqlCommand _sqlCmd = new SqlCommand($"insert into dbo.{_tableName} ({_columns}) values({_values})", _sqlConn);  
                    _sqlCmd.ExecuteNonQuery();
                } 
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        ///<summary>
        ///Возвращает True если соединен с базой данных, иначе Else
        ///</summary>
        public bool isConnected(SqlConnection _sqlConn)
        {
            if(_sqlConn.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        ///<summary>
        ///Обновление данных
        ///</summary>
        public void UpdateData(string _tableName, string _query, string _cond)
        {
            try
            {
                SqlConnection _sqlConn = new SqlConnection(this.ConnectionString());
                _sqlConn.Open();
                if(this.isConnected(_sqlConn))
                {
                    SqlCommand _sqlCmd = new SqlCommand($"update {_tableName} set {_query} where {_cond}", _sqlConn);  
                    _sqlCmd.ExecuteNonQuery();
                } 
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        ///<summary>
        ///Возвращает данные по заданому запросу
        ///</summary>
        public SqlDataReader Select(string _query)
        {
            SqlDataReader reader = null;
            try
            {
                SqlConnection _sqlConn = new SqlConnection(this.ConnectionString());
                _sqlConn.Open();
                if(this.isConnected(_sqlConn))
                {
                    SqlCommand _sqlCmd = new SqlCommand(_query, _sqlConn);
                    reader = _sqlCmd.ExecuteReader();
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
            return reader;
        }
    }
}
