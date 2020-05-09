using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Util {
    public class StringReader {

        public int Cursor;
        public readonly string Data;

        /// <summary>
        /// 通过指定游标与字符串创建StringReader
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="data"></param>
        public StringReader(int cursor, string data) {
            Cursor = cursor;
            Data = data;
        }

        /// <summary>
        /// 通过字符串与默认游标（0）创建StringReader
        /// </summary>
        /// <param name="data"></param>
        public StringReader(string data) {
            Data = data;
            Cursor = 0;
        }

        /// <summary>
        /// 通过上一个StringReader复制一个StringReader
        /// </summary>
        /// <param name="reader"></param>
        public StringReader(StringReader reader) {
            Cursor = reader.Cursor;
            Data = reader.Data;
        }

        /// <summary>
        /// 哦按段该StringReader是否还有可读内容
        /// </summary>
        public bool HasMore => Cursor < Data.Length;

        /// <summary>
        /// 获取下一个字符，但不移动游标
        /// </summary>
        public char Peek => Data[Cursor];

        /// <summary>
        /// 获取下一个字符，并移动游标
        /// </summary>
        public char Next => Data[Cursor++];

        /// <summary>
        /// 跳过一个字符，并移动游标
        /// </summary>
        /// <returns></returns>
        public bool Skip() => ++Cursor < Data.Length;

        /// <summary>
        /// 从游标位置读取指定字符串
        /// </summary>
        /// <param name="s">试图读取的字符串</param>
        /// <returns>是否读取成功</returns>
        public bool Read(string s) {
            if (Data.Length - Cursor < s.Length) {
                return false;
            }
            int start = Cursor;
            foreach(char c in s) {
                if (c != Data[Cursor++]) {
                    Cursor = start;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 从当前游标读取指定字符
        /// </summary>
        /// <param name="ch">指定的字符</param>
        /// <returns>是否读取成功</returns>
        public bool Read(char ch) {
            if (!HasMore || Data[Cursor] != ch) {
                return false;
            }
            Cursor++;
            return true;
        }

        /// <summary>
        /// 跳过空白字符
        /// </summary>
        /// <returns>跳过后是否还有后续字符</returns>
        public bool SkipSpace() {
            while(HasMore && char.IsWhiteSpace(Data, Cursor)) {
                Cursor++;
            }
            return HasMore;
        }

        /// <summary>
        /// 跳过除了换行外的空白字符
        /// </summary>
        /// <returns>跳过后是否还有后续字符</returns>
        public bool SkipSpaceExpectLineSeparator() {
            while (HasMore && char.IsWhiteSpace(Data, Cursor) && Data[Cursor] != '\n') {
                Cursor++;
            }
            return HasMore;
        }

        /// <summary>
        /// 从游标开始读取指定字符集合中的字符
        /// </summary>
        /// <param name="chs">指定字符集合</param>
        /// <returns>读取到的字符串</returns>
        public string Read(IEnumerable<char> chs) {
            int start = Cursor;
            while (HasMore && chs.Contains(Data[Cursor])) {
                Cursor++;
            }
            return Data.Substring(start, Cursor - start);
        }

        /// <summary>
        /// 从游标开始读取不包含在指定字符集合中的字符
        /// </summary>
        /// <param name="chs">排除的字符集合</param>
        /// <returns>读取到的字符串</returns>
        public string ReadTo(IEnumerable<char> chs) {
            int start = Cursor;
            while (HasMore && !chs.Contains(Data[Cursor])) {
                Cursor++;
            }
            return Data.Substring(start, Cursor - start);
        }

        /// <summary>
        /// 从游标开始读取到指定字符集合的结束，但是只返回遇到指定字符之前的部分
        /// </summary>
        /// <param name="chs">指定字符集合</param>
        /// <returns>读取到指定字符前的字符串</returns>
        public string ReadOver(IEnumerable<char> chs) {
            int start = Cursor;
            while (HasMore && !chs.Contains(Data[Cursor])) {
                Cursor++;
            }
            string res = Data.Substring(start, Cursor - start);
            while (HasMore && chs.Contains(Data[Cursor])) {
                Cursor++;
            }
            return res;
        }

        /// <summary>
        /// 从游标开始读取符合过滤器的字符
        /// </summary>
        /// <param name="predicate">过滤器</param>
        /// <returns>读取到的字符串</returns>
        public string Read(Predicate<char> predicate) {
            int start = Cursor;
            while (HasMore && predicate(Data[Cursor])) {
                Cursor++;
            }
            return Data.Substring(start, Cursor - start);
        }

        /// <summary>
        /// 从游标开始读取不符合过滤器的字符
        /// </summary>
        /// <param name="predicate">过滤器</param>
        /// <returns>读取到的字符串</returns>
        public string ReadTo(Predicate<char> predicate) {
            int start = Cursor;
            while (HasMore && !predicate(Data[Cursor])) {
                Cursor++;
            }
            return Data.Substring(start, Cursor - start);
        }

        /// <summary>
        /// 从游标开始读取到符合过滤器的结束，但是只返回遇到指定字符之前的部分
        /// </summary>
        /// <param name="predicate">过滤器</param>
        /// <returns>读取到符合过滤器前的字符串</returns>
        public string ReadOver(Predicate<char> predicate) {
            int start = Cursor;
            while (HasMore && !predicate(Data[Cursor])) {
                Cursor++;
            }
            string res = Data.Substring(start, Cursor - start);
            while (HasMore && predicate(Data[Cursor])) {
                Cursor++;
            }
            return res;
        }
    }
}
