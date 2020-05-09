using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public class CmdVar : Name {

        public new static ASTLeafFactory Factory = (token) => new CmdVar(token);

        public CmdVar(Token token) : base(token) {
        }

        public override string ToString() {
            return "$" + Token.Literal;
        }
    }
}
