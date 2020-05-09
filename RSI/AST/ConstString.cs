using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public class ConstString : ASTLeaf {

        public static ASTLeafFactory Factory = (token) => new ConstString(token);

        public ConstString(Token token) : base(token) { }

        public override Value GetValue(Value baseVaue, NestedEnv env) {
            return new StringValue(Token.Literal);
        }

    }
}
