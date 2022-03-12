using System;
using System.Collections.Generic;

namespace Parser {
	namespace Mathematical {
		public partial class MathParser {
			private static readonly List<Variable> Variables = new() { new("x") };

			private static readonly Operator        LeftParenthesis = new("("),
													RightParenthesis = new(")"), Comma = new(",");

			private static readonly UnaryOperator   UnaryPlus = new("un+", 6, (x) => x),
													UnaryMinus = new("un-", 6, (x) => -x);

			private static readonly BinaryOperator  Plus = new("+", 2, (x, y) => x + y),
													Minus = new("-", 2, (x, y) => x - y);

			public static readonly List<Operator>   DefaultOperators = new() {
				LeftParenthesis,
				RightParenthesis,
				Comma,
				UnaryPlus,
				UnaryMinus,
				Plus,
				Minus,
				new BinaryOperator("*", 4, (x, y) => x * y),
				new BinaryOperator("/", 4, (x, y) => x / y),
				new BinaryOperator("^", 8, Math.Pow),
				new BinaryOperator("%", 8, (x, y) => x % y)
			};

			public static readonly List<Function>   DefaultFunctions = new() {
				new UnaryFunction("sqrt", Math.Sqrt),
				new UnaryFunction("sin", Math.Sin),
				new UnaryFunction("cos", Math.Cos),
				new UnaryFunction("tan", Math.Tan),
				new UnaryFunction("ln", Math.Log),
				new UnaryFunction("exp", Math.Exp),
				new UnaryFunction("abs", Math.Abs),
				new BinaryFunction("log", Math.Log),
			};

			public static readonly Dictionary<string, Constant> Constants = new() {
				{ "pi", Math.PI },
				{ "e", Math.E }
			};
		}
	}
}