using IntermediateTheory_Include_Introduce_Dapper.Data;
using IntermediateTheory_Include_Introduce_Dapper.Models;
using Microsoft.Extensions.Configuration;
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

            StringBuilder sql = new StringBuilder(@"
                insert into [TutorialAppSchema].[Computer] (
                    Motherboard,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard 
                ) values ");

            sql.AppendLine("(")
                .AppendLine($"'{computer.Motherboard}',")
                .AppendLine($"'{computer.HasWifi}',")
                .AppendLine($"'{computer.HasLTE}',")
                .AppendLine($"'{computer.ReleaseDate}',")
                .AppendLine($"'{computer.Price}',")
                .AppendLine($"'{computer.VideoCard}'")
                .AppendLine(")");

            //Console.WriteLine(sql);

            //int result = dbConnection.Execute(sql.ToString());

            string sqlSelect = @"
                select
                    Computer.Motherboard,
                    Computer.HasWifi,
                    Computer.HasLTE,
                    Computer.ReleaseDate,
                    Computer.Price,
                    Computer.VideoCard 
                from TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach (Computer singleComputer in ef.Computer.ToList())
            {   
                Console.WriteLine(new StringBuilder()                
                .Append("(")
                .Append($"'{singleComputer.Motherboard}',")
                .Append($"'{singleComputer.HasWifi}',")
                .Append($"'{singleComputer.HasLTE}',")
                .Append($"'{singleComputer.ReleaseDate}',")
                .Append($"'{singleComputer.Price}',")
                .Append($"'{singleComputer.VideoCard}'")
                .AppendLine(")").ToString());
            }
        }
    }
}