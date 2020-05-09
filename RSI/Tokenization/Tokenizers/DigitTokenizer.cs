using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization.Tokenizers {
    public class DigitTokenizer : ITokenizer {

        public bool Tokenize(StringReader reader, out Tokenization.Token token) {
            string s = reader.Read(char.IsDigit);
            if (string.IsNullOrEmpty(s) || !int.TryParse(s, out int digit)) {
                token = null;
                return false;
            } else {
                token = new Token("DIG", new NumberValue(digit));
                return true;
            }
        }
    }
}
