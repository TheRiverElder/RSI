using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Computing {
    public abstract class Value {


        /// <summary>
        /// 该数值的类型
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// 获取C#原始的值对象
        /// </summary>
        /// <returns>原始对象</returns>
        public abstract object GetCSValue();

        /// <summary>
        /// 以布尔值表示该值
        /// </summary>
        /// <returns>布尔值</returns>
        public abstract bool AsBool();

        /// <summary>
        /// 以数字表示该值
        /// </summary>
        /// <returns>数字</returns>
        public abstract double AsNumber();

        /// <summary>
        /// 以布尔值表示该值
        /// </summary>
        /// <returns>数字</returns>
        public abstract string AsString();

        public Dictionary<string, Value> Members { get; private set; } = null;

        /// <summary>
        /// 获取或设值成员变量，若没有相应值，必须返回UNDEFINED
        /// </summary>
        /// <param name="key">成员的键</param>
        /// <returns>成员的值或者UNDEFINED</returns>
        public virtual Value this[Value key] {
            get {
                if (Members != null && Members.TryGetValue(key.AsString(), out Value value)) {
                    return value;
                } else {
                    return NilValue.Nil;
                }
            }
            set {
                if (Members == null) {
                    Members = new Dictionary<string, Value>();
                }
                Members[key.AsString()] = value;
            }
        }

        /// <summary>
        /// 以字符串形式获取或设值成员变量，若没有相应值，必须返回UNDEFINED
        /// </summary>
        /// <param name="key">成员的字符串键</param>
        /// <returns>成员的值或者UNDEFINED</returns>
        public virtual Value this[string key] {
            get {
                if (Members != null && Members.TryGetValue(key, out Value value)) {
                    return value;
                } else {
                    return NilValue.Nil;
                }
            }
            set {
                if (Members == null) {
                    Members = new Dictionary<string, Value>();
                }
                Members[key] = value;
            }
        }

        /// <summary>
        /// 与另一个值之间使用双目操作符进行计算
        /// </summary>
        /// <param name="optr">双目操作符</param>
        /// <param name="another">另一个数值</param>
        /// <returns>计算结果</returns>
        public virtual Value ComputeWith(string optr, Value another) {
            switch (optr) {
                case ComputingOperator.Add: return new NumberValue(AsNumber() + another.AsNumber());
                case ComputingOperator.Sub: return new NumberValue(AsNumber() - another.AsNumber());
                case ComputingOperator.Mul: return new NumberValue(AsNumber() * another.AsNumber());
                case ComputingOperator.Div: return new NumberValue(AsNumber() / another.AsNumber());
                case ComputingOperator.Mod: return new NumberValue(AsNumber() % another.AsNumber());
                case ComputingOperator.GT: return new BoolValue(AsNumber() > another.AsNumber());
                case ComputingOperator.LT: return new BoolValue(AsNumber() < another.AsNumber());
                case ComputingOperator.GE: return new BoolValue(AsNumber() >= another.AsNumber());
                case ComputingOperator.LE: return new BoolValue(AsNumber() <= another.AsNumber());
                case ComputingOperator.EQ: return new BoolValue(AsNumber() == another.AsNumber());
                case ComputingOperator.NE: return new BoolValue(AsNumber() != another.AsNumber());
                case ComputingOperator.And: return new BoolValue(AsBool() && another.AsBool());
                case ComputingOperator.Or: return AsBool() ? this : another;
                case ComputingOperator.Con: return new StringValue(AsString() + another.AsString());
                default: return NilValue.Nil;
            }
        }

        /// <summary>
        /// 以函数形式调用该对象
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>结果</returns>
        public virtual Value Invoke(Value[] args) {
            if (Members != null && Members.TryGetValue(ComputingMagicName.Inv, out Value inv) && inv != this) {
                return inv.Invoke(args);
            } else {
                return NilValue.Nil;
            }
        }

        public override string ToString() {
            return AsString();
        }

        public static implicit operator Value(string v) {
            throw new NotImplementedException();
        }
    }
}
