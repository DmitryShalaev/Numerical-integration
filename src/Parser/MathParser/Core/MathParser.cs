using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Mathematical;
public partial class MathParser {

	private List<Token> RPNExpression;

	public char DecimalSeparator = ',';

	public void SetVariable(string Variable) {
		Variables.Clear();
		Variables.Add(new(Variable));
	}

	public void Parse(string Expression) => RPNExpression = ConvertToRPN(FormatString(Expression));

	public double Evaluate(double Variable = 0) {
		if(Variables.Count != 0)
			Variables[0].Value = Variable;

		double tmp = CalculateRPN(RPNExpression);
		return double.IsInfinity(tmp) || double.IsNaN(tmp)
			? throw new Exception("Numerical integration failed, most likely because the integral diverges")
			: tmp;
	}

	private string FormatString(string Expression) {
		if(string.IsNullOrEmpty(Expression)) throw new ArgumentNullException("Expression is null or empty");
		StringBuilder FormattedString = new();

		int UnbalancedParanthesis = 0;

		foreach(char ch in Expression) {
			if(ch == '(')
				UnbalancedParanthesis++;
			else if(ch == ')') {
				if(UnbalancedParanthesis == 0)
					throw new FormatException("Close bracket found without opening");

				UnbalancedParanthesis--;
			}

			if(char.IsWhiteSpace(ch)) continue;
			else _ = FormattedString.Append(ch);
		}

		return UnbalancedParanthesis != 0
			? throw new FormatException("Number of left and right parenthesis is not equal")
			: FormattedString.ToString().Replace(")(", ")*(");
	}
}