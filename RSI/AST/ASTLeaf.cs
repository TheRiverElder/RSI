using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public class ASTLeaf : IASTNode {

        public IASTNode this[int index] => null;

        public bool IsLeaf => true;

        public string Name => Token.Type;

        public Token Token { get; }

        public IASTNode[] Children => null;

        public int ChildrenCount => 0;

        public ASTLeaf(Token token) {
            Token = token;
        }

        public virtual Value GetValue(Value baseVaue, NestedEnv env) => Token.Value;

        public virtual Value SetValue(Value baseVaue, NestedEnv env, Value value) => NilValue.Nil;

        public virtual IASTNode Reduce() => this;

        public override string ToString() => Token.ToString();
    }
}
