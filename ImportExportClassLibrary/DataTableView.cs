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

namespace ImportExportClassLibrary
{
	// Token: 0x0200003D RID: 61
	public partial class DataTableView : Form
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00016174 File Offset: 0x00015174
		public DataTableView(DataTable dataTable)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = dataTable;
			this.message = "";
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00016199 File Offset: 0x00015199
		public DataTableView(DataTable dataTable, string Message)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = dataTable;
			this.message = Message;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000161BA File Offset: 0x000151BA
		public DataTableView(DataView dataView, string Message)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = dataView;
			this.message = Message;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000161DB File Offset: 0x000151DB
		public DataTableView(DataSet ds, string title)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = ds;
			this.message = title;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00016F58 File Offset: 0x00015F58
		public static DialogResult ShowDataTableView(DataTable t, IWin32Window owner)
		{
			DataTableView dataTableView = new DataTableView(t);
			return dataTableView.ShowDialog(owner);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00016F74 File Offset: 0x00015F74
		public static DialogResult ShowDataTableView(DataSet ds, IWin32Window owner, string title)
		{
			DataTableView dataTableView = new DataTableView(ds, title);
			return dataTableView.ShowDialog(owner);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00016F90 File Offset: 0x00015F90
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00016F98 File Offset: 0x00015F98
		private void btn_fakeOK_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00016FA0 File Offset: 0x00015FA0
		private void OK()
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00016FB0 File Offset: 0x00015FB0
		private void DataTableView_Load(object sender, EventArgs e)
		{
			this.label1.ForeColor = this.panelEx1.Style.ForeColor.Color;
			if (this.message.Length > 0)
			{
				Graphics graphics = this.label1.CreateGraphics();
				SizeF sizeF = graphics.MeasureString(this.message, this.label1.Font, this.label1.Width);
				this.label1.Height = Convert.ToInt32(sizeF.Height);
				this.label1.Text = this.message;
			}
			else
			{
				this.label1.Visible = false;
			}
			if (this.dataGrid1.CaptionText.Length < 1)
			{
				this.dataGrid1.CaptionVisible = false;
			}
			this.FixColumnWidths();
			base.Activate();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0001707C File Offset: 0x0001607C
		private void FixColumnWidths()
		{
			DataView dataView;
			if (this.dataGrid1.DataSource is DataTable)
			{
				dataView = new DataView((DataTable)this.dataGrid1.DataSource);
			}
			else
			{
				if (!(this.dataGrid1.DataSource is DataView))
				{
					return;
				}
				dataView = (DataView)this.dataGrid1.DataSource;
			}
			int num = -1;
			DataTable table = dataView.Table;
			if (num < table.Rows.Count)
			{
				num = table.Rows.Count;
			}
			if (num < 0)
			{
				num = table.Rows.Count;
			}
			this.dataGrid1.TableStyles.Clear();
			DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();
			dataGridTableStyle.MappingName = table.TableName;
			if (table.Columns.Count > 0 && table.Rows.Count > 0)
			{
				int[] array = new int[table.Columns.Count];
				Graphics graphics = this.dataGrid1.CreateGraphics();
				for (int i = 0; i < table.Columns.Count; i++)
				{
					array[i] = Convert.ToInt32(graphics.MeasureString(table.Columns[i].ColumnName, this.dataGrid1.HeaderFont).Width);
					DataGridTextBoxColumn dataGridTextBoxColumn = new DataGridTextBoxColumn();
					dataGridTextBoxColumn.TextBox.Enabled = true;
					dataGridTextBoxColumn.HeaderText = table.Columns[i].ColumnName;
					dataGridTextBoxColumn.MappingName = table.Columns[i].ColumnName;
					for (int j = 0; j < table.Rows.Count; j++)
					{
						DataRow dataRow = table.Rows[j];
						int num2 = Convert.ToInt32(graphics.MeasureString(dataRow[i].ToString().Trim(), this.dataGrid1.Font).Width);
						if (num2 > array[i] && num2 < 300)
						{
							array[i] = num2;
						}
					}
					dataGridTextBoxColumn.Width = array[i];
					dataGridTableStyle.GridColumnStyles.Add(dataGridTextBoxColumn);
				}
				this.dataGrid1.TableStyles.Add(dataGridTableStyle);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000172B0 File Offset: 0x000162B0
		public static void AutoSizeColumns(DataGrid dg, int maxNumRowsToCheck)
		{
			if (dg == null || dg.DataSource == null)
			{
				return;
			}
			DataTable dataTable;
			if (dg.DataSource is DataTable)
			{
				dataTable = (DataTable)dg.DataSource;
			}
			else
			{
				if (!(dg.DataSource is DataView))
				{
					return;
				}
				dataTable = ((DataView)dg.DataSource).Table;
			}
			if (dataTable.Columns.Count < 1)
			{
				return;
			}
			int[] array = new int[dataTable.Columns.Count];
			DataGridTextBoxColumn[] array2 = new DataGridTextBoxColumn[dataTable.Columns.Count];
			Graphics graphics = dg.CreateGraphics();
			Font font = dg.Font;
			dg.TableStyles.Clear();
			DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();
			dataGridTableStyle.MappingName = dataTable.TableName;
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new DataGridTextBoxColumn
				{
					TextBox = 
					{
						Enabled = true
					},
					HeaderText = dataTable.Columns[i].ColumnName,
					MappingName = dataTable.Columns[i].ColumnName
				};
				int num = Convert.ToInt32(graphics.MeasureString(dataTable.Columns[i].ColumnName, font).Width);
				array[i] = num + 40;
			}
			Type type = Type.GetType("System.DateTime");
			int num2 = 0;
			while (num2 < dataTable.Rows.Count && (maxNumRowsToCheck < 0 || num2 < maxNumRowsToCheck))
			{
				DataRow dataRow = dataTable.Rows[num2];
				for (int j = 0; j < array.Length; j++)
				{
					string text;
					if (dataTable.Columns[j].DataType == type)
					{
						text = ((DateTime)dataRow[j]).ToShortDateString();
					}
					else
					{
						text = dataRow[j].ToString();
					}
					int num3 = Convert.ToInt32(graphics.MeasureString(text, font).Width);
					if (num3 > array[j])
					{
						array[j] = num3;
					}
				}
				num2++;
			}
			for (int k = 0; k < array.Length; k++)
			{
				array2[k].Width = array[k];
				dataGridTableStyle.GridColumnStyles.Add(array2[k]);
			}
			dg.TableStyles.Add(dataGridTableStyle);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000174FC File Offset: 0x000164FC
		public string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			string extension = Path.GetExtension(tempFileName);
			return tempFileName.Replace(extension, fnExtension);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00017520 File Offset: 0x00016520
		private DataView GetCurrentDataView()
		{
			if (this.dataGrid1.DataSource == null)
			{
				return null;
			}
			if (this.dataGrid1.DataSource is DataTable)
			{
				return ((DataTable)this.dataGrid1.DataSource).DefaultView;
			}
			if (this.dataGrid1.DataSource is DataView)
			{
				return (DataView)this.dataGrid1.DataSource;
			}
			return null;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00017588 File Offset: 0x00016588
		private void exportToformattedTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataView currentDataView = this.GetCurrentDataView();
			if (currentDataView != null)
			{
				string tempFilename = this.GetTempFilename(".txt");
				TemplatesClass.ExportToFormattedText(currentDataView, tempFilename, false);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000175B4 File Offset: 0x000165B4
		private void btn_exportToExcel_Click(object sender, EventArgs e)
		{
			DataView currentDataView = this.GetCurrentDataView();
			if (currentDataView != null)
			{
				string tempFilename = this.GetTempFilename(".xls");
				TemplatesClass.ExportToExcel(currentDataView, tempFilename, "", false);
				if (File.Exists(tempFilename))
				{
					TemplatesClass.OpenExcel(tempFilename);
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000175F4 File Offset: 0x000165F4
		private void btn_exportToTabDelimiteredText_Click(object sender, EventArgs e)
		{
			this.ExportDelimitered('\t'.ToString());
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00017611 File Offset: 0x00016611
		private void btn_exportToDelimiteredText_Click(object sender, EventArgs e)
		{
			this.ExportDelimitered(",");
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00017620 File Offset: 0x00016620
		private void btn_exportToXml_Click(object sender, EventArgs e)
		{
			DataView currentDataView = this.GetCurrentDataView();
			if (currentDataView != null)
			{
				DataSet dataSet = new DataSet();
				DataTable dataTable = currentDataView.Table.Clone();
				foreach (object obj in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					dataTable.LoadDataRow(row.ItemArray, true);
				}
				dataSet.Tables.Add(dataTable);
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "All Files (*.*)|*.*|XML (.xml)|*.xml";
				saveFileDialog.FilterIndex = 2;
				DialogResult dialogResult = saveFileDialog.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					try
					{
						dataSet.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00017710 File Offset: 0x00016710
		private void btn_importFromXml_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "All Files (*.*)|*.*|XML (.xml)|*.xml";
			openFileDialog.FilterIndex = 2;
			DialogResult dialogResult = openFileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				try
				{
					DataSet dataSet = new DataSet();
					dataSet.ReadXml(openFileDialog.FileName, XmlReadMode.ReadSchema);
					if (dataSet.Tables.Count > 0)
					{
						this.dataGrid1.DataSource = dataSet.Tables[0];
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0001779C File Offset: 0x0001679C
		private void btn_properties_Click(object sender, EventArgs e)
		{
			DataTableViewProperties dataTableViewProperties = new DataTableViewProperties(this.dataGrid1);
			dataTableViewProperties.ShowDialog(this);
			dataTableViewProperties.Dispose();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000177C8 File Offset: 0x000167C8
		private void btn_print_Click(object sender, EventArgs e)
		{
			bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			if (this.dataGrid1.DataSource == null)
			{
				return;
			}
			DataTable dataTable;
			if (this.dataGrid1.DataSource is DataTable)
			{
				dataTable = (DataTable)this.dataGrid1.DataSource;
			}
			else
			{
				if (!(this.dataGrid1.DataSource is DataView))
				{
					return;
				}
				DataView dataView = (DataView)this.dataGrid1.DataSource;
				dataTable = dataView.Table;
			}
			PrintingDataTable printingDataTable = new PrintingDataTable();
			printingDataTable.PrintDataTable(dataTable, null, false, this.Font, flag, this);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001785F File Offset: 0x0001685F
		private void btn_ok_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00017867 File Offset: 0x00016867
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00017870 File Offset: 0x00016870
		private void ExportDelimitered(string delimiter)
		{
			DataView currentDataView = this.GetCurrentDataView();
			if (currentDataView != null)
			{
				string tempFilename = this.GetTempFilename(".txt");
				TemplatesClass.ExportToDelimeteredText(currentDataView, tempFilename, "", false, delimiter, Environment.NewLine);
				if (File.Exists(tempFilename))
				{
					try
					{
						Process.Start(tempFilename);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
			}
		}

		// Token: 0x04000121 RID: 289
		public string message;
	}
}
