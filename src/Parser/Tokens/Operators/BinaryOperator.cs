﻿using System;

namespace Parser {
	public class BinaryOperator : Operator {
		private readonly Func<double, double, double> Function;

		public BinaryOperator(string Keyword, int Priority, Func<double, double, double> Function)
				: base(Keyword, Priority) {
			this.Function = Function;
		}

		public double Invoke(double Arg1, double Arg2) { return Function(Arg1, Arg2); }
	}
}
