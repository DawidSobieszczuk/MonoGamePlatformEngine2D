using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine {
    static class Extensions {
        public static T GetKey<T, K>(this Dictionary<T, K> dictionary, K value) {
            return dictionary.FirstOrDefault(x => x.Value.Equals(value)).Key;
        }
    }
}
