using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;
using ImportExportClassLibrary.Properties;
using PrintingClassLibrary;
using SettingsPermissions;

namespace ImportExportClassLibrary
{
	// Token: 0x02000042 RID: 66
	public partial class ImportManager : Form
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00018AFF File Offset: 0x00017AFF
		public ImportManager(Form parentForm, Settings settings, ImportODBC _ImportODBC, string _StartDirectory)
		{
			this.InitializeComponent();
			this._importODBC = _ImportODBC;
			this.startDirectory = _StartDirectory;
			this._settings = settings;
			this._parentForm = parentForm;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000270 RID: 624 RVA: 0x0001A75C File Offset: 0x0001975C
		// (remove) Token: 0x06000271 RID: 625 RVA: 0x0001A794 File Offset: 0x00019794
		public event EventHandler FinishedInit;

		// Token: 0x06000272 RID: 626 RVA: 0x0001A7C9 File Offset: 0x000197C9
		private void MENU_cmlv_ignoreThisItem_Click(object sender, EventArgs e)
		{
			this.IgnoreSelectedItems(true, false);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001A7D4 File Offset: 0x000197D4
		private void cm_lv_Popup(object sender, EventArgs e)
		{
			bool flag = this.lv.SelectedItems.Count > 1;
			bool flag2 = this.lv.SelectedItems.Count > 0;
			if (flag2 && !flag)
			{
				this.lastListViewItemSingleSelected = this.lv.SelectedItems[0];
			}
			else
			{
				this.lastListViewItemSingleSelected = null;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ArrayList arrayList = new ArrayList();
			string text = "";
			if (flag2)
			{
				foreach (object obj in this.lv.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					ImportItem importItem = (ImportItem)listViewItem.Tag;
					if (importItem.imported)
					{
						num++;
					}
					else if (importItem._ImportProblems == null)
					{
						num3++;
					}
					else
					{
						num2++;
						foreach (ImportProblem importProblem in importItem._ImportProblems)
						{
							if (importProblem._problemSolutions != null)
							{
								if (text.Length > 0)
								{
									text += ", ";
								}
								text += importProblem._problemDescription;
								foreach (ProblemSolution problemSolution in importProblem._problemSolutions)
								{
									if (!arrayList.Contains(problemSolution))
									{
										arrayList.Add(problemSolution);
									}
								}
							}
						}
					}
				}
			}
			int num4 = num2 + num3;
			bool enabled = num3 > 0;
			bool flag3 = num2 > 0;
			this.MENU_cm_lv_importThisItem.Enabled = enabled;
			this.MENU_cm_lv_problem.Visible = flag3;
			this.MENU_cm_lv_problemSpacer.Visible = flag3;
			foreach (object obj2 in this.cm_lv.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj2;
				if (menuItem is MyMenuItem)
				{
					if (flag3)
					{
						MyMenuItem myMenuItem = (MyMenuItem)menuItem;
						ProblemSolution problemSolution2 = (ProblemSolution)myMenuItem.Tag;
						menuItem.Visible = arrayList.Contains(problemSolution2);
					}
					else
					{
						menuItem.Visible = false;
					}
				}
			}
			this.MENU_cm_lv_problem.Text = "Problem(s): " + text;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001AA58 File Offset: 0x00019A58
		public void Init()
		{
			ProblemType problemType = this._importODBC.ImportToMemory();
			string text = "Finished importing [" + this._importODBC.DataRowCount.ToString() + "] : ";
			switch (problemType)
			{
			case ProblemType.None:
				text += "No problems - ready to import into ClockWork.";
				break;
			case ProblemType.Warning:
				text += "At least one warning!";
				break;
			case ProblemType.Error:
				text += "At least one problem!";
				break;
			case ProblemType.WarningAndError:
				text += "At least one problem and at least one warning!";
				break;
			}
			this.toolStripStatusLabel1.Text = text;
			this._importODBC.FillListView(this.lv);
			this.ShowCurrentListInfo();
			EventHandler onClick = new EventHandler(this.mmi_Click);
			int num = 0;
			foreach (string str in ImportProblem.ProblemSolutionDescriptions)
			{
				MenuItem item = new MyMenuItem("Solve: " + str, onClick, (ProblemSolution)(num++));
				this.cm_lv.MenuItems.Add(item);
			}
			this.FireFinishedEvent();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0001AB75 File Offset: 0x00019B75
		private void FireFinishedEvent()
		{
			if (this.FinishedInit != null)
			{
				this.FinishedInit(this, new EventArgs());
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001AB90 File Offset: 0x00019B90
		private void ImportManager_Load(object sender, EventArgs e)
		{
			this.listViewColumnSortings = new bool[this.lv.Columns.Count];
			for (int i = 0; i < this.lv.Columns.Count; i++)
			{
				this.listViewColumnSortings[i] = false;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001ABDC File Offset: 0x00019BDC
		private void mmi_Click(object sender, EventArgs e)
		{
			if (this.lastListViewItemSingleSelected != null && this.lastListViewItemSingleSelected.Tag != null)
			{
				MyMenuItem myMenuItem = (MyMenuItem)sender;
				ProblemSolution ps = (ProblemSolution)myMenuItem.Tag;
				ImportItem ii = (ImportItem)this.lastListViewItemSingleSelected.Tag;
				this.FixProblem(ii, ps);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001AC2C File Offset: 0x00019C2C
		private void FixProblem(ImportItem ii, ProblemSolution ps)
		{
			HowProblemWasFixed howProblemWasFixed = this._importODBC.FixProblem(ii, ps);
			if (howProblemWasFixed == HowProblemWasFixed.ItemDiscarded)
			{
				this.lv.Items.Remove(this.lastListViewItemSingleSelected);
				this.lastListViewItemSingleSelected = null;
			}
			else if (howProblemWasFixed == HowProblemWasFixed.ProblemSolved)
			{
				this.lastListViewItemSingleSelected.ImageIndex = 0;
				foreach (object obj in this.lv.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					ImportItem importItem = (ImportItem)listViewItem.Tag;
					if (importItem._ImportProblems == null || importItem._ImportProblems.Length < 1)
					{
						listViewItem.ImageIndex = 0;
					}
				}
			}
			this.ShowCurrentListInfo();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0001ACF4 File Offset: 0x00019CF4
		private void ShowCurrentListInfo()
		{
			this.ShowExtraMessage(this.lv.Items.Count.ToString() + " item(s) in list.");
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001AD29 File Offset: 0x00019D29
		public void RefreshListView(object sender, EventArgs e)
		{
			base.Activate();
			this._importODBC.FillListView(this.lv);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0001AD42 File Offset: 0x00019D42
		private void Save(bool closeMe)
		{
			this._importODBC.Save();
			base.DialogResult = DialogResult.OK;
			if (closeMe)
			{
				base.Close();
				return;
			}
			this._importODBC.FillListView(this.lv);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001AD74 File Offset: 0x00019D74
		private void ImportManager_Closing(object sender, CancelEventArgs e)
		{
			if (base.DialogResult != DialogResult.OK)
			{
				int num = this._importODBC.NumChanges();
				if (num > 0)
				{
					DialogResult dialogResult = MessageBox.Show("Would you like to save your changes?", "Changes Will Be Lost!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						this.Save(false);
						return;
					}
					if (dialogResult == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0001ADC4 File Offset: 0x00019DC4
		private void PrintTable(bool printPreview)
		{
			PrintingDataTable printingDataTable = new PrintingDataTable();
			printingDataTable.PrintDataTable(this._importODBC.ImportTable, null, true, this.lv.Font, printPreview, this);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001ADF8 File Offset: 0x00019DF8
		public string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			string extension = Path.GetExtension(tempFileName);
			return tempFileName.Replace(extension, fnExtension);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001AE1C File Offset: 0x00019E1C
		public void ExportToDelimiteredText(DataTable T, bool askUserForDelimiters)
		{
			string defaultText = ",";
			string newLine = Environment.NewLine;
			if (askUserForDelimiters)
			{
				if (InputBox.GetUserInput(this, "Change Column Delimiter", "Please enter the new column delimiter:", defaultText) == null)
				{
					return;
				}
				if (InputBox.GetUserInput(this, "Change Row Delimiter", "Please enter the new row string indicator: ", newLine) == null)
				{
					return;
				}
			}
			this.ExportToDelimeteredText(T, ",", Environment.NewLine);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001AE78 File Offset: 0x00019E78
		private void ExportToDelimeteredText(DataTable T, string colDelimiter, string rowDelimiter)
		{
			DataTable importTable = this._importODBC.ImportTable;
			string tempFilename = this.GetTempFilename(".csv");
			StreamWriter streamWriter = new StreamWriter(tempFilename);
			int num = 0;
			for (int i = 0; i < importTable.Columns.Count; i++)
			{
				if (num++ > 0)
				{
					streamWriter.Write(colDelimiter);
				}
				streamWriter.Write(importTable.Columns[i].ColumnName);
			}
			streamWriter.Write(rowDelimiter);
			foreach (object obj in importTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				for (int j = 0; j < importTable.Columns.Count; j++)
				{
					if (j > 0)
					{
						streamWriter.Write(colDelimiter);
					}
					streamWriter.Write(dataRow[j].ToString());
				}
				streamWriter.Write(rowDelimiter);
			}
			streamWriter.Close();
			TemplatesClass.ShowDelimiteredTextFile(tempFilename);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001AF8C File Offset: 0x00019F8C
		private void label1_Click(object sender, EventArgs e)
		{
			bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			if (flag)
			{
				string userInput = InputBox.GetUserInput(this, "Enter SQL", "Enter SQL", "");
				if (userInput != null)
				{
					DataTable dataTable = this._importODBC.ExecuteQuery(userInput);
					DataTableView dataTableView = new DataTableView(dataTable);
					dataTableView.ShowDialog(this);
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001AFE4 File Offset: 0x00019FE4
		public void IncrementProgressBar(object sender, EventArgs e)
		{
			this.Text += ".";
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001AFFC File Offset: 0x00019FFC
		private void smartExplorerTaskPane1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001B000 File Offset: 0x0001A000
		private void IgnoreSelectedItems(bool onlySelected, bool onlyProblems)
		{
			ListViewItem[] array;
			if (onlySelected)
			{
				if (this.lv.SelectedItems.Count > 0)
				{
					array = new ListViewItem[this.lv.SelectedItems.Count];
					this.lv.SelectedItems.CopyTo(array, 0);
				}
				else
				{
					array = null;
				}
			}
			else if (this.lv.Items.Count > 0)
			{
				array = new ListViewItem[this.lv.Items.Count];
				this.lv.Items.CopyTo(array, 0);
			}
			else
			{
				array = null;
			}
			if (array != null)
			{
				foreach (ListViewItem listViewItem in array)
				{
					ImportItem importItem = (ImportItem)listViewItem.Tag;
					if (!onlyProblems || importItem._ImportProblems != null)
					{
						this._importODBC.IgnoreItem(importItem);
						this.lv.Items.Remove(listViewItem);
					}
				}
			}
			this.ShowCurrentListInfo();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001B0E6 File Offset: 0x0001A0E6
		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001B0E8 File Offset: 0x0001A0E8
		public void ShowExtraMessage(string msg)
		{
			this.lbl_msg.Visible = true;
			this.lbl_msg.Text = msg;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0001B104 File Offset: 0x0001A104
		private void lv_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			bool flag = !this.listViewColumnSortings[e.Column];
			this.listViewColumnSortings[e.Column] = flag;
			this.lv.ListViewItemSorter = new ListViewMultipleColCompare(new int[]
			{
				e.Column
			}, flag);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001B154 File Offset: 0x0001A154
		private void lv_DoubleClick(object sender, EventArgs e)
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.lv.SelectedItems[0];
				this.lastListViewItemSingleSelected = listViewItem;
				ImportItem importItem = (ImportItem)listViewItem.Tag;
				ImportProblem[] importProblems = importItem._ImportProblems;
				if (importProblems != null && importProblems.Length == 1)
				{
					ImportProblem importProblem = importProblems[0];
					ProblemSolution[] problemSolutions = importProblem._problemSolutions;
					ProblemSolution problemSolution = ProblemSolution.Unkown;
					for (int i = 0; i < problemSolutions.Length; i++)
					{
						if (problemSolutions[i] != ProblemSolution.Ignore && problemSolutions[i] != ProblemSolution.Discard)
						{
							if (problemSolution != ProblemSolution.Unkown)
							{
								problemSolution = ProblemSolution.Unkown;
								break;
							}
							problemSolution = problemSolutions[i];
						}
					}
					if (problemSolution != ProblemSolution.Unkown)
					{
						this.FixProblem(importItem, problemSolution);
					}
				}
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001B200 File Offset: 0x0001A200
		private void MENU_discard_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.lv.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.Tag != null)
				{
					ImportItem importItem = (ImportItem)listViewItem.Tag;
					this._importODBC.DiscardItem(importItem);
					this.lv.Items.Remove(listViewItem);
				}
			}
			this.ShowCurrentListInfo();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001B290 File Offset: 0x0001A290
		private void MENU_selectAllItems_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Selected = true;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001B2F0 File Offset: 0x0001A2F0
		private void dtv_Closing(object sender, CancelEventArgs e)
		{
			if (this.dg != null && this.dg.TopLevelControl is DataTableView)
			{
				DataTableView dataTableView = (DataTableView)this.dg.TopLevelControl;
				dataTableView.Closing -= this.dtv_Closing;
				dataTableView.dataGrid1.ContextMenu = null;
				this.dg = null;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001B350 File Offset: 0x0001A350
		private void MENU_DATAGRID_changeThisValue_Click(object sender, EventArgs e)
		{
			int columnNumber = this.dg.CurrentCell.ColumnNumber;
			string dataGridCellValue = this.GetDataGridCellValue();
			string userInput = InputBox.GetUserInput(this, "Change all values = '" + dataGridCellValue + "'", "Please enter the new value:", dataGridCellValue);
			if (userInput != null)
			{
				int num = 0;
				foreach (object obj in this.lv.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					string text = listViewItem.SubItems[columnNumber].Text.Trim().ToLower();
					if (text.CompareTo(dataGridCellValue) == 0)
					{
						listViewItem.SubItems[columnNumber].Text = userInput;
						ImportItem importItem = (ImportItem)listViewItem.Tag;
						DataRow dataRow = importItem._dataRow;
						dataRow[columnNumber] = userInput;
						num++;
						this.dg[this.dg.CurrentCell.RowNumber, columnNumber] = userInput;
					}
				}
				MessageBox.Show("Done.  Made " + num.ToString() + " change(s).");
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001B494 File Offset: 0x0001A494
		private void MENU_DATAGRID_removeThisItem_Click(object sender, EventArgs e)
		{
			int columnNumber = this.dg.CurrentCell.ColumnNumber;
			string dataGridCellValue = this.GetDataGridCellValue();
			DialogResult dialogResult = MessageBox.Show(string.Concat(new string[]
			{
				"Are you sure you want to remove all items with the value '",
				dataGridCellValue,
				"' in the column #",
				columnNumber.ToString(),
				"?"
			}), "Remove items with value='" + dataGridCellValue + "'", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.lv.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					string text = listViewItem.SubItems[columnNumber].Text.Trim().ToLower();
					if (text.CompareTo(dataGridCellValue) == 0)
					{
						arrayList.Add(listViewItem);
					}
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					ListViewItem listViewItem2 = (ListViewItem)arrayList[i];
					ImportItem ii = (ImportItem)listViewItem2.Tag;
					this._importODBC.IgnoreItem(ii);
					this.lv.Items.Remove(listViewItem2);
					listViewItem2.Tag = null;
				}
				this.dg[this.dg.CurrentCell.RowNumber, columnNumber] = "";
				this.dg.CurrentRowIndex = this.dg.CurrentCell.RowNumber;
				MessageBox.Show("Done.  Removed " + arrayList.Count.ToString() + " item(s).");
				arrayList.Clear();
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001B66C File Offset: 0x0001A66C
		private string GetDataGridCellValue()
		{
			if (this.dg != null)
			{
				return this.dg[this.dg.CurrentCell].ToString().Trim().ToLower();
			}
			return "";
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001B6A4 File Offset: 0x0001A6A4
		private void btn_uniqueColumnValues_Click(object sender, EventArgs e)
		{
			if (this.lv.Columns.Count < 1)
			{
				MessageBox.Show("No columns!");
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			ArrayList[] array = new ArrayList[this.lv.Columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ArrayList(this.lv.Items.Count);
			}
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				for (int j = 0; j < this.lv.Columns.Count; j++)
				{
					string text = listViewItem.SubItems[j].Text.Trim().ToLower();
					if (!array[j].Contains(text))
					{
						array[j].Add(text);
					}
				}
			}
			int num = 0;
			foreach (ArrayList arrayList in array)
			{
				if (arrayList.Count > num)
				{
					num = arrayList.Count;
				}
			}
			DataTable dataTable = new DataTable();
			for (int l = 0; l < this.lv.Columns.Count; l++)
			{
				dataTable.Columns.Add(this.lv.Columns[l].Text);
			}
			for (int m = 0; m < num; m++)
			{
				object[] array3 = new object[dataTable.Columns.Count];
				for (int n = 0; n < dataTable.Columns.Count; n++)
				{
					if (m < array[n].Count)
					{
						array3[n] = (string)array[n][m];
					}
					else
					{
						array3[n] = "";
					}
				}
				dataTable.Rows.Add(array3);
			}
			DataTableView dataTableView = new DataTableView(dataTable);
			dataTableView.dataGrid1.ReadOnly = true;
			dataTableView.dataGrid1.CaptionText = "This table lists all unique values for each column.  Select a cell, then right-click on the left blank column to access the popup menu.";
			dataTableView.Closing += this.dtv_Closing;
			dataTableView.dataGrid1.ContextMenu = this.cm_uniqueValuesDataGrid;
			this.dg = dataTableView.dataGrid1;
			DialogResult dialogResult = dataTableView.ShowDialog();
			this.ShowCurrentListInfo();
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001B92C File Offset: 0x0001A92C
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			if (this._importODBC.Save())
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			else
			{
				MessageBox.Show("Something seems to have gone wrong with the save.");
			}
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001B96B File Offset: 0x0001A96B
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0001B974 File Offset: 0x0001A974
		private void btn_fixAllProblems_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			bool flag = false;
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				ImportItem importItem = (ImportItem)listViewItem.Tag;
				if (importItem._ImportProblems != null)
				{
					foreach (ImportProblem importProblem in importItem._ImportProblems)
					{
						ProblemSolution problemSolution;
						if (importProblem._problemSolutions.Length > 1)
						{
							SolutionChooser solutionChooser = new SolutionChooser(importItem, importProblem, importProblem._problemSolutions);
							DialogResult dialogResult = solutionChooser.ShowDialog(this);
							if (dialogResult != DialogResult.OK)
							{
								flag = true;
								break;
							}
							problemSolution = solutionChooser.selectedSolution;
						}
						else
						{
							problemSolution = importProblem._problemSolutions[0];
						}
						if (problemSolution != ProblemSolution.Unkown)
						{
							HowProblemWasFixed howProblemWasFixed = this._importODBC.FixProblem(importItem, problemSolution);
							if (howProblemWasFixed == HowProblemWasFixed.ItemDiscarded)
							{
								break;
							}
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			this.Cursor = Cursors.Default;
			this._importODBC.FillListView(this.lv);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001BA98 File Offset: 0x0001AA98
		private void btn_fixSelectedProblem_Click(object sender, EventArgs e)
		{
			Point point = Cursor.Position;
			point = ((Control)sender).PointToClient(point);
			this.cm_lv.Show((Control)sender, point);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001BACA File Offset: 0x0001AACA
		private void btn_ignoreSelectedProblem_Click(object sender, EventArgs e)
		{
			this.IgnoreSelectedItems(true, false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001BAD4 File Offset: 0x0001AAD4
		private void btn_ignoreAllProblems_Click(object sender, EventArgs e)
		{
			this.IgnoreSelectedItems(false, true);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001BADE File Offset: 0x0001AADE
		private void btn_printList_Click(object sender, EventArgs e)
		{
			this.PrintTable(false);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0001BAE7 File Offset: 0x0001AAE7
		private void btn_printPreviewList_Click(object sender, EventArgs e)
		{
			this.PrintTable(true);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001BAF0 File Offset: 0x0001AAF0
		private void btn_exportList_Click(object sender, EventArgs e)
		{
			bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			ExportTypeChooser exportTypeChooser = new ExportTypeChooser();
			DialogResult dialogResult = exportTypeChooser.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DataTable importTable = this._importODBC.ImportTable;
				new RichTextBox();
				string tempFilename;
				switch (exportTypeChooser.listBox1.SelectedIndex)
				{
				case 0:
					if (this._importODBC.ImportTable == null || this._importODBC.ImportTable.Rows.Count <= 0)
					{
						return;
					}
					tempFilename = this.GetTempFilename(".xls");
					TemplatesClass.ExportToExcel(this._importODBC.ImportTable, tempFilename, this.startDirectory, flag);
					if (!File.Exists(tempFilename))
					{
						return;
					}
					try
					{
						TemplatesClass.OpenExcel(tempFilename);
						return;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						Process.Start(tempFilename, "");
						return;
					}
					break;
				case 1:
					break;
				case 2:
					goto IL_154;
				case 3:
				{
					tempFilename = this.GetTempFilename(".txt");
					StreamWriter streamWriter = new StreamWriter(tempFilename);
					int[] array = new int[importTable.Columns.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = importTable.Columns[i].ColumnName.Length + 1;
					}
					for (int j = 0; j < importTable.Rows.Count; j++)
					{
						DataRow dataRow = importTable.Rows[j];
						for (int k = 0; k < importTable.Columns.Count; k++)
						{
							int length = dataRow[k].ToString().Length;
							if (length > array[k])
							{
								array[k] = length + 1;
							}
						}
					}
					string text = "";
					for (int l = 0; l < importTable.Columns.Count; l++)
					{
						string columnName = importTable.Columns[l].ColumnName;
						int num = array[l] - columnName.Length;
						string str;
						if (num > 0)
						{
							str = new string(' ', num);
						}
						else
						{
							str = "";
							array[l] = columnName.Length + 1;
						}
						text = text + columnName + str;
					}
					streamWriter.WriteLine(text);
					foreach (object obj in importTable.Rows)
					{
						DataRow dataRow2 = (DataRow)obj;
						text = "";
						for (int m = 0; m < importTable.Columns.Count; m++)
						{
							string text2 = dataRow2[m].ToString().Trim();
							int num2 = array[m] - text2.Length;
							string str2;
							if (num2 > 0)
							{
								str2 = new string(' ', num2);
							}
							else
							{
								str2 = "";
								if (num2 < 0)
								{
									text2 = text2.Substring(0, array[m]);
								}
							}
							text = text + text2 + str2;
						}
						streamWriter.WriteLine(text);
					}
					streamWriter.Close();
					if (File.Exists(tempFilename))
					{
						Process.Start(tempFilename, "");
						return;
					}
					return;
				}
				default:
					return;
				}
				if (importTable == null || importTable.Rows.Count <= 0)
				{
					return;
				}
				tempFilename = this.GetTempFilename(".mdb");
				TemplatesClass.ExportToAccess("table1", importTable, tempFilename, this.startDirectory, flag);
				if (!File.Exists(tempFilename))
				{
					return;
				}
				try
				{
					Process.Start(tempFilename, "");
					return;
				}
				catch (Exception ex2)
				{
					MessageBox.Show(ex2.Message);
					return;
				}
				IL_154:
				this.ExportToDelimiteredText(importTable, flag);
				return;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001BEB0 File Offset: 0x0001AEB0
		private void btn_emailSelecteditems_Click(object sender, EventArgs e)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.lv.SelectedItems)
			{
				ListViewItem value = (ListViewItem)obj;
				arrayList.Add(value);
			}
			if (arrayList.Count > 0)
			{
				Email.EmailItemsOneEmail(base.ParentForm, arrayList, this.startDirectory, this._settings);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001BF38 File Offset: 0x0001AF38
		private void btn_exportToTemplate_Click(object sender, EventArgs e)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.lv.SelectedItems)
			{
				ListViewItem value = (ListViewItem)obj;
				arrayList.Add(value);
			}
			if (arrayList.Count > 0)
			{
				TemplatesClass.ExportItemsOneTemplate(this, arrayList, this.startDirectory, this._settings);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0001BFBC File Offset: 0x0001AFBC
		private void toolStripStatusLabel1_Click(object sender, EventArgs e)
		{
			bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			if (flag)
			{
				DataTable mainDataTable = this._importODBC.GetMainDataTable();
				if (mainDataTable != null)
				{
					DataTableView dataTableView = new DataTableView(mainDataTable);
					dataTableView.ShowDialog(this);
					return;
				}
				MessageBox.Show("NULL");
			}
		}

		// Token: 0x04000167 RID: 359
		private ImportODBC _importODBC;

		// Token: 0x04000168 RID: 360
		private string startDirectory;

		// Token: 0x04000169 RID: 361
		private Settings _settings;

		// Token: 0x0400016A RID: 362
		private Form _parentForm;

		// Token: 0x0400016C RID: 364
		private ListViewItem lastListViewItemSingleSelected;

		// Token: 0x0400016D RID: 365
		private bool[] listViewColumnSortings;

		// Token: 0x0400016E RID: 366
		private DataGrid dg;
	}
}
