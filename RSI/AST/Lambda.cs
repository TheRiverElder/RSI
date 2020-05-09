using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Lambda : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Lambda(nodes);

        public Lambda(IEnumerable<IASTNode> children) : base(children) {
        }

        IASTNode Params => this[0];

        IASTNode Body => this[1];

        public override Value GetValue(Value baseValue, NestedEnv env) {
            IASTNode psn = Params;
            string[] ps = new string[psn.ChildrenCount];
            for (int i = 0; i < ps.Length; i++) {
                ps[i] = psn[i].Token.Literal;
            }
            return new FuncValue(env, ps, Body);
        }


        public override IASTNode Reduce() {
            return new Lambda(new IASTNode[] { Params.Reduce(), Body.Reduce() }); ;
        }

        public override string ToString() {
            return Params.ToString() + " => " + Body.ToString();
        }
    }
}
