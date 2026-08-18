using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200010B RID: 267
	internal partial class SqlDataSourceConfigureSortForm : DesignerForm
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x0003A494 File Offset: 0x00038694
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0003A7A8 File Offset: 0x000389A8
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.ConfigureSort";
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0003A7AF File Offset: 0x000389AF
		public IList<SqlDataSourceOrderClause> OrderClauses
		{
			get
			{
				return this._tableQuery.OrderClauses;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0003B2D8 File Offset: 0x000394D8
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

		// Token: 0x06000997 RID: 2455 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0003B4C4 File Offset: 0x000396C4
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

		// Token: 0x06000999 RID: 2457 RVA: 0x0003B584 File Offset: 0x00039784
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

		// Token: 0x0600099A RID: 2458 RVA: 0x0003B644 File Offset: 0x00039844
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

		// Token: 0x0600099B RID: 2459 RVA: 0x000357ED File Offset: 0x000339ED
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0003B6BB File Offset: 0x000398BB
		private void OnSortAscendingRadioButton1CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0003B6BB File Offset: 0x000398BB
		private void OnSortAscendingRadioButton2CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0003B6BB File Offset: 0x000398BB
		private void OnSortAscendingRadioButton3CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateOrderClauses();
			this.UpdatePreview();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0003B6CC File Offset: 0x000398CC
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

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003B730 File Offset: 0x00039930
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

		// Token: 0x060009A1 RID: 2465 RVA: 0x0003B870 File Offset: 0x00039A70
		private void UpdatePreview()
		{
			SqlDataSourceQuery selectQuery = this._tableQuery.GetSelectQuery();
			this._previewTextBox.Text = ((selectQuery == null) ? string.Empty : selectQuery.Command);
		}

		// Token: 0x040005C6 RID: 1478
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x040005C7 RID: 1479
		private SqlDataSourceTableQuery _tableQuery;

		// Token: 0x040005C8 RID: 1480
		private bool _loadingClauses;

		// Token: 0x0200043F RID: 1087
		private sealed class ColumnItem
		{
			// Token: 0x060028F6 RID: 10486 RVA: 0x000F93A7 File Offset: 0x000F75A7
			public ColumnItem(DesignerDataColumn designerDataColumn)
			{
				this._designerDataColumn = designerDataColumn;
			}

			// Token: 0x170008A6 RID: 2214
			// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000F93B6 File Offset: 0x000F75B6
			public DesignerDataColumn DesignerDataColumn
			{
				get
				{
					return this._designerDataColumn;
				}
			}

			// Token: 0x060028F8 RID: 10488 RVA: 0x000F93BE File Offset: 0x000F75BE
			public override string ToString()
			{
				if (this._designerDataColumn != null)
				{
					return this._designerDataColumn.Name;
				}
				return SR.GetString("SqlDataSourceConfigureSortForm_SortNone");
			}

			// Token: 0x04001D14 RID: 7444
			private DesignerDataColumn _designerDataColumn;
		}
	}
}
