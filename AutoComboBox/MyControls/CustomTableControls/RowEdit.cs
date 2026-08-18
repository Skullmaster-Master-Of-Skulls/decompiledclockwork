using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000103 RID: 259
	public class RowEdit : UserControl
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0004E418 File Offset: 0x0004D418
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0004E450 File Offset: 0x0004D450
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "RowEdit";
			base.Size = new Size(457, 301);
			base.Load += this.RowEdit_Load;
			base.ResumeLayout(false);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0004E4C0 File Offset: 0x0004D4C0
		public RowEdit(TableProperty tp, DataGridViewRow row, bool showApply)
		{
			this.InitializeComponent();
			Control parent = base.Parent;
			while (parent != null && !(parent is Form))
			{
				parent = parent.Parent;
			}
			if (parent == null)
			{
				throw new ArgumentException("I cannot find a form to close");
			}
			this.customizeComponents(tp, row, parent as Form, showApply);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0004E540 File Offset: 0x0004D540
		public RowEdit(TableProperty tp, DataGridViewRow row, Form parent, bool showApply)
		{
			this.InitializeComponent();
			this.customizeComponents(tp, row, parent, showApply);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004E570 File Offset: 0x0004D570
		private void customizeComponents(TableProperty tp, DataGridViewRow row, Form parent, bool showApply)
		{
			this.__tp = tp;
			this.__row = row;
			this.__pForm = parent;
			this.bPanel = new BtmPanel(parent, new BtmPanel.BoolReturnMethod(this.commit), new BtmPanel.VoidReturnMethod(this.resume), showApply);
			this.bPanel.Dock = DockStyle.Bottom;
			base.Controls.Add(this.bPanel);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0004E5D8 File Offset: 0x0004D5D8
		private void RowEdit_Load(object sender, EventArgs e)
		{
			ColumnDefinition[] columnDefinitions = this.__tp.ColumnDefinitions;
			List<Label> list = new List<Label>();
			int num = 0;
			foreach (ColumnDefinition colDef in columnDefinitions)
			{
				Label label = this.getNameLabel(colDef);
				if (label.Width > num)
				{
					num = label.Width;
				}
				list.Add(label);
			}
			int num2 = 5;
			int j = 0;
			while (j < columnDefinitions.Length)
			{
				Label label = list[j];
				int num3 = 5 + num;
				label.Location = new Point(2, num2);
				label.TextAlign = ContentAlignment.MiddleRight;
				base.Controls.Add(label);
				this.putEditingComponent(new Point(num3 + 5, num2), columnDefinitions[j].ColumnType, this.__row.Cells[j].Value);
				num2 += 20;
				j++;
				num2 += 20;
			}
			base.Size = new Size(5 + num + 5 + 200 + 5, num2 + 20 + this.bPanel.Size.Height);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0004E718 File Offset: 0x0004D718
		private Label getNameLabel(ColumnDefinition colDef)
		{
			Label label = new Label();
			Graphics graphics = label.CreateGraphics();
			label.Text = colDef.ColumnName;
			label.Width = (int)graphics.MeasureString(colDef.ColumnName, label.Font).Width + 6;
			label.TextAlign = ContentAlignment.MiddleRight;
			return label;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0004E774 File Offset: 0x0004D774
		private void putEditingComponent(Point start, ColumnTypeDef colType, object value)
		{
			Control control;
			if (colType is CheckBoxDef)
			{
				control = new CheckBox
				{
					Checked = (value != null && (bool)value)
				};
			}
			else if (colType is DroplistDef)
			{
				ComboBox comboBox = new ComboBox();
				string[] selections = (colType as DroplistDef).Selections;
				comboBox.Items.AddRange(selections);
				int i;
				for (i = 0; i < selections.Length; i++)
				{
					if (selections[i].Equals(value))
					{
						comboBox.SelectedIndex = i;
						break;
					}
				}
				if (i == selections.Length)
				{
					comboBox.Text = (value as string);
				}
				control = comboBox;
			}
			else
			{
				control = new TextBox();
				control.Text = (value as string);
			}
			control.Location = start;
			control.Width = 200;
			base.Controls.Add(control);
			this.editingControls.Add(control);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0004E888 File Offset: 0x0004D888
		private bool commit()
		{
			ColumnDefinition[] columnDefinitions = this.__tp.ColumnDefinitions;
			int i = 0;
			int num = columnDefinitions.Length;
			while (i < num)
			{
				ColumnDefinition columnDefinition = columnDefinitions[i];
				ColumnTypeDefEnum columnTypeEnum = columnDefinition.ColumnTypeEnum;
				if (columnTypeEnum != ColumnTypeDefEnum.DROPLIST)
				{
					if (columnTypeEnum != ColumnTypeDefEnum.CHECKBOX)
					{
						DataGridViewTextBoxCell dataGridViewTextBoxCell = this.__row.Cells[i] as DataGridViewTextBoxCell;
						dataGridViewTextBoxCell.Value = (this.editingControls[i] as TextBox).Text;
					}
					else
					{
						DataGridViewCheckBoxCell dataGridViewCheckBoxCell = this.__row.Cells[i] as DataGridViewCheckBoxCell;
						dataGridViewCheckBoxCell.Value = (this.editingControls[i] as CheckBox).Checked;
					}
				}
				else
				{
					DataGridViewTextBoxCell dataGridViewTextBoxCell2 = this.__row.Cells[i] as DataGridViewTextBoxCell;
					dataGridViewTextBoxCell2.Value = (this.editingControls[i] as ComboBox).Text;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0004E98C File Offset: 0x0004D98C
		private void resume()
		{
		}

		// Token: 0x04000771 RID: 1905
		private const int EDIT_FIELD_LEN = 200;

		// Token: 0x04000772 RID: 1906
		private IContainer components = null;

		// Token: 0x04000773 RID: 1907
		private TableProperty __tp;

		// Token: 0x04000774 RID: 1908
		private DataGridViewRow __row;

		// Token: 0x04000775 RID: 1909
		private Form __pForm;

		// Token: 0x04000776 RID: 1910
		private BtmPanel bPanel;

		// Token: 0x04000777 RID: 1911
		private List<Control> editingControls = new List<Control>();
	}
}
