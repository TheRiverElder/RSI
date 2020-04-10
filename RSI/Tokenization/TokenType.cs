using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Tokenization {
    public class TokenType {

        public string Name { get; }

        public TokenType(string name) {
            Name = name;
        }

        public override string ToString() => this.Name;
    }
}
