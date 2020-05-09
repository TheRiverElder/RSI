using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Tokenization {
    public enum TokenType {

        // 字面量
        Integer,
        Decimal,
        String,
        Identity,

        // 基础符号
        Add,
        Substract,
        Muliply,
        Divide,
        Modulo,

        // 比较运算
        GreatThen,
        LessThen,
        Equal,
        NotEqual,
        GreatOrEqual,
        LessOrEqual,

        // 逻辑运算
        And,
        Or,
        Not,

        // 括号
        OpenParen,
        CloseParen,
        OpenBracket,
        ClosedBracket,
        OpenBrace,
        CloseBrace,

        // 分隔符
        EndOfLine,
        Comma,
        Semicolon,

        // 其它符号
        Arrow,
        At,
        Colon,
        Question,
        Comment,
    }
}
