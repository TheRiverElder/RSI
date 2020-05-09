using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public class ASTList : IASTNode {

        // 留下这段代码仅仅是为了方便复制而已
        //public static readonly ASTListFactory Factory = (nodes) => new ASTList(nodes);

        public IASTNode this[int index] => Children[index];

        public bool IsLeaf => false;

        public Token Token => null;

        public IASTNode[] Children { get; }

        public int ChildrenCount => Children.Length;

        public string Name { get; }

        public ASTList(string name, IEnumerable<IASTNode> children) {
            Name = name;
            Children = children.ToArray();
        }

        public ASTList(IEnumerable<IASTNode> children) {
            Children = children.ToArray();
        }

        public virtual Value GetValue(Value baseValue, NestedEnv env) {
            Value result = NilValue.Nil;
            foreach (IASTNode node in Children) {
                result = node.GetValue(null, env);
            }
            return result;
        }

        public virtual Value SetValue(Value baseValue, NestedEnv env, Value value) {
            return NilValue.Nil;
        }

        public virtual IASTNode Reduce() {
            if (ChildrenCount == 1) {
                return this[0].Reduce();
            }
            IASTNode[] nodes = new IASTNode[ChildrenCount];
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i] = this[i].Reduce();
            }
            return new ASTList(nodes);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append('(');
            if (Children.Length > 0) {
                builder.Append(Children[0].ToString());
            }
            for (int i = 1; i < Children.Length; i++) {
                builder.Append(' ').Append(Children[i].ToString());
            }
            builder.Append(')');
            return builder.ToString();
        }
    }
}
