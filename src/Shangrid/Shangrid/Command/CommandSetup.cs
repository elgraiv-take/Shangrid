using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shangrid.Command
{
    
    [DataContract]
    public class CommandSetup
    {
        [DataContract]
        public class DataRow
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }
            [DataMember(Name = "data")]
            public List<float> Data { get; set; }
        }

        [DataMember(Name = "header_row")]
        public List<string> Header { get; set; }

        [DataMember(Name ="rows")]
        public List<DataRow> Rows { get; set; }
    }
}
