using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class OptionalParserBuilder : ParserBuilder {

        private IParserGetter ParserGetter;

        public OptionalParserBuilder() {
        }

        public OptionalParserBuilder(IParserGetter parserGetter) {
            ParserGetter = parserGetter;
        }

        public override Parser Create() => new OptionalParser();

        public override void Build(Parser parser, ParserBuindEnv env) {
            parser.Fill(new Parser[] { ParserGetter.GetParser(env) });
        }
    }
}
