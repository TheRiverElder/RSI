using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.Program {
    public class VisualMachine {

        public int ProgramCounter { get; set; } = 0;

        public List<IInstruction> Instructions = new List<IInstruction>(); 

        public Stack<Value> Data { get; } = new Stack<Value>();

        public Value Pop() {
            return Data.Pop();
        }

        public void Push(Value value) {
            Data.Push(value);
        }



        public void Append(IEnumerable<IInstruction> instructions) {
            Instructions.AddRange(instructions);
        }

        public void Reset() {
            ProgramCounter = 0;
            Data.Clear();
        }

        public void Run() {
            while (ProgramCounter >= 0 && ProgramCounter < Instructions.Count) {
                IInstruction instruction = Instructions[ProgramCounter];
                ProgramCounter++;
                instruction.Exec(this);
            }
        }

    }
}
