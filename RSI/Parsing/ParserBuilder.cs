using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Parsing.Builders;

namespace top.riverelder.RSI.Parsing {
    public abstract class ParserBuilder : IParserGetter {


        public abstract Parser Create();

        public abstract void Build(Parser parser, ParserBuindEnv env);

        public Parser GetParser(ParserBuindEnv env) {
            Parser parser = Create();
            Build(parser, env);
            return parser;
        }


        //public ParserBuilder Optional(string parserName) => new OptionalParserBuilder(new NameParserGetter(parserName));
        //public ParserBuilder Then(string parserName) => Then(new NameParserGetter(parserName));
        //public ParserBuilder Or(string parserName) => Or(new NameParserGetter(parserName));
        //public ParserBuilder Repeat(string parserName) => Repeat(new NameParserGetter(parserName));

        //public virtual ParserBuilder AsAST(ASTListFactory factory) => new ASTParserBuilder(factory, this);
        //public virtual ParserBuilder AsOptional() => new OptionalParserBuilder(this);

        //public virtual ParserBuilder Optional(IParserGetter pg) => new OptionalParserBuilder(pg);
        //public virtual ParserBuilder Then(IParserGetter pg) => new SequenceParserBuilder(pg);
        //public virtual ParserBuilder Or(IParserGetter pg) => new ForkParserBuilder(pg);
        //public virtual ParserBuilder Repeat(IParserGetter pg) => new ForkParserBuilder(pg);
        //public virtual ParserBuilder Id(params string[] ids) => new TokenParserBuilder("id", ids);
        //public virtual ParserBuilder Token(string type, params string[] values) => new TokenParserBuilder(type, values);

    }
}
