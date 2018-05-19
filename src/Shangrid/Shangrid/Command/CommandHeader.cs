using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shangrid.Command
{
    [DataContract]
    public class CommandHeader<BodyType>
    {
        [DataMember(Name ="type")]
        public CommandType CommandBodyType { get; set; }
        
        [DataMember(Name ="body")]
        public BodyType CommandBody { get; set; } 
    }
}
