using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace FormulaTest
{
    [TestClass]
    public class FormulaTests
    {
        // TESTS FOR CONSTRUCTOR METHOD/S
        /// <summary>
        /// constructor should not fail when given valid expression
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            Formula f = new Formula("1 + (a2 * 3)");
            Formula f2 = new Formula("3 / a2 - 4", a => a, a => a == "a2");
        }

        /// <summary>
        /// constructor should throw when given empty expression
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestConstructorEmptyError()
        {
            Formula f = new Formula("");
        }

        /// <summary>
        /// constructor should throw if there is not balanced parens
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestConstructorBadParenError()
        {
            Formula f = new Formula("2 + (a - 2");
        }

        /// <summary>
        /// constructor should throw if there is an ) before a ( appears
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestConstructorBadParenError2()
        {
            Formula f = new Formula("b + 3) - 2");
        }

        /// <summary>
        /// constructor should throw when it gets a token out of turn
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestConstructorBadTokenError()
        {
            Formula f = new Formula("+");
        }

        /// <summary>
        /// constructor should throw if it does not end in a float literal or variable or paren
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestConstructorEndingError()
        {
            Formula f = new Formula("4 +");
        }

        // TESTS FOR EVALUATE METHOD
        /// <summary>
        /// evaluate method should evaluate formulas correctly
        /// </summary>
        [TestMethod]
        public void TestEvaluate()
        {
            Formula f = new Formula("(1 + 2) * (4 / 2) - a");
            Assert.AreEqual(11, (double)f.Evaluate(a => -5), 1e-9);
        }

        /// <summary>
        /// evaluate method should return a formula error struct on lookup error
        /// </summary>
        [TestMethod]
        public void TestEvaluateError()
        {
            Formula f = new Formula("(1 + 2) * (4 / 2) - a");
            Assert.IsTrue(f.Evaluate(a => { throw new System.Exception("not a var"); }) is FormulaError);
        }

        /// <summary>
        /// Divide by zero should throw FormulaError
        /// </summary>
        [TestMethod]
        public void TestEvaluateErrorZero()
        {
            Formula f = new Formula("3 / 0");
            Assert.IsInstanceOfType(f.Evaluate(a => 3), typeof(FormulaError));
        }

        /// <summary>
        /// evaluate method should evaluate multivariable formulas correctly
        /// </summary>
        [TestMethod]
        public void TestEvaluate2()
        {
            Formula f = new Formula("6 * 7 / 3 + ((c - b))");
            Assert.AreEqual(15, (double)f.Evaluate(a => a == "c" ? 10 : 9), 1e-9);
        }

        // TESTS FOR GET VARIABLES METHOD
        /// <summary>
        /// get variables should return an IEnumerable of the variables
        /// </summary>
        [TestMethod]
        public void TestGetVariables()
        {
            Formula f = new Formula("a2 + b3 - 7");
            List<string> vars = new List<string>(f.GetVariables());
            Assert.AreEqual("a2", vars[0]);
            Assert.AreEqual("b3", vars[1]);
            Assert.AreEqual(2, vars.Count);
        }

        // TESTS FOR TO STRING METHOD
        /// <summary>
        /// tostring should return a string representation with no spaces which is equivalent 
        /// when passed into new constructor
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            Formula f = new Formula("1 + 3 + 9 * a");
            Assert.AreEqual("1+3+9*a", f.ToString());
            Assert.AreEqual(f, new Formula(f.ToString()));
        }

        // TESTS FOR ==
        /// <summary>
        /// == should return false if left is null and right is not
        /// </summary>
        [TestMethod]
        public void TestEqualsOperatorNull()
        {
            Formula f = new Formula("1 + 2");
            Assert.IsFalse(null == f);
        }

        /// <summary>
        /// == should return true if the expressions are equivalent
        /// </summary>
        [TestMethod]
        public void TestEqualsOperator()
        {
            Formula f1 = new Formula("1 + 2");
            Formula f2 = new Formula("1 + 2.0");
            Assert.IsTrue(f1 == f2);
        }

        /// <summary>
        /// equals method should return false when given null
        /// </summary>
        [TestMethod]
        public void TestEqualsFuncNull()
        {
            Formula f = new Formula("1 + 2");
            Assert.IsFalse(f.Equals(null));
        }

        /// <summary>
        /// != should return true when not equal
        /// </summary>
        [TestMethod]
        public void TestNotEquals()
        {
            Formula f1 = new Formula("1 + 2");
            Formula f2 = new Formula("1 + 3");
            Assert.IsTrue(f1 != f2);
        }

        // TESTS FOR GET HASH CODE METHOD
        /// <summary>
        /// gethashcode method should be the same for two equivalent expressions
        /// </summary>
        [TestMethod]
        public void TestHashCode()
        {
            Formula f1 = new Formula("1 + 2");
            Formula f2 = new Formula("1 + 2.0");
            Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
        }


    }
}