using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Block : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Block(nodes);

        public Block(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Content => this[0];

        public override Value GetValue(Value baseValue, NestedEnv env) {
            return Content.GetValue(null, env);
        }

        public override IASTNode Reduce() {
            return new Block(new IASTNode[] { Content.Reduce() });
        }

        public override string ToString() {
            return "{ " + Content.ToString() + "}";
        }
    }
}
