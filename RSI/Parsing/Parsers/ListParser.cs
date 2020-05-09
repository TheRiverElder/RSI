using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class ListParser : Parser {

        private Parser Element;
        private Parser Seperator;

        protected override void DoFill(Parser[] elements) {
            Element = elements[0];
            Seperator = elements[1];
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            List<IASTNode> r = new List<IASTNode>();
            if (!Element.Parse(ts, r)) {
                return false;
            }
            int start = ts.Index;
            int count = r.Count;
            while (ts.HasMore && Seperator.Parse(ts, r)) {
                if (!Element.Parse(ts, r)) {
                    ts.Index = start;
                    r.RemoveRange(count, r.Count - count);
                    break;
                }
                count = r.Count;
            }
            res.AddRange(r);
            return true;
        }
    }
}
