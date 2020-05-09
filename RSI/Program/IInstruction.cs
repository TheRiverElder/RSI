using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Program {
    public interface IInstruction {

        void Exec(VisualMachine vm);

    }
}
