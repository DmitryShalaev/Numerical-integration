using Integral;
using Parser;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Program {
    class Program {
        static void Main() {
            bool quit = false;
            MathParser Parser = new();

            Regex regEx = new(@"[a-z0-9(),+-/%^]+\s*[|]\s*[a-z0-9(),+-/%^]+\s*[a-z0-9(),+-/%^]");

            while(!quit) {
                try {
                    Console.Write(">");
                    string text = Console.ReadLine().ToLower().Replace('.', ',');

                    if(regEx.Match(text).Success) {
                        string[] str = Regex.Split(text, @"^\s*([a-z0-9(),+-/%^]+)");
                        Parser.Parse(str[1]);
                        double a = Parser.Evaluate();
                        str = Regex.Split(str[2], @"[|]\s*([a-z0-9(),+-/%^]+)");
                        Parser.Parse(str[1]);
                        double b = Parser.Evaluate();
                        if(a >= b) throw new FormatException("a >= b");

                        Parser.Parse(str[2]);

                        Console.Write("Delta: ");
                        double delta = Convert.ToDouble(Console.ReadLine().Replace('.', ','));

                        Console.WriteLine("Left rectangle rule:\t\t" + Method.left_rectangle_rule(Parser.Evaluate, a, b, delta));
                        Console.WriteLine("Right rectangle rule:\t\t" + Method.right_rectangle_rule(Parser.Evaluate, a, b, delta));
                        Console.WriteLine("Midpoint rectangle rule:\t" + Method.midpoint_rectangle_rule(Parser.Evaluate, a, b, delta));
                        Console.WriteLine("Trapezoid rule:\t\t\t" + Method.trapezoid_rule(Parser.Evaluate, a, b, delta));
                        Console.WriteLine("Simpson rule:\t\t\t" + Method.simpson_rule(Parser.Evaluate, a, b, delta));

                    } else {
                        switch(text) {
                            case "/help":
                            case "help":
                                Console.WriteLine(Help.Get_Help());
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
    }
}
