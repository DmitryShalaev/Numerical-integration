using System;

namespace Parser {
	public abstract class Function : Token {
		public Function(string Keyword, int Priority = 10) : base(Keyword, Priority) { }
	}

	public class BinaryFunction : Function {
		private readonly Func<double, double, double> Procedure;

		public BinaryFunction(string Keyword, Func<double, double, double> Procedure)
				: base(Keyword) {
			this.Procedure = Procedure;
		}

		public double Invoke(double Arg1, double Arg2) { return Procedure(Arg1, Arg2); }
	}

	public class UnaryFunction : Function {
		private readonly Func<double, double> Procedure;

		public UnaryFunction(string Keyword, Func<double, double> Procedure)
				: base(Keyword) {
			this.Procedure = Procedure;
		}
		public double Invoke(double Arg) { return Procedure(Arg); }
	}
}
