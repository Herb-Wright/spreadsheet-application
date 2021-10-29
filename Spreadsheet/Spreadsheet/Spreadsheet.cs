using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

// comment for branch
namespace SS
{
    /// <summary>
    /// Spreadsheet class that inherits from AbstractSpreadsheet class.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// graph of the dependencies of the cells
        /// </summary>
        private DependencyGraph graph;
        /// <summary>
        /// a dictionary that maps a string to a Cell object
        /// </summary>
        private Dictionary<string, Cell> cells;

        /// <summary>
        /// constructor initializes the dependency graph and the cell dict
        /// </summary>
        public Spreadsheet() : this("default")
        {
        }

        /// <summary>
        /// constructor that just takes version
        /// </summary>
        /// <param name="version"></param>
        public Spreadsheet(string version) : this(s => Regex.IsMatch(s, "^[a-zA-Z]+[0-9]+$"), t => t, version)
        {
        }

        /// <summary>
        /// constructor that takes three arguments
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="normalizer"></param>
        /// <param name="version"></param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalizer, string version) : base(isValid, normalizer, version)
        {
            graph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
            Changed = false;
        }

        /// <summary>
        /// constructor that takes four arguments
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isValid"></param>
        /// <param name="normalizer"></param>
        /// <param name="version"></param>
        public Spreadsheet(string path, Func<string, bool> isValid, Func<string, string> normalizer, string version) : base(isValid, normalizer, version)
        {
            graph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
            Version = version;
            try
            {
                using (XmlReader reader = XmlReader.Create(path))
                {
                    reader.ReadToFollowing("spreadsheet");
                    if (reader.GetAttribute("version") != Version) { throw new SpreadsheetReadWriteException("wrong version"); }
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "cell")
                        {
                            reader.ReadToFollowing("name");
                            string n = reader.ReadElementContentAsString();
                            reader.ReadToFollowing("contents");
                            SetContentsOfCell(n, reader.ReadElementContentAsString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
            Changed = false;
        }

        public override bool Changed { get; protected set; }

        /// <summary>
        /// returns the cell contents of the cell with the given name
        /// </summary>
        /// <param name="name">the name of the cell</param>
        /// <returns>the value of the cell</returns>
        public override object GetCellContents(string name)
        {
            name = CheckName(name);
            return cells.ContainsKey(name) ? cells[name].Content : "";
        }

        public string GetCellString(string name)
        {
            name = CheckName(name);
            return cells.ContainsKey(name) ? cells[name].ToString() : "";
        }

        public override object GetCellValue(string name)
        {
            name = CheckName(name);
            return cells.ContainsKey(name) ? cells[name].Value : "";
        }

        /// <summary>
        /// returns a IEnumerable of the names of all cells that have content.
        /// </summary>
        /// <returns>IEnumerable of strings.</returns>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells() => cells.Keys;

        public override string GetSavedVersion(string filename)
        {
            string version;
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    reader.ReadToFollowing("spreadsheet");
                    version = reader.GetAttribute("version");
                }
                return version;
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        public override void Save(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", Version);
                    foreach (KeyValuePair<string, Cell> pair in cells)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", pair.Key);
                        writer.WriteElementString("contents", pair.Value.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("error while writing file");
            }
            Changed = false;
        }

        /// <summary>
        /// sets the cell to a number cell with the given value
        /// </summary>
        /// <param name="name">the name of the cell</param>
        /// <param name="number">the number to be in the cell</param>
        /// <returns>a list of dependees of the new cell joined with the new cell</returns>
        protected override IList<string> SetCellContents(string name, double number)
        {
            // add cell
            cells[name] = new Cell(number);
            graph.ReplaceDependees(name, new List<string>());
            Changed = true;
            return Recalculate(new List<string>(GetCellsToRecalculate(name)));
        }

        /// <summary>
        /// sets the cell to a text cell with the given value
        /// </summary>
        /// <param name="name">the name of the cell</param>
        /// <param name="text">the text to be in the cell</param>
        /// <returns>a list of dependees of the new cell joined with the new cell</returns>
        protected override IList<string> SetCellContents(string name, string text)
        {
            // add/remove cell
            if (text == "") // maintain invariant in dictionary of cells
            {
                cells.Remove(name);
            }
            else
            {
                cells[name] = new Cell(text);
            }
            graph.ReplaceDependees(name, new List<string>());
            Changed = true;
            return Recalculate(new List<string>(GetCellsToRecalculate(name)));
        }

        /// <summary>
        /// sets the cell to a formula cell with the given value
        /// </summary>
        /// <param name="name">the name of the cell</param>
        /// <param name="formula">the formula to be in the cell</param>
        /// <returns>a list of dependees of the new cell joined with the new cell</returns>
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            // add cell
            object oldVal = GetCellContents(name);
            cells[name] = new Cell(formula);
            graph.ReplaceDependees(name, formula.GetVariables());
            // test for circular dependency, if there, reverse adding the cell
            try
            {
                List<string> cellsToCalculate = new List<string>(GetCellsToRecalculate(name));
                Changed = true;
                return Recalculate(cellsToCalculate);
            }
            catch (CircularException)
            {
                if (oldVal is string)
                {
                    SetCellContents(name, (string)oldVal);
                }
                else if (oldVal is double)
                {
                    SetCellContents(name, (double)oldVal);
                }
                else if (oldVal is Formula)
                {
                    SetCellContents(name, (Formula)oldVal);
                }
                throw new CircularException();
            }
        }

        public override IList<string> SetContentsOfCell(string name, string content)
        {
            name = CheckName(name);
            if (content is null) { throw new ArgumentNullException(); }
            double number;
            if (double.TryParse(content, out number))
            {
                return SetCellContents(name, number);
            }
            else if (content.Length > 0 && content[0] == '=')
            {
                try
                {
                    return SetCellContents(name, new Formula(content.Substring(1), Normalize, IsValid));
                }
                catch (Exception)
                {
                    return new List<string>();
                }
            }
            else
            {
                return SetCellContents(name, content);
            }
        }

        private IList<string> Recalculate(IList<string> cellsToRecalculate)
        {
            foreach (string cell in cellsToRecalculate)
            {
                if (cells.ContainsKey(cell))
                {
                    cells[cell].Calculate(s => (double)GetCellValue(s));
                }
            }
            return cellsToRecalculate;
        }

        /// <summary>
        /// returns the dependents of a given cell
        /// </summary>
        /// <param name="name">the cell</param>
        /// <returns>the dependents</returns>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            return graph.GetDependents(name);
        }

        /// <summary>
        /// Helper method that checks for valid name (throws if invalid)
        /// </summary>
        /// <param name="name">the name</param>
        private string CheckName(string name)
        {
            if (name is null || !Regex.IsMatch(name, "^[a-zA-Z_][a-zA-Z0-9_]*$") || !IsValid(name))
            { throw new InvalidNameException(); }
            return Normalize(name);
        }

        /// <summary>
        /// Class for a cell.
        /// </summary>
        private class Cell
        {
            /// <summary>
            /// the value of the cell
            /// </summary>
            internal object Value { get; private set; }
            /// <summary>
            /// the content of the cell
            /// </summary>
            internal object Content { get; private set; }

            /// <summary>
            /// the constructor for a text cell
            /// </summary>
            /// <param name="_text">the text in the cell</param>
            public Cell(object content)
            {
                Content = content;
                if (content is string || content is double)
                {
                    Value = content;
                }
            }


            /// <summary>
            /// Calculate cell if it is formula
            /// </summary>
            /// <param name="f">a lookup function</param>
            internal void Calculate(Func<string, double> f)
            {
                if (Content is Formula)
                {
                    Value = ((Formula)Content).Evaluate(f);
                }
            }


            /// <summary>
            /// returns string of Cell
            /// </summary>
            /// <returns>content string</returns>
            public override string ToString() => (Content is Formula ? "=" : "") + Content.ToString();
        }
    }
}
