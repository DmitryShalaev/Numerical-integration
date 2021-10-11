namespace Parser {
	public class Operator : Token {
		public Operator(string Keyword, int Priority = 0) : base(Keyword, Priority) { }

		public static readonly Operator LeftParenthesis = new("("),
				RightParenthesis = new(")");
	}
}
