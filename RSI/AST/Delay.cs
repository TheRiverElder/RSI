using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.AST {
    public class Delay : ASTList {

        public static readonly ASTListFactory Factory = (nodes) => new Delay(nodes);

        public Delay(IEnumerable<IASTNode> children) : base(children) {
        }

        public IASTNode Body => this[0];

        public IASTNode Optr => this[1];

        public override Value GetValue(Value baseValue, NestedEnv env) {
            Value prev = Body.GetValue(baseValue, env);
            double act = prev.AsNumber();
            switch (Optr.Token.Literal) {
                case "++": act = prev.AsNumber() + 1; break;
                case "--": act = prev.AsNumber() - 1; break;
            }
            Body.SetValue(baseValue, env, new NumberValue(act));
            return prev;
        }

        public override IASTNode Reduce() {
            return new Delay(new IASTNode[] { Body.Reduce(), Optr.Reduce() });
        }

        public override string ToString() {
            return Body.ToString() + Optr.ToString();
        }
    }
}
