using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FormulaEvaluator
{
    /// <summary>
    /// Evaluator class.
    /// </summary>
    public static class Evaluator
    {
        public delegate int Lookup(string v);

        private static Stack<string> operatorStack;
        private static Stack<int> valueStack;

        /// <summary>
        /// Evaluates a given integer expression. Supports string variables, integer literals, and operations +, -, *, and /.
        /// </summary>
        /// <param name="exp">A string expression to be evaluated.</param>
        /// <param name="variableEvaluator">A function that takes a string and returns its corresponding value as an int.</param>
        /// <returns>An integer evaluation of the expression given. (or an error)</returns>
        public static int Evaluate(string exp, Lookup variableEvaluator)
        {
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            operatorStack = new Stack<string>();
            valueStack = new Stack<int>();

            try
            {
                // loop through substrings
                foreach (string token in substrings)
                {
                    string t = token.Trim();
                    if (t == "") { continue; } // whitespace is stupid, if thats all it is, we can just skip to next token.
                    // check for a number or variable
                    if (Regex.IsMatch(t, "^[a-zA-Z]*[0-9]*$") && Regex.IsMatch(t, "[0-9]$"))
                    {
                        int value;
                        if (!int.TryParse(t, out value)) { value = variableEvaluator(t); } // checks if variable, then finds its value
                        valueStack.Push(value);
                        EvaluateHelper(false);
                    }
                    // check for '+' or '-'
                    else if (Regex.IsMatch(t, "^(\\+|-)$"))
                    {
                        EvaluateHelper(true);
                        operatorStack.Push(t);
                    }
                    // check for '(', '*', or '/'
                    else if (Regex.IsMatch(t, "^(\\(|\\*|/)$")) { operatorStack.Push(t); }
                    // checks for ')'
                    else if (t == ")")
                    {
                        EvaluateHelper(true);
                        if (operatorStack.Pop() != "(") { throw new ArithmeticException("invalid expression"); }
                        EvaluateHelper(false);
                    }
                    else { throw new Exception("invalid token"); }
                }

                // final check to make sure there is one value left
                EvaluateHelper(true);
                if (valueStack.Count != 1) { throw new Exception("more than one item left on stack"); }

                // return solution
                return (int)valueStack.Pop();
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Helper function for Evaluate function
        /// </summary>
        /// <param name="isAdd">Bool that is true if the operations to be done are + and -, false if * and /.</param>
        private static void EvaluateHelper(bool isAdd)
        {
            if(operatorStack.Count > 0)
            {
                if((isAdd && Regex.IsMatch(operatorStack.Peek(), "^(\\+|-)$")) || (!isAdd && Regex.IsMatch(operatorStack.Peek(), "^(\\*|/)$")))
                {
                    switch (operatorStack.Pop())
                    {
                        case "+":
                            valueStack.Push(valueStack.Pop() + valueStack.Pop());
                            break;
                        case "-":
                            int value = valueStack.Pop();
                            valueStack.Push(valueStack.Pop() - value);
                            break;
                        case "*":
                            valueStack.Push(valueStack.Pop() * valueStack.Pop());
                            break;
                        case "/":
                            int value1 = valueStack.Pop();
                            valueStack.Push(valueStack.Pop() / value1);
                            break;
                    }
                }
            }
        }
    }
}
