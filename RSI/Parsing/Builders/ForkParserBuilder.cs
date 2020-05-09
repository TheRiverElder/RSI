using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class ForkParserBuilder : ParserBuilder {

        private List<IParserGetter> Forkings = new List<IParserGetter>();

        public ForkParserBuilder() {
        }

        public ForkParserBuilder(IParserGetter pg) {
            Forkings.Add(pg);
        }

        public ForkParserBuilder Or(IParserGetter pg) {
            Forkings.Add(pg);
            return this;
        }

        public ForkParserBuilder Or(string pn) {
            Forkings.Add(new NameParserGetter(pn));
            return this;
        }

        public override Parser Create() => new ForkParser();

        public override void Build(Parser parser, ParserBuindEnv env) {
            Parser[] elements = new Parser[Forkings.Count];
            for (int i = 0; i < elements.Length; i++) {
                elements[i] = Forkings[i].GetParser(env);
            }
            parser.Fill(elements);
        }
    }
}
