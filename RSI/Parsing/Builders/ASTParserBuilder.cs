using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class ASTParserBuilder : ParserBuilder {

        private ASTListFactory Factory;
        private IParserGetter ParserGetter;

        public ASTParserBuilder(ASTListFactory factory, IParserGetter parserGetter) {
            Factory = factory;
            ParserGetter = parserGetter;
        }

        public override void Build(Parser parser, ParserBuindEnv env) {
            parser.Fill(new Parser[] { ParserGetter.GetParser(env) });
        }

        public override Parser Create() => new ASTParser(Factory);
    }
}
