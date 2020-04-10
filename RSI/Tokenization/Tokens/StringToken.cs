using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Tokenization.Tokens {
    public class StringToken : Token {

        public static TokenType TypeString = new TokenType("string");

        public string Str { get; }

        public override object Value => Str;

        public override string Literal => '"' + MakeEscapedString(Str) + '"';

        public override TokenType Type => TypeString;

        public StringToken(string str) {
            Str = str;
        }

        public static string MakeEscapedString(string origin) {
            return origin
                .Replace("\"", "\\\"")
                .Replace("\n", @"\n")
                .Replace("\t", @"\t")
                .Replace("\r", @"\r")
                ;
        }
    }
}
