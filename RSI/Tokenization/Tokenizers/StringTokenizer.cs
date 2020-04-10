using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Tokenization.Tokens;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization.Tokenizers {
    public class StringTokenizer : ITokenizer {

        private static char EscapeChar = '\\';
        private static Dictionary<char, char> QuotePairs = new Dictionary<char, char> {
            ['"'] = '"',
            ['“'] = '”',
            ['\''] = '\'',
        };

        public string Hint => "STRING";

        public bool Tokenize(StringReader reader, out Token token) {

            if (!QuotePairs.TryGetValue(reader.Next, out char quoteClose)) {
                token = null;
                return false;
            }
            bool markedEsacpe = false;
            StringBuilder builder = new StringBuilder();
            while (reader.HasMore) {
                if (markedEsacpe) {
                    builder.Append(reader.Next);
                    markedEsacpe = false;
                    continue;
                }
                char next = reader.Next;
                if (EscapeChar == next) {
                    markedEsacpe = true;
                } else if (quoteClose == next) {
                    token = new StringToken(builder.ToString());
                    return true;
                } else {
                    builder.Append(next);
                }
            }
            token = null;
            return false;
        }
    }
}
