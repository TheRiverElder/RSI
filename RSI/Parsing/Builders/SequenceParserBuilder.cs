using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class SequenceParserBuilder : ParserBuilder {

        private List<IParserGetter> Sequence = new List<IParserGetter>();

        public SequenceParserBuilder() {
        }

        public SequenceParserBuilder(IParserGetter pg) {
            Sequence.Add(pg);
        }

        public SequenceParserBuilder Then(IParserGetter pg) {
            Sequence.Add(pg);
            return this;
        }

        public SequenceParserBuilder Then(string pn) {
            Sequence.Add(new NameParserGetter(pn));
            return this;
        }

        public override void Build(Parser parser, ParserBuindEnv env) {
            Parser[] elements = new Parser[Sequence.Count];
            for (int i = 0; i < elements.Length; i++) {
                elements[i] = Sequence[i].GetParser(env);
            }
            parser.Fill(elements);
        }

        public override Parser Create() => new SequenceParser();
    }
}
