using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Lex {
    public class Production {
        
        public int Index { get; }
        public string Name { get; }
        public string[] Elems { get; }
        public ReduceAction Action { get; private set; }

        public Production(string name, string[] elems) {
            Name = name;
            Elems = elems;
        }

        public Production(int index, string name, string[] elems) {
            Index = index;
            Name = name;
            Elems = elems;
        }

        public void Act(ReduceAction action) {
            Action = action;
        }
    }
}
