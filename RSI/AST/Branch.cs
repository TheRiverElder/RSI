using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Branch : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Branch(nodes);

        public Branch(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Cond() => this[0];

        public IASTNode TruePart() => this[1];

        public IASTNode FalsePart() => ChildrenCount > 2 ? this[2] : null;

        public override Value GetValue(Value baseValue, NestedEnv env) {
            Value result = Cond().GetValue(null, env);
            if (result.AsBool()) {
                return TruePart().GetValue(null, env);
            } else if (ChildrenCount > 2) {
                return this[2].GetValue(null, env);
            } else {
                return NilValue.Nil;
            }
        }

        public override IASTNode Reduce() {
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Branch(nodes);
        }

        public override string ToString() {
            return Cond().ToString() + " ? " + TruePart().ToString() + (ChildrenCount > 2 ? " : " + this[2].ToString() : "");
        }
    }
}
