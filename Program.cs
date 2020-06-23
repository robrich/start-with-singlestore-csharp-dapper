
using Dapper;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Data;

namespace DapperMemsqlConsole
{
    class Program
    {
        static void Main(string[] args)
        {  
            MySqlConnection connection = new MySqlConnection("server=173.249.51.78;Uid=ntime;database=ZilloDb;port=3306;password=P@ssw0rd");
            var testTableList = connection.Query<TestTable>("Select * From TestTable;").AsList();

            var p = new DynamicParameters();
            p.Add("$Id", 5);
            var testTableList1 = connection.Query<TestTable>("sp_ntime_TestTable_GetAll", p, commandType: CommandType.StoredProcedure).AsList();
      
            // Console.WriteLine("Hello World!");
        }
    }


    public class TestTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
