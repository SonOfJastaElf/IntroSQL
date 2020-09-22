using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IntroSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            Print(depos);

            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is this new department called?");
                userResponse = Console.ReadLine();
                
                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            Console.WriteLine("Have a nice day!");
        }

        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }
        }
    }
}
