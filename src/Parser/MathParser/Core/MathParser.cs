using System;
using System.Collections.Generic;
using System.Text;

namespace Parser {
	namespace Mathematical {
		public partial class MathParser {

			private List<Token> RPNExpression;

			public char DecimalSeparator = ',';

			public void Parse(string Expression) { RPNExpression = ConvertToRPN(FormatString(Expression)); }

			public double Evaluate(double x = 0) {
				Variables[0].Value = x;
				double tmp = CalculateRPN(RPNExpression);
				if(double.IsInfinity(tmp) || double.IsNaN(tmp))
					throw new Exception("Numerical integration failed, most likely because the integral diverges");
				return tmp;
			}

			private string FormatString(string Expression) {
				if(string.IsNullOrEmpty(Expression)) throw new ArgumentNullException("Expression is null or empty");
				StringBuilder FormattedString = new();

				Stack<char> BalancedStack = new();					

				foreach(char ch in Expression) {
					if(ch == '(')
						BalancedStack.Push(ch);
					else if(ch == ')') {
						if(BalancedStack.Count == 0)
							throw new FormatException("Close bracket found without opening");

						BalancedStack.Pop();
					}

					if(char.IsWhiteSpace(ch)) continue;
					else FormattedString.Append(ch);
				}

				if(BalancedStack.Count != 0)
					throw new FormatException("Number of left and right parenthesis is not equal");
				

				return FormattedString.ToString().Replace(")(", ")*(");
			}
		}
	}
}