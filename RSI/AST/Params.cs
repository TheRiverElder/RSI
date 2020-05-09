using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.AST {
    public class Params : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Params(nodes);

        public Params(IEnumerable<IASTNode> children) : base(children) {
        }

        public override IASTNode Reduce() {
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Params(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append('(');
            for (int i = 0; i < ChildrenCount; i++) {
                if (i > 0) {
                    builder.Append(", ");
                }
                builder.Append(this[0].Token.Literal);
            }
            return builder.Append(')').ToString();
        }
    }
}
