using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Tokenization;
using static top.riverelder.RSI.Lex.LexActionType;

namespace top.riverelder.RSI.Lex {
    public class Lexer {

        private readonly Dictionary<string, LexAction>[] Table;
        private readonly PureProduction[] Productions;

        private readonly Stack<int> Status = new Stack<int>();
        private readonly Stack<IASTNode> Symbols = new Stack<IASTNode>();

        public Lexer(Dictionary<string, LexAction>[] table, PureProduction[] productions) {
            Table = table;
            Productions = productions;
        }

        public void Reset() {
            Status.Clear();
            Symbols.Clear();
            Status.Push(0);
            Symbols.Push(new ASTLeaf(Token.EOF));
        }

        public bool TryParse(TokenStream ts, out IASTNode result) {
            Reset();
            //PrintStatus();
            while (true) {
                LexAction action = GetAction(ts.Peek());
                //Console.WriteLine($"Token: {ts.Peek().Type}, Action: {action}");
                switch (action.Type) {
                    case Accept: result = Symbols.Pop(); return true;
                    case Error: result = null; return false;
                    case Reduce: {
                            PureProduction production = Productions[action.Target];
                            Stack<IASTNode> tmp = new Stack<IASTNode>();
                            for (int i = 0; i < production.ElemCount; i++) {
                                Status.Pop();
                                tmp.Push(Symbols.Pop());
                            }
                            IASTNode node = production.Action != null ?
                                production.Action(tmp.ToArray()) :
                                (tmp.Count == 1 ? tmp.Pop() : new ASTList(tmp.ToArray()));
                            Symbols.Push(node);
                            if (Table[Status.Peek()].TryGetValue(production.Name, out LexAction a)) {
                                Status.Push(a.Target);
                            } else {
                                result = null;
                                return false;
                            }
                            break;
                        }
                    case Shift: {
                            Token token = ts.Read();
                            Symbols.Push(new ASTLeaf(token));
                            Status.Push(action.Target);
                            break;
                        }
                }
                //PrintStatus();
            }
        }

        private static readonly int CellWidth = 3;
        private void PrintStatus() {
            foreach (int s in GetList(Status)) {
                PrintCell(s);
                Console.Write(' ');
            }
            Console.WriteLine();
            foreach (IASTNode n in GetList(Symbols)) {
                PrintCell(n.Name);
                Console.Write(' ');
            }
            Console.WriteLine();
        }

        private T[] GetList<T>(Stack<T> stack) {
            Stack<T> tmp = new Stack<T>();
            foreach (T s in stack) {
                tmp.Push(s);
            }
            return tmp.ToArray();
        }

        private void PrintCell(object content) {
            string c = Convert.ToString(content);
            for (int i = 0; i < CellWidth; i++) {
                if (i < c.Length) {
                    Console.Write(c[i]);
                } else {
                    Console.Write(' ');
                }
            }
        }

        private LexAction GetAction(Token token) {
            if (Status.Count == 0 || 
                Status.Peek() < 0 || 
                Status.Peek() >= Table.Length ||
                !Table[Status.Peek()].TryGetValue(token.Type, out LexAction action)) {
                return new LexAction(Error, 0);
            }
            return action;
        }

    }
}
