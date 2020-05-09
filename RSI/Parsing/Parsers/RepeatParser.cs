using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class RepeatParser : Parser {

        private Parser Parser;

        protected override void DoFill(Parser[] elements) {
            Parser = elements[0];
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            if (!Parser.Parse(ts, res)) {
                return false;
            }
            while (ts.HasMore && Parser.Parse(ts, res)) {

            }
            return true;
        }
    }
}
