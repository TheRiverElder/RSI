//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using top.riverelder.RSI.Computing;
//using top.riverelder.RSI.Util;

//namespace top.riverelder.RSI.Tokenization.Tokenizers {
//    public class RSITokenizer {



//        /// <summary>
//        /// 根据输入解析Token
//        /// </summary>
//        /// <param name="reader">输入流</param>
//        /// <param name="type">解析出的Token类型</param>
//        /// <param name="value">解析出的值</param>
//        /// <returns>是否解析成功</returns>
//        public bool Tokenize(StringReader reader, out string type, out Value value) {
//            reader.SkipSpaceExpectLineSeparator();
//            NextDigit(reader, out int digit);
//            if (reader.HasMore && reader.Peek == '.') {
//                reader.
//            }
//            if (!reader.HasMore || reader.Peek != '.') {
//                token = null;
//                return false;
//            }
//            digitPart = string.IsNullOrEmpty(digitPart) ? "0" : digitPart;
//            string decimalPart = null;
//            if (!reader.Skip()
//                || string.IsNullOrEmpty(decimalPart = reader.Read(char.IsDigit))
//                || !float.TryParse(digitPart + '.' + decimalPart, out float dec)) {
//                token = null;
//                return false;
//            }
//            token = new DecimalToken(dec);
//            return true;
//        }

//        /// <summary>
//        /// 解析下一个数字
//        /// </summary>
//        /// <param name="reader">输入流</param>
//        /// <param name="digit">解析出的整数</param>
//        /// <returns>受否解析成功</returns>
//        private bool NextNumber(StringReader reader, out string type, out Value value) {
//            string digitPart = reader.Read(char.IsDigit);
//            if (string.IsNullOrEmpty(digitPart) && !(reader.HasMore && reader.Peek == '.')) {
//                type = null;
//                value = null;
//                return false;
//            }
//            if (reader.HasMore && reader.Peek == '.') {
//                reader.Skip();

//            }
//            digitPart = string.IsNullOrEmpty(digitPart) ? "0" : digitPart;
//            string decimalPart = null;
//            if (!string.IsNullOrEmpty(decimalPart = reader.Read(char.IsDigit))) {

//            }
//            if (double.TryParse(digitPart + '.' + decimalPart, out double dec)) {
//                type = "DEC";
//                value = new NumberValue(dec);
//                return false;
//            }
//            token = new DecimalToken(dec);
//            return true;
//        }

//        /// <summary>
//        /// 解析下一个整数
//        /// </summary>
//        /// <param name="reader">输入流</param>
//        /// <param name="digit">解析出的整数</param>
//        /// <returns>受否解析成功</returns>
//        private bool NextDigit(StringReader reader, out int digit) {
//            string str = reader.Read(char.IsDigit);
//            if (!string.IsNullOrEmpty(str) && int.TryParse(str, out digit)) {
//                return true;
//            }
//            digit = 0;
//            return false;
//        }



//    }
//}
