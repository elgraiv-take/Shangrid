using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Shangrid.Command
{
    [DataContract]
    public enum CommandType
    {
        [EnumMember(Value="setup")]
        Setup,
        [EnumMember(Value = "change")]
        Change,
        [EnumMember(Value = "select")]
        Select,

    }
    public static class CommandTypeDictionary
    {
        public static Dictionary<CommandType, Type> Dict { get; } = new Dictionary<CommandType, Type>()
        {
            { CommandType.Setup,typeof(CommandSetup) },
            { CommandType.Change,typeof(CommandChange) },
            { CommandType.Select,typeof(CommandSelect) },
        };
    }
}
