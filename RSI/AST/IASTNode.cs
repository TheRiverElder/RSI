using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Tokenization;

namespace top.riverelder.RSI.AST {
    public interface IASTNode : IGetValue {

        bool IsLeaf { get; }

        string Name { get; }

        Token Token { get; }

        IASTNode[] Children { get; }

        IASTNode this[int index] { get; }

        int ChildrenCount { get; }

        IASTNode Reduce();

        Value SetValue(Value baseValue, NestedEnv env, Value value);
    }
}
