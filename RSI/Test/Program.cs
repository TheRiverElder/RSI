using System;
using top.riverelder.RSI.Tokenization;
using top.riverelder.RSI.Tokenization.Tokenizers;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Test {
    public class Program {

        public static void Main() {
            TestTokenStream();
            Console.WriteLine();
            Console.WriteLine("========");
            Console.WriteLine("EOT");
            Console.ReadKey();
        }

        public static void TestTokenStream() {
            string s = "   123 123. 456 778 \"Hello World!\" \"I said \\\"Hello World\\\"\" “我说“问世界好在\\””  999";
            StringReader reader = new StringReader(s);
            TokenStream ts = new TokenStream(reader);

            ts.AddTokenizers(
                new DecimalTokenizer(),
                new DigitTokenizer(),
                new StringTokenizer()
                );

            while (ts.HasMore) {
                Console.WriteLine(ts.Read());
            }
        }

    }
}
