using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization.Tokenizers {
    public class NameTokenizer : ITokenizer {

        public bool Tokenize(StringReader reader, out Token token) {
            char head = reader.Next;
            if (!IsValidNameHead(head)) {
                token = null;
                return false;
            }
            string name = head + reader.Read(IsValidNameChar);
            token = new Token("IDN", new StringValue(name));
            return true;
        }

        public bool IsValidNameChar(char ch) {
            return ch == '_' || char.IsLetterOrDigit(ch);
        }

        public bool IsValidNameHead(char ch) {
            return ch == '_' || char.IsLetter(ch);
        }
    }
}
