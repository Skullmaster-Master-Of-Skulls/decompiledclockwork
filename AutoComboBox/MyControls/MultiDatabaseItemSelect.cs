using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EncryptionClassLibrary;
using UnivOleDb;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000015 RID: 21
	public class MultiDatabaseItemSelect : UserControl, MyDynamicControl
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000052B7 File Offset: 0x000042B7
		public MultiDatabaseItemSelect()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000052DE File Offset: 0x000042DE
		public void HideCaption()
		{
			this.lbl.Visible = false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000052EE File Offset: 0x000042EE
		public void Initialize(string caption, DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.lbl.Text = caption;
			this.FillLv(dv, da, tripleDES);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000530C File Offset: 0x0000430C
		public void Initialize(string caption, string sql, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = sql;
			da.SelectCommand.Parameters.Clear();
			da.Fill(dataTable);
			this.Initialize(caption, dataTable.DefaultView, da, tripleDES);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005358 File Offset: 0x00004358
		public bool FilledIn
		{
			get
			{
				string text = this.ToString();
				return text.Length > 0;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000537C File Offset: 0x0000437C
		private void FillLv(DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.lv.BeginUpdate();
			this.ignoreCheckedChanged = true;
			try
			{
				byte[] array = new byte[0];
				DataTable table = dv.Table;
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text = "";
					for (int i = 1; i < table.Columns.Count; i++)
					{
						string text2;
						if (row[i] == DBNull.Value)
						{
							text2 = "";
						}
						else if (table.Columns[i].DataType == array.GetType())
						{
							text2 = tripleDES.Decrypt((byte[])row[i]);
						}
						else
						{
							text2 = row[i].ToString();
						}
						if (text2.Trim().Length > 0)
						{
							if (text.Length > 0)
							{
								text += " ";
							}
							text += text2;
						}
					}
					ListViewItem listViewItem = new ListViewItem(text);
					listViewItem.Tag = row;
					this.lv.Items.Add(listViewItem);
				}
			}
			finally
			{
				this.lv.EndUpdate();
				this.ignoreCheckedChanged = false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005554 File Offset: 0x00004554
		public object ReportObject
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000556C File Offset: 0x0000456C
		public new string ToString()
		{
			string text = "";
			foreach (object obj in this.lv.CheckedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				DataRow dataRow = (DataRow)listViewItem.Tag;
				if (text.Length > 0)
				{
					text += ",";
				}
				text += dataRow[0].ToString();
			}
			return text;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005624 File Offset: 0x00004624
		public void FromString(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(0);
			}
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (text.Trim().Length > 0)
				{
					try
					{
						list[i] = int.Parse(text);
					}
					catch
					{
					}
				}
			}
			this.ignoreCheckedChanged = true;
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				DataRow dataRow = (DataRow)listViewItem.Tag;
				int item = (int)dataRow[0];
				listViewItem.Checked = list.Contains(item);
			}
			this.ignoreCheckedChanged = false;
			this.RefreshSelectedSummary();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000576C File Offset: 0x0000476C
		public void SelectAll()
		{
			this.lv.BeginUpdate();
			try
			{
				foreach (object obj in this.lv.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					listViewItem.Checked = true;
				}
			}
			finally
			{
				this.lv.EndUpdate();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005804 File Offset: 0x00004804
		private void RefreshSelectedSummary()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.lv.CheckedItems.Count; i++)
			{
				ListViewItem listViewItem = this.lv.CheckedItems[i];
				if (i > 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.AppendFormat("• {0}", listViewItem.Text);
			}
			this.txt_selected.Text = stringBuilder.ToString();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005886 File Offset: 0x00004886
		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SelectAll(true);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005891 File Offset: 0x00004891
		private void selectnonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SelectAll(false);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000589C File Offset: 0x0000489C
		private void SelectAll(bool checkThem)
		{
			this.lv.BeginUpdate();
			this.ignoreCheckedChanged = true;
			try
			{
				foreach (object obj in this.lv.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (listViewItem != null && listViewItem.Checked != checkThem)
					{
						listViewItem.Checked = checkThem;
					}
				}
				this.RefreshSelectedSummary();
			}
			finally
			{
				this.lv.EndUpdate();
				this.ignoreCheckedChanged = false;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000595C File Offset: 0x0000495C
		private void lv_SizeChanged(object sender, EventArgs e)
		{
			int num = base.Width - 4 - SystemInformation.VerticalScrollBarWidth;
			if (num > 0)
			{
				this.lv.Columns[0].Width = num;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000599C File Offset: 0x0000499C
		private void lv_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (!this.ignoreCheckedChanged)
			{
				if (!this.multipleChecksAllowed)
				{
					if (e.Item.Checked && this.lv.CheckedIndices.Count > 1)
					{
						this.ignoreCheckedChanged = true;
						try
						{
							this.SelectAll(false);
							e.Item.Checked = true;
						}
						finally
						{
							this.ignoreCheckedChanged = false;
						}
					}
				}
				try
				{
					this.ignoreCheckedChanged = true;
					this.RefreshSelectedSummary();
				}
				finally
				{
					this.ignoreCheckedChanged = false;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00005A58 File Offset: 0x00004A58
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00005A70 File Offset: 0x00004A70
		public bool MultipleChecksAllowed
		{
			get
			{
				return this.multipleChecksAllowed;
			}
			set
			{
				this.multipleChecksAllowed = value;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005A7C File Offset: 0x00004A7C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005AB4 File Offset: 0x00004AB4
		private void InitializeComponent()
		{
			this.components = new Container();
			this.lbl = new Label();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new ToolStripMenuItem();
			this.selectnonToolStripMenuItem = new ToolStripMenuItem();
			this.panel1 = new Panel();
			this.txt_selected = new TextBox();
			this.label1 = new Label();
			this.expandableSplitter1 = new ExpandableSplitter();
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.lbl.AutoSize = true;
			this.lbl.Dock = DockStyle.Top;
			this.lbl.Location = new Point(0, 0);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(42, 16);
			this.lbl.TabIndex = 0;
			this.lbl.Text = "label1";
			this.lv.CheckBoxes = true;
			this.lv.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv.ContextMenuStrip = this.contextMenuStrip1;
			this.lv.Dock = DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HeaderStyle = ColumnHeaderStyle.None;
			this.lv.Location = new Point(0, 16);
			this.lv.Margin = new Padding(3, 4, 3, 4);
			this.lv.Name = "lv";
			this.lv.Size = new Size(282, 259);
			this.lv.TabIndex = 1;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = View.Details;
			this.lv.ItemChecked += this.lv_ItemChecked;
			this.lv.SizeChanged += this.lv_SizeChanged;
			this.columnHeader1.Width = 328;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.selectAllToolStripMenuItem,
				this.selectnonToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(153, 70);
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new Size(152, 22);
			this.selectAllToolStripMenuItem.Text = "Select &all";
			this.selectAllToolStripMenuItem.Click += this.selectAllToolStripMenuItem_Click;
			this.selectnonToolStripMenuItem.Name = "selectnonToolStripMenuItem";
			this.selectnonToolStripMenuItem.Size = new Size(152, 22);
			this.selectnonToolStripMenuItem.Text = "Select &none";
			this.selectnonToolStripMenuItem.Click += this.selectnonToolStripMenuItem_Click;
			this.panel1.Controls.Add(this.txt_selected);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Right;
			this.panel1.Location = new Point(292, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(77, 259);
			this.panel1.TabIndex = 2;
			this.txt_selected.Dock = DockStyle.Fill;
			this.txt_selected.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txt_selected.Location = new Point(0, 16);
			this.txt_selected.Multiline = true;
			this.txt_selected.Name = "txt_selected";
			this.txt_selected.ReadOnly = true;
			this.txt_selected.ScrollBars = ScrollBars.Both;
			this.txt_selected.Size = new Size(77, 243);
			this.txt_selected.TabIndex = 0;
			this.txt_selected.WordWrap = false;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Top;
			this.label1.Location = new Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(59, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Selected";
			this.expandableSplitter1.BackColor2 = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = DockStyle.Right;
			this.expandableSplitter1.ExpandableControl = this.panel1;
			this.expandableSplitter1.ExpandFillColor = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.ExpandFillColorSchemePart = 53;
			this.expandableSplitter1.ExpandLineColor = SystemColors.ControlText;
			this.expandableSplitter1.ExpandLineColorSchemePart = 40;
			this.expandableSplitter1.GripDarkColor = SystemColors.ControlText;
			this.expandableSplitter1.GripDarkColorSchemePart = 40;
			this.expandableSplitter1.GripLightColor = Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.GripLightColorSchemePart = 0;
			this.expandableSplitter1.HotBackColor = Color.FromArgb(254, 142, 75);
			this.expandableSplitter1.HotBackColor2 = Color.FromArgb(255, 207, 139);
			this.expandableSplitter1.HotBackColor2SchemePart = 35;
			this.expandableSplitter1.HotBackColorSchemePart = 34;
			this.expandableSplitter1.HotExpandFillColor = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotExpandFillColorSchemePart = 53;
			this.expandableSplitter1.HotExpandLineColor = SystemColors.ControlText;
			this.expandableSplitter1.HotExpandLineColorSchemePart = 40;
			this.expandableSplitter1.HotGripDarkColor = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotGripDarkColorSchemePart = 53;
			this.expandableSplitter1.HotGripLightColor = Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.HotGripLightColorSchemePart = 0;
			this.expandableSplitter1.Location = new Point(282, 16);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new Size(10, 259);
			this.expandableSplitter1.TabIndex = 3;
			this.expandableSplitter1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.lv);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.lbl);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MultiDatabaseItemSelect";
			base.Size = new Size(369, 275);
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400007E RID: 126
		private DataView dv;

		// Token: 0x0400007F RID: 127
		private bool ignoreCheckedChanged = false;

		// Token: 0x04000080 RID: 128
		private bool multipleChecksAllowed = true;

		// Token: 0x04000081 RID: 129
		private IContainer components = null;

		// Token: 0x04000082 RID: 130
		private Label lbl;

		// Token: 0x04000083 RID: 131
		private ListView lv;

		// Token: 0x04000084 RID: 132
		private ColumnHeader columnHeader1;

		// Token: 0x04000085 RID: 133
		private ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000086 RID: 134
		private ToolStripMenuItem selectAllToolStripMenuItem;

		// Token: 0x04000087 RID: 135
		private ToolStripMenuItem selectnonToolStripMenuItem;

		// Token: 0x04000088 RID: 136
		private Panel panel1;

		// Token: 0x04000089 RID: 137
		private TextBox txt_selected;

		// Token: 0x0400008A RID: 138
		private Label label1;

		// Token: 0x0400008B RID: 139
		private ExpandableSplitter expandableSplitter1;
	}
}
