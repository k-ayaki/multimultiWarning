using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace multimultiWarning
{
    /*
     * 引用部分クラス 
     * 
     */
    class Citation : IDisposable
    {
        private bool disposedValue;
        public string mt { get; set; }
        public string rs { get; set; }  // 引用部分テキスト
        public string[] _ref { get; set; }
        public string bk { get; set; }  // 引用部分の後
        public string bk20 { get; set; }
        public bool or { get; set; }    // および
        public bool and { get; set; }   // または
        public bool ei { get; set; }    // 何れか
        public bool m { get; set; }
        public bool rm { get; set; }
        public bool mm { get; set; }
        public bool rmm { get; set; }

        public Citation(string a_mt)
        {
            Regex reg0 = new Regex(@"(請求項[0-9][,\/&0-9]*)(.*)", RegexOptions.Compiled);

            Match ma = reg0.Match(a_mt);
            if (ma.Success == false)
            {
                mt = "";
                this.rs = "";
                this.bk = "";
                this.bk20 = "";
                this.or = false;
                this.and = false;
                this.ei = false;
                this.m = false;
                this.rm = false;
                this.mm = false;
                this.rmm = false;
            }
            else
            {
                this.mt = ma.Groups[2].Value;
                this.rs = ma.Groups[1].Value;
                string ma1 = ma.Groups[1].Value;
                this.and = false;
                this.or = false;

                if (ma1.IndexOf('&') != -1)
                {
                    this.and = true;
                } else
                if (ma1.IndexOf('/') != -1)
                {
                    this.or = true;
                }
                ma1 = Regex.Replace(ma1, @"請求項", "");
                ma1 = Regex.Replace(ma1, @"[,\/ &]", "\t");
                ma1 = Regex.Replace(ma1, @"\t+", "\t");
                this._ref = ma1.Split('\t');
                this.bk = ma.Groups[2].Value;
                if(this.bk.IndexOf("請求項") != -1)
                {
                    this.bk.Substring(0, this.bk.IndexOf("請求項"));
                }
                this.ei = false;
                this.m = false;
                this.rm = false;
                this.mm = false;
                this.rmm = false;

                if (this.bk.Length < 20)
                {
                    this.bk20 = this.bk;
                }
                else
                {
                    this.bk20 = this.bk.Substring(0, 20);
                }

                if (this._ref.Length > 1)
                {
                    this.m = true;
                }

                if (this.and
                && this.bk20.IndexOf('_') != -1)
                {
                    this.ei = true;
                }
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
        // ~Citation()
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
