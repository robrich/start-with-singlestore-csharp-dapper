using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace SingleStoreExample
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public static class Program
    {

        // TODO: adjust these connection details to match your SingleStore deployment:
        public const string HOST = "localhost";
        public const int PORT = 3306;
        public const string USER = "root";
        public const string PASSWORD = "password_here";
        public const string DATABASE = "acme";

        public static void Main()
        {
            string connstr = $"Server={HOST};Port={PORT};Uid={USER};Pwd={PASSWORD};Database={DATABASE};";
            IDbConnection conn = new MySqlConnection(connstr);
            try
            {
                long id = Create(conn, "Inserted row");
                Console.WriteLine($"Inserted row id {id}");

                Message msg = ReadOne(conn, id);
                Console.WriteLine("Read one row:");
                if (msg == null)
                {
                    Console.WriteLine("not found");
                }
                else
                {
                    Console.WriteLine($"{msg.Id}, {msg.Content}, {msg.CreateDate}");
                }

                Update(conn, id, "Updated row");
                Console.WriteLine($"Updated row id {id}");

                List<Message> messages = ReadAll(conn);
                Console.WriteLine("Read all rows:");
                foreach (var message in messages)
                {
                    Console.WriteLine($"{message.Id}, {message.Content}, {message.CreateDate}");
                }

                Delete(conn, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }

        }

        private static long Create(IDbConnection conn, string content)
        {
            ulong id = conn.QuerySingle<ulong>("INSERT INTO messages (content) VALUES (@content); select last_insert_id();", new { content });
            return (long)id;
        }

        private static Message ReadOne(IDbConnection conn, long id)
        {
            return conn.QueryFirst<Message>("SELECT * FROM messages where id = @id;", new { id });
        }

        private static List<Message> ReadAll(IDbConnection conn)
        {
            return conn.Query<Message>("SELECT * FROM messages ORDER BY id;").ToList();
        }

        private static int Update(IDbConnection conn, long id, string content)
        {
            return conn.Execute("UPDATE messages SET content = @content where id = @id;", new { id, content });
        }

        private static int Delete(IDbConnection conn, long id)
        {
            return conn.Execute("DELETE FROM messages where id = @id;", new { id });
        }

    }
}
