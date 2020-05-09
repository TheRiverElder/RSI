using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Parsing.Parsers {
    public class ASTParser : Parser {

        private ASTListFactory Factory;
        private Parser Parser;

        public ASTParser(ASTListFactory factory) {
            Factory = factory;
        }

        protected override void DoFill(Parser[] elements) {
            Parser = elements[0];
        }

        protected override bool DoParse(TokenStream ts, List<IASTNode> res) {
            if (TryParseAST(ts, out IASTNode node)) {
                res.Add(node);
                return true;
            }
            return false;
        }

        public bool TryParseAST(TokenStream ts, out IASTNode node) {
            List<IASTNode> res = new List<IASTNode>();
            if (Parser.Parse(ts, res)) {
                node = Factory(res.ToArray());
                return true;
            }
            node = null;
            return false;
        }
    }
}
