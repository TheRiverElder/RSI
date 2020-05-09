using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Computing;

namespace top.riverelder.RSI.Tokenization {

    /// <summary>
    /// 从文档流中解析出的Token
    /// </summary>
    public class Token {

        public static Token EOF => new Token("EOF");

        /// <summary>
        /// 该Token的类型
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// 该Token的数值，若该Token仅为符号则Value为null
        /// </summary>
        public Value Value { get; }

        /// <summary>
        /// 该Token所在文档
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// 该Token在文档流中的起始位置（包括）
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// 该Token在文档流中的结束位置（不包括）
        /// </summary>
        public int EndIndex { get; set; }

        /// <summary>
        /// 根据类型和值构造Token
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public Token(string type, Value value) {
            Type = type;
            Value = value;
        }

        /// <summary>
        /// 仅仅根据类型构造Token，其值为null
        /// </summary>
        /// <param name="type"></param>
        public Token(string type) {
            Type = type;
            Value = null;
        }

        /// <summary>
        /// 用于设置该Token在文档中的位置
        /// </summary>
        /// <param name="document">文档</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="endIndex">结束位置</param>
        public Token SetRange(string document, int startIndex, int endIndex) {
            Document = document;
            StartIndex = startIndex;
            EndIndex = endIndex;
            return this;
        }

        public string Literal => StartIndex >= 0 ? Document.Substring(StartIndex, EndIndex - StartIndex) : "EOF";

        public override string ToString() => $"<{Type}{Value?? ", " + Value}>";
    }
}
