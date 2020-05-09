using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class OptionalParser : Parser {

        private Parser Parser;

        public override bool Parse(TokenStream ts, List<IASTNode> res) {
            return DoParse(ts, res);
        }

        protected override void DoFill(Parser[] elements) {
            Parser = elements[0];
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            List<IASTNode> nodes = new List<IASTNode>();
            if (Parser.Parse(ts, nodes)) {
                res.AddRange(nodes);
            }
            return true;
        }

    }
}
