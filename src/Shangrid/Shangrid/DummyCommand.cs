using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    class DummyCommand
    {
        public static List<object> GetTestCommand()
        {
            var list = new List<object>();

            list.Add(
                new Command.CommandSetup()
                {
                    Header=new List<string>()
                    {
                        "BoneA","BoneB","BoneC"
                    },
                    Rows=new List<Command.CommandSetup.DataRow>()
                    {
                        new Command.CommandSetup.DataRow()
                        {
                            Name="v[0]",
                            Data=new List<float>() {1.0f,0.0f,0.0f }
                        },
                        new Command.CommandSetup.DataRow()
                        {
                            Name="v[1]",
                            Data=new List<float>() {0.3f,0.4f,0.3f }
                        },
                        new Command.CommandSetup.DataRow()
                        {
                            Name="v[2]",
                            Data=new List<float>() {1.0f,0.5f,0.5f }
                        },
                    }
                    
                }
                );

            list.Add(
                new Command.CommandChange()
                {
                    ChangedCell = new List<Command.CommandChange.ValueChange>()
                    {
                        new Command.CommandChange.ValueChange()
                        {
                            RowName="v[0]",ColumnName="BoneB",NewValue=0.5f,
                        },
                    }
                }
                );
            list.Add(
                new Command.CommandChange()
                {
                    ChangedCell = new List<Command.CommandChange.ValueChange>()
                    {
                        new Command.CommandChange.ValueChange()
                        {
                            RowName="v[1]",ColumnName="BoneA",NewValue=1.5f,
                        },
                        new Command.CommandChange.ValueChange()
                        {
                            RowName="v[2]",ColumnName="BoneC",NewValue=0.0f,
                        },
                    }
                }
                );
            return list;
        }
    }
}
