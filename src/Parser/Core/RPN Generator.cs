using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Parser {
    public partial class MathParser {
        private List<Token> ConvertToRPN(string Expression) {
            int Position = 0;
            List<Token> RPNOutput = new();
            Stack<Token> Stack = new();
            while(Position < Expression.Length)
                SyntaxAnalysisInfixNotation(LexicalAnalysisInfixNotation(Expression, ref Position), RPNOutput, Stack);
            while(Stack.Count > 0) {
                if(Stack.Peek().IsOperator) RPNOutput.Add(Stack.Pop());
                else throw new FormatException("Format exception, there is function without parenthesis");
            }
            return RPNOutput;
        }

        private Token LexicalAnalysisInfixNotation(string Expression, ref int Position) {
            StringBuilder Word = new();
            Word.Append(Expression[Position]);
            if(IsOperatorDefined(Word.ToString())) {
                bool IsUnary = Position == 0 || Expression[Position - 1] == '(';
                Position++;
                switch(Word.ToString()) {
                    case "+":
                        return IsUnary ? (Operator)UnaryPlus : Plus;
                    case "-":
                        return IsUnary ? (Operator)UnaryMinus : Minus;
                    case ",":
                        return Comma;
                    default:
                        return FindOperator(Word.ToString());
                }
            } else if(char.IsLetter(Word[0]) || IsFunctionDefined(Word.ToString())
                                        || Constants.ContainsKey(Word.ToString()) || IsVariableDefined(Word.ToString())) {
                while(++Position < Expression.Length && char.IsLetter(Expression[Position]))
                    Word.Append(Expression[Position]);
                if(IsFunctionDefined(Word.ToString()))
                    return FindFunction(Word.ToString());
                else if(Constants.ContainsKey(Word.ToString()))
                    return Constants[Word.ToString()];
                else if(IsVariableDefined(Word.ToString()))
                    return FindVariable(Word.ToString());
                else throw new ArgumentException("Unknown token");
            } else if(char.IsDigit(Word[0]) || Word[0] == DecimalSeparator) {
                if(char.IsDigit(Word[0]))
                    while(++Position < Expression.Length && char.IsDigit(Expression[Position]))
                        Word.Append(Expression[Position]);
                else Word.Clear();
                if(Position < Expression.Length && Expression[Position] == DecimalSeparator) {
                    Word.Append(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    while(++Position < Expression.Length
                    && char.IsDigit(Expression[Position]))
                        Word.Append(Expression[Position]);
                }
                if(Position + 1 < Expression.Length && Expression[Position] == 'e'
                        && (char.IsDigit(Expression[Position + 1])
                                || (Position + 2 < Expression.Length
                                        && (Expression[Position + 1] == '+'
                                                || Expression[Position + 1] == '-')
                                                        && char.IsDigit(Expression[Position + 2])))) {
                    Word.Append(Expression[Position++]);
                    if(Expression[Position] == '+' || Expression[Position] == '-')
                        Word.Append(Expression[Position++]);
                    while(Position < Expression.Length && char.IsDigit(Expression[Position]))
                        Word.Append(Expression[Position++]);
                    return (Constant)Convert.ToDouble(Word.ToString());
                }
                return (Constant)Convert.ToDouble(Word.ToString());
            } else throw new ArgumentException("Unknown token in expression");
        }

        private static void SyntaxAnalysisInfixNotation(Token Token, List<Token> OutputList, Stack<Token> Stack) {
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
                while(Stack.Count > 0 && (Token <= Stack.Peek()))
                    OutputList.Add(Stack.Pop());
                Stack.Push(Token);
            }
        }
    }
}