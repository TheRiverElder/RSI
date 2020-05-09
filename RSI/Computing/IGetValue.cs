using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Computing {
    public interface IGetValue {

        Value GetValue(Value baseValue, NestedEnv env);

    }
}
