using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox
{
	// Token: 0x0200001E RID: 30
	public partial class InputList : Form
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00008B18 File Offset: 0x00007B18
		public InputList(string title, string caption, DataTable t, string displayMember, bool MultipleSelect)
		{
			this.InitializeComponent();
			this.Init(title, caption, t, displayMember, MultipleSelect, false, null);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00008B3A File Offset: 0x00007B3A
		public InputList(string title, string caption, DataView dv, string displayMember, bool MultipleSelect)
		{
			this.InitializeComponent();
			this.Init(title, caption, dv, displayMember, MultipleSelect, false, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008B5C File Offset: 0x00007B5C
		public InputList(string title, string caption, DataTable t, string displayMember, bool MultipleSelect, bool AllowReordering)
		{
			this.InitializeComponent();
			this.Init(title, caption, t, displayMember, MultipleSelect, AllowReordering, null);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00008B80 File Offset: 0x00007B80
		private void Init(string title, string caption, DataTable t, string displayMember, bool MultipleSelect, bool AllowReordering, ArrayList SelectedIndices)
		{
			this.listBox1.DataSource = t;
			this.listBox1.DisplayMember = displayMember;
			this.label1.Text = caption;
			this.Text = title;
			this.selectedIndices = SelectedIndices;
			if (MultipleSelect)
			{
				this.listBox1.SelectionMode = SelectionMode.MultiExtended;
			}
			else
			{
				this.listBox1.SelectionMode = SelectionMode.One;
			}
			this.allowReordering = AllowReordering;
			this.btn_up.Visible = this.allowReordering;
			this.btn_down.Visible = this.allowReordering;
			this.btn_splitUpDown.Visible = this.allowReordering;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008C2C File Offset: 0x00007C2C
		private void Init(string title, string caption, DataView dv, string displayMember, bool MultipleSelect, bool AllowReordering, ArrayList SelectedIndices)
		{
			this.listBox1.DataSource = dv;
			this.listBox1.DisplayMember = displayMember;
			this.label1.Text = caption;
			this.Text = title;
			this.selectedIndices = SelectedIndices;
			if (MultipleSelect)
			{
				this.listBox1.SelectionMode = SelectionMode.MultiExtended;
			}
			else
			{
				this.listBox1.SelectionMode = SelectionMode.One;
			}
			this.allowReordering = AllowReordering;
			this.btn_up.Visible = this.allowReordering;
			this.btn_down.Visible = this.allowReordering;
			this.btn_splitUpDown.Visible = this.allowReordering;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00008CD8 File Offset: 0x00007CD8
		public int SelectedIndex
		{
			get
			{
				int result;
				if (this.listBox1.SelectedIndices.Count > 0)
				{
					if (this.listBox1.DataSource is DataView)
					{
						DataView dataView = (DataView)this.listBox1.DataSource;
						DataRowView dataRowView = dataView[this.listBox1.SelectedIndices[0]];
						DataRow row = dataRowView.Row;
						DataTable table = row.Table;
						for (int i = 0; i < table.Rows.Count; i++)
						{
							DataRow dataRow = table.Rows[i];
							if (dataRow == row)
							{
								return i;
							}
						}
					}
					else if (this.listBox1.DataSource is DataTable)
					{
						return this.listBox1.SelectedIndices[0];
					}
					result = -1;
				}
				else
				{
					result = -1;
				}
				return result;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008DE3 File Offset: 0x00007DE3
		public InputList(string title, string caption, DataTable t, string displayMember, ArrayList SelectedIndices, bool MultipleSelect)
		{
			this.InitializeComponent();
			this.Init(title, caption, t, displayMember, MultipleSelect, false, SelectedIndices);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008E06 File Offset: 0x00007E06
		public InputList(string title, string caption, DataTable t, string displayMember, ArrayList SelectedIndices, bool AllowReordering, bool MultipleSelect)
		{
			this.InitializeComponent();
			this.Init(title, caption, t, displayMember, MultipleSelect, AllowReordering, SelectedIndices);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009984 File Offset: 0x00008984
		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedIndex >= 0)
			{
				this.btn_ok_Click(this.btn_ok, null);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000099B2 File Offset: 0x000089B2
		public void OKClicked()
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000099C4 File Offset: 0x000089C4
		private void btn_fake_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000099CE File Offset: 0x000089CE
		private void btn_fakeAccept_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000099D4 File Offset: 0x000089D4
		private void InputList_Load(object sender, EventArgs e)
		{
			this.btn_selectAll.Visible = (this.listBox1.SelectionMode != SelectionMode.One);
			this.btn_selectNone.Visible = this.btn_selectAll.Visible;
			this.listBox1.SelectedIndex = -1;
			if (this.selectedIndices != null)
			{
				foreach (object obj in this.selectedIndices)
				{
					int index = (int)obj;
					try
					{
						this.listBox1.SetSelected(index, true);
					}
					catch
					{
					}
				}
			}
			base.ActiveControl = this.listBox1;
			if ((this.selectedIndices == null || this.selectedIndices.Count < 1) && this.listBox1.Items.Count >= 1)
			{
				this.listBox1.SetSelected(0, true);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009AF4 File Offset: 0x00008AF4
		private void SwapDataRows(DataRow dr1, DataRow dr2)
		{
			for (int i = 0; i < dr1.Table.Columns.Count; i++)
			{
				object value = dr1[i];
				dr1[i] = dr2[i];
				dr2[i] = value;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009B44 File Offset: 0x00008B44
		private void SelectAll(bool setSelected)
		{
			for (int i = 0; i < this.listBox1.Items.Count; i++)
			{
				this.listBox1.SetSelected(i, setSelected);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00009B84 File Offset: 0x00008B84
		private void Print(bool printPreviewOnly)
		{
			this.currentPrintingIndex = 0;
			this.y = 0;
			DialogResult dialogResult = this.printDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				if (printPreviewOnly)
				{
					PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
					printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
					printPreviewDialog.Document = this.printDocument;
					printPreviewDialog.Load += this.ppd_Load;
					printPreviewDialog.ShowDialog();
				}
				else
				{
					this.printDocument.Print();
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009C15 File Offset: 0x00008C15
		private void ppd_Load(object sender, EventArgs e)
		{
			((Form)sender).WindowState = FormWindowState.Maximized;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00009C28 File Offset: 0x00008C28
		public DataRow SelectedRow
		{
			get
			{
				DataRow result;
				if (this.listBox1.SelectedIndex < 0)
				{
					result = null;
				}
				else
				{
					result = ((DataTable)this.listBox1.DataSource).Rows[this.listBox1.SelectedIndex];
				}
				return result;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00009C78 File Offset: 0x00008C78
		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			SolidBrush brush = new SolidBrush(Color.Black);
			int num = e.MarginBounds.Bottom - this.listBox1.Font.Height * 2;
			while (this.currentPrintingIndex < this.stringsToPrint.Count)
			{
				string s = (string)this.stringsToPrint[this.currentPrintingIndex++];
				e.Graphics.DrawString(s, this.listBox1.Font, brush, (float)e.MarginBounds.Left, (float)(e.MarginBounds.Top + this.y));
				this.y += this.listBox1.Font.Height + 2;
				if (this.y >= num)
				{
					break;
				}
			}
			e.HasMorePages = (this.currentPrintingIndex < this.stringsToPrint.Count);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00009D84 File Offset: 0x00008D84
		private void btn_print_Click(object sender, EventArgs e)
		{
			bool printPreviewOnly = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			DataView dataView;
			if (this.listBox1.DataSource is DataTable)
			{
				dataView = new DataView((DataTable)this.listBox1.DataSource);
			}
			else if (this.listBox1.DataSource is DataView)
			{
				dataView = (DataView)this.listBox1.DataSource;
			}
			else
			{
				dataView = null;
			}
			if (dataView != null)
			{
				int columnIndex = dataView.Table.Columns.IndexOf(this.listBox1.DisplayMember);
				this.stringsToPrint = new ArrayList();
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string value = row[columnIndex].ToString().Trim();
					this.stringsToPrint.Add(value);
				}
				this.printDocument = new PrintDocument();
				this.printDocument.PrintPage += this.printDocument_PrintPage;
				this.printDialog = new PrintDialog();
				this.printDialog.UseEXDialog = true;
				this.printDialog.Document = this.printDocument;
				this.Print(printPreviewOnly);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009F1C File Offset: 0x00008F1C
		private void btn_up_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItems.Count > 1)
			{
				int num = 0;
				bool flag = false;
				ArrayList arrayList = new ArrayList(this.listBox1.SelectedItems.Count);
				foreach (object obj in this.listBox1.SelectedItems)
				{
					DataRowView dataRowView = (DataRowView)obj;
					arrayList.Add(dataRowView);
				}
				foreach (object obj2 in arrayList)
				{
					DataRowView dataRowView = (DataRowView)obj2;
					if (this.listBox1.Items.IndexOf(dataRowView) != num)
					{
						DataRowView dataRowView2 = (DataRowView)this.listBox1.Items[num];
						DataRow row = dataRowView.Row;
						DataRow row2 = dataRowView2.Row;
						this.SwapDataRows(row, row2);
						flag = true;
					}
					num++;
				}
				if (!flag)
				{
					for (int i = arrayList.Count - 2; i >= 0; i--)
					{
						DataRowView dataRowView3 = (DataRowView)this.listBox1.Items[arrayList.Count - 1];
						DataRowView dataRowView4 = (DataRowView)arrayList[i];
						this.SwapDataRows(dataRowView3.Row, dataRowView4.Row);
					}
				}
				for (int i = 0; i < this.listBox1.Items.Count; i++)
				{
					this.listBox1.SetSelected(i, i < arrayList.Count);
				}
			}
			else if (this.listBox1.SelectedItems.Count > 0)
			{
				int num2 = this.listBox1.SelectedIndices[0];
				if (num2 > 0)
				{
					DataRowView dataRowView5 = (DataRowView)this.listBox1.Items[num2];
					DataRowView dataRowView2 = (DataRowView)this.listBox1.Items[num2 - 1];
					DataRow row = dataRowView5.Row;
					DataRow row2 = dataRowView2.Row;
					this.SwapDataRows(row, row2);
					this.listBox1.SetSelected(0, false);
					this.listBox1.SetSelected(num2, false);
					this.listBox1.SetSelected(num2 - 1, true);
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A1F0 File Offset: 0x000091F0
		private void btn_down_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedItems.Count > 1)
			{
				int num = 0;
				bool flag = false;
				ArrayList arrayList = new ArrayList(this.listBox1.SelectedItems.Count);
				foreach (object obj in this.listBox1.SelectedItems)
				{
					DataRowView dataRowView = (DataRowView)obj;
					arrayList.Add(dataRowView);
				}
				foreach (object obj2 in arrayList)
				{
					DataRowView dataRowView = (DataRowView)obj2;
					if (this.listBox1.Items.IndexOf(dataRowView) != num)
					{
						DataRowView dataRowView2 = (DataRowView)this.listBox1.Items[num];
						DataRow row = dataRowView.Row;
						DataRow row2 = dataRowView2.Row;
						this.SwapDataRows(row, row2);
						flag = true;
					}
					num++;
				}
				if (!flag)
				{
					for (int i = 1; i < arrayList.Count; i++)
					{
						DataRowView dataRowView3 = (DataRowView)this.listBox1.Items[0];
						DataRowView dataRowView4 = (DataRowView)this.listBox1.Items[i];
						this.SwapDataRows(dataRowView3.Row, dataRowView4.Row);
					}
				}
				for (int i = 0; i < this.listBox1.Items.Count; i++)
				{
					this.listBox1.SetSelected(i, i < arrayList.Count);
				}
			}
			else if (this.listBox1.SelectedItems.Count > 0)
			{
				int num2 = this.listBox1.SelectedIndices[0];
				if (num2 < this.listBox1.Items.Count - 1)
				{
					DataRowView dataRowView5 = (DataRowView)this.listBox1.Items[num2];
					DataRowView dataRowView2 = (DataRowView)this.listBox1.Items[num2 + 1];
					DataRow row = dataRowView5.Row;
					DataRow row2 = dataRowView2.Row;
					this.SwapDataRows(row, row2);
					this.listBox1.SetSelected(0, false);
					this.listBox1.SetSelected(num2, false);
					this.listBox1.SetSelected(num2 + 1, true);
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A4D4 File Offset: 0x000094D4
		private void btn_selectAll_Click(object sender, EventArgs e)
		{
			this.SelectAll(true);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A4DF File Offset: 0x000094DF
		private void btn_selectNone_Click(object sender, EventArgs e)
		{
			this.SelectAll(false);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A4EA File Offset: 0x000094EA
		private void btn_ok_Click(object sender, EventArgs e)
		{
			this.OKClicked();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A4F4 File Offset: 0x000094F4
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A500 File Offset: 0x00009500
		public void AddButtonRemoveEh(object btn, EventHandler eh)
		{
			if (btn != null && btn is ToolStripButton)
			{
				ToolStripButton toolStripButton = (ToolStripButton)btn;
				toolStripButton.Click -= eh;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A538 File Offset: 0x00009538
		public object AddButton(string text, int resourceImageIndex, EventHandler eh)
		{
			string[] manifestResourceNames = base.GetType().Assembly.GetManifestResourceNames();
			int length = manifestResourceNames.GetLength(0);
			Image image;
			if (length > 0 && resourceImageIndex <= length)
			{
				Bitmap bitmap = new Bitmap(base.GetType().Assembly.GetManifestResourceStream(manifestResourceNames[resourceImageIndex]));
				image = bitmap;
			}
			else
			{
				image = null;
			}
			ToolStripButton toolStripButton = new ToolStripButton(text, image, eh);
			this.toolStrip1.Items.Add(toolStripButton);
			return toolStripButton;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A5B6 File Offset: 0x000095B6
		public void RemoveCancelButton()
		{
			this.btn_cancel.Visible = false;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000A5C8 File Offset: 0x000095C8
		public ToolStripButton btn_OK2
		{
			get
			{
				return this.btn_ok;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000A5E0 File Offset: 0x000095E0
		public ToolStripButton btn_cancel2
		{
			get
			{
				return this.btn_cancel;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000A5F8 File Offset: 0x000095F8
		private void InputList_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000A5FB File Offset: 0x000095FB
		private void listBox1_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000A5FE File Offset: 0x000095FE
		private void listBox1_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000A604 File Offset: 0x00009604
		protected override bool ProcessCmdKey(ref Message m, Keys k)
		{
			bool result;
			if (m.Msg == 256 && k == Keys.Return)
			{
				this.btn_ok_Click(this.btn_ok, new EventArgs());
				result = true;
			}
			else
			{
				result = base.ProcessCmdKey(ref m, k);
			}
			return result;
		}

		// Token: 0x0400013F RID: 319
		private ArrayList selectedIndices;

		// Token: 0x04000140 RID: 320
		private bool allowReordering;

		// Token: 0x04000141 RID: 321
		private ArrayList stringsToPrint;

		// Token: 0x04000142 RID: 322
		private PrintDialog printDialog;

		// Token: 0x04000143 RID: 323
		private PrintDocument printDocument;

		// Token: 0x04000144 RID: 324
		private int y;

		// Token: 0x04000145 RID: 325
		private int currentPrintingIndex;
	}
}
