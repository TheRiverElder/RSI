using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Computing {
    public class BoolValue : Value {

        public static BoolValue True => new BoolValue(true);
        public static BoolValue False => new BoolValue(false);

        public override string Type => "bool";

        public readonly bool Value;

        public BoolValue(bool value) {
            Value = value;
        }

        public override bool AsBool() => Value;

        public override double AsNumber() => Value ? 1 : 0;

        public override string AsString() => Value ? "true" : "false";

        public override object GetCSValue() => Value;
    }
}
