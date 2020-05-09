using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Prefixed : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Prefixed(nodes);

        public Prefixed(IASTNode[] children) : base(children) {
        }

        public string Prefix => this[0].Token.Literal;

        public IASTNode Body => this[1];

        public override Value GetValue(Value baseVaue, NestedEnv env) {
            Value bodyValue = Body.GetValue(null, env);
            switch (Prefix) {
                case "+": return new NumberValue(bodyValue.AsNumber());
                case "-": return new NumberValue(-bodyValue.AsNumber());
                case "!": return new BoolValue(!bodyValue.AsBool());
                default: return NilValue.Nil;
            }
        }

        public override IASTNode Reduce() => new Prefixed(new IASTNode[] { this[0].Reduce(), Body.Reduce() });

        public override string ToString() => Prefix + Body.ToString();
    }
}
