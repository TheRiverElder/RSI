using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Parsing {
    public interface IParserGetter {
        Parser GetParser(ParserBuindEnv env);
    }
}
