using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Lex {
    public class Symbol {

        public string Name { get; }

        public Symbol(string name) {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }
}
