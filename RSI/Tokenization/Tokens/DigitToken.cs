using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Tokenization.Tokens {
    public class DigitToken : Token {

        public static TokenType TypeDigit = new TokenType("digit");

        public int Digit { get; }

        public override object Value => Digit;

        public override string Literal => Convert.ToString(Value);

        public override TokenType Type => TypeDigit;

        public DigitToken(int digit) {
            Digit = digit;
        }
    }
}
