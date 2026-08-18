using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200010A RID: 266
	internal class SqlDataSourceConfigureSelectPanel : WizardPanel
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x00037AD2 File Offset: 0x00035CD2
		public SqlDataSourceConfigureSelectPanel(SqlDataSourceDesigner sqlDataSourceDesigner)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00037AF4 File Offset: 0x00035CF4
		private static string GetOldValuesFormatString(SqlDataSource sqlDataSource, bool adjustForOptimisticConcurrency)
		{
			string result;
			try
			{
				string text = string.Format(CultureInfo.InvariantCulture, sqlDataSource.OldValuesParameterFormatString, new object[]
				{
					"test"
				});
				if (string.Equals(text, sqlDataSource.OldValuesParameterFormatString, StringComparison.Ordinal))
				{
					result = (adjustForOptimisticConcurrency ? "original_{0}" : "{0}");
				}
				else if (adjustForOptimisticConcurrency && string.Equals("test", text))
				{
					result = "original_{0}";
				}
				else
				{
					result = sqlDataSource.OldValuesParameterFormatString;
				}
			}
			catch
			{
				result = (adjustForOptimisticConcurrency ? "original_{0}" : "{0}");
			}
			return result;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00037B88 File Offset: 0x00035D88
		private void InitializeComponent()
		{
			this._retrieveDataLabel = new System.Windows.Forms.Label();
			this._tableRadioButton = new System.Windows.Forms.RadioButton();
			this._customSqlRadioButton = new System.Windows.Forms.RadioButton();
			this._advancedOptionsButton = new System.Windows.Forms.Button();
			this._previewTextBox = new System.Windows.Forms.TextBox();
			this._previewLabel = new System.Windows.Forms.Label();
			this._tableNameLabel = new System.Windows.Forms.Label();
			this._addSortButton = new System.Windows.Forms.Button();
			this._addFilterButton = new System.Windows.Forms.Button();
			this._selectDistinctCheckBox = new System.Windows.Forms.CheckBox();
			this._fieldsCheckedListBox = new CheckedListBox();
			this._fieldsLabel = new System.Windows.Forms.Label();
			this._tablesComboBox = new AutoSizeComboBox();
			this._columnsTableLayoutPanel = new TableLayoutPanel();
			this._optionsTableLayoutPanel = new TableLayoutPanel();
			this._fieldChooserPanel = new System.Windows.Forms.Panel();
			this._columnsTableLayoutPanel.SuspendLayout();
			this._optionsTableLayoutPanel.SuspendLayout();
			this._fieldChooserPanel.SuspendLayout();
			base.SuspendLayout();
			this._retrieveDataLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._retrieveDataLabel.Location = new Point(0, 0);
			this._retrieveDataLabel.Name = "_retrieveDataLabel";
			this._retrieveDataLabel.Size = new Size(544, 16);
			this._retrieveDataLabel.TabIndex = 10;
			this._customSqlRadioButton.Location = new Point(7, 19);
			this._customSqlRadioButton.Name = "_customSqlRadioButton";
			this._customSqlRadioButton.Size = new Size(537, 18);
			this._customSqlRadioButton.TabIndex = 20;
			this._customSqlRadioButton.CheckedChanged += this.OnCustomSqlRadioButtonCheckedChanged;
			this._tableRadioButton.Location = new Point(7, 38);
			this._tableRadioButton.Name = "_tableRadioButton";
			this._tableRadioButton.Size = new Size(537, 18);
			this._tableRadioButton.TabIndex = 30;
			this._tableRadioButton.CheckedChanged += this.OnTableRadioButtonCheckedChanged;
			this._fieldChooserPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._fieldChooserPanel.Controls.Add(this._tableNameLabel);
			this._fieldChooserPanel.Controls.Add(this._tablesComboBox);
			this._fieldChooserPanel.Controls.Add(this._fieldsLabel);
			this._fieldChooserPanel.Controls.Add(this._columnsTableLayoutPanel);
			this._fieldChooserPanel.Controls.Add(this._previewLabel);
			this._fieldChooserPanel.Controls.Add(this._previewTextBox);
			this._fieldChooserPanel.Location = new Point(25, 58);
			this._fieldChooserPanel.Name = "_fieldChooserPanel";
			this._fieldChooserPanel.Size = new Size(519, 216);
			this._fieldChooserPanel.TabIndex = 40;
			this._tableNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._tableNameLabel.Location = new Point(0, 0);
			this._tableNameLabel.Name = "_tableNameLabel";
			this._tableNameLabel.Size = new Size(519, 16);
			this._tableNameLabel.TabIndex = 10;
			this._tablesComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._tablesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._tablesComboBox.Location = new Point(0, 16);
			this._tablesComboBox.Name = "_tablesComboBox";
			this._tablesComboBox.Size = new Size(263, 21);
			this._tablesComboBox.TabIndex = 20;
			this._tablesComboBox.SelectedIndexChanged += this.OnTablesComboBoxSelectedIndexChanged;
			this._fieldsLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._fieldsLabel.Location = new Point(0, 42);
			this._fieldsLabel.Name = "_fieldsLabel";
			this._fieldsLabel.Size = new Size(519, 16);
			this._fieldsLabel.TabIndex = 30;
			this._columnsTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._columnsTableLayoutPanel.ColumnCount = 2;
			this._columnsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this._columnsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this._columnsTableLayoutPanel.Controls.Add(this._optionsTableLayoutPanel, 0, 1);
			this._columnsTableLayoutPanel.Controls.Add(this._fieldsCheckedListBox, 0, 0);
			this._columnsTableLayoutPanel.Controls.Add(this._selectDistinctCheckBox, 1, 0);
			this._columnsTableLayoutPanel.Location = new Point(0, 58);
			this._columnsTableLayoutPanel.Name = "_columnsTableLayoutPanel";
			this._columnsTableLayoutPanel.RowCount = 2;
			this._columnsTableLayoutPanel.RowStyles.Add(new RowStyle());
			this._columnsTableLayoutPanel.RowStyles.Add(new RowStyle());
			this._columnsTableLayoutPanel.Size = new Size(519, 100);
			this._columnsTableLayoutPanel.TabIndex = 40;
			this._previewLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._previewLabel.Location = new Point(0, 164);
			this._previewLabel.Name = "_previewLabel";
			this._previewLabel.Size = new Size(519, 16);
			this._previewLabel.TabIndex = 50;
			this._previewTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._previewTextBox.BackColor = SystemColors.Control;
			this._previewTextBox.Location = new Point(0, 180);
			this._previewTextBox.Multiline = true;
			this._previewTextBox.Name = "_previewTextBox";
			this._previewTextBox.ReadOnly = true;
			this._previewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._previewTextBox.Size = new Size(519, 36);
			this._previewTextBox.TabIndex = 60;
			this._previewTextBox.Text = "";
			this._fieldsCheckedListBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._fieldsCheckedListBox.CheckOnClick = true;
			this._fieldsCheckedListBox.IntegralHeight = false;
			this._fieldsCheckedListBox.Location = new Point(0, 0);
			this._fieldsCheckedListBox.MultiColumn = true;
			this._fieldsCheckedListBox.Name = "_fieldsCheckedListBox";
			this._fieldsCheckedListBox.Margin = new Padding(0, 0, 3, 0);
			this._columnsTableLayoutPanel.SetRowSpan(this._fieldsCheckedListBox, 2);
			this._fieldsCheckedListBox.Size = new Size(388, 100);
			this._fieldsCheckedListBox.TabIndex = 10;
			this._fieldsCheckedListBox.ItemCheck += this.OnFieldsCheckedListBoxItemCheck;
			this._selectDistinctCheckBox.AutoSize = true;
			this._selectDistinctCheckBox.Location = new Point(394, 2);
			this._selectDistinctCheckBox.Margin = new Padding(3, 0, 0, 0);
			this._selectDistinctCheckBox.Name = "_selectDistinctCheckBox";
			this._selectDistinctCheckBox.Size = new Size(15, 14);
			this._selectDistinctCheckBox.TabIndex = 20;
			this._selectDistinctCheckBox.CheckedChanged += this.OnSelectDistinctCheckBoxCheckedChanged;
			this._optionsTableLayoutPanel.AutoSize = true;
			this._optionsTableLayoutPanel.ColumnCount = 1;
			this._optionsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this._optionsTableLayoutPanel.Controls.Add(this._addFilterButton, 0, 0);
			this._optionsTableLayoutPanel.Controls.Add(this._addSortButton, 0, 1);
			this._optionsTableLayoutPanel.Controls.Add(this._advancedOptionsButton, 0, 2);
			this._optionsTableLayoutPanel.Location = new Point(394, 19);
			this._optionsTableLayoutPanel.Margin = new Padding(3, 0, 0, 0);
			this._optionsTableLayoutPanel.Name = "_optionsTableLayoutPanel";
			this._optionsTableLayoutPanel.RowCount = 3;
			this._optionsTableLayoutPanel.RowStyles.Add(new RowStyle());
			this._optionsTableLayoutPanel.RowStyles.Add(new RowStyle());
			this._optionsTableLayoutPanel.RowStyles.Add(new RowStyle());
			this._optionsTableLayoutPanel.Size = new Size(115, 81);
			this._optionsTableLayoutPanel.TabIndex = 30;
			this._addFilterButton.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this._addFilterButton.AutoSize = true;
			this._addFilterButton.Location = new Point(0, 2);
			this._addFilterButton.Margin = new Padding(0, 2, 0, 2);
			this._addFilterButton.MinimumSize = new Size(115, 23);
			this._addFilterButton.Name = "_addFilterButton";
			this._addFilterButton.Size = new Size(115, 23);
			this._addFilterButton.TabIndex = 10;
			this._addFilterButton.Click += this.OnAddFilterButtonClick;
			this._addSortButton.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this._addSortButton.AutoSize = true;
			this._addSortButton.Location = new Point(0, 29);
			this._addSortButton.Margin = new Padding(0, 2, 0, 2);
			this._addSortButton.MinimumSize = new Size(115, 23);
			this._addSortButton.Name = "_addSortButton";
			this._addSortButton.Size = new Size(115, 23);
			this._addSortButton.TabIndex = 20;
			this._addSortButton.Click += this.OnAddSortButtonClick;
			this._advancedOptionsButton.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this._advancedOptionsButton.AutoSize = true;
			this._advancedOptionsButton.Location = new Point(0, 56);
			this._advancedOptionsButton.Margin = new Padding(0, 2, 0, 2);
			this._advancedOptionsButton.MinimumSize = new Size(115, 23);
			this._advancedOptionsButton.Name = "_advancedOptionsButton";
			this._advancedOptionsButton.Size = new Size(115, 23);
			this._advancedOptionsButton.TabIndex = 30;
			this._advancedOptionsButton.Click += this.OnAdvancedOptionsButtonClick;
			base.Controls.Add(this._fieldChooserPanel);
			base.Controls.Add(this._customSqlRadioButton);
			base.Controls.Add(this._tableRadioButton);
			base.Controls.Add(this._retrieveDataLabel);
			base.Name = "SqlDataSourceConfigureSelectPanel";
			base.Size = new Size(544, 274);
			this._columnsTableLayoutPanel.ResumeLayout(false);
			this._columnsTableLayoutPanel.PerformLayout();
			this._optionsTableLayoutPanel.ResumeLayout(false);
			this._optionsTableLayoutPanel.PerformLayout();
			this._fieldChooserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00038644 File Offset: 0x00036844
		private DesignerDataColumn GetColumnFromTable(DesignerDataTableBase designerDataTable, string columnName)
		{
			foreach (object obj in designerDataTable.Columns)
			{
				DesignerDataColumn designerDataColumn = (DesignerDataColumn)obj;
				if (designerDataColumn.Name == columnName)
				{
					return designerDataColumn;
				}
			}
			return null;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000386AC File Offset: 0x000368AC
		private void InitializeUI()
		{
			base.Caption = SR.GetString("SqlDataSourceConfigureSelectPanel_PanelCaption");
			this._retrieveDataLabel.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_RetrieveDataLabel");
			this._tableRadioButton.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_TableLabel");
			this._customSqlRadioButton.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_CustomSqlLabel");
			this._previewLabel.Text = SR.GetString("SqlDataSource_General_PreviewLabel");
			this._tableNameLabel.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_TableNameLabel");
			this._addSortButton.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_SortButton");
			this._addFilterButton.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_FilterLabel");
			this._selectDistinctCheckBox.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_SelectDistinctLabel");
			this._advancedOptionsButton.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_AdvancedOptions");
			this._fieldsLabel.Text = SR.GetString("SqlDataSourceConfigureSelectPanel_FieldsLabel");
			this._tableRadioButton.AccessibleDescription = this._retrieveDataLabel.Text;
			this._tableRadioButton.AccessibleName = this._tableRadioButton.Text;
			this._customSqlRadioButton.AccessibleDescription = this._retrieveDataLabel.Text;
			this._customSqlRadioButton.AccessibleName = this._customSqlRadioButton.Text;
			this.UpdateFonts();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000387FC File Offset: 0x000369FC
		private bool LoadTableQueryState(Hashtable tableQueryState)
		{
			SqlDataSource sqlDataSource = this._sqlDataSourceDesigner.Component as SqlDataSource;
			int num = (int)tableQueryState["Conn_ConnectionStringHash"];
			string a = (string)tableQueryState["Conn_ProviderName"];
			if (num != this._dataConnection.ConnectionString.GetHashCode() || a != this._dataConnection.ProviderName)
			{
				return false;
			}
			int num2 = (int)tableQueryState["Generate_Mode"];
			string b = (string)tableQueryState["Table_Name"];
			SqlDataSourceConfigureSelectPanel.TableItem tableItem = null;
			foreach (object obj in this._tablesComboBox.Items)
			{
				SqlDataSourceConfigureSelectPanel.TableItem tableItem2 = (SqlDataSourceConfigureSelectPanel.TableItem)obj;
				if (tableItem2.DesignerDataTable.Name == b)
				{
					tableItem = tableItem2;
					break;
				}
			}
			if (tableItem == null)
			{
				return false;
			}
			DesignerDataTableBase designerDataTable = tableItem.DesignerDataTable;
			int num3 = (int)tableQueryState["Fields_Count"];
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < num3; i++)
			{
				string columnName = (string)tableQueryState["Fields_FieldName" + i.ToString(CultureInfo.InvariantCulture)];
				DesignerDataColumn columnFromTable = this.GetColumnFromTable(designerDataTable, columnName);
				if (columnFromTable == null)
				{
					return false;
				}
				arrayList.Add(columnFromTable);
			}
			bool flag = (bool)tableQueryState["AsteriskField"];
			bool flag2 = (bool)tableQueryState["Distinct"];
			List<Parameter> list = new List<Parameter>();
			foreach (object obj2 in sqlDataSource.SelectParameters)
			{
				ICloneable cloneable = (ICloneable)obj2;
				list.Add((Parameter)cloneable.Clone());
			}
			DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this._dataConnection.ProviderName);
			bool flag3 = SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory);
			int num4 = (int)tableQueryState["Filters_Count"];
			ArrayList arrayList2 = new ArrayList();
			for (int j = 0; j < num4; j++)
			{
				string columnName2 = (string)tableQueryState["Filters_FieldName" + j.ToString(CultureInfo.InvariantCulture)];
				string operatorFormat = (string)tableQueryState["Filters_OperatorFormat" + j.ToString(CultureInfo.InvariantCulture)];
				bool isBinary = (bool)tableQueryState["Filters_IsBinary" + j.ToString(CultureInfo.InvariantCulture)];
				string value = (string)tableQueryState["Filters_Value" + j.ToString(CultureInfo.InvariantCulture)];
				string text = (string)tableQueryState["Filters_ParameterName" + j.ToString(CultureInfo.InvariantCulture)];
				DesignerDataColumn columnFromTable2 = this.GetColumnFromTable(designerDataTable, columnName2);
				if (columnFromTable2 == null)
				{
					return false;
				}
				Parameter parameter = null;
				if (text != null)
				{
					if (flag3)
					{
						foreach (Parameter parameter2 in list)
						{
							if (parameter2.Name == text)
							{
								parameter = parameter2;
								break;
							}
						}
						if (parameter != null)
						{
							list.Remove(parameter);
						}
						else
						{
							parameter = new Parameter(text);
						}
					}
					else if (list.Count > 0)
					{
						parameter = list[0];
						list.RemoveAt(0);
					}
					else
					{
						parameter = new Parameter(text);
					}
				}
				arrayList2.Add(new SqlDataSourceFilterClause(this._dataConnection, designerDataTable, columnFromTable2, operatorFormat, isBinary, value, parameter));
			}
			int num5 = (int)tableQueryState["Orders_Count"];
			ArrayList arrayList3 = new ArrayList();
			for (int k = 0; k < num5; k++)
			{
				string columnName3 = (string)tableQueryState["Orders_FieldName" + k.ToString(CultureInfo.InvariantCulture)];
				bool isDescending = (bool)tableQueryState["Orders_IsDescending" + k.ToString(CultureInfo.InvariantCulture)];
				DesignerDataColumn columnFromTable3 = this.GetColumnFromTable(designerDataTable, columnName3);
				if (columnFromTable3 == null)
				{
					return false;
				}
				arrayList3.Add(new SqlDataSourceOrderClause(this._dataConnection, designerDataTable, columnFromTable3, isDescending));
			}
			SqlDataSourceTableQuery sqlDataSourceTableQuery = new SqlDataSourceTableQuery(this._dataConnection, designerDataTable);
			foreach (object obj3 in arrayList)
			{
				DesignerDataColumn item = (DesignerDataColumn)obj3;
				sqlDataSourceTableQuery.Fields.Add(item);
			}
			sqlDataSourceTableQuery.AsteriskField = flag;
			sqlDataSourceTableQuery.Distinct = flag2;
			foreach (object obj4 in arrayList2)
			{
				SqlDataSourceFilterClause item2 = (SqlDataSourceFilterClause)obj4;
				sqlDataSourceTableQuery.FilterClauses.Add(item2);
			}
			foreach (object obj5 in arrayList3)
			{
				SqlDataSourceOrderClause item3 = (SqlDataSourceOrderClause)obj5;
				sqlDataSourceTableQuery.OrderClauses.Add(item3);
			}
			bool includeOldValues = num2 == 2;
			string oldValuesFormatString = SqlDataSourceConfigureSelectPanel.GetOldValuesFormatString(sqlDataSource, false);
			SqlDataSourceQuery selectQuery = sqlDataSourceTableQuery.GetSelectQuery();
			SqlDataSourceQuery insertQuery = sqlDataSourceTableQuery.GetInsertQuery();
			SqlDataSourceQuery updateQuery = sqlDataSourceTableQuery.GetUpdateQuery(oldValuesFormatString, includeOldValues);
			SqlDataSourceQuery deleteQuery = sqlDataSourceTableQuery.GetDeleteQuery(oldValuesFormatString, includeOldValues);
			if (selectQuery != null && sqlDataSource.SelectCommand != selectQuery.Command)
			{
				return false;
			}
			if (insertQuery != null && sqlDataSource.InsertCommand.Trim().Length > 0 && sqlDataSource.InsertCommand != insertQuery.Command)
			{
				return false;
			}
			if (updateQuery != null && sqlDataSource.UpdateCommand.Trim().Length > 0 && sqlDataSource.UpdateCommand != updateQuery.Command)
			{
				return false;
			}
			if (deleteQuery != null && sqlDataSource.DeleteCommand.Trim().Length > 0 && sqlDataSource.DeleteCommand != deleteQuery.Command)
			{
				return false;
			}
			this._tableQuery = new SqlDataSourceTableQuery(this._dataConnection, designerDataTable);
			this._tablesComboBox.SelectedItem = tableItem;
			ArrayList arrayList4 = new ArrayList();
			foreach (object obj6 in arrayList)
			{
				DesignerDataColumn designerDataColumn = (DesignerDataColumn)obj6;
				foreach (object obj7 in this._fieldsCheckedListBox.Items)
				{
					SqlDataSourceConfigureSelectPanel.ColumnItem columnItem = (SqlDataSourceConfigureSelectPanel.ColumnItem)obj7;
					if (columnItem.DesignerDataColumn == designerDataColumn)
					{
						arrayList4.Add(this._fieldsCheckedListBox.Items.IndexOf(columnItem));
					}
				}
			}
			foreach (object obj8 in arrayList4)
			{
				int index = (int)obj8;
				this._fieldsCheckedListBox.SetItemChecked(index, true);
			}
			if (flag)
			{
				this._fieldsCheckedListBox.SetItemChecked(0, true);
			}
			this._selectDistinctCheckBox.Checked = flag2;
			this._generateMode = num2;
			foreach (object obj9 in arrayList2)
			{
				SqlDataSourceFilterClause item4 = (SqlDataSourceFilterClause)obj9;
				this._tableQuery.FilterClauses.Add(item4);
			}
			foreach (object obj10 in arrayList3)
			{
				SqlDataSourceOrderClause item5 = (SqlDataSourceOrderClause)obj10;
				this._tableQuery.OrderClauses.Add(item5);
			}
			return true;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00039060 File Offset: 0x00037260
		private void OnAddFilterButtonClick(object sender, EventArgs e)
		{
			SqlDataSourceConfigureFilterForm sqlDataSourceConfigureFilterForm = new SqlDataSourceConfigureFilterForm(this._sqlDataSourceDesigner, this._tableQuery);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(this._sqlDataSourceDesigner.Component.Site, sqlDataSourceConfigureFilterForm);
			if (dialogResult == DialogResult.OK)
			{
				this._tableQuery.FilterClauses.Clear();
				foreach (SqlDataSourceFilterClause item in sqlDataSourceConfigureFilterForm.FilterClauses)
				{
					this._tableQuery.FilterClauses.Add(item);
				}
				this.UpdatePreview();
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000390FC File Offset: 0x000372FC
		private void OnAddSortButtonClick(object sender, EventArgs e)
		{
			SqlDataSourceConfigureSortForm sqlDataSourceConfigureSortForm = new SqlDataSourceConfigureSortForm(this._sqlDataSourceDesigner, this._tableQuery);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(this._sqlDataSourceDesigner.Component.Site, sqlDataSourceConfigureSortForm);
			if (dialogResult == DialogResult.OK)
			{
				this._tableQuery.OrderClauses.Clear();
				foreach (SqlDataSourceOrderClause item in sqlDataSourceConfigureSortForm.OrderClauses)
				{
					this._tableQuery.OrderClauses.Add(item);
				}
				this.UpdatePreview();
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00039198 File Offset: 0x00037398
		private void OnAdvancedOptionsButtonClick(object sender, EventArgs e)
		{
			SqlDataSourceAdvancedOptionsForm sqlDataSourceAdvancedOptionsForm = new SqlDataSourceAdvancedOptionsForm(base.ServiceProvider);
			sqlDataSourceAdvancedOptionsForm.SetAllowAutogenerate(this._tableQuery.IsPrimaryKeySelected() && !this._selectDistinctCheckBox.Checked);
			sqlDataSourceAdvancedOptionsForm.GenerateStatements = (this._generateMode > 0);
			sqlDataSourceAdvancedOptionsForm.OptimisticConcurrency = (this._generateMode == 2);
			if (this._dataConnection.ProviderName == "System.Data.SqlServerCe.4.0")
			{
				sqlDataSourceAdvancedOptionsForm.OptimisticConcurrencySupported = false;
			}
			DialogResult dialogResult = UIServiceHelper.ShowDialog(base.ServiceProvider, sqlDataSourceAdvancedOptionsForm);
			if (dialogResult == DialogResult.OK)
			{
				this._generateMode = 0;
				if (sqlDataSourceAdvancedOptionsForm.GenerateStatements)
				{
					if (sqlDataSourceAdvancedOptionsForm.OptimisticConcurrency)
					{
						this._generateMode = 2;
						return;
					}
					this._generateMode = 1;
				}
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00039248 File Offset: 0x00037448
		protected internal override void OnComplete()
		{
			if (!this._tableRadioButton.Checked)
			{
				this._sqlDataSourceDesigner.TableQueryState = null;
				return;
			}
			this._sqlDataSourceDesigner.TableQueryState = this.SaveTableQueryState();
			SqlDataSource sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			bool flag = this._generateMode == 2;
			sqlDataSource.OldValuesParameterFormatString = SqlDataSourceConfigureSelectPanel.GetOldValuesFormatString(sqlDataSource, flag);
			if (flag)
			{
				sqlDataSource.ConflictDetection = ConflictOptions.CompareAllValues;
				return;
			}
			sqlDataSource.ConflictDetection = ConflictOptions.OverwriteChanges;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000392BA File Offset: 0x000374BA
		private void OnCustomSqlRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledUI();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000392C4 File Offset: 0x000374C4
		private void OnFieldsCheckedListBoxItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (this._ignoreFieldCheckChanges)
			{
				return;
			}
			this.UpdateEnabledUI();
			base.ParentWizard.NextButton.Enabled = (e.NewValue != CheckState.Unchecked || this._fieldsCheckedListBox.CheckedItems.Count != 1);
			if (e.Index == 0 && e.NewValue == CheckState.Checked)
			{
				this._tableQuery.AsteriskField = true;
				this._ignoreFieldCheckChanges = true;
				for (int i = 1; i < this._fieldsCheckedListBox.Items.Count; i++)
				{
					this._fieldsCheckedListBox.SetItemChecked(i, false);
				}
				this._ignoreFieldCheckChanges = false;
			}
			else
			{
				this._tableQuery.AsteriskField = false;
				this._ignoreFieldCheckChanges = true;
				this._fieldsCheckedListBox.SetItemChecked(0, false);
				if (e.Index > 0)
				{
					if (e.NewValue == CheckState.Checked)
					{
						this._tableQuery.Fields.Add(((SqlDataSourceConfigureSelectPanel.ColumnItem)this._fieldsCheckedListBox.Items[e.Index]).DesignerDataColumn);
					}
					else
					{
						this._tableQuery.Fields.Remove(((SqlDataSourceConfigureSelectPanel.ColumnItem)this._fieldsCheckedListBox.Items[e.Index]).DesignerDataColumn);
					}
				}
				this._ignoreFieldCheckChanges = false;
			}
			if (!this._tableQuery.IsPrimaryKeySelected() || this._selectDistinctCheckBox.Checked)
			{
				this._generateMode = 0;
			}
			this.UpdatePreview();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0003942C File Offset: 0x0003762C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0003943C File Offset: 0x0003763C
		public override bool OnNext()
		{
			if (this._tableRadioButton.Checked)
			{
				SqlDataSourceQuery sqlDataSourceQuery = this._tableQuery.GetSelectQuery();
				if (sqlDataSourceQuery == null)
				{
					sqlDataSourceQuery = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
				}
				SqlDataSourceQuery sqlDataSourceQuery2;
				SqlDataSourceQuery sqlDataSourceQuery3;
				SqlDataSourceQuery sqlDataSourceQuery4;
				if (this._generateMode > 0)
				{
					SqlDataSource sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
					bool flag = this._generateMode == 2;
					string oldValuesFormatString = SqlDataSourceConfigureSelectPanel.GetOldValuesFormatString(sqlDataSource, flag);
					sqlDataSourceQuery2 = this._tableQuery.GetInsertQuery();
					if (sqlDataSourceQuery2 == null)
					{
						sqlDataSourceQuery2 = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					}
					sqlDataSourceQuery3 = this._tableQuery.GetUpdateQuery(oldValuesFormatString, flag);
					if (sqlDataSourceQuery3 == null)
					{
						sqlDataSourceQuery3 = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					}
					sqlDataSourceQuery4 = this._tableQuery.GetDeleteQuery(oldValuesFormatString, flag);
					if (sqlDataSourceQuery4 == null)
					{
						sqlDataSourceQuery4 = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					}
				}
				else
				{
					sqlDataSourceQuery2 = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					sqlDataSourceQuery3 = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
					sqlDataSourceQuery4 = new SqlDataSourceQuery(string.Empty, SqlDataSourceCommandType.Text, new Parameter[0]);
				}
				SqlDataSourceSummaryPanel sqlDataSourceSummaryPanel = base.NextPanel as SqlDataSourceSummaryPanel;
				if (sqlDataSourceSummaryPanel == null)
				{
					sqlDataSourceSummaryPanel = ((SqlDataSourceWizardForm)base.ParentWizard).GetSummaryPanel();
					base.NextPanel = sqlDataSourceSummaryPanel;
				}
				sqlDataSourceSummaryPanel.SetQueries(this._dataConnection, sqlDataSourceQuery, sqlDataSourceQuery2, sqlDataSourceQuery3, sqlDataSourceQuery4);
				return true;
			}
			SqlDataSourceCustomCommandPanel sqlDataSourceCustomCommandPanel = base.NextPanel as SqlDataSourceCustomCommandPanel;
			if (sqlDataSourceCustomCommandPanel == null)
			{
				sqlDataSourceCustomCommandPanel = ((SqlDataSourceWizardForm)base.ParentWizard).GetCustomCommandPanel();
				base.NextPanel = sqlDataSourceCustomCommandPanel;
			}
			SqlDataSource sqlDataSource2 = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			this._sqlDataSourceDesigner.CopyList(sqlDataSource2.SelectParameters, arrayList);
			this._sqlDataSourceDesigner.CopyList(sqlDataSource2.InsertParameters, arrayList2);
			this._sqlDataSourceDesigner.CopyList(sqlDataSource2.UpdateParameters, arrayList3);
			this._sqlDataSourceDesigner.CopyList(sqlDataSource2.DeleteParameters, arrayList4);
			sqlDataSourceCustomCommandPanel.SetQueries(this._dataConnection, new SqlDataSourceQuery(sqlDataSource2.SelectCommand, sqlDataSource2.SelectCommandType, arrayList), new SqlDataSourceQuery(sqlDataSource2.InsertCommand, sqlDataSource2.InsertCommandType, arrayList2), new SqlDataSourceQuery(sqlDataSource2.UpdateCommand, sqlDataSource2.UpdateCommandType, arrayList3), new SqlDataSourceQuery(sqlDataSource2.DeleteCommand, sqlDataSource2.DeleteCommandType, arrayList4));
			return true;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00003937 File Offset: 0x00001B37
		public override void OnPrevious()
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0003969A File Offset: 0x0003789A
		private void OnSelectDistinctCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this._tableQuery.Distinct = this._selectDistinctCheckBox.Checked;
			if (!this._tableQuery.IsPrimaryKeySelected() || this._selectDistinctCheckBox.Checked)
			{
				this._generateMode = 0;
			}
			this.UpdatePreview();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000392BA File Offset: 0x000374BA
		private void OnTableRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledUI();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000396DC File Offset: 0x000378DC
		private void OnTablesComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			SqlDataSourceConfigureSelectPanel.TableItem tableItem = this._tablesComboBox.SelectedItem as SqlDataSourceConfigureSelectPanel.TableItem;
			if (tableItem != null && this._previousTable == tableItem)
			{
				return;
			}
			Cursor value = Cursor.Current;
			this._fieldsCheckedListBox.Items.Clear();
			this._selectDistinctCheckBox.Checked = false;
			this._generateMode = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (tableItem != null)
				{
					ICollection columns = tableItem.DesignerDataTable.Columns;
					this._tableQuery = new SqlDataSourceTableQuery(this._dataConnection, tableItem.DesignerDataTable);
					this._fieldsCheckedListBox.Items.Add(new SqlDataSourceConfigureSelectPanel.ColumnItem());
					foreach (object obj in columns)
					{
						DesignerDataColumn designerDataColumn = (DesignerDataColumn)obj;
						this._fieldsCheckedListBox.Items.Add(new SqlDataSourceConfigureSelectPanel.ColumnItem(designerDataColumn));
					}
					this._tableQuery.AsteriskField = true;
					this._fieldsCheckedListBox.SetItemChecked(0, true);
				}
				else
				{
					this._tableQuery = null;
				}
				this._previousTable = tableItem;
			}
			catch (Exception ex)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, ex, SR.GetString("SqlDataSourceConfigureSelectPanel_CouldNotGetTableSchema"));
			}
			finally
			{
				UIHelper.UpdateFieldsCheckedListBoxColumnWidth(this._fieldsCheckedListBox);
				this.UpdateEnabledUI();
				this.UpdatePreview();
				Cursor.Current = value;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00039854 File Offset: 0x00037A54
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				base.ParentWizard.FinishButton.Enabled = false;
				DesignerDataConnection designerDataConnection = ((SqlDataSourceWizardForm)base.ParentWizard).DesignerDataConnection;
				if (!SqlDataSourceDesigner.ConnectionsEqual(this._dataConnection, designerDataConnection))
				{
					this._dataConnection = designerDataConnection;
					this._requiresRefresh = true;
				}
				if (this._requiresRefresh)
				{
					Cursor value = Cursor.Current;
					try
					{
						Cursor.Current = Cursors.WaitCursor;
						this._tablesComboBox.SelectedIndex = -1;
						this._tablesComboBox.Items.Clear();
						IDataEnvironment dataEnvironment = ((SqlDataSourceWizardForm)base.ParentWizard).DataEnvironment;
						IDesignerDataSchema designerDataSchema = null;
						if (this._dataConnection != null)
						{
							designerDataSchema = dataEnvironment.GetConnectionSchema(this._dataConnection);
						}
						if (designerDataSchema != null)
						{
							List<SqlDataSourceConfigureSelectPanel.TableItem> list = new List<SqlDataSourceConfigureSelectPanel.TableItem>();
							if (designerDataSchema.SupportsSchemaClass(DesignerDataSchemaClass.Tables))
							{
								ICollection schemaItems = designerDataSchema.GetSchemaItems(DesignerDataSchemaClass.Tables);
								if (schemaItems != null)
								{
									foreach (object obj in schemaItems)
									{
										DesignerDataTable designerDataTable = (DesignerDataTable)obj;
										if (!designerDataTable.Name.ToLowerInvariant().StartsWith("AspNet_".ToLowerInvariant(), StringComparison.Ordinal))
										{
											list.Add(new SqlDataSourceConfigureSelectPanel.TableItem(designerDataTable));
										}
									}
								}
							}
							if (designerDataSchema.SupportsSchemaClass(DesignerDataSchemaClass.Views))
							{
								ICollection schemaItems2 = designerDataSchema.GetSchemaItems(DesignerDataSchemaClass.Views);
								if (schemaItems2 != null)
								{
									foreach (object obj2 in schemaItems2)
									{
										DesignerDataView designerDataTable2 = (DesignerDataView)obj2;
										list.Add(new SqlDataSourceConfigureSelectPanel.TableItem(designerDataTable2));
									}
								}
							}
							list.Sort((SqlDataSourceConfigureSelectPanel.TableItem a, SqlDataSourceConfigureSelectPanel.TableItem b) => string.Compare(a.DesignerDataTable.Name, b.DesignerDataTable.Name, StringComparison.InvariantCultureIgnoreCase));
							ComboBox.ObjectCollection items = this._tablesComboBox.Items;
							object[] items2 = list.ToArray();
							items.AddRange(items2);
							this._tablesComboBox.InvalidateDropDownWidth();
						}
						if (this._tablesComboBox.Items.Count > 0)
						{
							Hashtable tableQueryState = this._sqlDataSourceDesigner.TableQueryState;
							bool flag = false;
							if (tableQueryState != null)
							{
								flag = this.LoadTableQueryState(tableQueryState);
							}
							if (!flag)
							{
								flag = this.LoadParsedSqlState();
							}
							if (!flag)
							{
								this._tablesComboBox.SelectedIndex = 0;
								SqlDataSource sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
								bool flag2 = sqlDataSource.SelectCommand.Trim().Length > 0 || sqlDataSource.InsertCommand.Trim().Length > 0 || sqlDataSource.UpdateCommand.Trim().Length > 0 || sqlDataSource.DeleteCommand.Trim().Length > 0;
								this._tableRadioButton.Checked = !flag2;
								this._customSqlRadioButton.Checked = flag2;
							}
							else
							{
								this._tableRadioButton.Checked = true;
								this._customSqlRadioButton.Checked = false;
							}
							this._tableRadioButton.Enabled = true;
						}
						else
						{
							this._customSqlRadioButton.Checked = true;
							this._tableRadioButton.Enabled = false;
						}
						this.UpdatePreview();
					}
					finally
					{
						Cursor.Current = value;
					}
					this._requiresRefresh = false;
				}
				this.UpdateEnabledUI();
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00039BD0 File Offset: 0x00037DD0
		private bool LoadParsedSqlState()
		{
			SqlDataSource sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			string[] array = SqlDataSourceCommandParser.ParseSqlString(this._sqlDataSourceDesigner.SelectCommand);
			if (array == null)
			{
				return false;
			}
			bool flag = false;
			string lastIdentifierPart = SqlDataSourceCommandParser.GetLastIdentifierPart(array[array.Length - 1]);
			if (lastIdentifierPart == null)
			{
				return false;
			}
			List<string> list = new List<string>();
			for (int i = 0; i < array.Length - 1; i++)
			{
				string lastIdentifierPart2 = SqlDataSourceCommandParser.GetLastIdentifierPart(array[i]);
				if (lastIdentifierPart2 == null)
				{
					return false;
				}
				if (lastIdentifierPart2 == "*")
				{
					flag = true;
				}
				else
				{
					if (lastIdentifierPart2.Length == 0)
					{
						return false;
					}
					list.Add(lastIdentifierPart2);
				}
			}
			if (flag && list.Count != 0)
			{
				return false;
			}
			SqlDataSourceConfigureSelectPanel.TableItem tableItem = null;
			foreach (object obj in this._tablesComboBox.Items)
			{
				SqlDataSourceConfigureSelectPanel.TableItem tableItem2 = (SqlDataSourceConfigureSelectPanel.TableItem)obj;
				if (tableItem2.DesignerDataTable.Name == lastIdentifierPart)
				{
					tableItem = tableItem2;
					break;
				}
			}
			if (tableItem == null)
			{
				return false;
			}
			DesignerDataTableBase designerDataTable = tableItem.DesignerDataTable;
			List<DesignerDataColumn> list2 = new List<DesignerDataColumn>();
			foreach (string columnName in list)
			{
				DesignerDataColumn columnFromTable = this.GetColumnFromTable(designerDataTable, columnName);
				if (columnFromTable == null)
				{
					return false;
				}
				list2.Add(columnFromTable);
			}
			bool flag2 = sqlDataSource.DeleteCommand.Trim().Length > 0 || sqlDataSource.InsertCommand.Trim().Length > 0 || sqlDataSource.UpdateCommand.Trim().Length > 0;
			if (flag2)
			{
				SqlDataSourceTableQuery sqlDataSourceTableQuery = new SqlDataSourceTableQuery(this._dataConnection, designerDataTable);
				foreach (DesignerDataColumn item in list2)
				{
					sqlDataSourceTableQuery.Fields.Add(item);
				}
				sqlDataSourceTableQuery.AsteriskField = flag;
				SqlDataSourceQuery insertQuery = sqlDataSourceTableQuery.GetInsertQuery();
				string oldValuesFormatString = SqlDataSourceConfigureSelectPanel.GetOldValuesFormatString(sqlDataSource, false);
				SqlDataSourceQuery updateQuery = sqlDataSourceTableQuery.GetUpdateQuery(oldValuesFormatString, false);
				SqlDataSourceQuery deleteQuery = sqlDataSourceTableQuery.GetDeleteQuery(oldValuesFormatString, false);
				if (insertQuery != null && sqlDataSource.InsertCommand.Trim().Length > 0 && sqlDataSource.InsertCommand != insertQuery.Command)
				{
					return false;
				}
				if (updateQuery != null && sqlDataSource.UpdateCommand.Trim().Length > 0 && sqlDataSource.UpdateCommand != updateQuery.Command)
				{
					return false;
				}
				if (deleteQuery != null && sqlDataSource.DeleteCommand.Trim().Length > 0 && sqlDataSource.DeleteCommand != deleteQuery.Command)
				{
					return false;
				}
			}
			this._tableQuery = new SqlDataSourceTableQuery(this._dataConnection, designerDataTable);
			this._tablesComboBox.SelectedItem = tableItem;
			ArrayList arrayList = new ArrayList();
			foreach (DesignerDataColumn designerDataColumn in list2)
			{
				foreach (object obj2 in this._fieldsCheckedListBox.Items)
				{
					SqlDataSourceConfigureSelectPanel.ColumnItem columnItem = (SqlDataSourceConfigureSelectPanel.ColumnItem)obj2;
					if (columnItem.DesignerDataColumn == designerDataColumn)
					{
						arrayList.Add(this._fieldsCheckedListBox.Items.IndexOf(columnItem));
					}
				}
			}
			foreach (object obj3 in arrayList)
			{
				int index = (int)obj3;
				this._fieldsCheckedListBox.SetItemChecked(index, true);
			}
			if (flag)
			{
				this._fieldsCheckedListBox.SetItemChecked(0, true);
			}
			this._generateMode = (flag2 ? 1 : 0);
			return true;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00039FF8 File Offset: 0x000381F8
		public void ResetUI()
		{
			this._tableRadioButton.Checked = true;
			this._customSqlRadioButton.Checked = false;
			this._generateMode = 0;
			this._tablesComboBox.Items.Clear();
			this._fieldsCheckedListBox.Items.Clear();
			this._requiresRefresh = true;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003A04C File Offset: 0x0003824C
		private Hashtable SaveTableQueryState()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("Conn_ConnectionStringHash", this._tableQuery.DesignerDataConnection.ConnectionString.GetHashCode());
			hashtable.Add("Conn_ProviderName", this._tableQuery.DesignerDataConnection.ProviderName);
			hashtable.Add("Generate_Mode", this._generateMode);
			hashtable.Add("Table_Name", this._tableQuery.DesignerDataTable.Name);
			hashtable.Add("Fields_Count", this._tableQuery.Fields.Count);
			for (int i = 0; i < this._tableQuery.Fields.Count; i++)
			{
				hashtable.Add("Fields_FieldName" + i.ToString(CultureInfo.InvariantCulture), this._tableQuery.Fields[i].Name);
			}
			hashtable.Add("AsteriskField", this._tableQuery.AsteriskField);
			hashtable.Add("Distinct", this._tableQuery.Distinct);
			hashtable.Add("Filters_Count", this._tableQuery.FilterClauses.Count);
			for (int j = 0; j < this._tableQuery.FilterClauses.Count; j++)
			{
				SqlDataSourceFilterClause sqlDataSourceFilterClause = this._tableQuery.FilterClauses[j];
				string str = j.ToString(CultureInfo.InvariantCulture);
				hashtable.Add("Filters_FieldName" + str, sqlDataSourceFilterClause.DesignerDataColumn.Name);
				hashtable.Add("Filters_OperatorFormat" + str, sqlDataSourceFilterClause.OperatorFormat);
				hashtable.Add("Filters_IsBinary" + str, sqlDataSourceFilterClause.IsBinary);
				hashtable.Add("Filters_Value" + str, sqlDataSourceFilterClause.Value);
				hashtable.Add("Filters_ParameterName" + str, (sqlDataSourceFilterClause.Parameter != null) ? sqlDataSourceFilterClause.Parameter.Name : null);
			}
			hashtable.Add("Orders_Count", this._tableQuery.OrderClauses.Count);
			for (int k = 0; k < this._tableQuery.OrderClauses.Count; k++)
			{
				hashtable.Add("Orders_FieldName" + k.ToString(CultureInfo.InvariantCulture), this._tableQuery.OrderClauses[k].DesignerDataColumn.Name);
				hashtable.Add("Orders_IsDescending" + k.ToString(CultureInfo.InvariantCulture), this._tableQuery.OrderClauses[k].IsDescending);
			}
			return hashtable;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0003A31C File Offset: 0x0003851C
		private void UpdateEnabledUI()
		{
			this._fieldChooserPanel.Enabled = this._tableRadioButton.Checked;
			if (this._customSqlRadioButton.Checked)
			{
				base.ParentWizard.NextButton.Enabled = true;
			}
			if (this._tableRadioButton.Checked)
			{
				base.ParentWizard.NextButton.Enabled = (this._tablesComboBox.Items.Count > 0 && this._fieldsCheckedListBox.CheckedItems.Count > 0);
				bool enabled = this._fieldsCheckedListBox.Items.Count > 0;
				this._fieldsLabel.Enabled = enabled;
				this._fieldsCheckedListBox.Enabled = enabled;
				this._selectDistinctCheckBox.Enabled = enabled;
				this._addFilterButton.Enabled = enabled;
				this._addSortButton.Enabled = enabled;
				this._advancedOptionsButton.Enabled = enabled;
				this._previewLabel.Enabled = enabled;
				this._previewTextBox.Enabled = enabled;
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003A41C File Offset: 0x0003861C
		private void UpdateFonts()
		{
			Font font = new Font(this.Font, FontStyle.Bold);
			this._retrieveDataLabel.Font = font;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0003A444 File Offset: 0x00038644
		private void UpdatePreview()
		{
			if (this._tableQuery != null)
			{
				SqlDataSourceQuery selectQuery = this._tableQuery.GetSelectQuery();
				this._previewTextBox.Text = ((selectQuery == null) ? string.Empty : selectQuery.Command);
				return;
			}
			this._previewTextBox.Text = string.Empty;
		}

		// Token: 0x04000599 RID: 1433
		private const string CompareAllValuesFormatString = "original_{0}";

		// Token: 0x0400059A RID: 1434
		private const string OverwriteChangesFormatString = "{0}";

		// Token: 0x0400059B RID: 1435
		private System.Windows.Forms.Label _retrieveDataLabel;

		// Token: 0x0400059C RID: 1436
		private System.Windows.Forms.RadioButton _tableRadioButton;

		// Token: 0x0400059D RID: 1437
		private System.Windows.Forms.RadioButton _customSqlRadioButton;

		// Token: 0x0400059E RID: 1438
		private System.Windows.Forms.Button _advancedOptionsButton;

		// Token: 0x0400059F RID: 1439
		private System.Windows.Forms.TextBox _previewTextBox;

		// Token: 0x040005A0 RID: 1440
		private System.Windows.Forms.Label _previewLabel;

		// Token: 0x040005A1 RID: 1441
		private System.Windows.Forms.Label _tableNameLabel;

		// Token: 0x040005A2 RID: 1442
		private System.Windows.Forms.Button _addSortButton;

		// Token: 0x040005A3 RID: 1443
		private System.Windows.Forms.Button _addFilterButton;

		// Token: 0x040005A4 RID: 1444
		private System.Windows.Forms.CheckBox _selectDistinctCheckBox;

		// Token: 0x040005A5 RID: 1445
		private CheckedListBox _fieldsCheckedListBox;

		// Token: 0x040005A6 RID: 1446
		private System.Windows.Forms.Label _fieldsLabel;

		// Token: 0x040005A7 RID: 1447
		private AutoSizeComboBox _tablesComboBox;

		// Token: 0x040005A8 RID: 1448
		private TableLayoutPanel _columnsTableLayoutPanel;

		// Token: 0x040005A9 RID: 1449
		private TableLayoutPanel _optionsTableLayoutPanel;

		// Token: 0x040005AA RID: 1450
		private System.Windows.Forms.Panel _fieldChooserPanel;

		// Token: 0x040005AB RID: 1451
		private bool _requiresRefresh = true;

		// Token: 0x040005AC RID: 1452
		private DesignerDataConnection _dataConnection;

		// Token: 0x040005AD RID: 1453
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x040005AE RID: 1454
		private SqlDataSourceConfigureSelectPanel.TableItem _previousTable;

		// Token: 0x040005AF RID: 1455
		private bool _ignoreFieldCheckChanges;

		// Token: 0x040005B0 RID: 1456
		private SqlDataSourceTableQuery _tableQuery;

		// Token: 0x040005B1 RID: 1457
		private int _generateMode;

		// Token: 0x0200043C RID: 1084
		private sealed class ColumnItem
		{
			// Token: 0x060028EC RID: 10476 RVA: 0x0000362F File Offset: 0x0000182F
			public ColumnItem()
			{
			}

			// Token: 0x060028ED RID: 10477 RVA: 0x000F9327 File Offset: 0x000F7527
			public ColumnItem(DesignerDataColumn designerDataColumn)
			{
				this._designerDataColumn = designerDataColumn;
			}

			// Token: 0x170008A4 RID: 2212
			// (get) Token: 0x060028EE RID: 10478 RVA: 0x000F9336 File Offset: 0x000F7536
			public DesignerDataColumn DesignerDataColumn
			{
				get
				{
					return this._designerDataColumn;
				}
			}

			// Token: 0x060028EF RID: 10479 RVA: 0x000F933E File Offset: 0x000F753E
			public override string ToString()
			{
				if (this._designerDataColumn != null)
				{
					return this._designerDataColumn.Name;
				}
				return "*";
			}

			// Token: 0x04001D10 RID: 7440
			private DesignerDataColumn _designerDataColumn;
		}

		// Token: 0x0200043D RID: 1085
		private sealed class TableItem
		{
			// Token: 0x060028F0 RID: 10480 RVA: 0x000F9359 File Offset: 0x000F7559
			public TableItem(DesignerDataTableBase designerDataTable)
			{
				this._designerDataTable = designerDataTable;
			}

			// Token: 0x170008A5 RID: 2213
			// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000F9368 File Offset: 0x000F7568
			public DesignerDataTableBase DesignerDataTable
			{
				get
				{
					return this._designerDataTable;
				}
			}

			// Token: 0x060028F2 RID: 10482 RVA: 0x000F9370 File Offset: 0x000F7570
			public override string ToString()
			{
				return this._designerDataTable.Name;
			}

			// Token: 0x04001D11 RID: 7441
			private DesignerDataTableBase _designerDataTable;
		}
	}
}
