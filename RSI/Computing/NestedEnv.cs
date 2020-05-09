using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Computing {
    public class NestedEnv {

        private readonly NestedEnv Parent;

        public NestedEnv(NestedEnv parent) {
            Parent = parent;
        }

        public NestedEnv() {
            Parent = null;
        }

        private readonly Dictionary<string, Value> Values = new Dictionary<string, Value>();
        private readonly HashSet<string> Constants = new HashSet<string>();

        public void SetToSelf(string name, Value value, bool isConst) {
            if (!Constants.Contains(name)) {
                Values[name] = value ?? NilValue.Nil;
                if (isConst) {
                    Constants.Add(name);
                }
            }
        }

        public void SetToSelf(string name, Value value) {
            SetToSelf(name, value, false);
        }

        public Value this[string name] {
            get => Locate(name).Values.TryGetValue(name, out Value value) ? value : NilValue.Nil;
            set => Locate(name).SetToSelf(name, value ?? NilValue.Nil, false);
        }
        
        private NestedEnv Locate(string name) {
            return TryLocate(name, out NestedEnv env) ? env : this;
        }

        private bool TryLocate(string name, out NestedEnv env) {
            if (Values.ContainsKey(name)) {
                env = this;
                return true;
            } else if (Parent != null && Parent.TryLocate(name, out env)) {
                return true;
            } else {
                env = null;
                return false;
            }
        }
    }
}
