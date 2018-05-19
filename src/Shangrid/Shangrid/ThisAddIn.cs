using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Windows;

namespace Shangrid
{
    public partial class ThisAddIn
    {
        private ShangridWorksheet m_worksheet;

        public Controller Controller { get; } = new Controller();
        public Window Window { get; private set; }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            m_worksheet = new ShangridWorksheet();
            m_worksheet.Initialize(Application);
            m_worksheet.ChangeEvent += Controller.Core.SendChangeCommand;
            m_worksheet.SheetDeleted += (o, ev) => Controller.Stop();
            Controller.Core.SetupEvent += m_worksheet.Setup;
            Controller.Core.ChangeEvent += m_worksheet.ValueChange;

            Window = new ControllerView();
            Window.DataContext = Controller;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Controller.Stop();
            Window.Close();
            Controller.Dispose();
            m_worksheet.Dispose();
        }
        

        #region VSTO で生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
