using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Assignment : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Assignment(nodes);

        public Assignment(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Left => this[0];
        public IASTNode Right => this[1];



        public override Value GetValue(Value baseValue, NestedEnv env) {
            return Left.SetValue(baseValue, env, Right.GetValue(null, env));
        }

        public override IASTNode Reduce() => new Assignment(new IASTNode[] { Left.Reduce(), Right.Reduce() });

        public override string ToString() => Left.ToString() + " = " + Right.ToString();
    }
}
