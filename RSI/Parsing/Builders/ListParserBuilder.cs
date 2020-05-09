using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class ListParserBuilder : ParserBuilder {

        private readonly string Element;
        private readonly string Seperator;

        public ListParserBuilder(string element, string seperator) {
            Element = element;
            Seperator = seperator;
        }

        public override Parser Create() => new ListParser();

        public override void Build(Parser parser, ParserBuindEnv env) {
            Parser element = env.GetParser(Element);
            Parser seperator = env.GetParser(Seperator);
            parser.Fill(new Parser[] { element, seperator});
        }
    }
}
