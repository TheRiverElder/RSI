using System.Collections.Generic;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization.Tokenizers {
    public class IdTokenizer : ITokenizer {

        public string[] Ids = new string[] {
            "!=", "==", ">=", "<=", ">", "<",
            "=>", "...", "..",
            "++", "--",
            "+", "-", "*", "/", "%",
            "(", ")", "[", "]", "{", "}",
            ":", "@", "#", "&&", "||", "!", "?",
            ";", ",", ".", "=", "$", "|", "~"
        };

        public Dictionary<string, string> Aliases = new Dictionary<string, string> {
            ["！="] = "!=",
            ["。。。"] = "...",
            ["。。"] = "..",
            ["，"] = ",",
            ["；"] = ";",
            ["："] = ":",
            ["？"] = "?",
            ["！"] = "!",
            ["（"] = "(",
            ["）"] = ")",
            ["\n"] = "EOL",
        };



        private readonly string[] ValidPunc;

        public IdTokenizer() {
            ValidPunc = Ids;
        }

        public IdTokenizer(string[] validPunc) {
            ValidPunc = validPunc;
        }



        public bool Tokenize(StringReader reader, out Token token) {
            foreach (string p in Aliases.Keys) {
                if (reader.Read(p)) {
                    token = new Token(Aliases[p]);
                    return true;
                }
            }
            foreach (string p in ValidPunc) {
                if (reader.Read(p)) {
                    token = new Token(p);
                    return true;
                }
            }
            token = null;
            return false;
        }
    }
}
