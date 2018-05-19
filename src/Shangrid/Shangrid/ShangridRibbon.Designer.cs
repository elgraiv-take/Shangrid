namespace Shangrid
{
    partial class ShangridRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ShangridRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.ShangridRibbonGroup = this.Factory.CreateRibbonGroup();
            this.ShowButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.ShangridRibbonGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.ShangridRibbonGroup);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // ShangridRibbonGroup
            // 
            this.ShangridRibbonGroup.Items.Add(this.ShowButton);
            this.ShangridRibbonGroup.Label = "Shangrid";
            this.ShangridRibbonGroup.Name = "ShangridRibbonGroup";
            // 
            // ShowButton
            // 
            this.ShowButton.Label = "Show Ctrl Panel";
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ShowButton_Click);
            // 
            // ShangridRibbon
            // 
            this.Name = "ShangridRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ShangridRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.ShangridRibbonGroup.ResumeLayout(false);
            this.ShangridRibbonGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup ShangridRibbonGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ShowButton;
    }

    partial class ThisRibbonCollection
    {
        internal ShangridRibbon ShangridRibbon
        {
            get { return this.GetRibbon<ShangridRibbon>(); }
        }
    }
}
