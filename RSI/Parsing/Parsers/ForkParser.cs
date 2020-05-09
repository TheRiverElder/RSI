using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class ForkParser : Parser {

        private Parser[] Forkings;

        public ForkParser() {
        }

        public ForkParser(params Parser[] forkings) {
            Forkings = forkings;
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            List<IASTNode> r = new List<IASTNode>();
            foreach (Parser parser in Forkings) {
                if (parser.Parse(ts, r)) {
                    res.AddRange(r);
                    return true;
                }
                r.Clear();
            }
            return false;
        }

        protected override void DoFill(Parser[] elements) {
            Forkings = elements;
        }
    }
}
