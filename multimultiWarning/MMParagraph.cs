using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using Word = Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;

namespace multimultiWarning
{
    class MMParagraph : IDisposable
    {
        private bool disposedValue;

        private Paragraph wParagraph;

        private Document wDocument;
        public string 項目 { get; set; }
        public Range 項目領域 { get; set; }
        public string 記載 { get; set; }
        public Range 記載領域 { get; set; }
        public string 書類名 { get; set; }
        public string 項目名 { get; set; }
        public string 段落番号 { get; set; }
        public string 数化表名 { get; set; }

        public MMParagraph(Document aDocument, Paragraph aParagraph, MMParagraph paraPrev = null)
        {
            wDocument = aDocument;
            wParagraph = aParagraph;
            if (paraPrev != null)
            {
                書類名 = paraPrev.書類名;
                項目名 = paraPrev.項目名;
                段落番号 = paraPrev.段落番号;
                数化表名 = paraPrev.数化表名;
            }
            else
            {
                書類名 = "";
                項目名 = "";
                段落番号 = "";
                数化表名 = "";
            }
            if (wParagraph.Range.OMaths.Count == 0 && wParagraph.Range.Tables.Count == 0 && wParagraph.Range.InlineShapes.Count == 0)
            {
                if (wParagraph.Range.Text.IndexOf("【", 0, wParagraph.Range.Text.Length, StringComparison.Ordinal) == -1)
                {
                    int idx = 0;
                    char[] charToTrim = { '\r', '\b' };
                    項目 = "";
                    項目領域 = null;
                    記載 = wParagraph.Range.Text.TrimEnd(charToTrim);
                    記載領域 = SetText2Rng(wParagraph.Range, idx, 記載);
                }
                else
                {
                    Regex rx = new Regex(@"(?:[\f　 \t]*)(?<item>【\S+】|)(?<contents>[\S 　\t]*)", RegexOptions.Compiled);
                    Match w_match = rx.Match(wParagraph.Range.Text.TrimEnd());
                    if (w_match.Success)
                    {
                        int idx;
                        idx = w_match.Groups["item"].Index;
                        項目 = w_match.Groups["item"].Value;
                        項目領域 = SetText2Rng(wParagraph.Range, idx, 項目);
                        idx = w_match.Groups["contents"].Index;
                        記載 = w_match.Groups["contents"].Value;
                        記載領域 = SetText2Rng(wParagraph.Range, idx, 記載);

                        if (項目領域 != null)
                        {
                            項目領域.Font.Shading.BackgroundPatternColor = Word.WdColor.wdColorAutomatic;
                            項目領域.Shading.ForegroundPatternColor = Word.WdColor.wdColorAutomatic;
                            if (項目 == "【書類名】")
                            {
                                char[] charToTrim = { ' ', '　', '\t', '\r', '\b' };
                                書類名 = 記載.Trim(charToTrim);
                                段落番号 = "";
                                数化表名 = "";
                                項目名 = 項目;
                            }
                            else
                            {
                                Regex rx2 = new Regex(@"【[０-９]{4,4}】", RegexOptions.None);
                                Match w_match2 = rx2.Match(項目);
                                if (w_match2.Success)
                                {
                                    段落番号 = 項目;
                                    数化表名 = "";
                                }
                                else
                                {
                                    Regex rx3 = new Regex(@"【(?:数|化|表)?[０-９]+】", RegexOptions.None);
                                    Match w_match3 = rx3.Match(項目);
                                    if (w_match3.Success)
                                    {
                                        数化表名 = 項目;
                                    }
                                    else
                                    {
                                        項目名 = 項目;
                                        数化表名 = "";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int idx = 0;
                        char[] charToTrim = { '\r', '\b' };
                        項目 = "";
                        項目領域 = null;
                        記載 = wParagraph.Range.Text.TrimEnd(charToTrim);
                        記載領域 = SetText2Rng(wParagraph.Range, idx, 記載);
                    }
                }
            }
        }
        // rng : 文字列を含む Range
        // idx : 
        // str : 指定文字列
        private Range SetText2Rng(
            Range rng,
            int idx,
            String str
            )
        {
            if (str.Length == 0)
            {
                return null;
            }
            object selS = rng.Characters[idx + 1].Start;
            object selE = rng.Characters[idx + str.Length].End;
            Range myRng = wDocument.Range(ref selS, ref selE);
            return myRng;
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
        // ~MMParagraph()
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
