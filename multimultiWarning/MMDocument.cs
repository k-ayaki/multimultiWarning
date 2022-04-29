using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using ltSupport01;

namespace multimultiWarning
{
    class MMDocument : IDisposable
    {
        private bool disposedValue;

        private Document wDocument;
        public bool TrackRevisions { get; set; }

        private ClaimList 請求の範囲2;
        public MMDocument(Document aDocument)
        {
            wDocument = aDocument;
            this.TrackRevisions = wDocument.TrackRevisions;
            if (wDocument.TrackRevisions == true)
            {
                MessageBox.Show("変更履歴の記録をオフしてくたさい");
                return;
            }
            請求の範囲2 = new ClaimList(wDocument);
        }
        public void 垂直タブを改行に()
        {
            object missing = null;

            int spos = wDocument.Application.Selection.Start;
            int epos = wDocument.Application.Selection.End;
            wDocument.Application.Selection.WholeStory();
            Find findObject = wDocument.Application.Selection.Find;
            findObject.ClearFormatting();
            findObject.Text = "^l";
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = "^p";
            findObject.MatchFuzzy = false;
            findObject.Forward = true;
            object findtext = "^l";
            object replacetext = "^p";
            object replaceAll = WdReplace.wdReplaceAll;
            findObject.Execute(ref findtext, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref replacetext,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing);
            wDocument.Application.Selection.End = epos;
            wDocument.Application.Selection.Start = spos;
        }
        public void eraseMarker()
        {
            int lastIndex = wDocument.Characters.Count - 1;
            if (lastIndex > 0)
            {
                Word.Range range = wDocument.Range(0, wDocument.Characters[lastIndex].End);
                range.Font.Shading.BackgroundPatternColor = WdColor.wdColorAutomatic;
                range.Shading.ForegroundPatternColor = WdColor.wdColorAutomatic;
                range.Shading.BackgroundPatternColor = WdColor.wdColorAutomatic;
            }
        }
        public void deleteComment()
        {
            if (wDocument.Comments.Count > 0)
                wDocument.DeleteAllComments();
        }
        public bool ReadClaim()
        {
            bool fRet = true;
            ProgressForm pd;
            pd = new ProgressForm("マルチマルチクレームのチェック",
                 new DoWorkEventHandler(ProgressDialog_Support_ReadClaim),
                    16);
            //進行状況ダイアログを表示する
            DialogResult result = pd.ShowDialog();
            //結果を取得する
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("キャンセルされました");
                fRet = false;
            }
            else if (result == DialogResult.Abort)
            {
                //エラー情報を取得する
                Exception ex = pd.Error;
                MessageBox.Show("エラー: " + ex.Message);
                fRet = false;
            }
            else if (result == DialogResult.OK)
            {
                //結果を取得する
                int stopTime = (int)pd.Result;
                fRet = true;
            }
            //後始末
            pd.Dispose();
            return fRet;
        }
        // DoMAイベントハンドラ
        // 形態素解析:符号検査
        private void ProgressDialog_Support_ReadClaim(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            DateTime currentDate;

            //パラメータを取得する
            int stopTime = (int)e.Argument;

            int i = 0;
            currentDate = DateTime.Now;
            long lastTick = currentDate.Ticks;
            long currTick;
            MMParagraph mmParagraph = null;
            foreach (Paragraph paragraph in wDocument.Paragraphs)
            {
                mmParagraph = new MMParagraph(wDocument, paragraph, mmParagraph);
                switch (mmParagraph.書類名)
                {
                    case "明細書":
                        break;
                    case "要約書":
                        break;
                    case "特許請求の範囲":
                    case "実用新案登録請求の範囲":
                    case "請求の範囲":
                        請求の範囲2.addParagraph(mmParagraph);
                        break;
                    default:
                        break;
                }

                i++;
                //キャンセルされたか調べる
                if (bw.CancellationPending)
                {
                    //キャンセルされたとき
                    e.Cancel = true;
                    return;
                }
                currentDate = DateTime.Now;
                currTick = currentDate.Ticks;
                if (currTick - lastTick > 60 * 10000)
                {
                    //指定された時間待機する
                    System.Threading.Thread.Sleep(5);

                    int percent = i * 100 / (wDocument.Paragraphs.Count);
                    bw.ReportProgress(percent, "");
                    lastTick = currTick;
                }
            }
            //結果を設定する
            e.Result = 0;
        }

        public void 結果判定()
        {
            if (請求の範囲2.m_請求項数 >= 1)
            {
                請求の範囲2.請求の範囲チェック();
                if (請求の範囲2.m_エラー個数 == 0)
                {
                    MessageBox.Show("請求の範囲にマルチマルチクレームと択一引用のエラーは発見されませんでした。", "マルチマルチチェッカー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("請求の範囲が記載されていません。", "マルチマルチチェッカー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    請求の範囲2.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~MMDocument()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
