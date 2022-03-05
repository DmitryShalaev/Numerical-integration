using System;

namespace Parser {
	namespace Mathematical {
		public class Operator : Token {
			public Operator(string Keyword, int Priority = 0) : base(Keyword, Priority) { }
		}

		public class BinaryOperator : Operator {
			private readonly Func<double, double, double> Function;

			public BinaryOperator(string Keyword, int Priority, Func<double, double, double> Function)
					: base(Keyword, Priority) {
				this.Function = Function;
			}

			public double Invoke(double Arg1, double Arg2) { return Function(Arg1, Arg2); }
		}

		public class UnaryOperator : Operator {
			private readonly Func<double, double> Function;

			public UnaryOperator(string Keyword, int Priority, Func<double, double> Function)
					: base(Keyword, Priority) {
				this.Function = Function;
			}

			public double Invoke(double Arg) { return Function(Arg); }
		}
	}
}