using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Lex {
    public class PureProduction {

        public string Name { get; }
        public int ElemCount { get; }
        public ReduceAction Action { get; }

        public PureProduction(string name, int elemCount, ReduceAction action) {
            Name = name;
            ElemCount = elemCount;
            Action = action;
        }
    }
}
