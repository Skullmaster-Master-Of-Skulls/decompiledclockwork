using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004C3 RID: 1219
	internal partial class SqlDataSourceConfigureSortForm : DesignerForm
	{
		// Token: 0x06002C1C RID: 11292 RVA: 0x000F6554 File Offset: 0x000F5554
		public SqlDataSourceConfigureSortForm(SqlDataSourceDesigner sqlDataSourceDesigner, SqlDataSourceTableQuery tableQuery) : base(sqlDataSourceDesigner.Component.Site)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._tableQuery = tableQuery.Clone();
			this.InitializeComponent();
			this.InitializeUI();
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				this._loadingClauses = true;
				this._fieldComboBox1.Items.Add(new SqlDataSourceConfigureSortForm.ColumnItem(null));
				this._fieldComboBox2.Items.Add(new SqlDataSourceConfigureSortForm.ColumnItem(null));
				this._fieldComboBox3.Items.Add(new SqlDataSourceConfigureSortForm.ColumnItem(null));
				foreach (object obj in this._tableQuery.DesignerDataTable.Columns)
				{
					DesignerDataColumn designerDataColumn = (DesignerDataColumn)obj;
					this._fieldComboBox1.Items.Add(new SqlDataSourceConfigureSortForm.ColumnItem(designerDataColumn));
					this._fieldComboBox2.Items.Add(new SqlDataSourceConfigureSortForm.ColumnItem(designerDataColumn));
					this._fieldComboBox3.Items.Add(new SqlDataSourceConfigureSortForm.ColumnItem(designerDataColumn));
				}
				this._fieldComboBox1.InvalidateDropDownWidth();
				this._fieldComboBox2.InvalidateDropDownWidth();
				this._fieldComboBox3.InvalidateDropDownWidth();
				this._sortByGroupBox2.Enabled = false;
				this._sortByGroupBox3.Enabled = false;
				this._sortDirectionPanel1.Enabled = false;
				this._sortDirectionPanel2.Enabled = false;
				this._sortDirectionPanel3.Enabled = false;
				this._sortAscendingRadioButton1.Checked = true;
				this._sortAscendingRadioButton2.Checked = true;
				this._sortAscendingRadioButton3.Checked = true;
				if (this._tableQuery.OrderClauses.Count >= 1)
				{
					SqlDataSourceOrderClause sqlDataSourceOrderClause = this._tableQuery.OrderClauses[0];
					this.SelectFieldItem(this._fieldComboBox1, sqlDataSourceOrderClause.DesignerDataColumn);
					this._sortAscendingRadioButton1.Checked = !sqlDataSourceOrderClause.IsDescending;
					this._sortDescendingRadioButton1.Checked = sqlDataSourceOrderClause.IsDescending;
					if (this._tableQuery.OrderClauses.Count >= 2)
					{
						SqlDataSourceOrderClause sqlDataSourceOrderClause2 = this._tableQuery.OrderClauses[1];
						this.SelectFieldItem(this._fieldComboBox2, sqlDataSourceOrderClause2.DesignerDataColumn);
						this._sortAscendingRadioButton2.Checked = !sqlDataSourceOrderClause2.IsDescending;
						this._sortDescendingRadioButton2.Checked = sqlDataSourceOrderClause2.IsDescending;
						if (this._tableQuery.OrderClauses.Count >= 3)
						{
							SqlDataSourceOrderClause sqlDataSourceOrderClause3 = this._tableQuery.OrderClauses[2];
							this.SelectFieldItem(this._fieldComboBox3, sqlDataSourceOrderClause3.DesignerDataColumn);
							this._sortAscendingRadioButton3.Checked = !sqlDataSourceOrderClause3.IsDescending;
							this._sortDescendingRadioButton3.Checked = sqlDataSourceOrderClause3.IsDescending;
						}
					}
				}
				this._loadingClauses = false;
				this.UpdateOrderClauses();
				this.UpdatePreview();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000F6868 File Offset: 0x000F5868
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.ConfigureSort";
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000F686F File Offset: 0x000F586F
		public IList<SqlDataSourceOrderClause> OrderClauses
		{
			get
			{
				return this._tableQuery.OrderClauses;
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000F7398 File Offset: 0x000F6398
		private void InitializeUI()
		{
			this._helpLabel.Text = SR.GetString("SqlDataSourceConfigureSortForm_HelpLabel");
			this._previewLabel.Text = SR.GetString("SqlDataSource_General_PreviewLabel");
			this._sortByGroupBox1.Text = SR.GetString("SqlDataSourceConfigureSortForm_SortByLabel");
			this._sortByGroupBox2.Text = SR.GetString("SqlDataSourceConfigureSortForm_ThenByLabel");
			this._sortByGroupBox3.Text = SR.GetString("SqlDataSourceConfigureSortForm_ThenByLabel");
			this._sortAscendingRadioButton1.Text = SR.GetString("SqlDataSourceConfigureSortForm_AscendingLabel");
			this._sortDescendingRadioButton1.Text = SR.GetString("SqlDataSourceConfigureSortForm_DescendingLabel");
			this._sortAscendingRadioButton2.Text = SR.GetString("SqlDataSourceConfigureSortForm_AscendingLabel");
			this._sortDescendingRadioButton2.Text = SR.GetString("SqlDataSourceConfigureSortForm_DescendingLabel");
			this._sortAscendingRadioButton3.Text = SR.GetString("SqlDataSourceConfigureSortForm_AscendingLabel");
			this._sortDescendingRadioButton3.Text = SR.GetString("SqlDataSourceConfigureSortForm_DescendingLabel");
			this._sortAscendingRadioButton1.AccessibleDescription = SR.GetString("SqlDataSourceConfigureSortForm_SortDirection1");
			this._sortDescendingRadioButton1.AccessibleDescription = SR.GetString("SqlDataSourceConfigureSortForm_SortDirection1");
			this._sortAscendingRadioButton2.AccessibleDescription = SR.GetString("SqlDataSourceConfigureSortForm_SortDirection2");
			this._sortDescendingRadioButton2.AccessibleDescription = SR.GetString("SqlDataSourceConfigureSortForm_SortDirection2");
			this._sortAscendingRadioButton3.AccessibleDescription = SR.GetString("SqlDataSourceConfigureSortForm_SortDirection3");
			this._sortDescendingRadioButton3.AccessibleDescription = SR.GetString("SqlDataSourceConfigureSortForm_SortDirection3");
			this._fieldComboBox1.AccessibleName = SR.GetString("SqlDataSourceConfigureSortForm_SortColumn1");
			this._fieldComboBox2.AccessibleName = SR.GetString("SqlDataSourceConfigureSortForm_SortColumn2");
			this._fieldComboBox3.AccessibleName = SR.GetString("SqlDataSourceConfigureSortForm_SortColumn3");
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this.Text = SR.GetString("SqlDataSourceConfigureSortForm_Caption");
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000F7583 File Offset: 0x000F6583
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000F7594 File Offset: 0x000F6594
		private void OnFieldComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._fieldComboBox1.SelectedIndex == -1 || (this._fieldComboBox1.SelectedIndex == 0 && ((SqlDataSourceConfigureSortForm.ColumnItem)this._fieldComboBox1.Items[0]).DesignerDataColumn == null))
			{
				this._sortDirectionPanel1.Enabled = false;
				this._sortAscendingRadioButton1.Checked = true;
				this._fieldComboBox2.SelectedIndex = -1;
				this._sortAscendingRadioButton2.Checked = true;
				this._sortByGroupBox2.Enabled = false;
				this._fieldComboBox2.Enabled = false;
			}
			else
			{
				this._sortDirectionPanel1.Enabled = true;
				this._sortByGroupBox2.Enabled = true;
				this._fieldComboBox2.Enabled = true;
			}
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000F7654 File Offset: 0x000F6654
		private void OnFieldComboBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._fieldComboBox2.SelectedIndex == -1 || (this._fieldComboBox2.SelectedIndex == 0 && ((SqlDataSourceConfigureSortForm.ColumnItem)this._fieldComboBox2.Items[0]).DesignerDataColumn == null))
			{
				this._sortDirectionPanel2.Enabled = false;
				this._sortAscendingRadioButton2.Checked = true;
				this._fieldComboBox3.SelectedIndex = -1;
				this._sortAscendingRadioButton3.Checked = true;
				this._sortByGroupBox3.Enabled = false;
				this._fieldComboBox3.Enabled = false;
			}
			else
			{
				this._sortDirectionPanel2.Enabled = true;
				this._sortByGroupBox3.Enabled = true;
				this._fieldComboBox3.Enabled = true;
			}
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000F7714 File Offset: 0x000F6714
		private void OnFieldComboBox3SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._fieldComboBox3.SelectedIndex == -1 || (this._fieldComboBox3.SelectedIndex == 0 && ((SqlDataSourceConfigureSortForm.ColumnItem)this._fieldComboBox3.Items[0]).DesignerDataColumn == null))
			{
				this._sortDirectionPanel3.Enabled = false;
				this._sortAscendingRadioButton3.Checked = true;
			}
			else
			{
				this._sortDirectionPanel3.Enabled = true;
			}
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000F778B File Offset: 0x000F678B
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000F779A File Offset: 0x000F679A
		private void OnSortAscendingRadioButton1CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000F77A8 File Offset: 0x000F67A8
		private void OnSortAscendingRadioButton2CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000F77B6 File Offset: 0x000F67B6
		private void OnSortAscendingRadioButton3CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000F77C4 File Offset: 0x000F67C4
		private void SelectFieldItem(ComboBox comboBox, DesignerDataColumn field)
		{
			foreach (object obj in comboBox.Items)
			{
				SqlDataSourceConfigureSortForm.ColumnItem columnItem = (SqlDataSourceConfigureSortForm.ColumnItem)obj;
				if (columnItem.DesignerDataColumn == field)
				{
					comboBox.SelectedItem = columnItem;
					break;
				}
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000F7828 File Offset: 0x000F6828
		private void UpdateOrderClauses()
		{
			if (this._loadingClauses)
			{
				return;
			}
			this._tableQuery.OrderClauses.Clear();
			if (this._fieldComboBox1.SelectedIndex >= 1)
			{
				SqlDataSourceOrderClause item = new SqlDataSourceOrderClause(this._tableQuery.DesignerDataConnection, this._tableQuery.DesignerDataTable, ((SqlDataSourceConfigureSortForm.ColumnItem)this._fieldComboBox1.SelectedItem).DesignerDataColumn, !this._sortAscendingRadioButton1.Checked);
				this._tableQuery.OrderClauses.Add(item);
			}
			if (this._fieldComboBox2.SelectedIndex >= 1)
			{
				SqlDataSourceOrderClause item2 = new SqlDataSourceOrderClause(this._tableQuery.DesignerDataConnection, this._tableQuery.DesignerDataTable, ((SqlDataSourceConfigureSortForm.ColumnItem)this._fieldComboBox2.SelectedItem).DesignerDataColumn, !this._sortAscendingRadioButton2.Checked);
				this._tableQuery.OrderClauses.Add(item2);
			}
			if (this._fieldComboBox3.SelectedIndex >= 1)
			{
				SqlDataSourceOrderClause item3 = new SqlDataSourceOrderClause(this._tableQuery.DesignerDataConnection, this._tableQuery.DesignerDataTable, ((SqlDataSourceConfigureSortForm.ColumnItem)this._fieldComboBox3.SelectedItem).DesignerDataColumn, !this._sortAscendingRadioButton3.Checked);
				this._tableQuery.OrderClauses.Add(item3);
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000F7968 File Offset: 0x000F6968
		private void UpdatePreview()
		{
			SqlDataSourceQuery selectQuery = this._tableQuery.GetSelectQuery();
			this._previewTextBox.Text = ((selectQuery == null) ? string.Empty : selectQuery.Command);
		}

		// Token: 0x04001E14 RID: 7700
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001E15 RID: 7701
		private SqlDataSourceTableQuery _tableQuery;

		// Token: 0x04001E16 RID: 7702
		private bool _loadingClauses;

		// Token: 0x020004C4 RID: 1220
		private sealed class ColumnItem
		{
			// Token: 0x06002C2C RID: 11308 RVA: 0x000F799C File Offset: 0x000F699C
			public ColumnItem(DesignerDataColumn designerDataColumn)
			{
				this._designerDataColumn = designerDataColumn;
			}

			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000F79AB File Offset: 0x000F69AB
			public DesignerDataColumn DesignerDataColumn
			{
				get
				{
					return this._designerDataColumn;
				}
			}

			// Token: 0x06002C2E RID: 11310 RVA: 0x000F79B3 File Offset: 0x000F69B3
			public override string ToString()
			{
				if (this._designerDataColumn != null)
				{
					return this._designerDataColumn.Name;
				}
				return SR.GetString("SqlDataSourceConfigureSortForm_SortNone");
			}

			// Token: 0x04001E17 RID: 7703
			private DesignerDataColumn _designerDataColumn;
		}
	}
}
