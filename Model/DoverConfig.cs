using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model
{
    public class DoverConfig
    {
        public string DbConnection { get; set; }
        public string IdentityURL { get; set; }
       public string Audience { get; set; }
        public string LogsFile { get; set; }

    }
}
