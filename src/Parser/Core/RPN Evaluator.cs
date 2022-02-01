using System;
using System.Collections.Generic;

namespace Parser {
    public partial class MathParser {
        private static double CalculateRPN(List<Token> Expression) {
            Stack<double> Stack = new();
            foreach(Token token in Expression)
                SyntaxAnalysisRPN(Stack, token);
            return Stack.Count > 1 ? throw new ArgumentException("Excess operand") : Stack.Pop();
        }

        private static void SyntaxAnalysisRPN(Stack<double> Stack, Token Token) {
            if(Token.IsNumber)
                Stack.Push((Constant)Token);
            else if(Token.IsVariable)
                Stack.Push(((Variable)Token).Value);
            else if(Token.IsUnaryOperator)
                Stack.Push((Token as UnaryOperator).Invoke(Stack.Pop()));
            else if(Token.IsUnaryFunction)
                Stack.Push((Token as UnaryFunction).Invoke(Stack.Pop()));
            else if(Token.IsBinaryOperator) {
                double Argument2 = Stack.Pop();
                double Argument1 = Stack.Pop();
                Stack.Push((Token as BinaryOperator).Invoke(Argument1, Argument2));
            } else if(Token.IsBinaryFuncion) {
                double Argument2 = Stack.Pop();
                double Argument1 = Stack.Pop();
                Stack.Push((Token as BinaryFunction).Invoke(Argument1, Argument2));
            }
        }
    }
}