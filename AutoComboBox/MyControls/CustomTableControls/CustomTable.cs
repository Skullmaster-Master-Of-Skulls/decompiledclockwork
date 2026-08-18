using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200009F RID: 159
	public class CustomTable : UserControl
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x000317B4 File Offset: 0x000307B4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000317EC File Offset: 0x000307EC
		private void InitializeComponent()
		{
			this.dg_Table = new DataGridView();
			this.btn_Add = new Button();
			this.btn_Edit = new Button();
			this.btn_Remove = new Button();
			((ISupportInitialize)this.dg_Table).BeginInit();
			base.SuspendLayout();
			this.dg_Table.AllowUserToAddRows = false;
			this.dg_Table.AllowUserToDeleteRows = false;
			this.dg_Table.AllowUserToOrderColumns = true;
			this.dg_Table.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.dg_Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dg_Table.EditMode = DataGridViewEditMode.EditProgrammatically;
			this.dg_Table.Location = new Point(0, 0);
			this.dg_Table.Name = "dg_Table";
			this.dg_Table.RowHeadersVisible = false;
			this.dg_Table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dg_Table.Size = new Size(475, 143);
			this.dg_Table.TabIndex = 0;
			this.dg_Table.DoubleClick += this.dg_Table_DoubleClick;
			this.dg_Table.KeyDown += this.dg_Table_KeyDown;
			this.btn_Add.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.btn_Add.Location = new Point(3, 148);
			this.btn_Add.Name = "btn_Add";
			this.btn_Add.Size = new Size(79, 25);
			this.btn_Add.TabIndex = 1;
			this.btn_Add.Text = "Add Row";
			this.btn_Add.UseVisualStyleBackColor = true;
			this.btn_Add.Click += this.btn_Add_Click;
			this.btn_Edit.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.btn_Edit.Location = new Point(88, 148);
			this.btn_Edit.Name = "btn_Edit";
			this.btn_Edit.Size = new Size(82, 25);
			this.btn_Edit.TabIndex = 2;
			this.btn_Edit.Text = "Edit Row";
			this.btn_Edit.UseVisualStyleBackColor = true;
			this.btn_Edit.Click += this.btn_Edit_Click;
			this.btn_Remove.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.btn_Remove.Location = new Point(176, 148);
			this.btn_Remove.Name = "btn_Remove";
			this.btn_Remove.Size = new Size(90, 25);
			this.btn_Remove.TabIndex = 3;
			this.btn_Remove.Text = "Remove Rows";
			this.btn_Remove.UseVisualStyleBackColor = true;
			this.btn_Remove.Click += this.btn_Remove_Click;
			base.AutoScaleDimensions = new SizeF(6f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.btn_Remove);
			base.Controls.Add(this.btn_Edit);
			base.Controls.Add(this.btn_Add);
			base.Controls.Add(this.dg_Table);
			this.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Name = "CustomTable";
			base.Size = new Size(476, 176);
			base.Load += this.CustomTable_Load;
			((ISupportInitialize)this.dg_Table).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00031B92 File Offset: 0x00030B92
		public CustomTable(TableProperty TableProperty)
		{
			this.__tp = TableProperty;
			this.InitializeComponent();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00031BB4 File Offset: 0x00030BB4
		private void CustomTable_Load(object sender, EventArgs e)
		{
			foreach (ColumnDefinition cd in this.__tp.ColumnDefinitions)
			{
				this.dg_Table.Columns.Add(this.getColumn(cd));
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00031C00 File Offset: 0x00030C00
		private DataGridViewColumn getColumn(ColumnDefinition cd)
		{
			DataGridViewColumn dataGridViewColumn;
			if (cd.ColumnTypeEnum == ColumnTypeDefEnum.CHECKBOX)
			{
				dataGridViewColumn = new DataGridViewCheckBoxColumn();
			}
			else
			{
				dataGridViewColumn = new DataGridViewTextBoxColumn();
			}
			dataGridViewColumn.Name = cd.ColumnName;
			return dataGridViewColumn;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00031C42 File Offset: 0x00030C42
		private void dg_Table_DoubleClick(object sender, EventArgs e)
		{
			this.editSelectedRow();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00031C4C File Offset: 0x00030C4C
		private void btn_Edit_Click(object sender, EventArgs e)
		{
			this.editSelectedRow();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00031C58 File Offset: 0x00030C58
		private void editSelectedRow()
		{
			if (this.dg_Table.SelectedRows.Count == 1)
			{
				RowEditForm rowEditForm = new RowEditForm(this.__tp, this.dg_Table.SelectedRows[0], true);
				rowEditForm.ShowDialog();
			}
			else
			{
				MessageBox.Show("Please select exactly 1 row to edit");
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00031CB8 File Offset: 0x00030CB8
		private void btn_Add_Click(object sender, EventArgs e)
		{
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			foreach (ColumnDefinition columnDefinition in this.__tp.ColumnDefinitions)
			{
				DataGridViewCell dataGridViewCell;
				if (columnDefinition.ColumnType is CheckBoxDef)
				{
					dataGridViewCell = new DataGridViewCheckBoxCell
					{
						Selected = false
					};
				}
				else
				{
					dataGridViewCell = new DataGridViewTextBoxCell
					{
						Value = ""
					};
				}
				cells.Add(dataGridViewCell);
			}
			RowEditForm rowEditForm = new RowEditForm(this.__tp, dataGridViewRow, false);
			if (rowEditForm.ShowDialog() == DialogResult.OK)
			{
				this.dg_Table.Rows.Add(dataGridViewRow);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00031D87 File Offset: 0x00030D87
		private void btn_Remove_Click(object sender, EventArgs e)
		{
			this.removeSelectedRows();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00031D94 File Offset: 0x00030D94
		private void dg_Table_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.removeSelectedRows();
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00031DBC File Offset: 0x00030DBC
		private void removeSelectedRows()
		{
			foreach (object obj in this.dg_Table.SelectedRows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				this.dg_Table.Rows.Remove(dataGridViewRow);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00031E34 File Offset: 0x00030E34
		public TablePersistObject TablePersistObject
		{
			get
			{
				TablePersistObject tablePersistObject = new TablePersistObject();
				ColumnDefinition[] columnDefinitions = this.__tp.ColumnDefinitions;
				int num = columnDefinitions.Length;
				int[] array = new int[num];
				ColumnTypeManager instance = ColumnTypeManager.getInstance();
				for (int i = 0; i < num; i++)
				{
					array[i] = instance.getColumnID(columnDefinitions[i]);
				}
				DataGridViewRowCollection rows = this.dg_Table.Rows;
				int count = rows.Count;
				RowPersistObject[] array2 = new RowPersistObject[count];
				for (int j = 0; j < count; j++)
				{
					array2[j] = new RowPersistObject();
					DataGridViewRow dataGridViewRow = rows[j];
					int count2 = dataGridViewRow.Cells.Count;
					CellPersistObject[] array3 = new CellPersistObject[count2];
					for (int i = 0; i < num; i++)
					{
						DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[i];
						array3[i] = new CellPersistObject();
						array3[i].ColumnID = array[i];
						array3[i].Data = dataGridViewCell.Value;
					}
					array2[j].Cells = array3;
				}
				tablePersistObject.Rows = array2;
				return tablePersistObject;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00031F68 File Offset: 0x00030F68
		public void importRows(TablePersistObject t)
		{
			ColumnDefinition[] columnDefinitions = this.__tp.ColumnDefinitions;
			int num = columnDefinitions.Length;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			ColumnTypeManager instance = ColumnTypeManager.getInstance();
			for (int i = 0; i < num; i++)
			{
				dictionary.Add(instance.getColumnID(columnDefinitions[i]), i);
			}
			foreach (RowPersistObject rowPersistObject in t.Rows)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				DataGridViewCell[] array = new DataGridViewCell[num];
				foreach (CellPersistObject cellPersistObject in rowPersistObject.Cells)
				{
					int num2 = dictionary[cellPersistObject.ColumnID];
					ColumnTypeDefEnum columnTypeEnum = columnDefinitions[num2].ColumnTypeEnum;
					if (columnTypeEnum != ColumnTypeDefEnum.CHECKBOX)
					{
						array[num2] = new DataGridViewTextBoxCell();
						array[num2].Value = cellPersistObject.Data;
					}
					else
					{
						array[num2] = new DataGridViewCheckBoxCell();
						array[num2].Value = cellPersistObject.Data;
					}
				}
				dataGridViewRow.Cells.AddRange(array);
				this.dg_Table.Rows.Add(dataGridViewRow);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000320A8 File Offset: 0x000310A8
		public void recreatTableFromTablePersistObject(TablePersistObject t)
		{
			this.clearTable();
			this.importRows(t);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000320BA File Offset: 0x000310BA
		public void clearTable()
		{
			this.dg_Table.Rows.Clear();
		}

		// Token: 0x040004DB RID: 1243
		private IContainer components = null;

		// Token: 0x040004DC RID: 1244
		private DataGridView dg_Table;

		// Token: 0x040004DD RID: 1245
		private Button btn_Add;

		// Token: 0x040004DE RID: 1246
		private Button btn_Edit;

		// Token: 0x040004DF RID: 1247
		private Button btn_Remove;

		// Token: 0x040004E0 RID: 1248
		private TableProperty __tp;
	}
}
