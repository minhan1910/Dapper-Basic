using IntermediateTheory_Include_Introduce_Dapper.Data;
using IntermediateTheory_Include_Introduce_Dapper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateTheory_Include_Introduce_Dapper.Seeder
{
    public class UserSeeder
    {
        private readonly DataContextDapper _dapper;

        public UserSeeder(DataContextDapper dataContextDapper)
        {
            _dapper = dataContextDapper;
        }

        private static IEnumerable<T> GetJsonOnFile<T>(string filePath)
            => JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(PathOf(filePath)));

        public void SeedUsers()
        {
            IEnumerable<Users> usersJson = GetJsonOnFile<Users>("Users.json");

            string sql = @"
            set IDENTITY_INSERT TutorialAppSchema.Users ON;      
            insert into TutorialAppSchema.Users
            (
	            [UserId]
                ,[FirstName]
                ,[LastName]
                ,[Email]
                ,[Gender]
                ,[Active]
            ) values ";

            foreach (Users user in usersJson)
            {
                string sqlCommand = new StringBuilder(sql)                                        
                   .Append("(")
                   .AppendLine($"'{user.UserId}',")
                   .AppendLine($"'{EscapeStringQuote(user.FirstName)}',")
                   .AppendLine($"'{EscapeStringQuote(user.LastName)}',")
                   .AppendLine($"'{EscapeStringQuote(user.Email)}',")
                   .AppendLine($"'{EscapeStringQuote(user.Gender)}',")
                   .AppendLine($"'{user.Active}'")
                   .Append(")").ToString();

                _dapper.ExecuteSql(sqlCommand);
            }
        }

        public void SeedUserJobInfo()
        {
            IEnumerable<UserJobInfo> userJobInfoJson = GetJsonOnFile<UserJobInfo>("UserJobInfo.json");

            string sql = @"
            set IDENTITY_INSERT TutorialAppSchema.Users ON;      
            insert into TutorialAppSchema.UserJobInfo
            (
	            [UserId]
                ,[JobTitle]
                ,[Department]
            ) values ";

            foreach (UserJobInfo user in userJobInfoJson)
            {
                string sqlCommand = new StringBuilder(sql)
                   .Append("(")
                   .AppendLine($"'{user.UserId}',")
                   .AppendLine($"'{user.JobTitle}',")
                   .AppendLine($"'{user.Department}'")
                   .Append(")").ToString();

                _dapper.ExecuteSql(sqlCommand);
            }
        }

        public void SeedUserSalary()
        {
            IEnumerable<UserSalary> userSalaryJson = GetJsonOnFile<UserSalary>("UserSalary.json");

            string sql = @"
            set IDENTITY_INSERT TutorialAppSchema.Users ON;      
            insert into TutorialAppSchema.UserSalary
            (
	            [UserId]
                ,[Salary]
            ) values ";

            foreach (UserSalary user in userSalaryJson)
            {
                string sqlCommand = new StringBuilder(sql)
                   .Append("(")
                   .AppendLine($"'{user.UserId}',")
                   .AppendLine($"'{user.Salary}'")
                   .Append(")").ToString();

                _dapper.ExecuteSql(sqlCommand);
            }
        }

        private static string EscapeStringQuote(string input)
        {
            string output = input.Replace("'", "''");
            return output;
        }

        private static string PathOf(string path)
            => Path.Combine(new DirectoryInfo(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName, path);
    }
}
