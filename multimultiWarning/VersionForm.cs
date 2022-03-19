using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;

namespace multimultiWarning
{
    public partial class VersionForm : Form
    {
        public VersionForm()
        {
            InitializeComponent();
            this.labelName.Text = Properties.Resources.Name;
            this.labelVersion.Text = Properties.Resources.Version;
            this.labelModifiedDate.Text = Properties.Resources.ModifiedDate;
            this.labelAuthor.Text = Properties.Resources.Author;

            this.pictureBoxIcon.Image = Properties.Resources.multimulti4; //canvas;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }
        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited
            // to true.
            linkLabelDownload.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://osdn.net/projects/multimultiwarning/");
        }
    }
}
