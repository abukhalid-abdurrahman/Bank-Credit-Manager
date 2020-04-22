using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    public interface ISQLManager
    {
        string ConnectionString();
        void InsertData(string _tableName, string _columns, string _values);
        SqlDataReader Select(string _query);
        bool isConnected(SqlConnection _sqlConn);
    }
}