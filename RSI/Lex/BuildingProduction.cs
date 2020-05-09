using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Lex {
    public class BuildingProduction {

        public bool IsCore { get; set; } = false;

        public Production Prod { get; }
        public string Name => Prod.Name;
        public string[] Elems => Prod.Elems;

        public int Index { get; }
        public HashSet<string> LookAheads { get; } = new HashSet<string>();

        private BuildingProduction Next = null;

        public BuildingProduction(Production prod, int index) {
            Prod = prod;
            Index = index;
        }

        public BuildingProduction(Production prod, int index, IEnumerable<string> lookAheads) {
            Prod = prod;
            Index = index;
            foreach (string e in lookAheads) {
                LookAheads.Add(e);
            }
        }

        public BuildingProduction(Production prod, int index, params string[] lookAheads) {
            Prod = prod;
            Index = index;
            foreach (string e in lookAheads) {
                LookAheads.Add(e);
            }
        }

        public bool GetNext(out BuildingProduction next) {
            if (Next != null) {
                next = Next;
                return true;
            } else if (Index < Elems.Length) {
                next = Next = new BuildingProduction(Prod, Index + 1, LookAheads);
                return true;
            }
            next = null;
            return false;
        }



        // 匹配产生式左部、右部、索引
        public bool CanPile(BuildingProduction another) {
            return another.Prod == Prod &&
                another.Index == Index;
        }

        // 匹配产生式左部、右部、索引、展望符
        public bool Equals(BuildingProduction another) {
            return another.Prod == Prod &&
                another.Index == Index &&
                Comparation.EqualsWithoutOrder(another.LookAheads, LookAheads);
        }

        // 仅仅匹配产生式右部和项目索引
        public bool IsConcentric(BuildingProduction another) {
            return Comparation.EqualsWithOrder(Elems, another.Elems) && another.Index == Index;
        }

        // 匹配产生式的左部、右部、索引、展望符，且另一个的展望符集要包含于自身的展望符集
        public bool IsCompatible(BuildingProduction another) {
            return Comparation.EqualsWithOrder(Elems, another.Elems) &&
                another.Index == Index &&
                Comparation.IncludesAll(LookAheads, another.LookAheads);
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append(Name).Append(" →");
            for (int i = 0; i < Elems.Length; i++) {
                if (i == Index) {
                    builder.Append(" ●");
                }
                builder.Append(' ').Append(Elems[i]);
            }
            if (Index == Elems.Length) {
                builder.Append(" ●");
            }
            builder.Append(" , ").Append(string.Join(" ", LookAheads));
            return builder.ToString();
        }
    }
}
