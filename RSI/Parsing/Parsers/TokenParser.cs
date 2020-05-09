using System.Collections.Generic;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class TokenParser : Parser {

        public readonly ASTLeafFactory Factory = Literal.Factory;
        public readonly string ValidType;
        public readonly HashSet<object> ValidValues;

        public TokenParser(string validType, ASTLeafFactory factory, params string[] validValues) {
            ValidType = validType;
            Factory = factory;
            ValidValues = validValues.Length != 0 ? new HashSet<object>(validValues) : null;
        }

        public TokenParser(string validType, ASTLeafFactory factory, IEnumerable<object> validValues) {
            ValidType = validType;
            Factory = factory;
            ValidValues = new HashSet<object>(validValues);
        }

        public TokenParser(string validType, ASTLeafFactory factory) {
            ValidType = validType;
            Factory = factory;
            ValidValues = null;
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            Token token = ts.Read();
            if (token.Type == ValidType && (ValidValues == null || ValidValues.Count == 0 || ValidValues.Contains(token.Literal))) {
                res.Add(Factory(token));
                return true;
            }
            return false;
        }

        protected override void DoFill(Parser[] elements) {}
    }
}
