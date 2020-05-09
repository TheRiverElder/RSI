using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class SequenceParser : Parser {

        private Parser[] Sequence;

        protected override void DoFill(Parser[] elements) {
            Sequence = elements;
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            foreach (Parser parser in Sequence) {
                if (!parser.Parse(ts, res)) {
                    return false;
                }
            }
            return true;
        }
    }
}
