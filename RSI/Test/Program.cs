using System;
using System.Collections.Generic;
using top.riverelder.RSI.AST;
using top.riverelder.RSI.Computing;
using top.riverelder.RSI.Lex;
using top.riverelder.RSI.Parsing;
using top.riverelder.RSI.Tokenization;
using top.riverelder.RSI.Tokenization.Tokenizers;
using top.riverelder.RSI.Util;
using static top.riverelder.RSI.Parsing.PresetBuilders;

namespace top.riverelder.RSI.Test {
    public class Program {

        public static void Main() {

            Console.Title = "River's Simple Interpreter";
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            TestLexer();
            Console.WriteLine();


            Console.Write($"Final result: {TestCount} tested, {PassCount} passed, {TestCount - PassCount} failed, ");
            Console.ForegroundColor = ConsoleColor.Black;
            if (PassCount == TestCount) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(" All Passed ");
            } else if (PassCount == 0) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(" All Failed ");
            } else {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write(" Exist Error ");
            }
            Console.ResetColor();
            Console.WriteLine('.');

            Console.WriteLine("========");
            Console.WriteLine("EOT");
            Console.ReadKey();
        }

        public static void TestLexer() {
            string s = "(996 + 1.2 - 6 * (prev - 1) / prev - 3 * 5 * 13.6 / ext / k)";
            StringReader reader = new StringReader(s);
            TokenStream ts = new TokenStream(reader);

            ts.AddTokenizers(
                new IdTokenizer(),
                new DecimalTokenizer(),
                new DigitTokenizer(),
                new StringTokenizer(),
                new NameTokenizer()
            );

            Lexer lexer = CreateLexer();

            //TestLexerWithInput(lexer, ts, "a + b * c + d", true);
            //TestLexerWithInput(lexer, ts, "(a + b) * c + d", true);
            //TestLexerWithInput(lexer, ts, "a + (b * c + d)", true);
            //TestLexerWithInput(lexer, ts, "a + (b) * c + d", true);
            //TestLexerWithInput(lexer, ts, "a - ((b) * (c + d))", true);
            //TestLexerWithInput(lexer, ts, "a", true);
            //TestLexerWithInput(lexer, ts, "(a)", true);
            //TestLexerWithInput(lexer, ts, "a + b + * c", false);
            //TestLexerWithInput(lexer, ts, "+ a +", false);
            //TestLexerWithInput(lexer, ts, "+ a", false);
            //TestLexerWithInput(lexer, ts, "a + ", false);
            //TestLexerWithInput(lexer, ts, "a + + +", false);
            //TestLexerWithInput(lexer, ts, "a - (b) * (c + d))", false);
            //TestLexerWithInput(lexer, ts, "a - b) * (c + d", false);
            //TestLexerWithInput(lexer, ts, "/print 123", true);
            //TestLexerWithInput(lexer, ts, "/print", true);
            //TestLexerWithInput(lexer, ts, "/print a", true);
            //TestLexerWithInput(lexer, ts, "/print a a:45", true);
            //TestLexerWithInput(lexer, ts, "/print (23 + 9) 8 (45)", true);
            //TestLexerWithInput(lexer, ts, "/print (23 + 9) 8 a b:34", true);
            //TestLexerWithInput(lexer, ts, "/print 34 c:8 a b:34", true);
            //TestLexerWithInput(lexer, ts, "/print (23 + 9) 8 a b:34 c:(23 - 4)", true);
            //TestLexerWithInput(lexer, ts, "/print (23 + 9) 8 a b:34 c:23-4", true);
            //TestLexerWithInput(lexer, ts, "/print (23 + 9) 8 a b:34 5:23", false);
            //TestLexerWithInput(lexer, ts, "/print (23 + 9) 8 a b:34 5:23 d (5)", false);
            TestLexerWithInput(lexer, ts, "(23 + 9) * 5", true);

            string input;
            Console.Write(">>> ");
            while (!string.IsNullOrEmpty(input = Console.ReadLine())) {
                TestLexerWithInput(lexer, ts, input, true);
                Console.Write(">>> ");
            }
        }

        private static NestedEnv Env = Buildin.CreateBuindinEnv();
        private static int TestCount = 0;
        private static int PassCount = 0;
        public static void TestLexerWithInput(Lexer lexer, TokenStream ts, string input, bool expect) {
            ts.Reset(new StringReader(input));
            Console.WriteLine("Test: " + input);
            bool result = lexer.TryParse(ts, out IASTNode node);
            Console.Write("Result: ");
            if (result) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Accept");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error");
            }
            Console.ResetColor();
            Console.Write(" => ");
            Console.ForegroundColor = ConsoleColor.Black;
            if (result == expect) {
                PassCount++;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(" Pass ");
            } else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(" Fail ");
            }
            TestCount++;
            Console.ResetColor();
            Console.WriteLine('.');
            if (result) {
                //Console.WriteLine("AST:");
                //PrintAST(node);
                Console.WriteLine("==> " + node.GetValue(null, Env));
            }
            Console.WriteLine("--------------------------------");
        }

        public static Lexer CreateLexer() {
            LexerBuilder builder = new LexerBuilder();
            builder.Add("P", "C");
            builder.Add("C", "/", "IDN", "AS").Act(children => { var l = new List<IASTNode>() { children[0] };l.AddRange(children[1].Children); return new Command(l.ToArray()); });
            builder.Add("AS", "AS", "A").Act(children => new ASTList(new List<IASTNode>(children[0].Children) { children[1] }));
            builder.Add("AS");
            builder.Add("A", "IDN", ":", "E");
            builder.Add("A", "E");
            //builder.Add("P", "E");
            builder.Add("E", "E", "+", "T").Act(children => new Calc(children));
            builder.Add("E", "E", "-", "T").Act(children => new Calc(children));
            builder.Add("E", "T");
            builder.Add("T", "T", "*", "F").Act(children => new Calc(children));
            builder.Add("T", "T", "/", "F").Act(children => new Calc(children));
            builder.Add("T", "T", "%", "F").Act(children => new Calc(children));
            builder.Add("T", "F");
            builder.Add("F", "(", "E", ")").Act(children => children[1]);
            builder.Add("F", "IDN");
            builder.Add("F", "DIG");
            builder.Add("F", "DEC");

            Lexer lexer = builder.Build("P");

            builder.PrintBuilder();
            builder.PrintTable();
            //builder.PrintLexerCode();

            return lexer;
        }

        public static void TestParser() {
            string s = "(996 + 1.2 - 6 * (prev - 1) / prev - 3 * 5 * 13.6 / ext / k)";
            StringReader reader = new StringReader(s);
            TokenStream ts = new TokenStream(reader);

            ts.AddTokenizers(
                new IdTokenizer(),
                new DecimalTokenizer(),
                new DigitTokenizer(),
                new StringTokenizer(),
                new NameTokenizer()
                );

            Parser parser = CreateProgramParser();

            NestedEnv env = Buildin.CreateBuindinEnv();
            List<IASTNode> res = new List<IASTNode>();
            Console.Write(">>> ");
            while ((s = Console.ReadLine()) != ".exit") {
                ts.Reset(new StringReader(s));
                if (!parser.Parse(ts, res) || res.Count == 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Parse failed");
                    Console.ResetColor();
                } else {
                    IASTNode node = res[0].Reduce();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Parse succeeded");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(node.ToString());
                    Console.ResetColor();
                    string result = node.GetValue(null, env).ToString();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("==> " + result);
                    Console.ResetColor();
                    //PrintAST(node);
                }
                res.Clear();
                Console.Write(">>> ");
            }
        }

        public static Parser CreateProgramParser() {

            ParserBuindEnv env = new ParserBuindEnv();

            #region expr
            env.AddBuilder("lambda", ASTNode(Lambda.Factory, Seq(Fork("params").Or(ASTNode(Params.Factory, "name"))).Then(Skip(Id("=>"))).Then("stmt")));
            env.AddBuilder("params", ASTNode(Params.Factory, Seq(Skip(Id("("))).Then(Optional(Seq("name").Then(Optional(Repeat(Seq(Skip(Id(","))).Then("name")))).Then(Optional(Skip(Id(",")))))).Then(Skip(Id(")")))));
            env.AddBuilder("valuable", Fork("branch").Or("loop").Or("assignment").Or("expr"));
            env.AddBuilder("branch", ASTNode(Branch.Factory, Seq("expr").Then(Skip(Id("?"))).Then("stmt").Then(Optional(Seq(Skip(Id(":"))).Then("stmt")))));
            env.AddBuilder("loop", ASTNode(Loop.Factory, Seq("expr").Then(Skip(Id("~"))).Then("stmt")));
            env.AddBuilder("name", Token(Name.Factory, "name"));
            env.AddBuilder("delay", ASTNode(Delay.Factory, Seq("name").Then(Id("++", "--"))));
            env.AddBuilder("closedExpr", Seq(Skip(Id("("))).Then("valuable").Then(Skip(Id(")"))));
            env.AddBuilder("expr", ASTNode(Calc.Factory, Seq("boollet").Then(Optional(Repeat(Seq(Id("&&", "||")).Then("boollet"))))));
            env.AddBuilder("boollet", ASTNode(Calc.Factory, Seq("comlet").Then(Optional(Repeat(Seq(Id(">", "<", ">=", "<=", "==", "!=")).Then("comlet"))))));
            env.AddBuilder("comlet", ASTNode(Calc.Factory, Seq("term").Then(Optional(Repeat(Seq(Id("+", "-", "..")).Then("term"))))));
            env.AddBuilder("term", ASTNode(Calc.Factory, Seq("factor").Then(Optional(Repeat(Seq(Id("*", "/")).Then("factor"))))));
            env.AddBuilder("factor", Fork("prefixed").Or("lambda").Or("chain").Or("closedExpr").Or("delay").Or("name").Or(Token("decimal")).Or(Token("digit")).Or(Token("string")));
            env.AddBuilder("prefixed", ASTNode(Prefixed.Factory, Seq(Id("+", "-", "!")).Then("factor")));
            #endregion

            #region suffix
            env.AddBuilder("chain", ASTNode(Chain.Factory, Seq(Fork("closedExpr").Or("name")).Then(Repeat("suffix"))));
            env.AddBuilder("suffix", Fork("arguments").Or("dot").Or("index"));
            env.AddBuilder("dot", ASTNode(Dot.Factory, Seq(Skip(Id("."))).Then("name")));
            env.AddBuilder("index", ASTNode(Index.Factory, Seq(Skip(Id("["))).Then("valuable").Then(Skip(Id("]")))));
            env.AddBuilder("arguments", ASTNode(Arguments.Factory, Seq(Skip(Id("("))).Then(Optional(Seq("valuable").Then(Optional(Repeat(Seq(Skip(Id(","))).Then("valuable")))).Then(Optional(Skip(Id(",")))))).Then(Skip(Id(")")))));
            #endregion

            #region stmts
            env.AddBuilder("stmts", ASTNode(Stmts.Factory, Seq("stmt").Then(Optional(Repeat(Seq(Skip(Id(";"))).Then("stmt")))).Then(Optional(Skip(Id(";"))))));
            env.AddBuilder("stmt", Fork("block").Or("command").Or("valuable"));
            env.AddBuilder("command", ASTNode(Command.Factory, Seq(Skip(Id("/"))).Then("name").Then(Optional(Repeat("cmdArg")))));
            env.AddBuilder("cmdArg", Fork(Seq(Skip(Id("$"))).Then(Token(CmdVar.Factory, "name"))).Or(Token(ConstString.Factory, "name")).Or("valuable"));
            env.AddBuilder("block", ASTNode(Block.Factory, Seq(Skip(Id("{"))).Then("stmts").Then(Skip(Id("}")))));
            env.AddBuilder("assignment", ASTNode(Assignment.Factory, Seq(Fork("chain").Or("name")).Then(Skip(Id("="))).Then("stmt")));
            #endregion


            env.Build();
            return env.GetParser("stmts");
        }

        public static void TestTokenStream() {
            string s = "   123 123.456 778 \"Hello World!\" \"I said \\\"Hello World\\\"\" “我说“问世界好在\\””  999";
            StringReader reader = new StringReader(s);
            TokenStream ts = new TokenStream(reader);

            ts.AddTokenizers(
                new DecimalTokenizer(),
                new DigitTokenizer(),
                new StringTokenizer()
                );

            while (ts.HasMore) {
                Console.WriteLine(ts.Read());
            }
        }

        public static void PrintAST(IASTNode node, int layer) {
            if (node.IsLeaf) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int i = 0; i < layer; i++) {
                    Console.Write(i == layer - 1 ? "|---" : "|   ");
                }
                Console.ResetColor();
                Console.WriteLine(node.Token.Type);
            } else {
                foreach (IASTNode child in node.Children) {
                    PrintAST(child, layer + 1);
                }
            }
        }

        public static void PrintAST(IASTNode node) {
            PrintAST(node, 0);
        }

    }
}
