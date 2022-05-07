using System;

namespace Parser.Mathematical {
	public partial class MathParser {
		private bool IsOperatorDefined(string Keyword) {
			foreach(Operator Operator in DefaultOperators)
				if(Operator.Keyword == Keyword)
					return true;
			return false;
		}

		private bool IsFunctionDefined(string Keyword) {
			foreach(Function Function in DefaultFunctions)
				if(Function.Keyword == Keyword)
					return true;
			return false;
		}

		private bool IsVariableDefined(string Keyword) {
			foreach(Variable Variable in Variables)
				if(Variable.Keyword == Keyword)
					return true;
			return false;
		}

		private Operator FindOperator(string Keyword) {
			foreach(Operator Operator in DefaultOperators)
				if(Operator.Keyword == Keyword) return Operator;
			throw new ArgumentException("Invalid Operator Token");
		}

		private Function FindFunction(string Keyword) {
			foreach(Function Function in DefaultFunctions)
				if(Function.Keyword == Keyword) return Function;
			throw new ArgumentException("Invalid Function Token");
		}

		private Variable FindVariable(string Keyword) {
			foreach(Variable Variable in Variables)
				if(Variable.Keyword == Keyword) return Variable;
			throw new ArgumentException("Undefined Variable");
		}
	}
}