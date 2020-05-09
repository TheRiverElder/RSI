using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.Program.Instructions {
    public class PushDirectValue : IInstruction {

        public Value DirectValue { get; }

        public PushDirectValue(Value directValue) {
            DirectValue = directValue;
        }

        public void Exec(VisualMachine vm) {
            vm.Data.Push(DirectValue);
        }
    }
}
