using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Util {
    public static class Collection {

        public static void AddAll<T>(this HashSet<T> set, IEnumerable<T> newElems) {
            foreach (T elem in newElems) {
                set.Add(elem);
            }
        }

    }
}
