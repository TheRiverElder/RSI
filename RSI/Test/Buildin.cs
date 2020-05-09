using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.Test {
    public static class Buildin {

        public static FuncValue FuncPrint = new FuncValue(null,
            new string[] { "content" },
            NativeFuncBody.Of(env => {
                Console.Write(env["content"].AsString());
                return NilValue.Nil;
            }));

        public static FuncValue FuncPrintln = new FuncValue(null,
            new string[] { "content" },
            NativeFuncBody.Of(env => {
                Console.WriteLine(env["content"].AsString());
                return NilValue.Nil;
            }));

        public static FuncValue FuncTypeof = new FuncValue(null,
            new string[] { "value" },
            NativeFuncBody.Of(env => {
                return new StringValue(env["value"].Type);
            }));

        public static FuncValue FuncInfo = new FuncValue(null,
            new string[] { "value" },
            NativeFuncBody.Of(env => {
                return new StringValue(InfoOf(env["value"]));
            }));

        public static FuncValue FuncPrinfo = new FuncValue(null,
            new string[] { "value" },
            NativeFuncBody.Of(env => {
                Console.Write(InfoOf(env["value"]));
                return NilValue.Nil;
            }));

        public static NestedEnv CreateBuindinEnv() {
            NestedEnv env = new NestedEnv();
            env.SetToSelf("print", FuncPrint, true);
            env.SetToSelf("println", FuncPrintln, true);
            env.SetToSelf("typeof", FuncTypeof, true);
            env.SetToSelf("info", FuncInfo, true);
            env.SetToSelf("prinfo", FuncPrinfo, true);
            env.SetToSelf("true", BoolValue.True, true);
            env.SetToSelf("false", BoolValue.False, true);
            env.SetToSelf("nil", NilValue.Nil, true);
            return env;
        }

        public static string InfoOf(Value value) {
            StringBuilder builder = new StringBuilder();
            builder.Append(value.Type).Append(' ');
            builder.Append(value.AsString()).Append(' ');
            if (value.Members != null) {
                builder.Append("{ ");
                var etr = value.Members.GetEnumerator();
                bool flag = etr.MoveNext();
                while (flag) {
                    var e = etr.Current;
                    builder.Append(e.Key).Append(": ").Append(e.Value);
                    if (flag = etr.MoveNext()) {
                        builder.Append(", ");
                    }
                }
                builder.Append(" }");
            }
            return builder.ToString();
        }
    }
    
}
