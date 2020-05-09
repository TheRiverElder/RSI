using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Parsing.Parsers;

namespace top.riverelder.RSI.Parsing.Builders {
    public class TokenParserBuilder : ParserBuilder {

        private ASTLeafFactory Factory = Literal.Factory;
        private string ValidType;

        private HashSet<string> ValidValues;

        public TokenParserBuilder(ASTLeafFactory factory, string validType, string[] validValues) {
            Factory = factory;
            ValidType = validType;
            ValidValues = new HashSet<string>(validValues);
        }

        public TokenParserBuilder(string validType, string[] validValues) {
            ValidType = validType;
            ValidValues = new HashSet<string>(validValues);
        }

        public TokenParserBuilder(string validType) {
            ValidType = validType;
            ValidValues = null;
        }

        public override Parser Create() => new TokenParser(ValidType, Factory, ValidValues?.ToArray());

        public override void Build(Parser parser, ParserBuindEnv env) { }
    }
}
