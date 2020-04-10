using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Tokenization.Tokens {
    public class DecimalToken : Token {

        public static TokenType TypeDecimal = new TokenType("decimal");

        public float Decimal;

        public override object Value => Decimal;

        public override string Literal => Convert.ToString(Decimal);

        public override TokenType Type => TypeDecimal;

        public DecimalToken(float dec) {
            Decimal = dec;
        }
    }
}
