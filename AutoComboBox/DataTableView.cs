using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using UnivOleDb;

namespace AutoComboBox
{
	// Token: 0x020000BA RID: 186
	public partial class DataTableView : Form
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00038994 File Offset: 0x00037994
		public DataTableView(DataTable dataTable)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = dataTable;
			this.message = "";
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000389CD File Offset: 0x000379CD
		public DataTableView(string Message)
		{
			this.InitializeComponent();
			this.message = Message;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000389F5 File Offset: 0x000379F5
		public DataTableView(DataTable dataTable, string Message)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = dataTable;
			this.message = Message;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00038A2A File Offset: 0x00037A2A
		public DataTableView(DataView dataView, string Message)
		{
			this.InitializeComponent();
			this.dataGrid1.DataSource = dataView;
			this.message = Message;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00039738 File Offset: 0x00038738
		public Label Label1
		{
			get
			{
				return this.label1;
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00039750 File Offset: 0x00038750
		public void SetupForSpecificFirstColIsTableName_SecondColIsColName_ThirdColIsVal(UnivDataAdapter _da)
		{
			this.dataGrid1.ContextMenu = this.contextMenu1;
			this.da = _da;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0003976C File Offset: 0x0003876C
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00039776 File Offset: 0x00038776
		private void btn_fakeOK_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00039780 File Offset: 0x00038780
		private void OK()
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00039794 File Offset: 0x00038794
		private void DataTableView_Load(object sender, EventArgs e)
		{
			if (this.message.Length > 0)
			{
				Graphics graphics = this.label1.CreateGraphics();
				SizeF sizeF = graphics.MeasureString(this.message, this.label1.Font, this.label1.Width);
				this.label1.Height = Convert.ToInt32(sizeF.Height) + this.messagePaddingTimesTwo;
				this.label1.Text = this.message;
				this.label1.Visible = true;
			}
			else
			{
				this.label1.Visible = false;
			}
			try
			{
				this.dataGrid1.AutoResizeColumns("-");
			}
			catch
			{
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00039864 File Offset: 0x00038864
		public static DialogResult ShowDataTableView(DataTable t)
		{
			DataTableView dataTableView = new DataTableView(t);
			return dataTableView.ShowDialog();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00039884 File Offset: 0x00038884
		public static DialogResult ShowDataTableView(IWin32Window owner, DataTable t, string message)
		{
			DataTableView dataTableView = new DataTableView(t, message);
			return dataTableView.ShowDialog(owner);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000398A8 File Offset: 0x000388A8
		public static void AutoSizeColumns(DataGrid dg, int maxNumRowsToCheck)
		{
			if (dg != null && dg.DataSource != null)
			{
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
				if (dataTable.Columns.Count >= 1)
				{
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
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						if (maxNumRowsToCheck >= 0 && j >= maxNumRowsToCheck)
						{
							break;
						}
						DataRow dataRow = dataTable.Rows[j];
						for (int i = 0; i < array.Length; i++)
						{
							string text;
							if (dataTable.Columns[i].DataType == type)
							{
								text = ((DateTime)dataRow[i]).ToShortDateString();
							}
							else
							{
								text = dataRow[i].ToString();
							}
							int num = Convert.ToInt32(graphics.MeasureString(text, font).Width);
							if (num > array[i])
							{
								array[i] = num;
							}
						}
					}
					for (int j = 0; j < array.Length; j++)
					{
						array2[j].Width = array[j];
						dataGridTableStyle.GridColumnStyles.Add(array2[j]);
					}
					dg.TableStyles.Add(dataGridTableStyle);
				}
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00039B73 File Offset: 0x00038B73
		public void HideOKButton()
		{
			this.btn_ok.Visible = false;
			this.btn_close.Text = "&Close";
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00039B94 File Offset: 0x00038B94
		public void HideImportButton()
		{
			this.btn_import.Visible = false;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00039BA4 File Offset: 0x00038BA4
		private void MENU_GenerateSQLToUpdate_Click(object sender, EventArgs e)
		{
			this.GenerateSQLToUpdate();
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00039BB0 File Offset: 0x00038BB0
		private void GenerateSQLToUpdate()
		{
			if (this.dataGrid1.CurrentRowIndex >= 0)
			{
				DataRow dataRow;
				if (this.dataGrid1.DataSource is DataTable)
				{
					dataRow = ((DataTable)this.dataGrid1.DataSource).Rows[this.dataGrid1.CurrentRowIndex];
				}
				else if (this.dataGrid1.DataSource is DataView)
				{
					dataRow = ((DataView)this.dataGrid1.DataSource)[this.dataGrid1.CurrentRowIndex].Row;
				}
				else
				{
					dataRow = null;
				}
				if (dataRow != null)
				{
					string defaultText = string.Concat(new string[]
					{
						"UPDATE ",
						dataRow[0].ToString(),
						" SET ",
						dataRow[1].ToString(),
						"=",
						dataRow[2].ToString(),
						" WHERE ",
						dataRow[1].ToString(),
						"=",
						dataRow[2].ToString()
					});
					string userInput = InputBox.GetUserInput(this, "Execute SQL", "Click OK to execute this sql statement:", defaultText, 200, false);
					if (userInput != null && userInput.Length > 0)
					{
						this.da.SelectCommand.CommandText = userInput;
						string text;
						this.da.Fill(new DataTable(), out text);
						if (text != null && text.Length > 0)
						{
							MessageBox.Show(text);
						}
						else
						{
							MessageBox.Show("Successful!");
						}
					}
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00039D88 File Offset: 0x00038D88
		public Label LabelLeft
		{
			get
			{
				return this.lbl_left;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00039DA0 File Offset: 0x00038DA0
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00039DA4 File Offset: 0x00038DA4
		private void btn_properties_Click(object sender, EventArgs e)
		{
			DataTableViewProperties dataTableViewProperties = new DataTableViewProperties(this.dataGrid1);
			dataTableViewProperties.ShowDialog(this);
			dataTableViewProperties.Dispose();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00039DD0 File Offset: 0x00038DD0
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

		// Token: 0x0600070E RID: 1806 RVA: 0x00039E7C File Offset: 0x00038E7C
		private void btn_exportToXml_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			if (this.dataGrid1.DataSource is DataTable)
			{
				DataTable dataTable = (DataTable)this.dataGrid1.DataSource;
				dataTable.TableName = "datatableview";
				dataSet.Tables.Add(dataTable);
			}
			else
			{
				if (!(this.dataGrid1.DataSource is DataView))
				{
					return;
				}
				DataView dataView = (DataView)this.dataGrid1.DataSource;
				dataView.Table.TableName = "dataviewview";
				dataSet.Tables.Add(dataView.Table);
			}
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
			dataSet.Tables.Clear();
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00039FA8 File Offset: 0x00038FA8
		private void btn_ok_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00039FB2 File Offset: 0x00038FB2
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00039FC4 File Offset: 0x00038FC4
		private void viewRowDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x04000585 RID: 1413
		private UnivDataAdapter da = null;

		// Token: 0x04000586 RID: 1414
		public string message;

		// Token: 0x04000587 RID: 1415
		public int messagePaddingTimesTwo = 16;
	}
}
