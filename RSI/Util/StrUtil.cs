using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Util {
    public static class StrUtil {

        /// <summary>
        /// 将字符串转义为可以使用双引号括起来的形式
        /// </summary>
        /// <param name="origin">原始字符串</param>
        /// <param name="quote">是否在前后加上双引号</param>
        /// <returns>转义结果</returns>
        public static string Escape(string origin, bool quote) {
            string escaped = origin
                .Replace("\"", "\\\"")
                .Replace("\n", "\\\n")
                .Replace("\\", "\\\\");
            return quote ? '"' + escaped + '"' : escaped;
        }

        /// <summary>
        /// 左对齐，并填充至指定长度，若太长则会截去多余长度
        /// 注意：是字符串长度，而不是显示长度
        /// </summary>
        /// <param name="origin">原始字符串</param>
        /// <param name="len">长度</param>
        /// <returns>对齐结果</returns>
        public static string PadStart(string origin, int len) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < len; i++) {
                builder.Append(i < origin.Length ? origin[i] : ' ');
            }
            return builder.ToString();
        }

    }
}
