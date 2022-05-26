using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Mathematical;
public partial class MathParser {
	private List<Token> ConvertToRPN(string Expression) {
		int Position = 0;
		List<Token> RPNOutput = new();
		Stack<Token> Stack = new();
		while(Position < Expression.Length)
			SyntaxAnalysisInfixNotation(LexicalAnalysisInfixNotation(Expression, ref Position), RPNOutput, Stack);
		while(Stack.Count > 0) {
			if(Stack.Peek().IsOperator) RPNOutput.Add(Stack.Pop());
			else throw new FormatException("Format exception, there is function without parentheses");
		}
		return RPNOutput;
	}

	private Token LexicalAnalysisInfixNotation(string Expression, ref int Position) {
		StringBuilder Word = new();
		_ = Word.Append(Expression[Position]);
		if(IsOperatorDefined(Word.ToString())) {
			bool IsUnary = Position == 0 || Expression[Position - 1] == '(';
			Position++;
			return Word.ToString() switch {
				"+" => IsUnary ? UnaryPlus : Plus,
				"-" => IsUnary ? UnaryMinus : Minus,
				"," => Comma,
				_ => FindOperator(Word.ToString()),
			};
		} else if(char.IsLetter(Word[0]) || IsFunctionDefined(Word.ToString())
									|| Constants.ContainsKey(Word.ToString()) || IsVariableDefined(Word.ToString())) {
			while(++Position < Expression.Length && char.IsLetter(Expression[Position]))
				_ = Word.Append(Expression[Position]);
			return IsFunctionDefined(Word.ToString())
				? FindFunction(Word.ToString())
				: Constants.ContainsKey(Word.ToString())
				? Constants[Word.ToString()]
				: IsVariableDefined(Word.ToString()) ? (Token)FindVariable(Word.ToString()) : throw new ArgumentException("Unknown token");
		} else if(char.IsDigit(Word[0]) || Word[0] == DecimalSeparator) {
			if(char.IsDigit(Word[0]))
				while(++Position < Expression.Length && char.IsDigit(Expression[Position]))
					_ = Word.Append(Expression[Position]);
			else _ = Word.Clear();
			if(Position < Expression.Length && Expression[Position] == DecimalSeparator) {
				_ = Word.Append(DecimalSeparator);
				while(++Position < Expression.Length && char.IsDigit(Expression[Position]))
					_ = Word.Append(Expression[Position]);
			}
			if(Position + 1 < Expression.Length && Expression[Position] == 'e'
												&& (char.IsDigit(Expression[Position + 1])
												|| Position + 2 < Expression.Length
												&& (Expression[Position + 1] == '+'
												|| Expression[Position + 1] == '-')
												&& char.IsDigit(Expression[Position + 2]))) {
				_ = Word.Append(Expression[Position++]);
				if(Expression[Position] is '+' or '-')
					_ = Word.Append(Expression[Position++]);
				while(Position < Expression.Length && char.IsDigit(Expression[Position]))
					_ = Word.Append(Expression[Position++]);
				return (Constant)Convert.ToDouble(Word.ToString());
			}
			return (Constant)Convert.ToDouble(Word.ToString());
		} else throw new ArgumentException("Unknown token in expression");
	}

	private void SyntaxAnalysisInfixNotation(Token Token, List<Token> OutputList, Stack<Token> Stack) {
		if(Token == Comma) return;
		else if(Token.IsNumber || Token.IsVariable) OutputList.Add(Token);
		else if(Token.IsFunction) Stack.Push(Token);
		else if(Token == LeftParenthesis) Stack.Push(Token);
		else if(Token == RightParenthesis) {
			Token Element;
			while((Element = Stack.Pop()) != LeftParenthesis) OutputList.Add(Element);
			if(Stack.Count > 0 && Stack.Peek().IsFunction)
				OutputList.Add(Stack.Pop());
		} else {
			while(Stack.Count > 0 && Token <= Stack.Peek())
				OutputList.Add(Stack.Pop());
			Stack.Push(Token);
		}
	}
}