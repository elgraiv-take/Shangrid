using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel=Microsoft.Office.Interop.Excel;

namespace Shangrid
{
    class ShangridWorksheet
    {
        struct CellPosition
        {
            public int Row;
            public int Column;
        }
        private Excel.Application m_application;
        private Excel.Worksheet m_worksheet;

        private int m_headerRow;
        private int m_headerColumn;
        private int m_rowNum;
        private int m_columnNum;
        private Dictionary<string, int> m_rowDict;
        private Dictionary<string, int> m_columnDict;
        private string[] m_rowName;
        private string[] m_columnName;
        private float[,] m_dataTable;
        private bool m_initializing;

        public event CommandChangeFunc ChangeEvent;

        public void Initialize(Excel.Application application)
        {
            m_application = application;
            m_headerRow = 2;
            m_headerColumn = 2;
            m_rowDict = new Dictionary<string, int>();
            m_columnDict = new Dictionary<string, int>();
        }

        public void Setup(Command.CommandSetup command)
        {
            if (m_worksheet == null)
            {
                initializeWorksheet();
            }
            m_initializing = true;
            m_rowDict.Clear();
            m_columnDict.Clear();

            m_columnNum = command.Header.Count;
            m_rowNum = command.Rows.Count;
            var initialTable = new object[m_rowNum+1, m_columnNum+1];
            m_dataTable = new float[m_rowNum, m_columnNum];
            m_rowName = new string[m_rowNum];
            m_columnName = new string[m_columnNum];

            initialTable[0, 0] = "";
            for(var j = 0; j < m_columnNum; j++)
            {
                initialTable[0, j + 1] = command.Header[j];
                m_columnDict[command.Header[j]] = j;
                m_columnName[j] = command.Header[j];
            }
            for(var i=0;i< m_rowNum; i++)
            {
                var rowData= command.Rows[i];
                initialTable[i + 1, 0] = rowData.Name;
                m_rowDict[rowData.Name] = i;
                m_rowName[i] = rowData.Name;
                for (var j = 0; j < m_columnNum; j++)
                {
                    if (j < rowData.Data.Count)
                    {
                        m_dataTable[i, j] = rowData.Data[j];
                        initialTable[i + 1, j + 1] = rowData.Data[j];
                    }
                    else
                    {
                        m_dataTable[i, j] = 0.0f;
                        initialTable[i + 1, j + 1] = 0.0f;
                    }
                }
            }
            m_worksheet.Cells.Clear();
            var start = m_worksheet.Range[
                m_worksheet.Cells[m_headerRow, m_headerColumn],
                m_worksheet.Cells[m_headerRow + m_rowNum, m_headerColumn + m_columnNum]
                ];
            start.Value2 = initialTable;
            m_initializing = false;
        }

        public void ValueChange(Command.CommandChange command)
        {
            if(m_worksheet== null)
            {
                return;
            }
            foreach(var changedCell in command.ChangedCell)
            {
                var position = findCell(changedCell.RowName, changedCell.ColumnName);
                
                if (position.Column<0||position.Row<0)
                {
                    continue;
                }
                m_dataTable[position.Row, position.Column] = changedCell.NewValue;
                dynamic cell = m_worksheet.Cells[position.Row + m_headerRow + 1, position.Column + m_headerColumn + 1];
                cell.Value = changedCell.NewValue;
                
            }
        }

        private CellPosition findCell(string row,string column)
        {
            /*
            //実際のセルから探す版(途中)．重そうなのでひとまずやめておく
            var headerRange = m_worksheet.Range[
                m_worksheet.Cells[m_headerRow, m_headerColumn],
                m_worksheet.Cells[m_headerRow, m_headerColumn + m_columnNum]
                ] as Excel.Range;
            headerRange.Find(column);
            */
            if(!m_columnDict.ContainsKey(column)||!m_rowDict.ContainsKey(row)){
                return new CellPosition(){Row=-1,Column=-1};
            }
            var columnIndex=m_columnDict[column];
            var rowIndex = m_rowDict[row];
            return new CellPosition() { Row = rowIndex, Column = columnIndex };
        }

        private void initializeWorksheet()
        {
            var newWorkbook = m_application.ActiveWorkbook;
            m_worksheet=(dynamic)newWorkbook.Worksheets.Add();
            m_worksheet.Name = "TCPSS";
            m_worksheet.Change += Cell_Change;
        }

        struct ChangedValue
        {
            public CellPosition Position;
            public float Value;
        }

        private void Cell_Change(Excel.Range target)
        {
            if (m_initializing)
            {
                return;
            }
            List<ChangedValue> changedList=new List<ChangedValue>();
            foreach(var cellOjbect in target.Cells)
            {
                var cell = cellOjbect as Excel.Range;
                int dataRowIndex = cell.Row - m_headerRow - 1;
                int dataColumnIndex = cell.Column - m_headerColumn - 1;
                if (dataColumnIndex < 0 || dataRowIndex < 0 ||dataColumnIndex>=m_columnNum||dataRowIndex>=m_rowNum)
                {
                    continue;
                }
                try
                {
                    var value = (float)cell.Value;
                    var preValue = m_dataTable[dataRowIndex, dataColumnIndex];
                    if (value != preValue)
                    {
                        m_dataTable[dataRowIndex, dataColumnIndex] = value;
                        ChangedValue changed;
                        changed.Position.Row = dataRowIndex;
                        changed.Position.Column = dataColumnIndex;
                        changed.Value = value;
                        changedList.Add(changed);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
                
            }
            if (changedList.Count > 0)
            {
                emitChange(changedList);
            }

        }
        private void emitChange(List<ChangedValue> changedList)
        {
            var command=new Command.CommandChange();
            command.ChangedCell = new List<Command.CommandChange.ValueChange>();
            foreach(var changed in changedList)
            {
                command.ChangedCell.Add(
                    new Command.CommandChange.ValueChange()
                    {
                        ColumnName = m_columnName[changed.Position.Column],
                        RowName = m_rowName[changed.Position.Row],
                        NewValue = changed.Value,
                    }
                    );
            }
            ChangeEvent?.Invoke(command);
        }
    }
}
