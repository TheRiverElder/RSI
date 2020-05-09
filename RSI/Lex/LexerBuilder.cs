using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Lex {
    public class LexerBuilder {

        private readonly List<Production> ProdList = new List<Production>();
        private readonly Dictionary<string, List<Production>> ProdGroups = new Dictionary<string, List<Production>>();
        private readonly Dictionary<string, string[]> FirstSets = new Dictionary<string, string[]>();
        private readonly Dictionary<string, bool> Emptyable = new Dictionary<string, bool>();
        private readonly List<List<BuildingProduction>> ItemList = new List<List<BuildingProduction>>();
        private readonly List<Dictionary<string, LexAction>> Table = new List<Dictionary<string, LexAction>>();

        public string StartName = null;

        // 判断一个名字是否是终结符
        public bool IsTerminal(string symbol) {
            return !ProdGroups.ContainsKey(symbol);
        }

        // 添加产生式
        public Production Add(string name, params string[] items) {
            if (!ProdGroups.TryGetValue(name, out List<Production> alternates)) {
                alternates = new List<Production>();
                ProdGroups[name] = alternates;
            }
            Production prod = new Production(ProdList.Count, name, items);
            ProdList.Add(prod);
            alternates.Add(prod);
            return prod;
        }

        // 根据产生式的内容，匹配项目的核心产生式，获取状态的索引，如果没有，也不添加
        public bool FindItem(BuildingProduction[] bps, out int index) {
            int max = 0;
            index = -1;
            int counter = 0;
            for (int i = 0; i < ItemList.Count; i++) {
                counter = 0;
                int coreCount = 0;
                foreach (BuildingProduction core in ItemList[i]) {
                    if (core.IsCore) {
                        coreCount++;
                    }
                }
                foreach (BuildingProduction bp in bps) {
                    foreach (BuildingProduction core in ItemList[i]) {
                        if (core.IsCore && core.Equals(bp)) {
                            counter++;
                            break;
                        }
                    }
                }
                if (bps.Length == coreCount && counter > max) {
                    max = counter;
                    index = i;
                }
            }
            return max == bps.Length;
        }

        // 构建所有状态
        public List<List<BuildingProduction>> BuildItemList(string name) {
            StartName = name;
            BuildClosure(new BuildingProduction[] { new BuildingProduction(ProdGroups[name][0], 0, "EOF") });
            return ItemList;
        }

        public Lexer Build(string name) {
            CalcEmptyalbe();
            CalcFirstSets();
            BuildItemList(name);
            PureProduction[] pps = new PureProduction[ProdList.Count];
            for (int i = 0; i < ProdList.Count; i++) {
                Production p = ProdList[i];
                pps[i] = new PureProduction(p.Name, p.Elems.Length, p.Action);
            }
            return new Lexer(Table.ToArray(), pps);
        }

        #region 计算空集
        // 计算空集
        private void CalcEmptyalbe() {
            while (Emptyable.Count < ProdGroups.Count) {
                foreach (string name in ProdGroups.Keys) {
                    CalcEmptyalbe(name, new HashSet<string>());
                }
            }
        }

        // 计算该名字能否推导出空集
        private void CalcEmptyalbe(string name, HashSet<string> chain) {
            if (Emptyable.ContainsKey(name) || chain.Contains(name)) {
                return;
            }
            int prodWithTerminalCount = 0;            
            foreach (Production prod in ProdGroups[name]) {
                if (prod.Elems.Length > 0) {
                    int prodEmptyableElemCount = 0;
                    foreach (string key in prod.Elems) {
                        if (ProdGroups.ContainsKey(key)) {
                            CalcEmptyalbe(key, new HashSet<string>(chain) { name });
                            if (Emptyable.TryGetValue(key, out bool emptyable)) {
                                if (emptyable) {
                                    prodEmptyableElemCount++;
                                } else {
                                    prodWithTerminalCount++;
                                    break;
                                }
                            }
                        } else {
                            prodWithTerminalCount++;
                            break;
                        }
                    }
                    if (prodEmptyableElemCount == prod.Elems.Length) {
                        Emptyable[name] = true;
                        return;
                    }
                } else {
                    Emptyable[name] = true;
                    return;
                }
            }
            if (prodWithTerminalCount == ProdGroups[name].Count) {
                Emptyable[name] = false;
            }
        }
        #endregion

        // 计算First集
        private void CalcFirstSets() {
            foreach (var e in ProdGroups) {
                FirstSets[e.Key] = CalcFirstSets(e.Key, e.Value, new HashSet<string>());
            }
        }

        // 计算指定的First集，要注意，该集不是克林闭包
        private string[] CalcFirstSets(string name, List<Production> prods, HashSet<string> chain) {
            if (FirstSets.TryGetValue(name, out string[] set)) {
                return set;
            }
            if (chain.Contains(name)) {
                return new string[0];
            }
            HashSet<string> firstSet = new HashSet<string>();
            foreach (Production prod in prods) {
                if (prod.Elems.Length > 0) {
                    string key = prod.Elems[0];
                    if (ProdGroups.TryGetValue(key, out List<Production> ps)) {
                        foreach (string e in CalcFirstSets(key, ps, new HashSet<string>(chain) { name })) {
                            firstSet.Add(e);
                        }
                    } else {
                        firstSet.Add(key);
                    }
                }
            }
            return FirstSets[name] = firstSet.ToArray();
        }

        // 构建状态Item，或者叫闭包Closure，并生成分析表
        private int BuildClosure(BuildingProduction[] productions) {
            // 如果已经存在，则不重复构建
            if (FindItem(productions, out int idx)) {
                return idx;
            }
            // 初始化
            int index = ItemList.Count;
            List<BuildingProduction> closure = new List<BuildingProduction> (productions);
            ItemList.Add(closure);
            foreach (BuildingProduction production in productions) {
                production.IsCore = true;
            }
            

            Dictionary<string, List<BuildingProduction>> sameNext = new Dictionary<string, List<BuildingProduction>>();
            List<BuildingProduction> toReduce = new List<BuildingProduction>();
            for (int i = closure.Count - productions.Length; i < closure.Count; i++) {
                BuildingProduction bp = closure[i];
                if (bp.Index < bp.Elems.Length) {
                    if (ProdGroups.TryGetValue(bp.Elems[bp.Index], out List<Production> prodList)) {
                        string[] lookAheads = GetFollowSet(bp);
                        foreach (Production prod in prodList) {
                            BuildingProduction nbp = new BuildingProduction(prod, 0, lookAheads);
                            bool exists = false;
                            foreach (var p in closure) {
                                if (p.CanPile(nbp)) {
                                    p.LookAheads.AddAll(nbp.LookAheads);
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists) {
                                closure.Add(nbp);
                            }
                        }
                    }
                    if (!sameNext.TryGetValue(bp.Elems[bp.Index], out var bpl)) {
                        bpl = new List<BuildingProduction>();
                        sameNext[bp.Elems[bp.Index]] = bpl;
                    }
                    bpl.Add(bp);
                } else {
                    toReduce.Add(bp);
                }
            }
            //Console.WriteLine(index + "  \n" + string.Join<BuildingProduction>("\n", productions));
            Dictionary<string, LexAction> row = new Dictionary<string, LexAction>();
            Table.Add(row);
            // 然后构建每个产生式项目的后继状态
            foreach (BuildingProduction bp in toReduce) {
                foreach (string lookAhead in bp.LookAheads) {
                    if (bp.Name == StartName) {
                        AddAction(row, lookAhead, new LexAction(LexActionType.Accept));
                    } else {
                        AddAction(row, lookAhead, new LexAction(LexActionType.Reduce, bp.Prod.Index));
                    }
                }
            }
            foreach (var e in sameNext) {
                List<BuildingProduction> nexts = new List<BuildingProduction>();
                foreach (BuildingProduction bp in e.Value) {
                    if (bp.GetNext(out BuildingProduction next)) {
                        nexts.Add(next);
                    }
                }
                int target = BuildClosure(nexts.ToArray());
                AddAction(row, e.Key, new LexAction(LexActionType.Shift, target));
            }
            return index;
        }

        private static void AddAction(Dictionary<string, LexAction> row, string key, LexAction action) {
            if (row.TryGetValue(key, out LexAction already)) {
                action.Crashes(already);
                row[key] = action;
            } else {
                row[key] = action;
            }
        }

        // 根据表达式以及其项目，获取该项目索引位置符号的Follow集
        private string[] GetFollowSet(BuildingProduction bp) {
            if (bp.Index == bp.Elems.Length - 1) {
                return bp.LookAheads.ToArray();
            } else if (!ProdGroups.TryGetValue(bp.Elems[bp.Index + 1], out List<Production> prods)) {
                return new string[] { bp.Elems[bp.Index + 1] };
            }
            HashSet<string> follow = new HashSet<string>();
            if (FirstSets.TryGetValue(bp.Elems[bp.Index + 1], out string[] firstSet) && firstSet.Length > 0) {
                follow.AddAll(firstSet);
            }
            return follow.ToArray();
        }

        #region Print

        public void PrintBuilder() {
            int numWidMax = (int)Math.Log10(ProdList.Count) + 1;
            int nameWidMax = 0;
            foreach (string name in ProdGroups.Keys) {
                nameWidMax = Math.Max(nameWidMax, name.Length);
            }
            nameWidMax += 1;
            int i = 0;
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Productions:");
            Console.WriteLine();
            foreach (Production p in ProdList) {
                Console.Write(StrUtil.PadStart(Convert.ToString(i++), numWidMax));
                Console.Write(". ");
                Console.Write(StrUtil.PadStart(p.Name, nameWidMax));
                Console.Write(" → ");
                Console.WriteLine(p.Elems.Length == 0 ? "ε" : string.Join(" ", p.Elems));
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Emptyable:");
            Console.WriteLine();
            foreach (var e in Emptyable) {
                Console.Write(StrUtil.PadStart(e.Key, nameWidMax));
                Console.Write(": ");
                Console.WriteLine(e.Value);
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Items:");
            Console.WriteLine();
            for (i = 0; i < ItemList.Count; i++) {
                Console.WriteLine($"I {i} :");
                foreach (var bp in ItemList[i]) {
                    Console.WriteLine(bp);
                }
                Console.WriteLine();
            }
        }

        private static readonly int CellWidth = 7;
        public void PrintTable() {
            HashSet<string> names = new HashSet<string>();
            foreach (var row in Table) {
                names.AddAll(row.Keys);
            }
            List<string> nameList = new List<string>(names);
            PrintRowDivider(nameList.Count + 1);
            Console.Write('|');
            PrintEmptyCell();
            foreach (string name in nameList) {
                Console.Write('|');
                PrintCell(name);
            }
            Console.WriteLine('|');
            PrintRowDivider(nameList.Count + 1);
            int i = 0;
            foreach (var row in Table) {
                Console.Write('|');
                PrintCell(Convert.ToString(i++));
                foreach (string name in nameList) {
                    Console.Write('|');
                    if (row.TryGetValue(name, out LexAction action)) {
                        if (action.Crashed) {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        PrintCell(action.ToString());
                        Console.ResetColor();
                    } else {
                        PrintEmptyCell();
                    }
                }
                Console.WriteLine('|');
                PrintRowDivider(nameList.Count + 1);
            }
        }

        private void PrintRowDivider(int nameCount) {
            Console.Write('+');
            for (int i = 0; i < nameCount; i++) {
                for (int j = 0; j < CellWidth; j++) {
                    Console.Write('-');
                }
                Console.Write('+');
            }
            Console.WriteLine();
        }

        private void PrintCell(string content) {
            for (int i = 0; i < CellWidth; i++) {
                if (i < content.Length) {
                    Console.Write(content[i]);
                } else {
                    Console.Write(' ');
                }
            }
        }

        private void PrintEmptyCell() {
            for (int i = 0; i < CellWidth; i++) {
                Console.Write(' ');
            }
        }

        public void PrintLexerCode() {
            Console.WriteLine("new Lexer(");
            // 打印产生式
            Console.WriteLine("    new PureProduction[] {");
            foreach (Production p in ProdList) {
                Console.Write("        new PureProduction(");
                Console.Write(StrUtil.Escape(p.Name, true));
                Console.Write(", ");
                Console.Write(p.Elems.Length);
                Console.WriteLine("),");
            }
            Console.WriteLine("    },");
            // 分析表
            Console.WriteLine("    new List<Dictionary<string, LexAction>> {");
            foreach (Dictionary<string, LexAction> row in Table) {
                Console.WriteLine("        new Dictionary<string, LexAction> {");
                foreach (var e in row) {
                    Console.Write("            [");
                    Console.Write(StrUtil.Escape(e.Key, true));
                    Console.Write("] = new LexAction(");
                    Console.Write(ConvertEnumToString<LexActionType>((int)e.Value.Type));
                    Console.Write(", ");
                    Console.Write(e.Value.Target);
                    Console.WriteLine("),");
                }
                Console.WriteLine("        },");
            }
            Console.WriteLine("    }");
            Console.WriteLine(")");
        }

        private string ConvertEnumToString<T>(int itemValue) {
            return Enum.Parse(typeof(T), itemValue.ToString()).ToString();
        }

        #endregion

    }
}
