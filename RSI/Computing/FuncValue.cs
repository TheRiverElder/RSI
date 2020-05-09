using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;

namespace top.riverelder.RSI.Computing {
    public class FuncValue : Value {

        public NestedEnv Env;
        public string[] Params;
        public string[] ParamTypes;
        public IASTNode[] Defaults;
        public IASTNode Body;

        public FuncValue(NestedEnv env, string[] ps, IASTNode body) {
            Env = env;
            Params = ps;
            ParamTypes = null;
            Body = body;
        }

        public FuncValue(NestedEnv env, string[] @params, string[] paramTypes, IASTNode[] defaults, IASTNode body) {
            Env = env;
            Params = @params;
            ParamTypes = paramTypes;
            Defaults = defaults;
            Body = body;
        }

        public override string Type => "func";

        public override bool AsBool() => true;

        public override double AsNumber() => 1;

        public override string AsString() => "(" + string.Join(", ", Params) + ") => " + Body.ToString();

        public override object GetCSValue() {
            return Body;
        }

        public override Value Invoke(Value[] args) {
            NestedEnv env = new NestedEnv(Env);
            if (CheckAndFill(args, Params, ParamTypes, Defaults, env, false, out string err)) {
                return Body.GetValue(null, env);
            } else {
                Value r = NilValue.Nil;
                r["err"] = new StringValue(err);
                return r;
            }
        }

        public static bool CheckAndFill(Value[] args, string[] ps, string[] types, IASTNode[] defaults, NestedEnv env, bool collect, out string err) {
            err = "";
            int i = 0;
            for (; i < ps.Length; i++) {
                if (i < args.Length) {
                    if (types != null && i < types.Length && types[i] != null && types[i] != args[i].Type) {
                        err += $"第{i}个参数，应为{types[i]}，却得到{args[i].Type}；";
                    } else {
                        env[ps[i]] = args[i];
                    }
                } else {
                    env[ps[i]] = (defaults != null && i < defaults.Length) ? defaults[i].GetValue(null, env) : NilValue.Nil;
                }
            }
            return err.Length == 0;
        }
    }
}
