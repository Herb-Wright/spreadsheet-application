// SpreadsheetForm class handles most of Controller and Views (combined with SpreadsheetPanel)
// Author: Herb Wright

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SS;
using System.Text.RegularExpressions;


namespace SpreadsheetGUI
{
    /// <summary>
    /// Spreadsheet form class
    /// </summary>
    public partial class SpreadsheetForm : Form
    {
        /// <summary>
        /// a spreadsheet object to contain the data
        /// </summary>
        Spreadsheet sheet;
        /// <summary>
        /// row and col of current selection
        /// </summary>
        int row, col;

        /// <summary>
        /// Constructor Initializes the form and stuff
        /// </summary>
        public SpreadsheetForm()
        {
            InitializeComponent();
            sheet = new Spreadsheet(a => Regex.IsMatch(a, "^[a-zA-Z][0-9]+$"), a => a.ToLower(), "default");
            constructorHelper();
        }

        /// <summary>
        /// Helper method for constructor, because the code is used in two methods (init values)
        /// </summary>
        private void constructorHelper()
        {
            row = 0;
            col = 0;
            textBoxCellName.Text = "a1";
            editBox.Text = sheet.GetCellValue("a1").ToString();
            spreadsheetPanel.SetSelection(0, 0);
        }

        /// <summary>
        /// Is called when a selection is changed, saves the old data, and then moves to the new cell
        /// </summary>
        /// <param name="sender"></param>
        private void spreadsheetPanel1_SelectionChanged(SS.SpreadsheetPanel sender)
        {
            //save previous
            string value = editBox.Text;

            foreach (string s in sheet.SetContentsOfCell(calcCell(col, row), value))
            {
                sender.SetValue(s[0] - 'a', int.Parse(s.Substring(1)) - 1, sheet.GetCellValue(s).ToString());
            }

            //display new cell text
            sender.GetSelection(out col, out row);
            editBox.Text = sheet.GetCellString(calcCell(col, row));
            textBoxCellName.Text = calcCell(col, row);
            textBoxValue.Text = sheet.GetCellValue(calcCell(col, row)).ToString();

            //change focus
            editBox.Focus();
            editBox.SelectAll();

        }

