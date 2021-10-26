using System;
using System.Collections.Generic;
using System.Text;

namespace Parser {
	public partial class MathParser {

		private List<Token> RPNExpression;

		public char DecimalSeparator = ',';

		public readonly List<Operator> Operators = new();

		public readonly List<Function> Functions = new();

		public readonly List<Variable> Variables = new();

		public MathParser() { Variables.Add(new("x")); }

		public void Parse(string Expression) { RPNExpression = ConvertToRPN(FormatString(Expression)); }

		public double Evaluate(double x = 0) {
			Variables[0].Value = x;
			double tmp = CalculateRPN(RPNExpression);
			if (double.IsInfinity(tmp) || double.IsNaN(tmp))
				throw new Exception("Numerical integration failed, most likely because the integral diverges");
			return  tmp;
		}


		private static string FormatString(string Expression) {
			if (string.IsNullOrEmpty(Expression)) throw new ArgumentNullException("Expression is null or empty");
			StringBuilder FormattedString = new();
			int UnbalancedParanthesis = 0;

			foreach (char ch in Expression) {
				if (ch == '(') UnbalancedParanthesis++;
				else if (ch == ')') UnbalancedParanthesis--;
				if (char.IsWhiteSpace(ch)) continue;
				else if (char.IsUpper(ch)) FormattedString.Append(Char.ToLower(ch));
				else FormattedString.Append(ch);
			}
			if (UnbalancedParanthesis != 0)
				throw new FormatException("Number of left and right parenthesis is not equal");
			return FormattedString.ToString().Replace(")(", ")*(");
		}
	}
}