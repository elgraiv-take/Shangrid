using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shangrid.Command
{
    
    [DataContract]
    public class CommandSelect
    {
        [DataContract]
        public class Selection
        {
            [DataMember(Name = "row")]
            public string RowName { get; set; }
            [DataMember(Name = "col")]
            public string ColumnName { get; set; }
        }

        [DataMember(Name ="selected")]
        public List<Selection> SelectedCell { get; set; }
    }
}
