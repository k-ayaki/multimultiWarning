
namespace multimultiWarning
{
    partial class VersionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelModifiedDate = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.linkLabelDownload = new System.Windows.Forms.LinkLabel();
            this.labelDL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(211, 184);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 34);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(161, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(86, 18);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "labelName";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(355, 25);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(99, 18);
            this.labelVersion.TabIndex = 5;
            this.labelVersion.Text = "labelVersion";
            // 
            // labelModifiedDate
            // 
            this.labelModifiedDate.AutoSize = true;
            this.labelModifiedDate.Location = new System.Drawing.Point(161, 61);
            this.labelModifiedDate.Name = "labelModifiedDate";
            this.labelModifiedDate.Size = new System.Drawing.Size(141, 18);
            this.labelModifiedDate.TabIndex = 6;
            this.labelModifiedDate.Text = "labelModifiedDate";
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(164, 98);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(93, 18);
            this.labelAuthor.TabIndex = 7;
            this.labelAuthor.Text = "labelAuthor";
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Location = new System.Drawing.Point(43, 40);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxIcon.TabIndex = 8;
            this.pictureBoxIcon.TabStop = false;
            // 
            // linkLabelDownload
            // 
            this.linkLabelDownload.AutoSize = true;
            this.linkLabelDownload.Location = new System.Drawing.Point(149, 135);
            this.linkLabelDownload.Name = "linkLabelDownload";
            this.linkLabelDownload.Size = new System.Drawing.Size(347, 18);
            this.linkLabelDownload.TabIndex = 9;
            this.linkLabelDownload.TabStop = true;
            this.linkLabelDownload.Text = "https://osdn.net/projects/multimultiwarning/";
            this.linkLabelDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDownload_LinkClicked);
            // 
            // labelDL
            // 
            this.labelDL.AutoSize = true;
            this.labelDL.Location = new System.Drawing.Point(23, 135);
            this.labelDL.Name = "labelDL";
            this.labelDL.Size = new System.Drawing.Size(106, 18);
            this.labelDL.TabIndex = 10;
            this.labelDL.Text = "ダウンロード先";
            // 
            // VersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 248);
            this.Controls.Add(this.labelDL);
            this.Controls.Add(this.linkLabelDownload);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelModifiedDate);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonOK);
            this.Name = "VersionForm";
            this.Text = "バージョン情報";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelModifiedDate;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.LinkLabel linkLabelDownload;
        private System.Windows.Forms.Label labelDL;
    }
}