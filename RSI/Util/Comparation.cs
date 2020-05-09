using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Util {
    public static class Comparation {

        public static bool IncludesAll<T>(HashSet<T> more, HashSet<T> less) {
            if (more.Count < less.Count) {
                return false;
            }
            foreach (T e in less) {
                if (!more.Contains(e)) {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsWithoutOrder<T>(HashSet<T> one, HashSet<T> another) {
            if (one.Count != another.Count) {
                return false;
            } 
            foreach (T e in one) {
                if (!another.Contains(e)) {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsWithOrder<T>(IList<T> one, IList<T> another) {
            if (one.Count != another.Count) {
                return false;
            }
            for (int i = 0; i < one.Count; i++) {
                if (!Equals(one[i], another[i])) {
                    return false;
                }
            }
            return true;
        }

    }
}
