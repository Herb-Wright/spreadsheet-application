using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        // constructor
        /// <summary>
        /// constructor should not throw an error
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            Spreadsheet s = new Spreadsheet();
        }

        [TestMethod]
        public void TestConstructorLoad()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "hello");
            s.SetContentsOfCell("a2", "=a3 + 2");
            s.SetContentsOfCell("a3", "2.1");
            s.SetContentsOfCell("a4", "3.2");
            s.SetContentsOfCell("a3", "");
            s.Save("file.xml");
            s = new Spreadsheet("file.xml", s => true, s => s, "default");
            Assert.AreEqual("hello", s.GetCellContents("a1"));
            Assert.AreEqual(new Formula("a3+2"), s.GetCellContents("a2"));
            Assert.AreEqual("", s.GetCellContents("a3"));
            Assert.AreEqual("3.2", s.GetCellContents("a4").ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestConstructorLoadError()
        {
            Spreadsheet s = new Spreadsheet("so34me/randoa55m/path44/file.xml", s => true, s => s, "default");
        }

        // set cell contents
        /// <summary>
        /// SetCellContents to double should not throw an error, and should return an Ilist of length 1
        /// </summary>
        [TestMethod]
        public void TestSetCellContentsNumber()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual(1, s.SetContentsOfCell("a1", "3.2").Count);
        }

        /// <summary>
        /// same as above, but for text
        /// </summary>
        [TestMethod]
        public void TestSetCellContentsText()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual(1, s.SetContentsOfCell("a1", "hello").Count);
        }

        /// <summary>
        /// same as above, but for formula
        /// </summary>
        [TestMethod]
        public void TestSetCellContentsFormula()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual(1, s.SetContentsOfCell("a1", "=a2 + 3").Count);
        }

        /// <summary>
        /// SetCellContents should return a list with all the cells that depend on it.
        /// </summary>
        [TestMethod]
        public void TestSetCellContentsReturn()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "=a2 + 3");
            List<string> myList = new List<string>(s.SetContentsOfCell("a2", "3.2"));
            Assert.AreEqual(2, myList.Count);
            Assert.AreEqual("a2", myList[0]);
            Assert.AreEqual("a1", myList[1]);

        }

        /// <summary>
        /// SetCellContents should throw if circular reference
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestSetCellContentsCircularException()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "=a1+1");
        }

        /// <summary>
        /// SetCellContents should throw with an invalid variable
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNameException()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("2a1", "hello");
        }

        /// <summary>
        /// same as above, but for formula constructor
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNameNull()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell((string)null, "3.5");
        }

        /// <summary>
        /// method should throw if text argument in null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetCellContentsNullStringException()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", (string)null);
        }

        // get cell contents
        /// <summary>
        /// GetCellContents should return an empty string when given a new cell name
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsEmpty()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual("", s.GetCellContents("a1"));
        }

        /// <summary>
        /// method should return contents of cell that has been filled
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsNumber()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "23.0");
            Assert.AreEqual(23.0, s.GetCellContents("a1"));
        }

        /// <summary>
        /// same as above but for text
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsText()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", s.GetCellContents("a1"));
        }

        /// <summary>
        /// same as above but for formula
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "=a2 + 3");
            Assert.AreEqual(new Formula("a2 + 3"), s.GetCellContents("a1"));
        }

        /// <summary>
        /// method should throw if invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsInvalidName()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("2a1");
        }

        /// <summary>
        /// method should throw if name is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsNullName()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents((string)null);
        }

        // get all non empty cells
        /// <summary>
        /// GetNonEmptyCells should return an empty list if there are no non empty cells
        /// </summary>
        [TestMethod]
        public void TestGetNonEmptyCellsEmpty()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual(0, new List<string>(s.GetNamesOfAllNonemptyCells()).Count);
        }

        /// <summary>
        /// method should return all added cells if none are deleted (set to '')
        /// </summary>
        [TestMethod]
        public void TestGetNonEmptyCellsAdded()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "3.2");
            s.SetContentsOfCell("a2", "=a3 + 2");
            List<string> myList = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(2, myList.Count);
            Assert.IsTrue(myList.Contains("a1") && myList.Contains("a2"));
        }

        /// <summary>
        /// method should return only cells that are not ''.
        /// </summary>
        [TestMethod]
        public void TestGetNonEmptyCellsRemoved()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "hello");
            s.SetContentsOfCell("a2", "=a3 + 2");
            s.SetContentsOfCell("a3", "2.1");
            s.SetContentsOfCell("a2", "");
            s.SetContentsOfCell("a3", "");
            List<string> myList = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(1, myList.Count);
            Assert.IsTrue(myList.Contains("a1"));
        }

        /// <summary>
        /// Save method should return an xml with all the cells
        /// </summary>
        [TestMethod]
        public void TestSaveXML()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "hello");
            s.SetContentsOfCell("a2", "=a3 + 2");
            s.SetContentsOfCell("a3", "2.1");
            s.SetContentsOfCell("a4", "3.2");
            s.SetContentsOfCell("a3", "");
            s.Save("file.xml");
            Dictionary<string, string> parsedCells = new Dictionary<string, string>();
            // now test that it is correct
            using(XmlReader reader = XmlReader.Create("file.xml"))
            {
                while(reader.Read())
                {
                    if(reader.NodeType == XmlNodeType.Element && reader.Name == "cell")
                    {
                        reader.ReadToFollowing("name");
                        string n = reader.ReadElementContentAsString();
                        reader.ReadToFollowing("contents");
                        parsedCells[n] = reader.ReadElementContentAsString();
                    }
                }
            }
            Assert.AreEqual("hello", parsedCells["a1"]);
            Assert.AreEqual(new Formula("a3+2"), new Formula(parsedCells["a2"].Substring(1)));
            Assert.IsFalse(parsedCells.ContainsKey("a3"));
            Assert.AreEqual("3.2", parsedCells["a4"]);
        }

        /// <summary>
        /// the GetSavedVersion method should return the version of the spreadsheet xml file
        /// </summary>
        [TestMethod]
        public void TestGetVersion()
        {
            Spreadsheet s = new Spreadsheet("3.1");
            using (XmlWriter writer = XmlWriter.Create("file.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "3.1");
                writer.WriteEndElement();
            }
            Assert.AreEqual("3.1", s.GetSavedVersion("file.xml"));

        }

        /// <summary>
        /// the test version should be default
        /// </summary>
        [TestMethod]
        public void TestGetVersionDefault()
        {
            Spreadsheet s = new Spreadsheet();
            s.Save("file.xml");
            Assert.AreEqual("default", s.GetSavedVersion("file.xml"));

        }

        /// <summary>
        /// error should be thrown with invalid path
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestSaveXMLError()
        {
            Spreadsheet s = new Spreadsheet();
            s.Save("/so56me/nons3ense/directory8/file.xml");
        }

        /// <summary>
        /// error should be thrown with invalid path
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestGetVersionError()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetSavedVersion("/so56me/nons3ense/directory8/file.xml");
        }

        /// <summary>
        /// error should be thrown if wrong version
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestConstructorGetVersionWrongVersion()
        {
            using(XmlWriter writer = XmlWriter.Create("file.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "ajepnfoia");
                writer.WriteEndElement();
            }
            Spreadsheet s = new Spreadsheet("file.xml", s => true, s => s, "default");
        }

        /// <summary>
        /// after circular ref error, spreadsheet should remain unchanged
        /// </summary>
        [TestMethod]
        public void TestCircularRefDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "3.2");
            try
            {
                s.SetContentsOfCell("a1", "=a1+2");
            }
            catch (Exception)
            {
                Assert.AreEqual(3.2, (double)s.GetCellContents("a1"));
            }
        }

        /// <summary>
        /// after a circular ref error, the spreadsheet should remain unchanged
        /// </summary>
        [TestMethod]
        public void TestCircularRefFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "=a3+3");
            try
            {
                s.SetContentsOfCell("a1", "=a1+2");
            }
            catch (Exception)
            {
                Assert.AreEqual(new Formula("a3+3"), (Formula)s.GetCellContents("a1"));
            }
        }

        /// <summary>
        /// should return string
        /// </summary>
        [TestMethod]
        public void TestGetCellValueString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", s.GetCellValue("a1"));
        }

        /// <summary>
        /// getcell value should return the double value of the cell
        /// </summary>
        [TestMethod]
        public void TestGetCellValueDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "4.3");
            Assert.AreEqual(4.3, s.GetCellValue("a1"));
        }

        /// <summary>
        /// get cell value should return the evaluation of the formula
        /// </summary>
        [TestMethod]
        public void TestGetCellValueFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "4.3");
            s.SetContentsOfCell("a2", "=a1+10");
            Assert.AreEqual(14.3, s.GetCellValue("a2"));
        }

        /// <summary>
        /// changed prop should be false, then true after setcontentsofcell
        /// </summary>
        [TestMethod]
        public void TestChangedVar()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.IsFalse(s.Changed);
            s.SetContentsOfCell("a1", "butterfly");
            Assert.IsTrue(s.Changed);
        }

        /// <summary>
        /// changed should be false after save is called
        /// </summary>
        [TestMethod]
        public void TestChangedVarSaved()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "butterfly");
            s.Save("file.xml");
            Assert.IsFalse(s.Changed);
        }

        /// <summary>
        /// stress test
        /// </summary>
        [TestMethod]
        public void StressTest()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "1.000");
            for (int i = 2; i < 100000; i++)
            {
                s.SetContentsOfCell("a" + (i).ToString(), "=a" + (i - 1).ToString() + "+1");
            }
            Assert.AreEqual(99999.0, (double)s.GetCellValue("a99999"), 1e-9);
        }
    }
}
