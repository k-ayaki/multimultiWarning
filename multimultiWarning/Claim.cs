using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Collections;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;


namespace multimultiWarning
{
    /*
     * 請求項クラス 
     * 
     */
    class Claim : IDisposable
    {
        private bool disposedValue;

        public List<MMParagraph> m_paraList = new List<MMParagraph>();
        //
        public List<int> m_CitationList = new List<int>();
        public int m_請求項番号 { get; set; }
        public bool m_連番エラー { get; set; }
        public string 項目 { get; set; }
        public Range 項目領域 { get; set; }
        public string m_記載 { get; set; }
        public string m_記載2 { get; set; }

        private Regex m_rx;
        public List<Citation> citations { get; set; }
        // 全クレームを参照
        public ClaimList m_ClaimList { get; set; }
        public List<string> m_エラーメッセージリスト { get; set; }

        private Document wDocument;
        public bool m { get; set; } // マルチクレーム
        public bool rm { get; set; } // マルチクレームの参照
        public bool mm { get; set; } // マルチマルチクレーム
        public bool rmm { get; set; } // マルチマルチクレームの参照

        public int m_エラー個数 { get; set; }
        public Claim(Document aDocument, Range a項目領域)
        {
            wDocument = aDocument;
            m_paraList.Clear();
            m_請求項番号 = 0;

            m_記載 = "";
            m_記載2 = "";
            m_エラーメッセージリスト = new List<string>();
            m_CitationList.Clear();
            項目領域 = a項目領域;
            if (項目領域 == null)
            {
                項目 = "";
            }
            else
            {
                項目 = 項目領域.Text;
            }
            m_連番エラー = false;

            m_rx = new Regex(@"【請求項(?<number>[０-９]+)】",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match match = m_rx.Match(項目);
            if (match.Success)
            {
                string numStr = match.Groups["number"].Value;
                if (numStr.Length > 0)
                {
                    m_請求項番号 = int.Parse(Strings.StrConv(numStr, VbStrConv.Narrow));

                }
            }
            citations = new List<Citation>();
            this.m = false;
            this.rm = false;
            this.mm = false;
            this.rmm = false;
            this.m_エラー個数 = 0;
        }
        public bool addParagraph(MMParagraph aParagraph)
        {
            if (aParagraph.項目名 == 項目
            && aParagraph.記載領域 != null)
            {
                m_paraList.Add(aParagraph);
                m_記載 += aParagraph.記載領域.Text;
                return true;
            }
            return false;
        }
        private static string ChangeAnk(Match m)
        {
            return Strings.StrConv(m.Value, VbStrConv.Narrow);
        }
        private static string CheckCitation2(Match m)
        {
            string ret = "";
            Regex reg0 = new Regex(@"([0-9]+)([,\/&\-]+)?", RegexOptions.Compiled);
            MatchCollection matches = reg0.Matches(m.Groups[1].Value);
            string thisNum = "";
            string thisOp = "";
            string lastNum = "";
            string lastOp = "";

            foreach (Match match in matches)
            {
                lastNum = thisNum;
                lastOp = thisOp;
                thisNum = match.Groups[1].Value;
                thisOp = match.Groups[2].Value;
                if(lastOp.Length > 0)
                {
                    ret += lastNum;
                    if(lastOp.IndexOf('-') != -1)
                    {
                        lastOp = "&";
                        int st = int.Parse(lastNum);
                        int ed = int.Parse(thisNum);
                        for (int jj = st + 1; jj < ed; jj++)
                        {
                            if (ret.Length > 0)
                            {
                                ret += "&";
                            }
                            ret += jj.ToString();
                        }
                    }
                    ret += lastOp;
                }
            }
            if (thisNum.Length > 0)
            {
                ret += thisNum;
            }
            if (thisOp.Length > 0)
            {
                ret += thisOp;
            }
            return "請求項" + ret;
        }
        public void 引用部分コード化()
        {
            Regex reg0 = new Regex(@"[Ａ-Ｚａ-ｚ０-９]", RegexOptions.Compiled);
            m_記載2 = reg0.Replace(m_記載, ChangeAnk);
            m_記載2 = Regex.Replace(m_記載2, @"\s", @"");
            m_記載2 = Regex.Replace(m_記載2, @"[\u30FC\uFF70\uFF0D\u002D\u2212\u301C\uFF5E\u007E\u02DC\u2053]", @"-");
            m_記載2 = Regex.Replace(m_記載2, @"請求([0-9]+)", @"請求項$1");
            m_記載2 = Regex.Replace(m_記載2, @"ないし|乃至|ー|～|－|から|~|より", @"-");
            m_記載2 = Regex.Replace(m_記載2, @"の?(うち|内|中)の?", @"+");
            m_記載2 = Regex.Replace(m_記載2, @"叉は|又は|または|叉|又|また|や|か|もしくは|若しくは|あるいは|或いは|或は|或|それとも|亦は|亦", @"/");
            m_記載2 = Regex.Replace(m_記載2, @"、|，|／|・|；|：|？", @",");
            m_記載2 = Regex.Replace(m_記載2, @"及び|および|及|と|ならびに|並びに|並び|並に|並|かつ|且つ|且", @"&");
            m_記載2 = Regex.Replace(m_記載2, @",+", @",");
            m_記載2 = Regex.Replace(m_記載2, @"(-+|,-+|-+,|,-+,)", @"-");
            m_記載2 = Regex.Replace(m_記載2, @"(/+|,/+|/+,|,/+,)", @"/");
            m_記載2 = Regex.Replace(m_記載2, @"(&+|,&+|&+,|,&+,)", @"&");
            m_記載2 = Regex.Replace(m_記載2, @"請求の範囲第?([0-9]+)項?", @"請求項$1");
            m_記載2 = Regex.Replace(m_記載2, @"([,\/&\-])第?([0-9]+)項?", @"$1$2");
            m_記載2 = Regex.Replace(m_記載2, @"の?(いずれか|いづれか|いずれ|いづれ|何れか|何か|何れ|どれか|どちらか|孰れか|孰か|孰れ|1項|一項|ひとつ)", @"_");

            Regex reg2 = new Regex(@"(請求項[,\/&\-0-9]+)(請求項)([,\/&\-0-9]+)", RegexOptions.Compiled);
            while(true)
            {
                Match ma = reg2.Match(m_記載2);
                if(ma.Success == false)
                {
                    break;
                }
                m_記載2 = Regex.Replace(m_記載2, @"(請求項[,\/&\-0-9]+)(請求項)([,\/&\-0-9]+)", @"$1$3");
            }
            Regex reg1 = new Regex(@"請求項([,\/&\-0-9]+)", RegexOptions.Compiled);
            m_記載2 = reg1.Replace(m_記載2, CheckCitation2);
        }
        public void マルチマルチチェック()
        {
            string mt = m_記載2;

            for(;;)
            {
                Citation citation = new Citation(mt);
                if(citation.mt.Length == 0)
                {
                    break;
                }
                citations.Add(citation);

                int cr = this.citations.Count - 1;

                if(citations[cr].and == true && citations[cr].ei == false)
                {
                    if (m_エラーメッセージリスト.Contains("択一的に引用されていません。") == false)
                    {
                        m_エラーメッセージリスト.Add("択一的に引用されていません。");
                    }
                }
                int j_len = citations[cr]._ref.Length;
                for (int j = 0; j < j_len; j++)
                {
                    int n = int.Parse(citations[cr]._ref[j]);
                    if(this.m_請求項番号 <= n)
                    {
                        if(m_エラーメッセージリスト.Contains("先に記載された請求項を引用していません。")==false)
                        {
                            m_エラーメッセージリスト.Add("先に記載された請求項を引用していません。");
                        }
                    } else
                    if (n < 1 && m_ClaimList.m_最大請求項番号 < n)
                    {
                        if (m_エラーメッセージリスト.Contains("存在しない請求項を引用しています。") == false)
                        {
                            m_エラーメッセージリスト.Add("存在しない請求項を引用しています。");
                        }
                    }
                    else
                    {
                        if(m_ClaimList.m_請求項[n-1].m || m_ClaimList.m_請求項[n-1].rm)
                        {
                            citations[cr].rm = true;
                        }
                        if (m_ClaimList.m_請求項[n-1].mm || m_ClaimList.m_請求項[n-1].rmm)
                        {
                            citations[cr].rmm = true;
                        }
                        if (citations[cr].rm && citations[cr].rmm) break;
                    }
                }
                this.m |= citations[cr].m;
                this.rm |= citations[cr].rm;
                this.rmm |= citations[cr].rmm;
                citations[cr].mm |= (citations[cr].m & citations[cr].rm);
                this.mm |= citations[cr].mm;
                if (this.mm && this.rmm) break;

                mt = citation.mt;
            }
        }
       
        public void AddComment項目(string msg, Word.WdColor wdColor)
        {
            AddComment(項目領域, msg, wdColor);
        }
        public void 記載マーキング(Word.WdColor wdColor)
        {
            foreach(MMParagraph paragraph in m_paraList)
            {
                paragraph.記載領域.Shading.Texture = Word.WdTextureIndex.wdTextureNone;
                paragraph.記載領域.Shading.ForegroundPatternColor = Word.WdColor.wdColorAutomatic;
                paragraph.記載領域.Shading.BackgroundPatternColor = wdColor;
            }
        }

        public void AddComment(Range range, string msg, Word.WdColor wdColor)
        {
            if (range != null)
            {
                range.Shading.Texture = Word.WdTextureIndex.wdTextureNone;
                range.Shading.ForegroundPatternColor = Word.WdColor.wdColorAutomatic;
                range.Shading.BackgroundPatternColor = wdColor;
                Word.Comment cmt = wDocument.Comments.Add(range, msg);
                cmt.Author = "AppLint";
                this.m_エラー個数++;
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
        // ~Claim()
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
