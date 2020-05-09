using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class SkipParserBuilder : ParserBuilder {

        private IParserGetter ParserGetter;

        public SkipParserBuilder(IParserGetter parserGetter) {
            ParserGetter = parserGetter;
        }

        public override void Build(Parser parser, ParserBuindEnv env) {
            parser.Fill(new Parser[] { ParserGetter.GetParser(env) });
        }

        public override Parser Create() => new SkipParser();
    }
}
