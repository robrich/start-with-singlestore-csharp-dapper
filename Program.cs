
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DapperMemsqlConsole
{
    class Program
    {
        static void Main(string[] args)
        {  
            MySqlConnection connection = new MySqlConnection("server=YOUR_SERVER;Uid=YOUR_USER;database=YOUR_DB;port=3306;password=YOUR_PASS;checkparameters=false;");
            // need `checkparameters=false` for stored procedures because there's no `mysql` database, it's named `information_schema` in MemSQL
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
