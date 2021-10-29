using System;
using FormulaEvaluator;
using System.Text.RegularExpressions;

namespace FormulaEvaluatorTest
{
    /// <summary>
    /// Class for testing Formula Evaluator.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main function for testing FormulaEvaluator.
        /// </summary>
        /// <param name="args">Don't worry about it.</param>
        static void Main(string[] args)
        {
            string expression = "w1 * w1 + (3 + 2) * 4 / 6 * ((b5))";

            Console.Write(expression + " = ");

            try
            {
                int result = Evaluator.Evaluate(expression, (string s) => 6);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!\n" + e.Message);
            }

        }
    }
}
