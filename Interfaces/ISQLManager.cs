using System.Data.SqlClient;

namespace Bank_Credit_Manager
{
    public interface ISQLManager
    {
        string ConnectionString();
        void InsertData(string _tableName, string _query);
        SqlDataReader Select(string _query);
        bool isConnected();
    }
}