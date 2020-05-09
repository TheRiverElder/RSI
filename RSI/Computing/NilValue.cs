using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace top.riverelder.RSI.Computing {
    public class NilValue : Value {

        public static NilValue Nil => new NilValue();



        private NilValue() { }

        public override string Type => "nil";

        public override bool AsBool() => false;

        public override double AsNumber() => 0;

        public override string AsString() => "nil";

        //public override Value ComputeWith(string optr, Value another) {
        //    if (optr == ComputingOperator.Add && another.Type == "stirng") {
        //        return new StringValue("nil" + another.AsString());
        //    } else {
        //        return Nil;
        //    }
        //}

        public override object GetCSValue() => null;
    }
}
