using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows;

namespace Shangrid
{
    public partial class ShangridRibbon
    {
        private Window m_window;

        private void ShangridRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            m_window = Globals.ThisAddIn.Window;
        }

        private void ShowButton_Click(object sender, RibbonControlEventArgs e)
        {
            m_window.Show();
        }
    }
}
