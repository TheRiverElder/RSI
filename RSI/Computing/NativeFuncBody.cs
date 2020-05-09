using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.Computing {
    public class NativeFuncBody : IASTNode {
        
        public static NativeFuncBody Of(Func<NestedEnv, Value> body) {
            return new NativeFuncBody(body);
        }

        public readonly Func<NestedEnv, Value> Body;

        private NativeFuncBody(Func<NestedEnv, Value> body) {
            Body = body;
        }

        public IASTNode this[int index] => null;

        public bool IsLeaf => false;

        public Token Token => null;

        public IASTNode[] Children => null;

        public int ChildrenCount => 0;

        public string Name => "native";

        public Value GetValue(Value baseValue, NestedEnv env) => Body(env);

        public IASTNode Reduce() => this;

        public Value SetValue(Value baseValue, NestedEnv env, Value value) => NilValue.Nil;

        public override string ToString() => "[native node]";
    }
}
