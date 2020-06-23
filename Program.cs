
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DapperMemsqlConsole
{
    class Program
    {
        static void Main(string[] args)
        {  
            MySqlConnection connection = new MySqlConnection("server=173.249.51.78;Uid=ntime;database=ZilloDb;port=3306;password=P@ssw0rd");
           //var testTableList = connection.Query<TestTable>("Select * From TestTable;").AsList();
           try
            {
                connection.Open();
                Console.WriteLine($"MySQL version : {connection.ServerVersion}");
                var cmd = new MySqlCommand("sp_ntime_TestTable_GetAll", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("$Id", 5);
                var reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                
            }


            //var p = new DynamicParameters();
            // p.Add("$Id", 5);
            // var testTableList1 = connection.Query<TestTable>("sp_ntime_TestTable_GetAll", p, commandType: CommandType.StoredProcedure).AsList();
      
            // Console.WriteLine("Hello World!");
        }
    }


    public class TestTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
