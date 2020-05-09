using System.Collections.Generic;
using System.Text;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization.Tokenizers {
    public class StringTokenizer : ITokenizer {

        private static char EscapeChar = '\\';
        private static Dictionary<char, char> QuotePairs = new Dictionary<char, char> {
            ['"'] = '"',
            ['“'] = '”',
            ['\''] = '\'',
            ['‘'] = '’',
        };

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
                    token = new Token("STR", new StringValue(builder.ToString()));
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
