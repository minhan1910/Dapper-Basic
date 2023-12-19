using AutoMapper;
using IntermediateTheory_Include_Introduce_Dapper.Data;
using IntermediateTheory_Include_Introduce_Dapper.Seeder;
using Microsoft.Extensions.Configuration;

namespace IntermediateTheory_Include_Introduce_Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            new UserSeeder(dapper)
                .SeedUsers();
            //.SeedUserSalary();
            //.SeedUserJobInfo();
        }
    }
}