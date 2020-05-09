using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Arguments : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Arguments(nodes);

        public Arguments(IEnumerable<IASTNode> children) : base(children) {
        }

        public override Value GetValue(Value baseValue, NestedEnv env) {
            Value[] args = new Value[ChildrenCount];
            for (int i = 0; i < ChildrenCount; i++) {
                args[i] = this[i].GetValue(null, env);
            }
            return baseValue.Invoke(args);
        }

        public override IASTNode Reduce() {
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Arguments(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder().Append('(');
            for (int i = 0; i < ChildrenCount; i++) {
                if (i != 0) {
                    builder.Append(", ");
                }
                builder.Append(this[i].ToString());
            }
            return builder.Append(')').ToString();
        }
    }
}