        /// <summary>
        /// overriden method that processes cmd keys.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys masked = keyData & ~Keys.Modifiers; // remove flags
            if (editBox.SelectionLength == editBox.Text.Length)
            {
                // do the arrow keys if arrow key
                switch (masked)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                        arrowKeyHelper(keyData);
                        return true;
                }
            }
            switch (keyData)
            {
                case Keys.Enter: // do the enter business
                    spreadsheetPanel.SetSelection(col, row + 1);
                    return true;
                case Keys.Tab: // toggle selected text in edit box
                    if(editBox.SelectionLength == editBox.Text.Length)
                    {
                        editBox.Select(editBox.Text.Length, 0);
                    }
                    else
                    {
                        editBox.Select(0, editBox.Text.Length);
                    }
                    return true;
                case Keys.Control | Keys.S: //ctrl+S
                    saveToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Control | Keys.O: //ctrl+O
                    openToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Control | Keys.N: //ctrl+N
                    newToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Control | Keys.E: //ctrl+E
                    closeToolStripMenuItem_Click(null, null);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        /// <summary>
        /// helper function that processes arrow keys
        /// </summary>
        /// <param name="key"></param>
        private void arrowKeyHelper(Keys key)
        {
            // remember what flags are there
            bool isCmd = key.HasFlag(Keys.Control);
            bool isShift = key.HasFlag(Keys.Shift);
            key = key & ~Keys.Modifiers; // remove flags
            // calc change in cell
            int dx = 1 * Convert.ToInt32(key == Keys.Right) - 1 * Convert.ToInt32(key == Keys.Left);
            int dy = 1 * Convert.ToInt32(key == Keys.Down) - 1 * Convert.ToInt32(key == Keys.Up);
            if (isCmd) // if we need to copy, copy
            {
                copyHelper(isShift, dx, dy);
            }
            spreadsheetPanel.SetSelection(col + dx, row + dy);
        }

        /// <summary>
        /// helper function for copying a cell to an adjacent cell
        /// </summary>
        /// <param name="isShift"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        private void copyHelper(bool isShift, int dx, int dy)
        {
            if (col + dx >= 0 && col + dx < 26 && row + dy >= 0 && row + dy < 100) // if new cell in bounds
            {
                string val = sheet.GetCellString(calcCell(col, row)); // get value of cell to copy from
                if (val.Length > 0 && !isShift && val[0] == '=') // if we gotta change the variables do it
                {
                    string newString = "";
                    foreach (string s in Regex.Split(val, "(=|\\+|-|/|\\*|\\)|\\()")) // iter through tokens
                    {
                        string temp = s.Trim();
                        if (Regex.IsMatch(temp, "^[a-zA-Z][0-9]+")) // if matches with variable, change the variable
                        {
                            temp = calcCell(temp[0] - 'a' + dx, int.Parse(temp.Substring(1)) - 1 + dy);
                            newString += temp; // build new string
                        }
                        else
                        {
                            newString += s; // build new string
                        }
                    }
                    val = newString; // reassign
                }
                sheet.SetContentsOfCell(calcCell(col + dx, row + dy), val);
                spreadsheetPanel.SetValue(col + dx, row + dy, sheet.GetCellValue(calcCell(col + dx, row + dy)).ToString());
            }
        }

        /// <summary>
        /// focus on editbox when form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadsheetForm_Shown(object sender, EventArgs e)
        {
            editBox.Focus();
        }

        /// <summary>
        /// handles the close menu item (closes form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sheet.Changed)
            {
                DialogResult dialogResult = MessageBox.Show("There are unsaved changes, are you sure you want to close?", "Unsaved Changes", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            Close();
        }

        /// <summary>
        /// handles the save menu item (saves form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                using (SaveFileDialog saveFileDialog = new SaveFileDialog()) //open dialog
                {
                    saveFileDialog.Filter = "spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the path of specified file
                        path = saveFileDialog.FileName;
                    }
                    else // if cancel pressed
                    { 
                        return;
                    }
                }
                sheet.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("there was an error saving the file: " + ex.Message); // if error, display
            }
        }

        /// <summary>
        /// handles the open menu item (opens from file)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                using (OpenFileDialog openFileDialog = new OpenFileDialog()) // open file dialog
                {
                    openFileDialog.Filter = "spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // get the path of specified file
                        path = openFileDialog.FileName;
                    }
                    else // if cancel pressed
                    {
                        return;
                    }
                }
                if (sheet.Changed) // if there are unsaved changes, confirm
                {
                    DialogResult dialogResult = MessageBox.Show("There are unsaved changes, are you sure you want to open?", "Unsaved Changes", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                //launch new form and close current one.
                // init values
                sheet = new Spreadsheet(path, a => Regex.IsMatch(a, "^[a-zA-Z][0-9]+$"), a => a.ToLower(), sheet.GetSavedVersion(path));
                constructorHelper();
                // update panel
                foreach (string s in sheet.GetNamesOfAllNonemptyCells())
                {
                    spreadsheetPanel.SetValue(((char)s[0] - 'a'), int.Parse(s.Substring(1)) - 1, sheet.GetCellValue(s).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("there was an error opening the file: " + ex.Message); // if error, notify
            }
        }

        /// <summary>
        /// handles the new menu item (opens blank spreadsheet)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SpreadsheetContext.getAppContext().RunForm(new SpreadsheetForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("there was an error opening the file: " + ex.Message);
            }
        }

        /// <summary>
        /// helper that returns string of cell name given col and row
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private string calcCell(int c, int r) => (char)('a' + c) + (r + 1).ToString();
    }
}
