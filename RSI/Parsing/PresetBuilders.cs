using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Parsing.Builders;

namespace top.riverelder.RSI.Parsing {
    public static class PresetBuilders {

        // SequenceParserBuilder
        public static SequenceParserBuilder Seq(IParserGetter pg) => new SequenceParserBuilder(pg);
        public static SequenceParserBuilder Seq(string pn) => new SequenceParserBuilder(new NameParserGetter(pn));
        public static SequenceParserBuilder Seq() => new SequenceParserBuilder();

        // ForkParserBuilder
        public static ForkParserBuilder Fork(IParserGetter pg) => new ForkParserBuilder(pg);
        public static ForkParserBuilder Fork(string pn) => new ForkParserBuilder(new NameParserGetter(pn));
        public static ForkParserBuilder Fork() => new ForkParserBuilder();

        // TokenParserBuilder
        public static TokenParserBuilder Id(params string[] ids) => new TokenParserBuilder("id", ids);
        public static TokenParserBuilder Token(string type, params string[] values) => new TokenParserBuilder(type, values);
        public static TokenParserBuilder Token(ASTLeafFactory factory, string type, params string[] values) => new TokenParserBuilder(factory, type, values);

        // RepeatParserBuilder
        public static RepeatParserBuilder Repeat(IParserGetter pg) => new RepeatParserBuilder(pg);
        public static RepeatParserBuilder Repeat(string pn) => new RepeatParserBuilder(new NameParserGetter(pn));

        // OptionalParserBuilder
        public static OptionalParserBuilder Optional(IParserGetter pg) => new OptionalParserBuilder(pg);
        public static OptionalParserBuilder Optional(string pn) => new OptionalParserBuilder(new NameParserGetter(pn));

        // SkipParserBuilder
        public static SkipParserBuilder Skip(IParserGetter pg) => new SkipParserBuilder(pg);
        public static SkipParserBuilder Skip(string pn) => new SkipParserBuilder(new NameParserGetter(pn));

        // ASTParserBuilder
        public static ASTParserBuilder ASTNode(ASTListFactory factory, IParserGetter pg) => new ASTParserBuilder(factory, pg);
        public static ASTParserBuilder ASTNode(ASTListFactory factory, string pn) => new ASTParserBuilder(factory, new NameParserGetter(pn));
    }
}
