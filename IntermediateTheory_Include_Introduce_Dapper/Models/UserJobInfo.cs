using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateTheory_Include_Introduce_Dapper.Models
{
    internal class UserJobInfo
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}
