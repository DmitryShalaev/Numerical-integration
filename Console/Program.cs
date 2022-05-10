using System.Text.RegularExpressions;
using Integral;
using Parser.Analog;
using Parser.Mathematical;

namespace ConsoleIntegral {
	class Program {
		static void Main() {
			bool quit = false;

			Regex regEx = new(@"[a-z0-9(),+-\/%^]+\s*[|]\s*[a-z0-9(),+-\/%^]+\s*[a-z0-9(),+-\/%^]+");

			while(!quit) {
				try {
					Console.Write(">");
					string text = Console.ReadLine().ToLower();

					if(regEx.Match(text).Success) {
						text = text.Replace('.', ',');

						MathParser Parser = new();

						string[] str = Regex.Split(text, @"[|]\s*([a-z0-9(),+-\/%^]+)");

						Parser.Parse(str[0]);
						double a = Parser.Evaluate();

						Parser.Parse(str[1]);
						double b = Parser.Evaluate();

						if(a >= b) throw new FormatException("a >= b");

						Parser.SetVariable("x");
						Console.WriteLine(str[2]);
						Parser.Parse(str[2]);

						Console.Write("Delta: ");
						double delta = Convert.ToDouble(Console.ReadLine().Replace('.', ','));

						Method method = new(Parser.Evaluate, a, b, delta);
						Console.WriteLine("Left rectangle rule:\t\t" + method.left_rectangle());
						Console.WriteLine("Right rectangle rule:\t\t" + method.right_rectangle());
						Console.WriteLine("Midpoint rectangle rule:\t" + method.midpoint_rectangle());
						Console.WriteLine("Trapezoid rule:\t\t\t" + method.trapezoid());
						Console.WriteLine("Simpson rule:\t\t\t" + method.simpson());

					} else {
						switch(text.Split()[0]) {
							case "file":
								AnalogParser analogParser = new(text.Split()[1]);

								Console.Write("Delta: ");
								double delta = Convert.ToDouble(Console.ReadLine().Replace('.', ','));

								Method method = new(analogParser.Interpolate, analogParser.LeftBorder, analogParser.RightBorder, delta);
								Console.WriteLine("Left rectangle rule:\t\t" + method.left_rectangle());
								Console.WriteLine("Right rectangle rule:\t\t" + method.right_rectangle());
								Console.WriteLine("Midpoint rectangle rule:\t" + method.midpoint_rectangle());
								Console.WriteLine("Trapezoid rule:\t\t\t" + method.trapezoid());
								Console.WriteLine("Simpson rule:\t\t\t" + method.simpson());
								break;

							case "/help":
							case "help":
								Console.WriteLine(Help());
								break;

							case "quit":
							case "q":
								quit = true;
								break;

							case "clear":
							case "cls":
								Console.Clear();
								break;

							default:
								Console.WriteLine("Unknown command. Type \"/help\" for help.");
								break;
						}
					}
				} catch(Exception e) {
					Console.WriteLine("ERROR: " + e.Message);
					Console.WriteLine("Type \"/help\" for help.");
				}
			}
		}

		static private string Help() {
			string str =    "\nIntegral expression input format: \"a|b f(x)dx\"\n" +
							"Where: a - lower limit, b - upper limit, f(x) - function of one variable, dx - integration variable\n" +
							"a<b\n\n";

			str += "Available Operators: ";
			foreach(var item in MathParser.DefaultOperators)
				str += item + " ";

			str += "\nAvailable Functions: ";
			foreach(var item in MathParser.DefaultFunctions)
				str += item + " ";

			str += "\nAvailable Constants: ";
			foreach(var item in MathParser.Constants)
				str += item.Key + " ";

			str += "\n\nTo calculate the integral from tabular data, use the command: \"file <path>\"";

			str += "\n\nClearing the screen: clear, cls\n" +
					"Exit the program: quit, q";

			return str;
		}
	}
}
