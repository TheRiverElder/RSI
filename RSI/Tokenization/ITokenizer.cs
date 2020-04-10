using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Tokenization {
    public interface ITokenizer {
        

        /// <summary>
        /// 从当前Reader中读取Token，若解析失败不会自动还原StringReader的游标
        /// </summary>
        /// <param name="reader">字符流</param>
        /// <param name="token">解析结果</param>
        /// <returns>是否解析成功</returns>
        bool Tokenize(StringReader reader, out Token token);

        /// <summary>
        /// 获取该Tokenizer的提示
        /// </summary>
        string Hint { get; }
    }
}
