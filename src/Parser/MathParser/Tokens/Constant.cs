namespace Parser {
	namespace Mathematical {
		public class Constant : Token {
			private Constant(double Value) : base(Value.ToString(), 0) { }

			public static implicit operator Constant(double Number) { return new Constant(Number); }

			public static implicit operator double(Constant Token) { return double.Parse(Token.Keyword); }
		}
	}
}