using System;

namespace Parser {
	public class UnaryFunction : Function {
		private readonly Func<double, double> Procedure;

		public UnaryFunction(string Keyword, Func<double, double> Procedure)
				: base(Keyword) {
			this.Procedure = Procedure;
		}
		public double Invoke(double Arg) { return Procedure(Arg); }
	}
}
