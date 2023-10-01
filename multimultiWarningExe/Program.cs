using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace multimultiWarningExe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Wordのインスタンスを作成
            Word.Application app;
            app = new Word.Application();

            //表示する
            app.Visible = true;

            //Documents コレクションのAdd メソッドを使用して、
            //Normal.dot に基づく新しい文書を作成します。
            object missingValue = Type.Missing;
            app.Documents.Add(ref missingValue, ref missingValue,
                ref missingValue, ref missingValue);
        }
    }
}
