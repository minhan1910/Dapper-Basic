using IntermediateTheory_Include_Introduce_Dapper.Data;
using IntermediateTheory_Include_Introduce_Dapper.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;

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
            DataContextEF ef = new DataContextEF(config);

            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            Computer computer = new Computer
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                VideoCard = "RTX 2060",
                Price = 943.38m,
            };

            StringBuilder sql = new StringBuilder("\n" + @"insert into [TutorialAppSchema].[Computer] (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard 
            ) values ");

            sql.Append("(")
                .Append($"'{computer.Motherboard}',")
                .Append($"'{computer.HasWifi}',")
                .Append($"'{computer.HasLTE}',")
                .Append($"'{computer.ReleaseDate}',")
                .Append($"'{computer.Price}',")
                .Append($"'{computer.VideoCard}'")
                .AppendLine(")");
            
            string currentPath = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\";

            File.WriteAllText(currentPath + "\\" + "log.txt", sql.ToString());

            string logtxt = $"{currentPath}log.txt";
            using StreamWriter openFile = new($"{currentPath}log.txt", append: true);
            openFile.WriteLine(sql.ToString());
            openFile.Close();

            Console.WriteLine(File.ReadAllText(logtxt));
        }
    }
}