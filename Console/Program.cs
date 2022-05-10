using Integral;
using Parser.Analog;
using Parser.Mathematical;
using System.Text.RegularExpressions;

namespace ConsoleIntegral {
	class Program {
		static void Main() {
			bool quit = false;

			Regex regEx = new(@"[a-z0-9(),+-\/%^]+\s*[|]\s*[a-z0-9(),+-\/%^]+\s*[a-z0-9(),+-\/%^]+\s*d[a-z]+");

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


						str = Regex.Split(text,  @"\s+([a-z0-9(),+-\/%^]+\s*d)");

						Parser.SetVariable(str[2]);
						Parser.Parse(str[1].Trim('d'));

						Console.Write("Delta: ");
						double delta = Convert.ToDouble(Console.ReadLine().Replace('.', ','));
						
						Console.WriteLine("Left rectangle rule:\t\t" + Method.left_rectangle(Parser.Evaluate, a, b, delta));
						Console.WriteLine("Right rectangle rule:\t\t" + Method.right_rectangle(Parser.Evaluate, a, b, delta));
						Console.WriteLine("Midpoint rectangle rule:\t" + Method.midpoint_rectangle(Parser.Evaluate, a, b, delta));
						Console.WriteLine("Trapezoid rule:\t\t\t" + Method.trapezoid(Parser.Evaluate, a, b, delta));
						Console.WriteLine("Simpson rule:\t\t\t" + Method.simpson(Parser.Evaluate, a, b, delta));

					} else {
						switch(text.Split()[0]) {
							case "file":
								AnalogParser analogParser = new(text.Split()[1]);

								Console.Write("Delta: ");
								double delta = Convert.ToDouble(Console.ReadLine().Replace('.', ','));

								Task[] tasks = new Task[5];
								tasks[0] = new Task(() => Console.WriteLine("Left rectangle rule:\t\t" + Method.left_rectangle(analogParser.Interpolate, analogParser.LeftBorder, analogParser.RightBorder, delta)));
								tasks[1] = new Task(() => Console.WriteLine("Right rectangle rule:\t\t" + Method.right_rectangle(analogParser.Interpolate, analogParser.LeftBorder, analogParser.RightBorder, delta)));
								tasks[2] = new Task(() => Console.WriteLine("Midpoint rectangle rule:\t" + Method.midpoint_rectangle(analogParser.Interpolate, analogParser.LeftBorder, analogParser.RightBorder, delta)));
								tasks[3] = new Task(() => Console.WriteLine("Trapezoid rule:\t\t\t" + Method.trapezoid(analogParser.Interpolate, analogParser.LeftBorder, analogParser.RightBorder, delta)));
								tasks[4] = new Task(() => Console.WriteLine("Simpson rule:\t\t\t" + Method.simpson(analogParser.Interpolate, analogParser.LeftBorder, analogParser.RightBorder, delta)));
								foreach(var item in tasks) {
									item.Start();
								}
								Task.WaitAll(tasks);

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
