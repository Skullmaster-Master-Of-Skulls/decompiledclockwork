using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoComboBox.HelperControls
{
	// Token: 0x020000ED RID: 237
	public class FilterPanel : UserControl
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00049DBD File Offset: 0x00048DBD
		public FilterPanel()
		{
			this.InitializeComponent();
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600096C RID: 2412 RVA: 0x00049DE0 File Offset: 0x00048DE0
		// (remove) Token: 0x0600096D RID: 2413 RVA: 0x00049E1C File Offset: 0x00048E1C
		public event FilterPanel.FilterEventHandler OnFilter;

		// Token: 0x0600096E RID: 2414 RVA: 0x00049E58 File Offset: 0x00048E58
		public void Init(DataView dv, string[] colsToFilterBy)
		{
			this.dv = dv;
			int num = 3;
			DataTable table = dv.Table;
			int num2 = 0;
			foreach (string text in colsToFilterBy)
			{
				string text2;
				if (text.Length > 0 && text[text.Length - 1] == '_')
				{
					text2 = text.Substring(0, text.Length - 1);
				}
				else
				{
					text2 = text;
				}
				if (table.Columns.Contains(text2))
				{
					AutoComboBox autoComboBox = new AutoComboBox();
					DataTable dataTable = dv.ToTable(true, new string[]
					{
						text2
					});
					DataRow row = dataTable.NewRow();
					dataTable.Rows.InsertAt(row, 0);
					autoComboBox.DataSource = dataTable;
					autoComboBox.DisplayMember = text2;
					autoComboBox.Tag = text;
					Label label = new Label();
					label.Text = text2;
					this.p_filters.Controls.Add(autoComboBox);
					autoComboBox.Left = num;
					autoComboBox.Top = 3;
					this.p_filters.Controls.Add(label);
					label.Left = num;
					label.Top = autoComboBox.Top + autoComboBox.Height + 1;
					label.Font = this.lbl.Font;
					num += autoComboBox.Width + 5;
					if (num2 == 0)
					{
						num2 = label.Top + label.Height + 2 + SystemInformation.HorizontalScrollBarHeight;
					}
					autoComboBox.TextChanged += this.cmb_TextChanged;
					autoComboBox.UserSelectedSomething += this.cmb_UserSelectedSomething;
				}
			}
			if (num2 > 0 && num2 != base.Height)
			{
				base.Height = num2;
			}
			if (this.p_filters.Controls.Count < 2)
			{
				base.Visible = false;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0004A078 File Offset: 0x00049078
		private void cmb_UserSelectedSomething(object sender)
		{
			this.Filter();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0004A082 File Offset: 0x00049082
		private void cmb_TextChanged(object sender, EventArgs e)
		{
			this.Filter();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0004A08C File Offset: 0x0004908C
		private void Filter()
		{
			if (!this.ignoreFilter)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.p_filters.Controls)
				{
					Control control = (Control)obj;
					if (control is AutoComboBox)
					{
						AutoComboBox autoComboBox = (AutoComboBox)control;
						string colname = (string)autoComboBox.Tag;
						this.AddFilter(ref stringBuilder, autoComboBox, colname);
					}
				}
				this.Filter(stringBuilder.ToString());
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0004A154 File Offset: 0x00049154
		private void AddFilter(ref StringBuilder sb, AutoComboBox cmb, string colname0)
		{
			string text = cmb.Text;
			if (text.Length > 0)
			{
				bool flag;
				string text2;
				if (colname0[colname0.Length - 1] == '_')
				{
					flag = true;
					text2 = colname0.Substring(0, colname0.Length - 1);
				}
				else
				{
					flag = false;
					text2 = colname0;
				}
				DataColumn dataColumn = this.dv.Table.Columns[text2];
				if (dataColumn != null)
				{
					if (sb.Length > 0)
					{
						sb.Append(" AND ");
					}
					sb.Append(text2);
					if (dataColumn.DataType == typeof(string))
					{
						if (flag)
						{
							sb.Append(" LIKE '%" + text + "%'");
						}
						else
						{
							sb.Append(" LIKE '" + text + "%'");
						}
					}
					else
					{
						sb.Append("='" + text + "'");
					}
				}
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0004A27C File Offset: 0x0004927C
		private void button1_Click(object sender, EventArgs e)
		{
			this.ignoreFilter = true;
			foreach (object obj in this.p_filters.Controls)
			{
				Control control = (Control)obj;
				if (control is AutoComboBox)
				{
					AutoComboBox autoComboBox = (AutoComboBox)control;
					autoComboBox.Text = "";
				}
			}
			this.Filter("");
			this.ignoreFilter = false;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004A324 File Offset: 0x00049324
		private void Filter(string filter)
		{
			if (this.OnFilter != null)
			{
				this.OnFilter(this, new EventArgs(), filter);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0004A354 File Offset: 0x00049354
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0004A38C File Offset: 0x0004938C
		private void InitializeComponent()
		{
			this.button1 = new Button();
			this.p_filters = new Panel();
			this.lbl = new Label();
			this.p_filters.SuspendLayout();
			base.SuspendLayout();
			this.button1.Dock = DockStyle.Left;
			this.button1.Location = new Point(0, 0);
			this.button1.Margin = new Padding(3, 4, 3, 4);
			this.button1.Name = "button1";
			this.button1.Size = new Size(59, 69);
			this.button1.TabIndex = 0;
			this.button1.Text = "&Clear filters";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			this.p_filters.AutoScroll = true;
			this.p_filters.Controls.Add(this.lbl);
			this.p_filters.Dock = DockStyle.Fill;
			this.p_filters.Location = new Point(59, 0);
			this.p_filters.Name = "p_filters";
			this.p_filters.Size = new Size(116, 69);
			this.p_filters.TabIndex = 1;
			this.lbl.AutoSize = true;
			this.lbl.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl.Location = new Point(26, 26);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(39, 14);
			this.lbl.TabIndex = 0;
			this.lbl.Text = "label1";
			this.lbl.Visible = false;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.p_filters);
			base.Controls.Add(this.button1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "FilterPanel";
			base.Size = new Size(175, 69);
			this.p_filters.ResumeLayout(false);
			this.p_filters.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040006D5 RID: 1749
		private DataView dv;

		// Token: 0x040006D7 RID: 1751
		private bool ignoreFilter = false;

		// Token: 0x040006D8 RID: 1752
		private IContainer components = null;

		// Token: 0x040006D9 RID: 1753
		private Button button1;

		// Token: 0x040006DA RID: 1754
		private Panel p_filters;

		// Token: 0x040006DB RID: 1755
		private Label lbl;

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x06000978 RID: 2424
		public delegate void FilterEventHandler(object sender, EventArgs e, string filter);
	}
}
