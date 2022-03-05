namespace Parser {
	namespace Mathematical {
		public class Token {
			public int Priority { get; private set; }

			public string Keyword { get; private set; }

			public Token(string Keyword, int Priority) {
				this.Keyword = Keyword.ToLower();
				this.Priority = Priority;
			}

			public override string ToString() { return Keyword; }

			public bool IsNumber => GetType() == typeof(Constant);

			public bool IsVariable => GetType() == typeof(Variable);

			public bool IsOperator => GetType().IsSubclassOf(typeof(Operator));

			public bool IsUnaryOperator => GetType() == typeof(UnaryOperator);

			public bool IsBinaryOperator => GetType() == typeof(BinaryOperator);

			public bool IsUnaryFunction => GetType() == typeof(UnaryFunction);

			public bool IsBinaryFuncion => GetType() == typeof(BinaryFunction);

			public bool IsFunction => GetType().IsSubclassOf(typeof(Function));

			public override bool Equals(object obj) {
				return (obj is Token) && (obj as Token).Keyword == Keyword;
			}

			public static bool operator <=(Token T1, Token T2) {
				return T1.Keyword == "^" ? T1.Priority < T2.Priority : T1.Priority <= T2.Priority;
			}

			public static bool operator >=(Token T1, Token T2) { return T1.Priority >= T2.Priority; }
		}
	}
}