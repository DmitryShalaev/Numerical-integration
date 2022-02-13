using Parser;

namespace Integral {
    public class Help {
        static public string Get_Help() {
            string str =    "\nIntegral expression input format: \"a|b f(x)\"\n" +
                            "Where: a - lower limit, b - upper limit, f(x) - function of x\n" +
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

            str += "\n\nClearing the screen: clear, cls\n" +
                    "Exit the program: quit, q\n";

            return str;
        }
    }
}