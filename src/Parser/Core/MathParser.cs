using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Parser {
	public partial class MathParser {

		private List<Token> RPNExpression;

		public char DecimalSeparator { get; set; }

		public readonly List<Operator> Operators = new();

		public readonly List<Function> Functions = new();

		public readonly List<Variable> Variables = new();

		public MathParser() {
			try {
				DecimalSeparator = char.Parse(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
			} catch (FormatException) {
				DecimalSeparator = '.';
			}
		}

		public void Parse(string Expression) { RPNExpression = ConvertToRPN(FormatString(Expression)); }

		public double Evaluate() { return CalculateRPN(RPNExpression); }

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