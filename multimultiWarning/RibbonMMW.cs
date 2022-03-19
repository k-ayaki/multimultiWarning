using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace multimultiWarning
{
    public partial class RibbonMMW
    {
        private void RibbonMMW_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void buttonMultiMulti_Click(object sender, RibbonControlEventArgs e)
        {
            MMDocument mmDocument = new MMDocument(Globals.ThisAddIn.Application.ActiveDocument);
            if (mmDocument != null)
            {
                if (mmDocument.TrackRevisions == false)
                {
                    mmDocument.垂直タブを改行に();
                    mmDocument.deleteComment();
                    mmDocument.eraseMarker();
                    mmDocument.ReadClaim();
                    mmDocument.結果判定();
                }
                mmDocument.Dispose();
            }
        }

        private void buttonClear_Click(object sender, RibbonControlEventArgs e)
        {
            MMDocument mmDocument = new MMDocument(Globals.ThisAddIn.Application.ActiveDocument);
            if (mmDocument != null)
            {
                if (mmDocument.TrackRevisions == false)
                {
                    mmDocument.垂直タブを改行に();
                    mmDocument.deleteComment();
                    mmDocument.eraseMarker();
                }
                mmDocument.Dispose();
            }
        }

        private void buttonVersion_Click(object sender, RibbonControlEventArgs e)
        {
            VersionForm f = new VersionForm();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
