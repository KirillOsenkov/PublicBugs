using System;

class Program
{
    static void Main(string[] args)
    {
        var poolManager = typeof(MySql.Data.MySqlClient.MySqlConnection).Assembly.GetType("MySql.Data.MySqlClient.MySqlPoolManager");
        var instance = Activator.CreateInstance(poolManager);
    }
}