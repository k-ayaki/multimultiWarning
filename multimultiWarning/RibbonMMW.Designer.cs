
namespace multimultiWarning
{
    partial class RibbonMMW : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonMMW()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupMMW = this.Factory.CreateRibbonGroup();
            this.buttonMultiMulti = this.Factory.CreateRibbonButton();
            this.buttonClear = this.Factory.CreateRibbonButton();
            this.buttonVersion = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.groupMMW.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupMMW);
            this.tab1.Label = "AppLint";
            this.tab1.Name = "tab1";
            // 
            // groupMMW
            // 
            this.groupMMW.Items.Add(this.buttonMultiMulti);
            this.groupMMW.Items.Add(this.buttonClear);
            this.groupMMW.Items.Add(this.buttonVersion);
            this.groupMMW.Label = "請求項";
            this.groupMMW.Name = "groupMMW";
            // 
            // buttonMultiMulti
            // 
            this.buttonMultiMulti.Image = global::multimultiWarning.Properties.Resources.multimulti4;
            this.buttonMultiMulti.Label = "マルチマルチ警告";
            this.buttonMultiMulti.Name = "buttonMultiMulti";
            this.buttonMultiMulti.ShowImage = true;
            this.buttonMultiMulti.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMultiMulti_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Label = "クリア";
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.OfficeImageId = "EraserMode";
            this.buttonClear.ShowImage = true;
            this.buttonClear.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonClear_Click);
            // 
            // buttonVersion
            // 
            this.buttonVersion.Label = "バージョン情報";
            this.buttonVersion.Name = "buttonVersion";
            this.buttonVersion.OfficeImageId = "VersionHistory";
            this.buttonVersion.ShowImage = true;
            this.buttonVersion.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonVersion_Click);
            // 
            // RibbonMMW
            // 
            this.Name = "RibbonMMW";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonMMW_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupMMW.ResumeLayout(false);
            this.groupMMW.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupMMW;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonMultiMulti;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonClear;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonVersion;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonMMW RibbonMMW
        {
            get { return this.GetRibbon<RibbonMMW>(); }
        }
    }
}
