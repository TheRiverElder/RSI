using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class SkipParser : Parser {

        private Parser Parser;

        protected override void DoFill(Parser[] elements) {
            Parser = elements[0];
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            List<IASTNode> r = new List<IASTNode>();
            return Parser.Parse(ts, r);
        }
    }
}
