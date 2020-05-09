using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Stmts : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Stmts(nodes);

        public Stmts(IEnumerable<IASTNode> children) : base(children) {
        }

        public override Value GetValue(Value baseVaue, NestedEnv env) {
            Value result = NilValue.Nil;
            foreach (IASTNode node in Children) {
                result = node.GetValue(null, env);
            }
            return result;
        }

        public override IASTNode Reduce() {
            if (ChildrenCount == 1) {
                return this[0].Reduce();
            }
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Stmts(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            foreach (IASTNode node in Children) {
                builder.Append(node.ToString()).Append("; ");
            }
            return builder.ToString();
        }
    }
}
