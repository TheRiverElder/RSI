using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Lex {
    public class LexAction {

        public LexActionType Type { get; }

        public int Target { get; }

        public LexAction Crash { get; private set; }

        public bool Crashed => Crash != null;

        public LexAction(LexActionType type, int target) {
            Type = type;
            Target = target;
        }

        public LexAction(LexActionType type) {
            Type = type;
            Target = 0;
        }

        public void Crashes(LexAction another) {
            if (Type == another.Type && Target == another.Target && Crash == null && another.Crash == null) {
                return;
            } else if (Crash == null || another.Type != Crash.Type || another.Target != Crash.Target) {
                Crash = another;
            }
        }

        public override string ToString() {
            string self;
            switch(Type) {
                case LexActionType.Error: self = "err"; break;
                case LexActionType.Accept: self = "acc"; break;
                case LexActionType.Shift: self = "s" + Target; break;
                case LexActionType.Reduce: self = "r" + Target; break;
                default: return "err";
            }
            return Crashed ? self + "/" + Crash.ToString() : self;
        }
    }

    public enum LexActionType {
        Error,
        Accept,
        Shift,
        Reduce,
    }
}
