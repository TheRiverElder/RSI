using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Parsing {
    public class ParserBuindEnv {

        private readonly Dictionary<string, ParserBuilder> Builders = new Dictionary<string, ParserBuilder>();
        private readonly Dictionary<string, Parser> Parsers = new Dictionary<string, Parser>();

        public ParserBuindEnv AddBuilder(string key, ParserBuilder builder) {
            Builders[key] = builder;
            return this;
        }

        public ParserBuindEnv AddParser(string key, Parser parser) {
            Parsers[key] = parser;
            return this;
        }

        public void Build() {
            foreach (var e in Builders) {
                Parsers[e.Key] = e.Value.Create();
            }
            foreach (var e in Builders) {
                e.Value.Build(Parsers[e.Key], this);
            }
        }

        public Parser GetParser(string key) {
            return Parsers[key];
        }



    }
}
