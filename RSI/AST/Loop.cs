using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Loop : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Loop(nodes);

        public Loop(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Cond => this[0];

        public IASTNode Body => this[1];

        public override Value GetValue(Value baseValue, NestedEnv env) {
            Value result = NilValue.Nil;
            while (Cond.GetValue(null, env).AsBool()) {
                result = Body.GetValue(null, env);
            }
            return result;
        }

        public override IASTNode Reduce() {
            return new Loop(new IASTNode[] { Cond.Reduce(), Body.Reduce() });
        }

        public override string ToString() {
            return Cond.ToString() + " ~ " + Body.ToString();
        }
    }
}
