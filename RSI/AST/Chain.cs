using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Chain : ASTList {
        
        public static readonly ASTListFactory Factory = (nodes) => new Chain(nodes);

        public Chain(IEnumerable<IASTNode> children) : base(children) {
        }

        public override Value GetValue(Value baseValue, NestedEnv env) {
            Value bv = null;
            for (int i = 0; i < ChildrenCount; i++) {
                bv = this[i].GetValue(bv, env);
            }
            return bv;
        }

        public override Value SetValue(Value baseValue, NestedEnv env, Value value) {
            Value bv = null;
            for (int i = 0; i < ChildrenCount - 1; i++) {
                bv = this[i].GetValue(bv, env);
            }
            return this[ChildrenCount - 1].SetValue(bv, env, value);
        }


        public override IASTNode Reduce() {
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Chain(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            foreach (IASTNode node in Children) {
                builder.Append(node.ToString());
            }
            return builder.ToString();
        }
    }
}
