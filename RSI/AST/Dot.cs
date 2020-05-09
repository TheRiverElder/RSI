using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Dot : ASTList {
        
        public static readonly ASTListFactory Factory = (nodes) => new Dot(nodes);


        public Dot(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Key => this[0];

        public override Value GetValue(Value baseValue, NestedEnv env) {
            return baseValue[Key.Token.Value];
        }

        public override Value SetValue(Value baseValue, NestedEnv env, Value value) {
            baseValue[Key.Token.Value] = value;
            return value;
        }

        public override IASTNode Reduce() => new Dot(new IASTNode[] { Key.Reduce() });

        public override string ToString() => '.' + Key.ToString();
    }
}
