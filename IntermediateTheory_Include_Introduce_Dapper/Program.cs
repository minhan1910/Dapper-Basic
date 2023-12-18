﻿using IntermediateTheory_Include_Introduce_Dapper.Data;
using IntermediateTheory_Include_Introduce_Dapper.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

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
            //DataContextEF ef = new DataContextEF(config);

            //string sqlCommand = "SELECT GETDATE()";
            //DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            //Computer computer = new Computer
            //{
            //    Motherboard = "Z690",
            //    HasWifi = true,
            //    HasLTE = false,
            //    ReleaseDate = DateTime.Now,
            //    VideoCard = "RTX 2060",
            //    Price = 943.38m,
            //};



            string computersJson = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName, "Computers.json");

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            //IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>
            //    (File.ReadAllText(computersJson), options);

            // using before .net 6
            // this is automatically camel case by Newtonsoft.Json
            IEnumerable<Computer>? computers = 
                JsonConvert.DeserializeObject<IEnumerable<Computer>>(File.ReadAllText(computersJson));

            if (computers is not null)
            {
                foreach (Computer computer in computers)
                {

                    StringBuilder sql = new StringBuilder("\n" + @"insert into [TutorialAppSchema].[Computer] (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard 
                    ) values ");

                    sql.Append("(")
                        .Append($"'{EscapeStringQuote(computer.Motherboard)}',")
                        .Append($"'{computer.HasWifi}',")
                        .Append($"'{computer.HasLTE}',")
                        .Append($"'{computer.ReleaseDate}',")
                        .Append($"'{computer.Price}',")
                        .Append($"'{EscapeStringQuote(computer.VideoCard)}'")
                        .AppendLine(")");                    

                    dapper.ExecuteSql(sql.ToString());
                }
            }

            string computersCopy1 = JsonConvert.SerializeObject(computers, settings);
            string computersCopy2 = System.Text.Json.JsonSerializer.Serialize(computers, options);
            
            File.WriteAllText(PathOf("computersCopyNewtonsoft.txt"), computersCopy1);
            File.WriteAllText(PathOf("computersCopySystem.txt"), computersCopy2);

        }
            
        static string EscapeStringQuote(string input)
        {
            string output = input.Replace("'", "''");
            return output;
        }

        static string PathOf(string path) 
            => Path.Combine(new DirectoryInfo(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName, path);
    }
}