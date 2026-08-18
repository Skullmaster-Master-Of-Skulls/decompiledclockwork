using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000076 RID: 118
	public class MyDataGrid : DataGrid
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x00025F88 File Offset: 0x00024F88
		public MyDataGrid()
		{
			this.showNullsAsBlanks = true;
			base.ReadOnly = true;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00025FB8 File Offset: 0x00024FB8
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00025FD0 File Offset: 0x00024FD0
		public string ColumnOrders
		{
			get
			{
				return this.columnOrders;
			}
			set
			{
				this.columnOrders = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00025FDC File Offset: 0x00024FDC
		public int[] ColumnOrderArray
		{
			get
			{
				DataTable dataSource = this.GetDataSource();
				int[] result;
				if (dataSource == null || this.columnOrders.Trim().Length < 1)
				{
					result = new int[0];
				}
				else
				{
					string[] array = this.columnOrders.Split(new char[]
					{
						','
					});
					ArrayList arrayList = new ArrayList();
					foreach (string text in array)
					{
						string s = text.Trim();
						if (this.IsNumeric(s))
						{
							int num = int.Parse(s);
							if (num >= 0 && num < dataSource.Columns.Count)
							{
								arrayList.Add(num);
							}
						}
					}
					int[] array3;
					if (arrayList.Count < 1)
					{
						array3 = new int[dataSource.Columns.Count];
						for (int j = 0; j < dataSource.Columns.Count; j++)
						{
							array3[j] = j;
						}
					}
					else
					{
						array3 = new int[arrayList.Count];
						for (int k = 0; k < arrayList.Count; k++)
						{
							array3[k] = (int)arrayList[k];
						}
					}
					result = array3;
				}
				return result;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002614C File Offset: 0x0002514C
		private bool IsNumeric(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsDigit(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002618C File Offset: 0x0002518C
		public DataTable GetDataSource()
		{
			DataTable result;
			if (base.DataSource is DataTable)
			{
				result = (DataTable)base.DataSource;
			}
			else if (base.DataSource is DataView)
			{
				result = ((DataView)base.DataSource).Table;
			}
			else if (base.DataSource is DataSet)
			{
				DataSet dataSet = (DataSet)base.DataSource;
				result = ((dataSet.Tables.Count > 0) ? dataSet.Tables[0] : null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0002622C File Offset: 0x0002522C
		public void AutoResizeColumns(string nullText)
		{
			this.AutoResizeColumns(nullText, false);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00026238 File Offset: 0x00025238
		public void AutoResizeColumns(string nullText, bool resizeRowHeights)
		{
			DataTable dataSource = this.GetDataSource();
			if (dataSource != null)
			{
				DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();
				dataGridTableStyle.MappingName = dataSource.TableName;
				if (dataSource.Columns.Count > 0 && dataSource.Rows.Count > 0)
				{
					int[] array = new int[dataSource.Columns.Count];
					Graphics graphics = base.CreateGraphics();
					int[] array2 = this.ColumnOrderArray;
					if (array2.Length < 1)
					{
						array2 = new int[dataSource.Columns.Count];
						for (int i = 0; i < dataSource.Columns.Count; i++)
						{
							array2[i] = i;
						}
					}
					foreach (int num in array2)
					{
						array[num] = Convert.ToInt32(graphics.MeasureString(dataSource.Columns[num].ColumnName, base.HeaderFont).Width) + 2;
						MyDataGridTextBoxColumn myDataGridTextBoxColumn = new MyDataGridTextBoxColumn();
						myDataGridTextBoxColumn.TextBox.Enabled = true;
						myDataGridTextBoxColumn.HeaderText = dataSource.Columns[num].ColumnName;
						myDataGridTextBoxColumn.MappingName = dataSource.Columns[num].ColumnName;
						myDataGridTextBoxColumn.NullText = nullText;
						Type dataType = dataSource.Columns[num].DataType;
						if (dataType == typeof(int) || dataType == typeof(bool) || dataType == typeof(double))
						{
							myDataGridTextBoxColumn.Alignment = HorizontalAlignment.Center;
						}
						if (dataSource.Columns[num].ColumnMapping == MappingType.Hidden)
						{
							myDataGridTextBoxColumn.Width = 0;
						}
						else
						{
							for (int k = 0; k < dataSource.Rows.Count; k++)
							{
								DataRow dataRow = dataSource.Rows[k];
								int num2 = Convert.ToInt32(graphics.MeasureString(dataRow[num].ToString().Trim(), this.Font).Width) + 2;
								if (num2 > array[num] && num2 < 300)
								{
									array[num] = num2;
								}
							}
							myDataGridTextBoxColumn.Width = array[num];
						}
						dataGridTableStyle.GridColumnStyles.Add(myDataGridTextBoxColumn);
						dataGridTableStyle.RowHeadersVisible = base.RowHeadersVisible;
					}
					base.TableStyles.Clear();
					base.TableStyles.Add(dataGridTableStyle);
					if (resizeRowHeights)
					{
						this.AutoSizeRowHeight(dataGridTableStyle.GridColumnStyles);
					}
				}
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00026520 File Offset: 0x00025520
		public void AutoSizeRowHeight(GridColumnStylesCollection gridColumnStyles)
		{
			DataTable dataTable = this.GetDataTable();
			int count = dataTable.Rows.Count;
			Graphics graphics = Graphics.FromHwnd(base.Handle);
			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
			Type type = base.GetType();
			Type baseType = type.BaseType;
			MethodInfo method = baseType.GetMethod("get_DataGridRows", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			Array array = (Array)method.Invoke(this, null);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in array)
			{
				if (obj.ToString().EndsWith("DataGridRelationshipRow"))
				{
					arrayList.Add(obj);
				}
			}
			for (int i = 0; i < count; i++)
			{
				int num = 20;
				for (int j = 0; j < gridColumnStyles.Count; j++)
				{
					DataGridColumnStyle dataGridColumnStyle = gridColumnStyles[j];
					int num2 = Convert.ToInt32(graphics.MeasureString(base[i, j].ToString(), this.Font, dataGridColumnStyle.Width, format).Height);
					if (num2 > num)
					{
						num = num2;
					}
				}
				num += 8;
				PropertyInfo property = arrayList[i].GetType().GetProperty("Height");
				property.SetValue(arrayList[i], num, null);
			}
			graphics.Dispose();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000266D8 File Offset: 0x000256D8
		public DataTable GetDataTable()
		{
			DataTable result;
			if (base.DataSource == null)
			{
				result = null;
			}
			else if (base.DataSource is DataTable)
			{
				result = (DataTable)base.DataSource;
			}
			else if (base.DataSource is DataView)
			{
				result = ((DataView)base.DataSource).Table;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0002674C File Offset: 0x0002574C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			DataGrid.HitTestInfo hitTestInfo = base.HitTest(e.X, e.Y);
			if (hitTestInfo.Type == DataGrid.HitTestType.Cell)
			{
				if (e.Button == MouseButtons.Left)
				{
					Keys modifierKeys = Control.ModifierKeys;
					if (modifierKeys != Keys.Shift)
					{
						if (modifierKeys != Keys.Control)
						{
							if (modifierKeys != (Keys.Shift | Keys.Control))
							{
								base.ResetSelection();
								this.mySelection.Clear();
								base.Select(hitTestInfo.Row);
								this.mySelection.Add(hitTestInfo.Row);
								this.myLastSelection = hitTestInfo.Row;
							}
							else if (this.myLastSelection < hitTestInfo.Row)
							{
								for (int i = this.myLastSelection; i <= hitTestInfo.Row; i++)
								{
									base.Select(i);
									this.mySelection.Add(i);
								}
							}
							else
							{
								for (int i = hitTestInfo.Row; i <= this.myLastSelection; i++)
								{
									base.Select(i);
									this.mySelection.Add(i);
								}
							}
						}
						else if (base.IsSelected(hitTestInfo.Row))
						{
							base.UnSelect(hitTestInfo.Row);
							this.mySelection.Remove(hitTestInfo.Row);
							this.myLastSelection = hitTestInfo.Row;
						}
						else
						{
							base.Select(hitTestInfo.Row);
							this.mySelection.Add(hitTestInfo.Row);
							this.myLastSelection = hitTestInfo.Row;
						}
					}
					else
					{
						base.ResetSelection();
						this.mySelection.Clear();
						if (this.myLastSelection < hitTestInfo.Row)
						{
							for (int i = this.myLastSelection; i <= hitTestInfo.Row; i++)
							{
								base.Select(i);
								this.mySelection.Add(i);
							}
						}
						else
						{
							for (int i = hitTestInfo.Row; i <= this.myLastSelection; i++)
							{
								base.Select(i);
								this.mySelection.Add(i);
							}
						}
					}
				}
			}
			else
			{
				base.OnMouseDown(e);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000269CC File Offset: 0x000259CC
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000269E4 File Offset: 0x000259E4
		public ArrayList SelectedIndices
		{
			get
			{
				return this.mySelection;
			}
			set
			{
				base.ResetSelection();
				this.mySelection.Clear();
				this.mySelection = value;
				foreach (object obj in this.mySelection)
				{
					int row = (int)obj;
					base.Select(row);
				}
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00026A68 File Offset: 0x00025A68
		public void SelectIndex(int index)
		{
			base.ResetSelection();
			this.mySelection.Clear();
			this.mySelection = new ArrayList();
			foreach (object obj in this.mySelection)
			{
				int row = (int)obj;
				base.Select(row);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00026AF0 File Offset: 0x00025AF0
		public ArrayList SelectedItems
		{
			get
			{
				CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[base.DataSource, base.DataMember];
				DataView dataView = (DataView)currencyManager.List;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.mySelection)
				{
					int num = (int)obj;
					if (num >= 0 && num < dataView.Count)
					{
						arrayList.Add(dataView[num].Row);
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00026BBC File Offset: 0x00025BBC
		public bool AtLeastOneItemSelected
		{
			get
			{
				CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[base.DataSource, base.DataMember];
				DataView dataView = (DataView)currencyManager.List;
				ArrayList arrayList = new ArrayList();
				return this.mySelection.Count > 0;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00026C0B File Offset: 0x00025C0B
		public void GoToRow(int RowIndex)
		{
			this.GridVScrolled(this, new ScrollEventArgs(ScrollEventType.LargeIncrement, RowIndex));
		}

		// Token: 0x040003F9 RID: 1017
		private ArrayList mySelection = new ArrayList();

		// Token: 0x040003FA RID: 1018
		private int myLastSelection;

		// Token: 0x040003FB RID: 1019
		private bool showNullsAsBlanks;

		// Token: 0x040003FC RID: 1020
		private string columnOrders = "";
	}
}
