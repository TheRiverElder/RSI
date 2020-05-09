using top.riverelder.RSI.Util;

namespace top.riverelder.RSI.Computing {
    public class StringValue : Value {


        public readonly string Value;
        public double DecValue;

        private readonly bool HasNumberValue = false;

        public StringValue(string value) {
            Value = value;
            HasNumberValue = double.TryParse(Value, out double DecValue);
        }

        public override string Type => "string";

        public override bool AsBool() => Value.Length > 0;

        public override double AsNumber() => HasNumberValue ? DecValue : 0;

        public override string AsString() => Value;

        public override object GetCSValue() => Value;

        public override string ToString() => StrUtil.Escape(Value, true);
    }
}
