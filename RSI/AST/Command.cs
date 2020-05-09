using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Command : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Command(nodes);

        public Command(IEnumerable<IASTNode> children) : base(children) {
        }

        public new string Name => this[0].Token.Value.AsString();

        public override Value GetValue(Value baseValue, NestedEnv env) {
            Value[] args = new Value[ChildrenCount - 1];
            for (int i = 1; i < ChildrenCount; i++) {
                args[i - 1] = this[i].GetValue(null, env);
            }
            return env[Name].Invoke(args);
        }

        public override IASTNode Reduce() {
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new Command(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append('/');
            foreach (IASTNode node in Children) {
                builder.Append(node.ToString()).Append(' ');
            }
            return builder.ToString();
        }
    }
}
