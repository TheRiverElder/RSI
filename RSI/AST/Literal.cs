using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public class Literal : ASTLeaf {

        public static readonly ASTLeafFactory Factory = (token) => new Literal(token);


        public Literal(Token token) : base(token) { }


        public override Value GetValue(Value baseVaue, NestedEnv env) {
            return Token.Value;
        }

        public override Value SetValue(Value baseVaue, NestedEnv env, Value value) {
            return NilValue.Nil;
        }
    }
}
