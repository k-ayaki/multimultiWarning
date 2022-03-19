using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace multimultiWarning
{
    class ClaimList : IDisposable
    {
        private bool disposedValue;

        public List<Claim> m_請求項 = new List<Claim>();

        private Regex m_rx;
        public int m_請求項数 { get; set; }

        public int m_最大請求項番号 { get; set; }

        private Document wDocument;

        public int m_エラー個数 { get; set; }

        public ClaimList(Document aDocument)
        {
            wDocument = aDocument;
            m_請求項.Clear();
            m_rx = new Regex(@"【請求項(?<number>[０-９0-9]+)】",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            m_請求項数 = 0;
            m_エラー個数 = 0;
        }

        public void addParagraph(MMParagraph aParagraph)
        {
            if ((aParagraph.書類名 == "請求の範囲"
            || aParagraph.書類名 == "実用新案請求の範囲"
            || aParagraph.書類名 == "特許請求の範囲")
            && aParagraph.項目名.Length > 0)
            {
                if (aParagraph.項目領域 != null)
                {
                    Match match = m_rx.Match(aParagraph.項目名);
                    if (match.Success)
                    {
                        Claim spClaim = new Claim(wDocument, aParagraph.項目領域);
                        spClaim.m_ClaimList = this;
                        m_請求項.Add(spClaim);
                        m_請求項[m_請求項.Count - 1].addParagraph(aParagraph);
                        m_請求項数 = m_請求項.Count;
                        if (m_請求項[m_請求項.Count - 1].m_請求項番号 != m_請求項.Count)
                        {
                            m_請求項[m_請求項.Count - 1].m_連番エラー = true;
                        }
                        if (m_請求項.Count >= 2
                        && m_請求項[m_請求項.Count - 2].m_連番エラー)
                        {
                            m_請求項[m_請求項.Count - 1].m_連番エラー = true;
                        } else
                        {
                            m_最大請求項番号 = m_請求項.Count;
                        }
                    }
                }
                else
                {
                    if (m_請求項.Count > 0)
                    {
                        m_請求項[m_請求項.Count - 1].addParagraph(aParagraph);
                    }
                }
            }
        }
        public void 請求の範囲チェック()
        {
            string エラーメッセージ = "";
            for(int i=0; i<this.m_請求項.Count(); i++)
            {
                Claim claim = this.m_請求項[i];
                if(claim.m_連番エラー)
                {
                    claim.AddComment項目("請求項が昇順に連続していません。" + this.m_請求項[i].項目, WdColor.wdColorAutomatic);
                    claim.記載マーキング(WdColor.wdColorLightYellow);
                }
                else
                {
                    エラーメッセージ = "";

                    claim.引用部分コード化();
                    claim.マルチマルチチェック();
                    if(claim.mm)
                    {
                        エラーメッセージ = "マルチマルチクレーム";
                        claim.記載マーキング(WdColor.wdColorLightYellow);
                    }
                    else
                    if(claim.rmm)
                    {
                        エラーメッセージ = "マルチマルチクレームの引用";
                        claim.記載マーキング(WdColor.wdColorLightTurquoise);
                    }
                    foreach (string エラー in claim.m_エラーメッセージリスト)
                    {
                        エラーメッセージ += ":" + エラー;
                    }
                    if(エラーメッセージ.Length > 0)
                    {
                        claim.AddComment項目(エラーメッセージ, WdColor.wdColorAutomatic);
                    }
                }
                this.m_エラー個数 += claim.m_エラー個数;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~ClaimList()
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
