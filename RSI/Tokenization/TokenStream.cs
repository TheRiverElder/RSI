using System.Collections.Generic;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization {
    public class TokenStream {


        private StringReader Reader;
        public int Index = 0;
        private readonly List<Token> ParsedTokens = new List<Token>();
        private readonly List<ITokenizer> Tokenizers = new List<ITokenizer>();

        public TokenStream(StringReader reader) {
            Reader = reader;
        }

        public TokenStream AddTokenizers(params ITokenizer[] tokenizers) {
            Tokenizers.AddRange(tokenizers);
            return this;
        }

        /// <summary>
        /// 使用心得字符流重置Token流
        /// </summary>
        /// <param name="reader">新的字符流</param>
        /// <returns>自身</returns>
        public TokenStream Reset(StringReader reader) {
            Reader = reader;
            Index = 0;
            ParsedTokens.Clear();
            return this;
        }

        /// <summary>
        /// 检查是否还有更多Token
        /// </summary>
        public bool HasMore => Fill(Index);

        /// <summary>
        /// 从流中读取下一个Token
        /// </summary>
        /// <returns>下一个Token</returns>
        public Token Read() {
            return Fill () ? ParsedTokens[Index++] : Token.EOF.SetRange(Reader.Data, -1, -1);
        }

        /// <summary>
        /// 从流中读取下一个Token，但是不移动游标
        /// </summary>
        /// <returns>下一个Token</returns>
        public Token Peek() {
            return Fill() ? ParsedTokens[Index] : Token.EOF.SetRange(Reader.Data, -1, -1);
        }

        /// <summary>
        /// 解析Token到能满足下一次Read或Peek
        /// </summary>
        /// <returns>是否填充到了所需长度</returns>
        public bool Fill() => Fill(Index);

        /// <summary>
        /// 接续出足够长度的Token以保证目标索引能够读取
        /// </summary>
        /// <param name="targetIndex">目标索引</param>
        /// <returns>是否填充到了所需长度</returns>
        public bool Fill(int targetIndex) {
            while (ParsedTokens.Count <= targetIndex && TryParse(Reader, out Token token)) {
                ParsedTokens.Add(token);
            }
            return ParsedTokens.Count > targetIndex;
        }

        /// <summary>
        /// 试图解析下一个Token
        /// </summary>
        /// <param name="reader">字符流</param>
        /// <param name="token">解析结果</param>
        /// <returns>是否解析成功</returns>
        public bool TryParse(StringReader reader, out Token token) {
            if (!reader.SkipSpaceExpectLineSeparator()) {
                token = null;
                return false;
            }
            int start = reader.Cursor;
            foreach (ITokenizer tokenizer in Tokenizers) {
                if (tokenizer.Tokenize(reader, out token)) {
                    token.SetRange(reader.Data, start, reader.Cursor);
                    return true;
                }
                reader.Cursor = start;
            }
            token = null;
            return false;
        }
    }
}
