using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Index : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Index(nodes);

        public Index(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Key => this[0];

        public override Value GetValue(Value baseValue, NestedEnv env) {
            return baseValue[Key.GetValue(null, env)];
        }

        public override Value SetValue(Value baseValue, NestedEnv env, Value value) {
            baseValue[Key.GetValue(null, env)] = value;
            return value;
        }

        public override IASTNode Reduce() => new Index(new IASTNode[] { Key.Reduce() });

        public override string ToString() => '[' + Key.ToString() + ']';
    }
}
