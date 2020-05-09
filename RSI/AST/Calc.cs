using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Calc : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Calc(nodes);


        public Calc(IASTNode[] nodes) : base(nodes) { }

        public override Value GetValue(Value baseVaue, NestedEnv env) {
            Value left = this[0].GetValue(null, env);
            for (int i = 1; i < ChildrenCount; i += 2) {
                Value right = this[i + 1].GetValue(null, env);
                left = left.ComputeWith(this[i].Token.Literal, right);
            }
            return left;
        }

        public override IASTNode Reduce() {
            if (ChildrenCount == 1) {
                return this[0].Reduce();
            }
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Calc(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder().Append('(');
            for (int i = 0; i < ChildrenCount; i++) {
                if (i != 0) {
                    builder.Append(' ');
                }
                builder.Append(this[i].ToString());
            }
            return builder.Append(')').ToString();
        }
    }
}
