using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Tokenization {
    public class Token {

        public virtual object Value { get; }
        public virtual string Literal { get; }
        public virtual TokenType Type { get; }

        public int Index { get; set; }

        public override string ToString() => $"Type: {Type}, Literal: {Literal}, Value: {Value}";
    }
}
