using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization.Tokenizers {
    public class DecimalTokenizer : ITokenizer {

        public bool Tokenize(StringReader reader, out Token token) {
            string digitPart = reader.Read(char.IsDigit);
            if (string.IsNullOrEmpty(digitPart) && reader.Peek != '.') {
                token = null;
                return false;
            }
            if (!reader.HasMore || reader.Peek != '.') {
                token = null;
                return false;
            }
            digitPart = string.IsNullOrEmpty(digitPart) ? "0" : digitPart;
            string decimalPart = null;
            if (!reader.Skip()
                || string.IsNullOrEmpty(decimalPart = reader.Read(char.IsDigit))
                || !float.TryParse(digitPart + '.' + decimalPart, out float dec)) {
                token = null;
                return false;
            }
            token = new Token("DEC", new NumberValue(dec));
            return true;
        }
    }
}
