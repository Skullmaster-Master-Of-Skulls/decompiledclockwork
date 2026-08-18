using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200008B RID: 139
	public class MyMaskedTextBox : UserControl, MyDynamicControl
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0002EC70 File Offset: 0x0002DC70
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002ECA8 File Offset: 0x0002DCA8
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MyMaskedTextBox));
			this.txt = new MaskedTextBox();
			this.btn = new Button();
			this.lv = new ListViewEx();
			this.columnHeader1 = new ColumnHeader();
			this.imageList1 = new ImageList(this.components);
			base.SuspendLayout();
			this.txt.BeepOnError = true;
			this.txt.Dock = DockStyle.Fill;
			this.txt.HidePromptOnLeave = true;
			this.txt.Location = new Point(1, 1);
			this.txt.Name = "txt";
			this.txt.PromptChar = ' ';
			this.txt.Size = new Size(355, 20);
			this.txt.TabIndex = 0;
			this.btn.Dock = DockStyle.Right;
			this.btn.Image = Resources.navigate_down;
			this.btn.Location = new Point(356, 1);
			this.btn.Name = "btn";
			this.btn.Size = new Size(24, 22);
			this.btn.TabIndex = 1;
			this.btn.UseVisualStyleBackColor = true;
			this.btn.Visible = false;
			this.btn.Click += this.btn_Click;
			this.lv.Border.Class = "ListViewBorder";
			this.lv.CheckBoxes = true;
			this.lv.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv.FullRowSelect = true;
			this.lv.HeaderStyle = ColumnHeaderStyle.None;
			this.lv.Location = new Point(150, 4);
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.Size = new Size(200, 140);
			this.lv.SmallImageList = this.imageList1;
			this.lv.TabIndex = 2;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = View.Details;
			this.lv.Visible = false;
			this.lv.ItemChecked += this.lv_ItemChecked;
			this.lv.SizeChanged += this.lv_SizeChanged;
			this.lv.KeyDown += this.lv_KeyDown;
			this.lv.Click += this.lv_Click;
			this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "delete.png");
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.lv);
			base.Controls.Add(this.txt);
			base.Controls.Add(this.btn);
			base.Name = "MyMaskedTextBox";
			base.Padding = new Padding(1);
			base.Size = new Size(381, 24);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0002F058 File Offset: 0x0002E058
		public new string ToString()
		{
			return this.Text;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002F070 File Offset: 0x0002E070
		public void FromString(string s)
		{
			this.Text = s;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0002F07C File Offset: 0x0002E07C
		public object ReportObject
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002F094 File Offset: 0x0002E094
		public MyMaskedTextBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0002F0B4 File Offset: 0x0002E0B4
		public bool FilledIn
		{
			get
			{
				return this.Text.Trim().Length > 0;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0002F0DC File Offset: 0x0002E0DC
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0002F0F9 File Offset: 0x0002E0F9
		public new Font Font
		{
			get
			{
				return this.txt.Font;
			}
			set
			{
				this.txt.Font = this.Font;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0002F110 File Offset: 0x0002E110
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0002F12D File Offset: 0x0002E12D
		public override string Text
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0002F140 File Offset: 0x0002E140
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0002F15D File Offset: 0x0002E15D
		public string Mask
		{
			get
			{
				return this.txt.Mask;
			}
			set
			{
				this.txt.Mask = value;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002F170 File Offset: 0x0002E170
		public void ConvertToMultiSelectList(DataView lookupList)
		{
			this.txt.ReadOnly = true;
			this.btn.Visible = true;
			this.lv.SuspendLayout();
			this.lv.BeginUpdate();
			foreach (object obj in lookupList)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				string text = row["LookupText"].ToString();
				ListViewItem listViewItem = new ListViewItem(text);
				if (text.Trim().Length > 0)
				{
					listViewItem.ImageIndex = -1;
				}
				else
				{
					listViewItem.ImageIndex = 0;
				}
				this.lv.Items.Add(listViewItem);
			}
			this.lv.ResumeLayout();
			this.lv.EndUpdate();
			this.lv.Visible = false;
			this.lv.KeyDown += this.lv_KeyDown;
			this.lv.ItemChecked += this.lv_ItemChecked;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0002F2B8 File Offset: 0x0002E2B8
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0002F2D5 File Offset: 0x0002E2D5
		public bool ReadOnly
		{
			get
			{
				return this.txt.ReadOnly;
			}
			set
			{
				this.txt.ReadOnly = value;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002F2E8 File Offset: 0x0002E2E8
		private void lv_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (!this.ignoreItemChecked)
			{
				this.RefreshText();
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002F308 File Offset: 0x0002E308
		private void RefreshText()
		{
			string text = "";
			foreach (object obj in this.lv.CheckedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (text.Length > 0)
				{
					text += "; ";
				}
				text += listViewItem.Text;
			}
			this.Text = text;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0002F3A8 File Offset: 0x0002E3A8
		private void RefreshChecked()
		{
			this.ignoreItemChecked = true;
			foreach (object obj in this.lv.CheckedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = false;
			}
			string[] array = this.Text.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string text2 = text.Trim();
				this.CheckItem(text2);
			}
			this.ignoreItemChecked = false;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002F47C File Offset: 0x0002E47C
		private void CheckItem(string text)
		{
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.Text.Trim().CompareTo(text) == 0)
				{
					listViewItem.Checked = true;
					break;
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002F508 File Offset: 0x0002E508
		private void lv_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.lv.Visible = false;
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002F538 File Offset: 0x0002E538
		private void btn_Click(object sender, EventArgs e)
		{
			Point position = Cursor.Position;
			Control control = this.FindRealParentForLv();
			if (control != null)
			{
				this.lv.Parent = control;
				this.lv.BringToFront();
			}
			Point location = this.lv.Parent.PointToClient(position);
			this.lv.Location = location;
			this.RefreshChecked();
			this.lv.Visible = true;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002F5AC File Offset: 0x0002E5AC
		private Control FindRealParentForLv()
		{
			Control parent = this.lv.Parent;
			Control result;
			if (parent == null || parent == this)
			{
				Control parent2 = base.Parent;
				while (parent2 != null)
				{
					if (parent2.Parent == null)
					{
						break;
					}
					parent2 = parent2.Parent;
					if (parent2 is Panel)
					{
						break;
					}
				}
				result = parent2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002F630 File Offset: 0x0002E630
		private void lv_SizeChanged(object sender, EventArgs e)
		{
			int num = this.lv.Width - SystemInformation.VerticalScrollBarWidth - 2;
			if (num > 0)
			{
				this.lv.Columns[0].Width = num;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002F678 File Offset: 0x0002E678
		private void lv_Click(object sender, EventArgs e)
		{
			Point p = Cursor.Position;
			p = this.lv.PointToClient(p);
			ListViewItem itemAt = this.lv.GetItemAt(p.X, p.Y);
			if (itemAt != null && itemAt.Text.Trim().Length < 1)
			{
				this.lv.Visible = false;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0002F6E4 File Offset: 0x0002E6E4
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0002F701 File Offset: 0x0002E701
		public char PromptChar
		{
			get
			{
				return this.txt.PromptChar;
			}
			set
			{
				this.txt.PromptChar = value;
			}
		}

		// Token: 0x0400049D RID: 1181
		private IContainer components = null;

		// Token: 0x0400049E RID: 1182
		private MaskedTextBox txt;

		// Token: 0x0400049F RID: 1183
		private Button btn;

		// Token: 0x040004A0 RID: 1184
		private ListViewEx lv;

		// Token: 0x040004A1 RID: 1185
		private ColumnHeader columnHeader1;

		// Token: 0x040004A2 RID: 1186
		private ImageList imageList1;

		// Token: 0x040004A3 RID: 1187
		private bool ignoreItemChecked = false;
	}
}
