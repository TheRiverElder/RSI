using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Parsing {
    public class NameParserGetter : IParserGetter {

        public readonly string Name;

        public NameParserGetter(string name) {
            Name = name;
        }

        public Parser GetParser(ParserBuindEnv env) => env.GetParser(Name);
    }
}
