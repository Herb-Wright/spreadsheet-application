
namespace SpreadsheetGUI
{
    partial class SpreadsheetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveSelectionArrowKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveCursorToEndTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionCtrlArrowKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBox = new System.Windows.Forms.TextBox();
            this.textBoxCellName = new System.Windows.Forms.TextBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.spreadsheetPanel = new SS.SpreadsheetPanel();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.BackColor = System.Drawing.Color.Silver;
            this.topMenu.GripMargin = new System.Windows.Forms.Padding(0);
            this.topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.topMenu.Size = new System.Drawing.Size(1118, 28);
            this.topMenu.TabIndex = 1;
            this.topMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.newToolStripMenuItem.Text = "New (Ctrl+N)";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.saveToolStripMenuItem.Text = "Save (Ctrl+S)";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.openToolStripMenuItem.Text = "Open (Ctrl+O)";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.closeToolStripMenuItem.Text = "Close (Ctrl+E)";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcutsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveSelectionArrowKeysToolStripMenuItem,
            this.toolStripMenuItem1,
            this.moveCursorToEndTabToolStripMenuItem,
            this.copySelectionCtrlArrowKeysToolStripMenuItem,
            this.copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem});
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.shortcutsToolStripMenuItem.Text = "Shortcuts";
            // 
            // moveSelectionArrowKeysToolStripMenuItem
            // 
            this.moveSelectionArrowKeysToolStripMenuItem.Name = "moveSelectionArrowKeysToolStripMenuItem";
            this.moveSelectionArrowKeysToolStripMenuItem.Size = new System.Drawing.Size(541, 26);
            this.moveSelectionArrowKeysToolStripMenuItem.Text = "Move Selection (Arrow Keys)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(541, 26);
            this.toolStripMenuItem1.Text = "Move Selection Down (Enter)";
            // 
            // moveCursorToEndTabToolStripMenuItem
            // 
            this.moveCursorToEndTabToolStripMenuItem.Name = "moveCursorToEndTabToolStripMenuItem";
            this.moveCursorToEndTabToolStripMenuItem.Size = new System.Drawing.Size(541, 26);
            this.moveCursorToEndTabToolStripMenuItem.Text = "Toggle Cursor to End/Select All (Tab)";
            // 
            // copySelectionCtrlArrowKeysToolStripMenuItem
            // 
            this.copySelectionCtrlArrowKeysToolStripMenuItem.Name = "copySelectionCtrlArrowKeysToolStripMenuItem";
            this.copySelectionCtrlArrowKeysToolStripMenuItem.Size = new System.Drawing.Size(541, 26);
            this.copySelectionCtrlArrowKeysToolStripMenuItem.Text = "Copy Selection (Ctrl+Arrows)";
            // 
            // copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem
            // 
            this.copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem.Name = "copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem";
            this.copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem.Size = new System.Drawing.Size(541, 26);
            this.copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem.Text = "Copy Selection No Change (Ctrl+Shift+Arrows)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(541, 26);
            this.toolStripMenuItem2.Text = "Save (Ctrl+S)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(541, 26);
            this.toolStripMenuItem3.Text = "Open (Ctrl+O)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(541, 26);
            this.toolStripMenuItem4.Text = "New (Ctrl+N)";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(541, 26);
            this.toolStripMenuItem5.Text = "Close (Ctrl+E)";
            // 
            // someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem
            // 
            this.someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem.Name = "someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem";
            this.someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem.Size = new System.Drawing.Size(541, 26);
            this.someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem.Text = "** Some commands require the whole edit box string to be selected";
            // 
            // editBox
            // 
            this.editBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editBox.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBox.Location = new System.Drawing.Point(211, 26);
            this.editBox.Margin = new System.Windows.Forms.Padding(0);
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(907, 27);
            this.editBox.TabIndex = 2;
            // 
            // textBoxCellName
            // 
            this.textBoxCellName.BackColor = System.Drawing.Color.Brown;
            this.textBoxCellName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCellName.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCellName.Location = new System.Drawing.Point(0, 26);
            this.textBoxCellName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxCellName.Name = "textBoxCellName";
            this.textBoxCellName.ReadOnly = true;
            this.textBoxCellName.Size = new System.Drawing.Size(100, 27);
            this.textBoxCellName.TabIndex = 3;
            // 
            // textBoxValue
            // 
            this.textBoxValue.BackColor = System.Drawing.Color.LightPink;
            this.textBoxValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxValue.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxValue.Location = new System.Drawing.Point(100, 26);
            this.textBoxValue.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            this.textBoxValue.Size = new System.Drawing.Size(111, 27);
            this.textBoxValue.TabIndex = 4;
            // 
            // spreadsheetPanel
            // 
            this.spreadsheetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spreadsheetPanel.BackColor = System.Drawing.Color.LightPink;
            this.spreadsheetPanel.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spreadsheetPanel.ForeColor = System.Drawing.Color.Black;
            this.spreadsheetPanel.Location = new System.Drawing.Point(0, 53);
            this.spreadsheetPanel.Margin = new System.Windows.Forms.Padding(0);
            this.spreadsheetPanel.Name = "spreadsheetPanel";
            this.spreadsheetPanel.Size = new System.Drawing.Size(1118, 396);
            this.spreadsheetPanel.TabIndex = 0;
            this.spreadsheetPanel.SelectionChanged += new SS.SelectionChangedHandler(this.spreadsheetPanel1_SelectionChanged);
            // 
            // SpreadsheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1118, 452);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.textBoxCellName);
            this.Controls.Add(this.editBox);
            this.Controls.Add(this.spreadsheetPanel);
            this.Controls.Add(this.topMenu);
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "SpreadsheetForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.SpreadsheetForm_Shown);
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel;
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxCellName;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveSelectionArrowKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveCursorToEndTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copySelectionCtrlArrowKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectionNoChangeCtrlShiftArrowKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem someCommandsRequireTheWholeEditBoxStringToBeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
    }
}

