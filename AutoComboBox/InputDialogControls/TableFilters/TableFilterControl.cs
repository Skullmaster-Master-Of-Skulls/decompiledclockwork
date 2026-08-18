using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox.InputDialogControls.TableFilters
{
	// Token: 0x020000AE RID: 174
	public class TableFilterControl : UserControl
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0003437D File Offset: 0x0003337D
		public TableFilterControl()
		{
			this.InitializeComponent();
			this.dataSource = null;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0003439D File Offset: 0x0003339D
		public TableFilterControl(object dataSource)
		{
			this.InitializeComponent();
			this.DataSource = dataSource;
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600067E RID: 1662 RVA: 0x000343C0 File Offset: 0x000333C0
		// (remove) Token: 0x0600067F RID: 1663 RVA: 0x000343FC File Offset: 0x000333FC
		public event EventHandler RemoveItem;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000680 RID: 1664 RVA: 0x00034438 File Offset: 0x00033438
		// (remove) Token: 0x06000681 RID: 1665 RVA: 0x00034474 File Offset: 0x00033474
		public event EventHandler AddItem;

		// Token: 0x06000682 RID: 1666 RVA: 0x000344B0 File Offset: 0x000334B0
		private void TableFilterControl_Load(object sender, EventArgs e)
		{
			string[] names = Enum.GetNames(typeof(TableFilterComparerType));
			this.cmb_comparerType.BeginUpdate();
			foreach (string text in names)
			{
				string item = text.Replace('_', ' ');
				this.cmb_comparerType.Items.Add(item);
			}
			this.cmb_comparerType.EndUpdate();
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00034528 File Offset: 0x00033528
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00034540 File Offset: 0x00033540
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
				this.cmb_cols.BeginUpdate();
				this.cmb_cols.Items.Clear();
				DataView dataView;
				if (this.dataSource is DataTable)
				{
					dataView = ((DataTable)this.dataSource).DefaultView;
				}
				else if (this.dataSource is DataView)
				{
					dataView = (DataView)this.dataSource;
				}
				else
				{
					dataView = null;
				}
				if (dataView != null)
				{
					foreach (object obj in dataView.Table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						this.cmb_cols.Items.Add(dataColumn.ColumnName);
					}
				}
				this.cmb_cols.EndUpdate();
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00034648 File Offset: 0x00033648
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00034768 File Offset: 0x00033768
		public TableFilter TableFilter
		{
			get
			{
				string colName;
				if (this.cmb_cols.SelectedItem != null)
				{
					colName = (string)this.cmb_cols.SelectedItem;
				}
				else
				{
					colName = "";
				}
				TableFilterComparerType comparerType;
				if (this.cmb_comparerType.SelectedItem != null)
				{
					string text = (string)this.cmb_comparerType.SelectedItem;
					string text2 = text.Replace(' ', '_');
					comparerType = TableFilterComparerType.Equals;
					foreach (object obj in Enum.GetValues(typeof(TableFilterComparerType)))
					{
						TableFilterComparerType tableFilterComparerType = (TableFilterComparerType)obj;
						string strB = tableFilterComparerType.ToString();
						if (text2.CompareTo(strB) == 0)
						{
							comparerType = tableFilterComparerType;
							break;
						}
					}
				}
				else
				{
					comparerType = TableFilterComparerType.Equals;
				}
				string text3 = this.txt_val.Text;
				return new TableFilter(colName, comparerType, text3);
			}
			set
			{
				this.SelectComboItem(this.cmb_cols, value.ColName);
				string val = value.ComparerType.ToString().Replace('_', ' ');
				this.SelectComboItem(this.cmb_comparerType, val);
				this.txt_val.Text = value.Val;
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000347C4 File Offset: 0x000337C4
		private void SelectComboItem(ComboBoxEx cmb, string val)
		{
			foreach (object obj in cmb.Items)
			{
				string text = (string)obj;
				if (text.CompareTo(val) == 0)
				{
					cmb.SelectedItem = text;
					break;
				}
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00034840 File Offset: 0x00033840
		private void btn_add_Click(object sender, EventArgs e)
		{
			if (this.AddItem != null)
			{
				this.AddItem(this, new EventArgs());
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00034870 File Offset: 0x00033870
		private void btn_remove_Click(object sender, EventArgs e)
		{
			if (this.RemoveItem != null)
			{
				this.RemoveItem(this, new EventArgs());
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000348A0 File Offset: 0x000338A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000348D8 File Offset: 0x000338D8
		private void InitializeComponent()
		{
			this.cmb_cols = new ComboBoxEx();
			this.cmb_comparerType = new ComboBoxEx();
			this.txt_val = new TextBoxX();
			this.label1 = new Label();
			this.label2 = new Label();
			this.btn_remove = new ButtonX();
			this.btn_add = new ButtonX();
			this.label3 = new Label();
			this.label4 = new Label();
			base.SuspendLayout();
			this.cmb_cols.DisplayMember = "Text";
			this.cmb_cols.Dock = DockStyle.Left;
			this.cmb_cols.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmb_cols.FormattingEnabled = true;
			this.cmb_cols.ItemHeight = 16;
			this.cmb_cols.Location = new Point(5, 5);
			this.cmb_cols.Margin = new Padding(3, 4, 3, 4);
			this.cmb_cols.Name = "cmb_cols";
			this.cmb_cols.Size = new Size(220, 22);
			this.cmb_cols.TabIndex = 0;
			this.cmb_comparerType.DisplayMember = "Text";
			this.cmb_comparerType.Dock = DockStyle.Left;
			this.cmb_comparerType.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmb_comparerType.FormattingEnabled = true;
			this.cmb_comparerType.ItemHeight = 16;
			this.cmb_comparerType.Location = new Point(259, 5);
			this.cmb_comparerType.Margin = new Padding(3, 4, 3, 4);
			this.cmb_comparerType.Name = "cmb_comparerType";
			this.cmb_comparerType.Size = new Size(140, 22);
			this.cmb_comparerType.TabIndex = 1;
			this.txt_val.Border.Class = "TextBoxBorder";
			this.txt_val.Dock = DockStyle.Fill;
			this.txt_val.Location = new Point(433, 5);
			this.txt_val.Margin = new Padding(3, 4, 3, 4);
			this.txt_val.Name = "txt_val";
			this.txt_val.Size = new Size(170, 22);
			this.txt_val.TabIndex = 2;
			this.label1.Dock = DockStyle.Left;
			this.label1.Location = new Point(225, 5);
			this.label1.Name = "label1";
			this.label1.Size = new Size(34, 22);
			this.label1.TabIndex = 3;
			this.label2.Dock = DockStyle.Left;
			this.label2.Location = new Point(399, 5);
			this.label2.Name = "label2";
			this.label2.Size = new Size(34, 22);
			this.label2.TabIndex = 4;
			this.btn_remove.AccessibleRole = AccessibleRole.PushButton;
			this.btn_remove.ColorTable = 3;
			this.btn_remove.Dock = DockStyle.Right;
			this.btn_remove.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_remove.Location = new Point(658, 5);
			this.btn_remove.Name = "btn_remove";
			this.btn_remove.Size = new Size(32, 22);
			this.btn_remove.TabIndex = 6;
			this.btn_remove.Text = "-";
			this.btn_remove.Click += this.btn_remove_Click;
			this.btn_add.AccessibleRole = AccessibleRole.PushButton;
			this.btn_add.ColorTable = 3;
			this.btn_add.Dock = DockStyle.Right;
			this.btn_add.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_add.Location = new Point(620, 5);
			this.btn_add.Name = "btn_add";
			this.btn_add.Size = new Size(32, 22);
			this.btn_add.TabIndex = 7;
			this.btn_add.Text = "+";
			this.btn_add.Click += this.btn_add_Click;
			this.label3.Dock = DockStyle.Right;
			this.label3.Location = new Point(652, 5);
			this.label3.Name = "label3";
			this.label3.Size = new Size(6, 22);
			this.label3.TabIndex = 8;
			this.label4.Dock = DockStyle.Right;
			this.label4.Location = new Point(603, 5);
			this.label4.Name = "label4";
			this.label4.Size = new Size(17, 22);
			this.label4.TabIndex = 9;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.Fixed3D;
			base.Controls.Add(this.txt_val);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.btn_add);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cmb_comparerType);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmb_cols);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btn_remove);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "TableFilterControl";
			base.Padding = new Padding(5, 5, 5, 5);
			base.Size = new Size(695, 32);
			base.Load += this.TableFilterControl_Load;
			base.ResumeLayout(false);
		}

		// Token: 0x04000520 RID: 1312
		private object dataSource;

		// Token: 0x04000521 RID: 1313
		private IContainer components = null;

		// Token: 0x04000522 RID: 1314
		private ComboBoxEx cmb_cols;

		// Token: 0x04000523 RID: 1315
		private ComboBoxEx cmb_comparerType;

		// Token: 0x04000524 RID: 1316
		private TextBoxX txt_val;

		// Token: 0x04000525 RID: 1317
		private Label label1;

		// Token: 0x04000526 RID: 1318
		private Label label2;

		// Token: 0x04000527 RID: 1319
		private ButtonX btn_remove;

		// Token: 0x04000528 RID: 1320
		private ButtonX btn_add;

		// Token: 0x04000529 RID: 1321
		private Label label3;

		// Token: 0x0400052A RID: 1322
		private Label label4;
	}
}
