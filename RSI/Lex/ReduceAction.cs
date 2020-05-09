using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;

namespace top.riverelder.RSI.Lex {
    public delegate IASTNode ReduceAction(IASTNode[] children);
}
