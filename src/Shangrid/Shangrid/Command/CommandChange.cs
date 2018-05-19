using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shangrid.Command
{
    [DataContract]
    public class CommandChange
    {
        [DataContract]
        public class ValueChange
        {
            [DataMember(Name = "row")]
            public string RowName { get; set; }
            [DataMember(Name = "col")]
            public string ColumnName { get; set; }

            [DataMember(Name = "value")]
            public float NewValue { get; set; }
        }

        [DataMember(Name = "changed")]
        public List<ValueChange> ChangedCell { get; set; }
    }
}
