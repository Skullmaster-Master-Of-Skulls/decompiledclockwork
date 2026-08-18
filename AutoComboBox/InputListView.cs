using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.Properties;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox
{
	// Token: 0x020000E9 RID: 233
	public partial class InputListView : Form
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x00045F1C File Offset: 0x00044F1C
		public InputListView(string title, string caption, DataView dv, int checkBoxColumnInd, bool allowOrdering, bool multiSelect)
		{
			this.InitializeComponent();
			this.b = new SolidBrush(this.lv.ForeColor);
			this.checkBoxColumnInd = checkBoxColumnInd;
			this.dv = dv;
			this.Text = title;
			this.lbl_caption.Text = caption;
			this.allowOrdering = allowOrdering;
			this.lv.MultiSelect = multiSelect;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00046D00 File Offset: 0x00045D00
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00046D18 File Offset: 0x00045D18
		public bool SelectAllItemsByDefault
		{
			get
			{
				return this.selectAllItemsByDefault;
			}
			set
			{
				this.selectAllItemsByDefault = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00046D24 File Offset: 0x00045D24
		public ListViewEx LV
		{
			get
			{
				return this.lv;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00046D3C File Offset: 0x00045D3C
		private void ToScreen()
		{
			DataTable table = this.dv.Table;
			if (this.dv != null && table.Columns.Count >= 1)
			{
				ArrayList arrayList = new ArrayList(table.Columns.Count);
				int num;
				if (this.checkBoxColumnInd >= 0)
				{
					this.lv.Columns.Add(table.Columns[this.checkBoxColumnInd].ColumnName, 35, HorizontalAlignment.Left);
					num = 1;
					arrayList.Add(this.checkBoxColumnInd);
				}
				else
				{
					num = 0;
				}
				for (int i = num; i < table.Columns.Count; i++)
				{
					if (i != this.checkBoxColumnInd && table.Columns[i].ColumnMapping != MappingType.Hidden)
					{
						arrayList.Add(i);
					}
				}
				if (arrayList.Count >= 1)
				{
					this.colInds = new int[arrayList.Count];
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.colInds[i] = (int)arrayList[i];
					}
					Graphics graphics = this.lv.CreateGraphics();
					for (int i = num; i < table.Columns.Count; i++)
					{
						if (i != this.checkBoxColumnInd && table.Columns[i].ColumnMapping != MappingType.Hidden)
						{
							int num2 = 25 + Convert.ToInt32(graphics.MeasureString(table.Columns[i].ColumnName, this.lv.Font).Width);
							foreach (object obj in this.dv)
							{
								DataRowView dataRowView = (DataRowView)obj;
								DataRow row = dataRowView.Row;
								string text = this.CellToString(row, i);
								SizeF sizeF = graphics.MeasureString(text, this.lv.Font);
								if (sizeF.Width > (float)num2)
								{
									num2 = Convert.ToInt32(sizeF.Width);
								}
							}
							this.lv.Columns.Add(table.Columns[i].ColumnName, num2 + 25, HorizontalAlignment.Left);
						}
					}
					graphics = null;
					foreach (object obj2 in this.dv)
					{
						DataRowView dataRowView = (DataRowView)obj2;
						DataRow row = dataRowView.Row;
						ListViewItem listViewItem = new ListViewItem(row[this.colInds[0]].ToString());
						for (int i = 1; i < this.lv.Columns.Count; i++)
						{
							listViewItem.SubItems.Add(this.CellToString(row, this.colInds[i]));
						}
						listViewItem.Tag = row;
						this.lv.Items.Add(listViewItem);
						if (this.selectAllItemsByDefault)
						{
							listViewItem.Selected = true;
						}
						else if (!string.IsNullOrEmpty(this.defaultSelectedColName))
						{
							int num3 = (row[this.defaultSelectedColName] == DBNull.Value) ? 0 : ((int)row[this.defaultSelectedColName]);
							if (num3 == this.defaultSelectedIndex)
							{
								listViewItem.Selected = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x170001DF RID: 479
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00047158 File Offset: 0x00046158
		public int DefaultSelectedIndex
		{
			set
			{
				this.defaultSelectedIndex = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00047162 File Offset: 0x00046162
		public string DefaultSelectedColname
		{
			set
			{
				this.defaultSelectedColName = value;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004716C File Offset: 0x0004616C
		private string CellToString(DataRow dr, int colInd)
		{
			string result;
			if (dr[colInd] == DBNull.Value)
			{
				result = "";
			}
			else
			{
				DataTable table = dr.Table;
				Type type = Type.GetType("System.DateTime");
				Type type2 = Type.GetType("System.Boolean");
				Type dataType = table.Columns[colInd].DataType;
				if (dataType == type)
				{
					result = ((DateTime)dr[colInd]).ToString(this.DateFormatString);
				}
				else if (dataType == type2)
				{
					result = ((bool)dr[colInd]).ToString();
				}
				else
				{
					result = dr[colInd].ToString();
				}
			}
			return result;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00047234 File Offset: 0x00046234
		private void lv_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index >= 0)
			{
				ListViewItem listViewItem = this.lv.Items[e.Index];
				DataRow dataRow = (DataRow)listViewItem.Tag;
				if (this.tasksListViewItemBufferImage == null)
				{
					Bitmap bitmap = new Bitmap(e.Bounds.Width, e.Bounds.Height);
					this.tasksListViewItemBufferImage = bitmap;
				}
				Graphics graphics = Graphics.FromImage(this.tasksListViewItemBufferImage);
				if (listViewItem.Selected)
				{
					graphics.FillRectangle(this.lv.BackColourSelectedBrush, 0, 0, this.tasksListViewItemBufferImage.Width, this.tasksListViewItemBufferImage.Height);
				}
				else
				{
					graphics.FillRectangle(this.lv.BackColourBrush, 0, 0, this.tasksListViewItemBufferImage.Width, this.tasksListViewItemBufferImage.Height);
				}
				Font font = this.lv.Font;
				for (int i = 0; i < this.lv.Columns.Count; i++)
				{
					if (this.lv.Columns[i].Width > 0)
					{
						Rectangle subItemBounds = this.lv.GetSubItemBounds(listViewItem, i);
						subItemBounds.Y = 0;
						subItemBounds.X -= e.Bounds.X;
						int num = this.colInds[i];
						if (num != this.checkBoxColumnInd)
						{
							string text = this.CellToString(dataRow, num);
							if (text != null && text.Length > 0)
							{
								int length = text.Length;
								int num2;
								int num3;
								graphics.MeasureString(text, font, subItemBounds.Size, this.sf, out num2, out num3);
								if (num2 < length)
								{
									if (num2 > 2)
									{
										text = text.Substring(0, num2 - 2) + "...";
									}
									else if (num2 > 1)
									{
										text = text.Substring(0, 1) + "...";
									}
									else
									{
										text = "...";
									}
								}
								graphics.DrawString(text, font, this.b, subItemBounds, this.sf);
							}
						}
						else
						{
							bool flag = dataRow[this.checkBoxColumnInd] != DBNull.Value && (bool)dataRow[this.checkBoxColumnInd];
							ButtonState state;
							if (flag)
							{
								state = ButtonState.Checked;
							}
							else
							{
								state = ButtonState.Normal;
							}
							int num4 = this.lv.ItemHeight - 2;
							int num5 = this.lv.Columns[0].Width - num4;
							int num6;
							if (num5 < 2)
							{
								num6 = 0;
							}
							else
							{
								num6 = Convert.ToInt32(Convert.ToDouble(num5) / 2.0);
							}
							ControlPaint.DrawCheckBox(graphics, subItemBounds.X + num6, subItemBounds.Y + 1, num4, num4, state);
						}
					}
				}
				e.Graphics.DrawImageUnscaled(this.tasksListViewItemBufferImage, e.Bounds);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00047590 File Offset: 0x00046590
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x000475A8 File Offset: 0x000465A8
		public int MyItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
				this.lv.ItemHeight = this.itemHeight;
				this.lv.Refresh();
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000475D0 File Offset: 0x000465D0
		private void InputListView_Load(object sender, EventArgs e)
		{
			this.lv.ItemHeight = this.itemHeight;
			this.ToScreen();
			this.AutoSelectRow();
			this.InputListView_SizeChanged(this.lv, null);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00047604 File Offset: 0x00046604
		private void lbl_caption_TextChanged(object sender, EventArgs e)
		{
			Graphics graphics = this.lbl_caption.CreateGraphics();
			int num;
			int num2;
			graphics.MeasureString(this.lbl_caption.Text, this.lbl_caption.Font, new SizeF((float)this.lbl_caption.Width, (float)Screen.PrimaryScreen.WorkingArea.Height), this.sf, out num, out num2);
			if (num2 > 0)
			{
				this.p_caption.Height = (this.lbl_caption.Font.Height + 4) * num2 + this.p_caption.DockPadding.Top + this.p_caption.DockPadding.Bottom;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000476B8 File Offset: 0x000466B8
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000476C4 File Offset: 0x000466C4
		public DataTable GetTableWithCheckedRowsOnly()
		{
			DataTable dataTable = this.dv.Table.Clone();
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				DataRow dataRow = (DataRow)listViewItem.Tag;
				if (dataRow[this.checkBoxColumnInd] != DBNull.Value && (bool)dataRow[this.checkBoxColumnInd])
				{
					dataTable.ImportRow(dataRow);
				}
			}
			return dataTable;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00047790 File Offset: 0x00046790
		public DataRow GetSelectedDataRow()
		{
			DataRow result;
			if (this.lv.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.lv.SelectedItems[0];
				result = (DataRow)listViewItem.Tag;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000477E0 File Offset: 0x000467E0
		private void lv_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.checkBoxColumnInd >= 0)
			{
				ListViewItem listViewItem;
				int subItemAt = this.lv.GetSubItemAt(e.X, e.Y, out listViewItem);
				if (listViewItem != null && subItemAt == 0)
				{
					DataRow dataRow = (DataRow)listViewItem.Tag;
					bool flag = dataRow[this.checkBoxColumnInd] != DBNull.Value && (bool)dataRow[this.checkBoxColumnInd];
					dataRow[this.checkBoxColumnInd] = !flag;
					listViewItem.SubItems[0].Text = this.CellToString(dataRow, this.checkBoxColumnInd);
				}
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000478A7 File Offset: 0x000468A7
		public void SetButtonOKText(string text)
		{
			this.btn_ok.Text = text;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000478B7 File Offset: 0x000468B7
		public void SetButtonCancelText(string text)
		{
			this.btn_cancel.Text = text;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000478C8 File Offset: 0x000468C8
		private void InputListView_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 'y' && this.btn_ok.Text.ToLower().CompareTo("&yes") == 0)
			{
				this.btn_ok_Click(this.btn_ok, null);
			}
			else if (e.KeyChar == 'n' && this.btn_cancel.Text.ToLower().CompareTo("&no") == 0)
			{
				this.btn_cancel_Click(this.btn_cancel, null);
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0004795C File Offset: 0x0004695C
		private void lv_DoubleClick(object sender, EventArgs e)
		{
			if (!this.ignoreDoubleClick)
			{
				if (this.lv.SelectedItems.Count > 0)
				{
					this.btn_ok_Click(this.btn_ok, null);
				}
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000479A4 File Offset: 0x000469A4
		private void InputListView_SizeChanged(object sender, EventArgs e)
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				if (this.lv.Columns.Count == 1)
				{
					int num = base.Width - SystemInformation.VerticalScrollBarWidth - SystemInformation.Border3DSize.Width * 4 - base.DockPadding.Left - base.DockPadding.Right;
					if (num > 0)
					{
						this.lv.Columns[0].Width = num;
						this.lv.Refresh();
					}
				}
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00047A46 File Offset: 0x00046A46
		private void lv_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = this.itemHeight;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00047A56 File Offset: 0x00046A56
		public void AutoSelectRow(int colInd, object match)
		{
			this.colInd = colInd;
			this.match = match;
			this.AutoSelectRow();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00047A70 File Offset: 0x00046A70
		private void lv_SizeChanged(object sender, EventArgs e)
		{
			if (this.lv.Columns.Count > 0 && base.WindowState != FormWindowState.Minimized)
			{
				int num = 0;
				for (int i = 0; i < this.lv.Columns.Count; i++)
				{
					num += this.lv.Columns[i].Width;
				}
				int num2 = this.lv.Width - SystemInformation.VerticalScrollBarWidth - SystemInformation.Border3DSize.Width * 2 - SystemInformation.BorderSize.Width * (this.lv.Columns.Count + 1);
				if (num < num2)
				{
					int width = num2 - num;
					this.lv.Columns[this.lv.Columns.Count - 1].Width = width;
				}
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00047B68 File Offset: 0x00046B68
		private void InputListView_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyCode == Keys.Add)
				{
					this.lv.Font = new Font("Arial", this.lv.Font.SizeInPoints + 1f);
					this.MyItemHeight = Convert.ToInt32(this.lv.Font.SizeInPoints);
				}
				else if (e.KeyCode == Keys.Subtract && this.lv.Font.SizeInPoints > 2f)
				{
					this.lv.Font = new Font("Arial", this.lv.Font.SizeInPoints - 1f);
					this.MyItemHeight = Convert.ToInt32(this.lv.Font.SizeInPoints);
				}
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00047C5C File Offset: 0x00046C5C
		private void LaunchFile(string fn)
		{
			if (File.Exists(fn))
			{
				try
				{
					Process.Start(fn);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00047CA4 File Offset: 0x00046CA4
		private void AutoSelectRow()
		{
			if (this.colInd >= 0)
			{
				foreach (object obj in this.lv.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					DataRow dataRow = (DataRow)listViewItem.Tag;
					if (dataRow[this.colInd] == DBNull.Value && (this.match == null || this.match == DBNull.Value))
					{
						listViewItem.Selected = true;
					}
					else if (dataRow[this.colInd] != DBNull.Value && this.match != null && this.match != DBNull.Value)
					{
						object obj2 = dataRow[this.colInd];
						if (this.match is DateTime)
						{
							DateTime dateTime = (DateTime)obj2;
							DateTime dateTime2 = (DateTime)this.match;
							if (dateTime.Year == dateTime2.Year && dateTime.Month == dateTime2.Month && dateTime.Day == dateTime2.Day)
							{
								listViewItem.Selected = true;
							}
						}
						else if (this.match is bool)
						{
							bool flag = (bool)obj2;
							bool flag2 = (bool)this.match;
							if (flag == flag2)
							{
								listViewItem.Selected = true;
							}
						}
						else if (this.match is int)
						{
							int num = (int)obj2;
							int num2 = (int)this.match;
							if (num == num2)
							{
								listViewItem.Selected = true;
							}
						}
						else if (this.match is string)
						{
							string text = (string)obj2;
							string text2 = (string)this.match;
							if (text.ToLower().Trim().CompareTo(text2.ToLower().Trim()) == 0)
							{
								listViewItem.Selected = true;
							}
						}
						else if (obj2 == this.match)
						{
							listViewItem.Selected = true;
						}
					}
					if (listViewItem.Selected && !this.lv.MultiSelect)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00047F7C File Offset: 0x00046F7C
		public object AddButton(string text, EventHandler eh)
		{
			return this.AddButton(text, -1, -1, eh);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00047F98 File Offset: 0x00046F98
		public object AddButton(string text, int imageIndex, int overImageIndex)
		{
			return this.AddButton(text, imageIndex, overImageIndex, null);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00047FB4 File Offset: 0x00046FB4
		public object AddButton(string text, int imageIndex, int overImageIndex, EventHandler eh)
		{
			ToolStripSeparator value = new ToolStripSeparator();
			this.toolStrip1.Items.Add(value);
			ToolStripButton toolStripButton = new ToolStripButton(text, null, eh);
			this.toolStrip1.Items.Add(toolStripButton);
			return toolStripButton;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00047FFC File Offset: 0x00046FFC
		public void AddButtonRemoveEventHandler(object btn, EventHandler eh)
		{
			if (btn is ToolStripButton)
			{
				ToolStripButton toolStripButton = (ToolStripButton)btn;
				toolStripButton.Click -= eh;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0004802B File Offset: 0x0004702B
		private void btn_fakeOk_Click(object sender, EventArgs e)
		{
			this.btn_ok_Click(this.btn_ok, null);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0004803C File Offset: 0x0004703C
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00048050 File Offset: 0x00047050
		private void btn_exportToExcel_Click(object sender, EventArgs e)
		{
			bool askUserToFilterColumns = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			string tempFilename = ExportClass.GetTempFilename(".xls");
			ExportClass.ExportToExcel(this.dv, tempFilename, ExportClass.GetStartDirectory(), askUserToFilterColumns);
			this.LaunchFile(tempFilename);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00048098 File Offset: 0x00047098
		private void btn_exportToAccess_Click(object sender, EventArgs e)
		{
			bool askUserToFilterColumns = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			string tempFilename = ExportClass.GetTempFilename(".mdb");
			ExportClass.ExportToAccess("table1", this.dv, tempFilename, ExportClass.GetStartDirectory(), askUserToFilterColumns);
			this.LaunchFile(tempFilename);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000480E4 File Offset: 0x000470E4
		private void btn_exportToDelimiteredText_Click(object sender, EventArgs e)
		{
			bool askUserToFilterColumns = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			string tempFilename = ExportClass.GetTempFilename(".txt");
			ExportClass.ExportToDelimeteredText(this.dv, tempFilename, ExportClass.GetStartDirectory(), askUserToFilterColumns, ",", Environment.NewLine);
			this.LaunchFile(tempFilename);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00048138 File Offset: 0x00047138
		private void btn_exportToTabDelimiteredText_Click(object sender, EventArgs e)
		{
			bool askUserToFilterColumns = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			string tempFilename = ExportClass.GetTempFilename(".txt");
			ExportClass.ExportToDelimeteredText(this.dv, tempFilename, ExportClass.GetStartDirectory(), askUserToFilterColumns, '\t'.ToString(), Environment.NewLine);
			this.LaunchFile(tempFilename);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00048190 File Offset: 0x00047190
		private void btn_exportToFormattedText_Click(object sender, EventArgs e)
		{
			bool askUserToFilterColumns = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			ExportClass.ExportToFormattedText(this.dv, ExportClass.GetTempFilename(".txt"), askUserToFilterColumns);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000481C8 File Offset: 0x000471C8
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000481D4 File Offset: 0x000471D4
		public void ShowCheckbox(string text, bool initialCheckedState)
		{
			this.chk.Text = text;
			this.chk.Visible = true;
			if (initialCheckedState)
			{
				this.chk.Checked = true;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00048214 File Offset: 0x00047214
		public bool chkChecked
		{
			get
			{
				return this.chk.Checked;
			}
		}

		// Token: 0x040006A2 RID: 1698
		public bool allowOrdering;

		// Token: 0x040006A3 RID: 1699
		public int checkBoxColumnInd = -1;

		// Token: 0x040006A4 RID: 1700
		public DataView dv;

		// Token: 0x040006A5 RID: 1701
		public string DateFormatString = "yyyy-MM-dd";

		// Token: 0x040006A6 RID: 1702
		public bool ignoreDoubleClick = false;

		// Token: 0x040006A7 RID: 1703
		private bool selectAllItemsByDefault = false;

		// Token: 0x040006A8 RID: 1704
		private int[] colInds;

		// Token: 0x040006A9 RID: 1705
		private int defaultSelectedIndex = 0;

		// Token: 0x040006AA RID: 1706
		private string defaultSelectedColName = null;

		// Token: 0x040006AB RID: 1707
		private Image tasksListViewItemBufferImage = null;

		// Token: 0x040006AC RID: 1708
		private StringFormat sf = new StringFormat(StringFormatFlags.LineLimit);

		// Token: 0x040006AD RID: 1709
		private SolidBrush b;

		// Token: 0x040006AE RID: 1710
		private int itemHeight = 8;

		// Token: 0x040006AF RID: 1711
		private int colInd = -1;

		// Token: 0x040006B0 RID: 1712
		private object match = null;
	}
}
