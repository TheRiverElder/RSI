using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.Program.Instructions {
    public class CalcAndPush : IInstruction {

        public string Optr { get; }

        public CalcAndPush(string optr) {
            Optr = optr;
        }

        public void Exec(VisualMachine vm) {
            Value right = vm.Data.Pop();
            Value left = vm.Data.Pop();
            vm.Data.Push(left.ComputeWith(Optr, right));
        }
    }
}
