using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Computing {
    public class NumberValue : Value {

        public double Value;

        public NumberValue(double value) {
            Value = value;
        }

        public override string Type => "number";

        public override bool AsBool() => Value != 0;

        public override double AsNumber() => Value;

        public override string AsString() => Value.ToString();

        public override object GetCSValue() => Value;


    }
}
