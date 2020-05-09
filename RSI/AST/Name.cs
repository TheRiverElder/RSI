using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public class Name : ASTLeaf {

        public static readonly ASTLeafFactory Factory = (token) => new Name(token);


        public Name(Token token) : base(token) { }

        public string NameStr => Token.Literal;

        public override Value GetValue(Value baseVaue, NestedEnv env) {
            string str = NameStr;
            switch (str) {
                case "nil": return NilValue.Nil;
                case "true": return BoolValue.True;
                case "false": return BoolValue.False;
                default: return env[NameStr];
            }
        }

        public override Value SetValue(Value baseVaue, NestedEnv env, Value value) {
            return env[NameStr] = value;
        }
    }
}
