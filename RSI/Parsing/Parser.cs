using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Parsing.Builders;
using top.riverelder.RSI.Parsing.Parsers;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing {
    public abstract class Parser : IParserGetter {

        private bool Sealed = false;

        public bool Fill(Parser[] elements) {
            if (!Sealed) {
                DoFill(elements);
                Sealed = true;
                return true;
            } else {
                return false;
            }
        }

        protected abstract void DoFill(Parser[] elements);

        public virtual bool Parse(TokenStream ts, List<IASTNode> res) {
            int start = ts.Index;
            int count = res.Count;
            if (ts.HasMore && DoParse(ts, res)) {
                return true;
            }
            ts.Index = start;
            res.RemoveRange(count, res.Count - count);
            return false;
        }

        protected abstract bool DoParse(TokenStream ts, List<IASTNode> res);

        public Parser GetParser(ParserBuindEnv env) => this;

        public SequenceParserBuilder Then(IParserGetter pg) {
            return new SequenceParserBuilder().Then(pg);
        }
    }
}
