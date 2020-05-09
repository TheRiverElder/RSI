using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {

    public delegate ASTLeaf ASTLeafFactory(Token token);
    public delegate ASTList ASTListFactory(IASTNode[] nodes);
}
