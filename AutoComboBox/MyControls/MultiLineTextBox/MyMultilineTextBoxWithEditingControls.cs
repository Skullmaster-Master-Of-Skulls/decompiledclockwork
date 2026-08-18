using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x02000042 RID: 66
	public class MyMultilineTextBoxWithEditingControls : UserControl
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000140C4 File Offset: 0x000130C4
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000140DC File Offset: 0x000130DC
		public int WhoAmIPid
		{
			get
			{
				return this.whoAmIPid;
			}
			set
			{
				this.whoAmIPid = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000140E8 File Offset: 0x000130E8
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00014100 File Offset: 0x00013100
		public string WhoAmIName
		{
			get
			{
				return this.whoAmIName;
			}
			set
			{
				this.whoAmIName = value;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001410A File Offset: 0x0001310A
		public MyMultilineTextBoxWithEditingControls()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0001412C File Offset: 0x0001312C
		private void btn_addNote_Click(object sender, EventArgs e)
		{
			MultiLineItem item = new MultiLineItem("", this.whoAmIName, DateTime.Now);
			int num = this.txt.AddItem(item);
			if (num >= 0)
			{
				this.txt.SelectedIndex = num;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00014174 File Offset: 0x00013174
		public void SetReadOnly()
		{
			this.toolStrip1.Enabled = false;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00014184 File Offset: 0x00013184
		public MyMultilineTextBox TextBox
		{
			get
			{
				return this.txt;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001419C File Offset: 0x0001319C
		public void SetItems(string xml)
		{
			this.txt.SetItems(xml);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000141AC File Offset: 0x000131AC
		public string GetItemsAsXml()
		{
			return this.txt.GetItemsAsXml();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000141C9 File Offset: 0x000131C9
		public void Clear()
		{
			this.txt.Items.Clear();
			this.txt.Text = "";
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000141F0 File Offset: 0x000131F0
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0001420D File Offset: 0x0001320D
		public string Caption
		{
			get
			{
				return this.lbl_caption.Text;
			}
			set
			{
				this.lbl_caption.Text = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00014220 File Offset: 0x00013220
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0001423D File Offset: 0x0001323D
		public bool ReadOnly
		{
			get
			{
				return this.txt.Enabled;
			}
			set
			{
				this.txt.Enabled = !value;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00014250 File Offset: 0x00013250
		public void SetHeight(int numLines)
		{
			Graphics graphics = this.txt.CreateGraphics();
			int num = Convert.ToInt32(graphics.MeasureString("aWqpIM", this.txt.Font).Height);
			int num2 = this.lbl_caption.Height + this.toolStrip1.Height + SystemInformation.Border3DSize.Height * 2;
			int num3 = num * numLines + num2;
			if (num3 > 0)
			{
				base.Height = num3;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000142D8 File Offset: 0x000132D8
		private void btn_removeNote_Click(object sender, EventArgs e)
		{
			if (this.txt.SelectedIndex >= 0)
			{
				this.txt.RemoveItemAt(this.txt.SelectedIndex);
				this.txt.RefreshList();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00014320 File Offset: 0x00013320
		private void btn_sort_Click(object sender, EventArgs e)
		{
			if (this.lastSortedAscending)
			{
				this.txt.SortDescending();
				this.lastSortedAscending = false;
			}
			else
			{
				this.txt.SortAscending();
				this.lastSortedAscending = true;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00014368 File Offset: 0x00013368
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000143A0 File Offset: 0x000133A0
		private void InitializeComponent()
		{
			this.toolStrip1 = new ToolStrip();
			this.btn_addNote = new ToolStripButton();
			this.btn_removeNote = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.btn_sort = new ToolStripButton();
			this.lbl_caption = new Label();
			this.txt = new MyMultilineTextBox();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_addNote,
				this.btn_removeNote,
				this.toolStripSeparator1,
				this.btn_sort
			});
			this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
			this.toolStrip1.Location = new Point(0, 14);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new Padding(0, 0, 2, 0);
			this.toolStrip1.Size = new Size(507, 23);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_addNote.AccessibleDescription = "Add note";
			this.btn_addNote.AccessibleName = "Add note";
			this.btn_addNote.Image = Resources.add;
			this.btn_addNote.ImageTransparentColor = Color.Magenta;
			this.btn_addNote.Name = "btn_addNote";
			this.btn_addNote.Size = new Size(49, 20);
			this.btn_addNote.Text = "&Add";
			this.btn_addNote.Click += this.btn_addNote_Click;
			this.btn_removeNote.AccessibleDescription = "Remove note";
			this.btn_removeNote.AccessibleName = "Remove note";
			this.btn_removeNote.Image = Resources.delete;
			this.btn_removeNote.ImageTransparentColor = Color.Magenta;
			this.btn_removeNote.Name = "btn_removeNote";
			this.btn_removeNote.Size = new Size(70, 20);
			this.btn_removeNote.Text = "&Remove";
			this.btn_removeNote.Click += this.btn_removeNote_Click;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 23);
			this.btn_sort.Image = Resources.up_down;
			this.btn_sort.ImageTransparentColor = Color.Magenta;
			this.btn_sort.Name = "btn_sort";
			this.btn_sort.Size = new Size(48, 20);
			this.btn_sort.Text = "S&ort";
			this.btn_sort.Click += this.btn_sort_Click;
			this.lbl_caption.AutoSize = true;
			this.lbl_caption.Dock = DockStyle.Top;
			this.lbl_caption.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl_caption.Location = new Point(0, 0);
			this.lbl_caption.Name = "lbl_caption";
			this.lbl_caption.Size = new Size(52, 14);
			this.lbl_caption.TabIndex = 2;
			this.lbl_caption.Text = "Caption:";
			this.lbl_caption.TextAlign = ContentAlignment.BottomLeft;
			this.txt.Dock = DockStyle.Fill;
			this.txt.DrawMode = DrawMode.OwnerDrawVariable;
			this.txt.FormattingEnabled = true;
			this.txt.IsReadOnly = false;
			this.txt.Location = new Point(0, 37);
			this.txt.Margin = new Padding(4);
			this.txt.Name = "txt";
			this.txt.ScrollAlwaysVisible = true;
			this.txt.Size = new Size(507, 196);
			this.txt.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.txt);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl_caption);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(4);
			base.Name = "MyMultilineTextBoxWithEditingControls";
			base.Size = new Size(507, 233);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001F2 RID: 498
		private int whoAmIPid;

		// Token: 0x040001F3 RID: 499
		private string whoAmIName;

		// Token: 0x040001F4 RID: 500
		private bool lastSortedAscending = true;

		// Token: 0x040001F5 RID: 501
		private IContainer components = null;

		// Token: 0x040001F6 RID: 502
		private MyMultilineTextBox txt;

		// Token: 0x040001F7 RID: 503
		private ToolStrip toolStrip1;

		// Token: 0x040001F8 RID: 504
		private ToolStripButton btn_addNote;

		// Token: 0x040001F9 RID: 505
		private Label lbl_caption;

		// Token: 0x040001FA RID: 506
		private ToolStripButton btn_removeNote;

		// Token: 0x040001FB RID: 507
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x040001FC RID: 508
		private ToolStripButton btn_sort;
	}
}
