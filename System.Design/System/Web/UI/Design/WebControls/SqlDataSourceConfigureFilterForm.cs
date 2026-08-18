using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Data.Common;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004B3 RID: 1203
	internal partial class SqlDataSourceConfigureFilterForm : DesignerForm
	{
		// Token: 0x06002B91 RID: 11153 RVA: 0x000F074C File Offset: 0x000EF74C
		public SqlDataSourceConfigureFilterForm(SqlDataSourceDesigner sqlDataSourceDesigner, SqlDataSourceTableQuery tableQuery) : base(sqlDataSourceDesigner.Component.Site)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._tableQuery = tableQuery.Clone();
			this.InitializeComponent();
			this.InitializeUI();
			this.CreateParameterList();
			foreach (SqlDataSourceConfigureFilterForm.ParameterEditor parameterEditor in SqlDataSourceConfigureFilterForm._parameterEditors.Values)
			{
				parameterEditor.Visible = false;
				this._propertiesPanel.Controls.Add(parameterEditor);
				this._sourceComboBox.Items.Add(parameterEditor);
				parameterEditor.ParameterChanged += this.OnParameterChanged;
			}
			this._sourceComboBox.InvalidateDropDownWidth();
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				foreach (object obj in tableQuery.DesignerDataTable.Columns)
				{
					DesignerDataColumn designerDataColumn = (DesignerDataColumn)obj;
					this._columnsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.ColumnItem(designerDataColumn));
				}
				this._columnsComboBox.InvalidateDropDownWidth();
				foreach (SqlDataSourceFilterClause filterClause in this._tableQuery.FilterClauses)
				{
					SqlDataSourceConfigureFilterForm.FilterClauseItem filterClauseItem = new SqlDataSourceConfigureFilterForm.FilterClauseItem(this._sqlDataSourceDesigner.Component.Site, this._tableQuery, filterClause, (SqlDataSource)this._sqlDataSourceDesigner.Component);
					this._whereClausesListView.Items.Add(filterClauseItem);
					filterClauseItem.Refresh();
				}
				if (this._whereClausesListView.Items.Count > 0)
				{
					this._whereClausesListView.Items[0].Selected = true;
					this._whereClausesListView.Items[0].Focused = true;
				}
				this._okButton.Enabled = false;
				this.UpdateDeleteButton();
				this.UpdateOperators();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000F09BC File Offset: 0x000EF9BC
		public IList<SqlDataSourceFilterClause> FilterClauses
		{
			get
			{
				return this._tableQuery.FilterClauses;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000F09C9 File Offset: 0x000EF9C9
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.ConfigureFilter";
			}
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000F09D0 File Offset: 0x000EF9D0
		private void CreateParameterList()
		{
			SqlDataSourceConfigureFilterForm._parameterEditors = new Dictionary<Type, SqlDataSourceConfigureFilterForm.ParameterEditor>();
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(Parameter), new SqlDataSourceConfigureFilterForm.StaticParameterEditor(base.ServiceProvider));
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(ControlParameter), new SqlDataSourceConfigureFilterForm.ControlParameterEditor(base.ServiceProvider, (SqlDataSource)this._sqlDataSourceDesigner.Component));
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(CookieParameter), new SqlDataSourceConfigureFilterForm.CookieParameterEditor(base.ServiceProvider));
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(FormParameter), new SqlDataSourceConfigureFilterForm.FormParameterEditor(base.ServiceProvider));
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(ProfileParameter), new SqlDataSourceConfigureFilterForm.ProfileParameterEditor(base.ServiceProvider));
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(QueryStringParameter), new SqlDataSourceConfigureFilterForm.QueryStringParameterEditor(base.ServiceProvider));
			SqlDataSourceConfigureFilterForm._parameterEditors.Add(typeof(SessionParameter), new SqlDataSourceConfigureFilterForm.SessionParameterEditor(base.ServiceProvider));
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000F157C File Offset: 0x000F057C
		private void InitializeUI()
		{
			this._helpLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_HelpLabel");
			this._columnLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ColumnLabel");
			this._operatorLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_OperatorLabel");
			this._whereClausesLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_WhereLabel");
			this._expressionLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ExpressionLabel");
			this._valueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ValueLabel");
			this._expressionColumnHeader.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ExpressionColumnHeader");
			this._valueColumnHeader.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ValueColumnHeader");
			this._propertiesGroupBox.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterPropertiesGroup");
			this._sourceLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_SourceLabel");
			this._addButton.Text = SR.GetString("SqlDataSourceConfigureFilterForm_AddButton");
			this._removeButton.Text = SR.GetString("SqlDataSourceConfigureFilterForm_RemoveButton");
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this.Text = SR.GetString("SqlDataSourceConfigureFilterForm_Caption");
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000F16C0 File Offset: 0x000F06C0
		private SqlDataSourceFilterClause GetCurrentFilterClause()
		{
			SqlDataSourceConfigureFilterForm.OperatorItem operatorItem = this._operatorsComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.OperatorItem;
			if (operatorItem == null)
			{
				return null;
			}
			SqlDataSourceConfigureFilterForm.ColumnItem columnItem = this._columnsComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.ColumnItem;
			if (columnItem == null)
			{
				return null;
			}
			Parameter parameter;
			string value;
			if (operatorItem.IsBinary)
			{
				SqlDataSourceConfigureFilterForm.ParameterEditor parameterEditor = this._sourceComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.ParameterEditor;
				if (parameterEditor == null)
				{
					return null;
				}
				parameter = parameterEditor.Parameter;
				if (parameter != null)
				{
					SqlDataSourceQuery selectQuery = this._tableQuery.GetSelectQuery();
					StringCollection stringCollection = new StringCollection();
					if (selectQuery != null && selectQuery.Parameters != null)
					{
						foreach (object obj in selectQuery.Parameters)
						{
							Parameter parameter2 = (Parameter)obj;
							stringCollection.Add(parameter2.Name);
						}
					}
					SqlDataSourceColumnData sqlDataSourceColumnData = new SqlDataSourceColumnData(this._tableQuery.DesignerDataConnection, columnItem.DesignerDataColumn, stringCollection);
					parameter.Name = sqlDataSourceColumnData.WebParameterName;
					DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this._tableQuery.DesignerDataConnection.ProviderName);
					if (SqlDataSourceDesigner.IsNewSqlServer2008Type(dbProviderFactory, columnItem.DesignerDataColumn.DataType))
					{
						parameter.DbType = columnItem.DesignerDataColumn.DataType;
						parameter.Type = TypeCode.Empty;
					}
					else
					{
						parameter.DbType = DbType.Object;
						parameter.Type = SqlDataSourceDesigner.ConvertDbTypeToTypeCode(columnItem.DesignerDataColumn.DataType);
					}
					value = sqlDataSourceColumnData.ParameterPlaceholder;
				}
				else
				{
					value = string.Empty;
				}
			}
			else
			{
				value = "";
				parameter = null;
			}
			return new SqlDataSourceFilterClause(this._tableQuery.DesignerDataConnection, this._tableQuery.DesignerDataTable, columnItem.DesignerDataColumn, operatorItem.OperatorFormat, operatorItem.IsBinary, value, parameter);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000F1880 File Offset: 0x000F0880
		private void OnAddButtonClick(object sender, EventArgs e)
		{
			SqlDataSourceFilterClause currentFilterClause = this.GetCurrentFilterClause();
			SqlDataSourceConfigureFilterForm.FilterClauseItem filterClauseItem = new SqlDataSourceConfigureFilterForm.FilterClauseItem(this._sqlDataSourceDesigner.Component.Site, this._tableQuery, currentFilterClause, (SqlDataSource)this._sqlDataSourceDesigner.Component);
			this._whereClausesListView.Items.Add(filterClauseItem);
			filterClauseItem.Selected = true;
			filterClauseItem.Focused = true;
			filterClauseItem.EnsureVisible();
			this._tableQuery.FilterClauses.Add(currentFilterClause);
			this._columnsComboBox.SelectedIndex = -1;
			this._okButton.Enabled = true;
			filterClauseItem.Refresh();
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000F1916 File Offset: 0x000F0916
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000F1925 File Offset: 0x000F0925
		private void OnColumnsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateOperators();
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000F192D File Offset: 0x000F092D
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000F193C File Offset: 0x000F093C
		private void OnOperatorsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateParameter();
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000F1944 File Offset: 0x000F0944
		private void OnParameterChanged(object sender, EventArgs e)
		{
			this.UpdateExpression();
			this.UpdateAddButtonEnabled();
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000F1954 File Offset: 0x000F0954
		private void OnRemoveButtonClick(object sender, EventArgs e)
		{
			if (this._whereClausesListView.SelectedItems.Count > 0)
			{
				int num = this._whereClausesListView.SelectedIndices[0];
				SqlDataSourceConfigureFilterForm.FilterClauseItem filterClauseItem = this._whereClausesListView.SelectedItems[0] as SqlDataSourceConfigureFilterForm.FilterClauseItem;
				this._whereClausesListView.Items.Remove(filterClauseItem);
				this._tableQuery.FilterClauses.Remove(filterClauseItem.FilterClause);
				this._okButton.Enabled = true;
				if (num < this._whereClausesListView.Items.Count)
				{
					ListViewItem listViewItem = this._whereClausesListView.Items[num];
					listViewItem.Selected = true;
					listViewItem.Focused = true;
					listViewItem.EnsureVisible();
					this._whereClausesListView.Focus();
					return;
				}
				if (this._whereClausesListView.Items.Count > 0)
				{
					ListViewItem listViewItem2 = this._whereClausesListView.Items[num - 1];
					listViewItem2.Selected = true;
					listViewItem2.Focused = true;
					listViewItem2.EnsureVisible();
					this._whereClausesListView.Focus();
				}
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000F1A61 File Offset: 0x000F0A61
		private void OnSourceComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateParameter();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000F1A69 File Offset: 0x000F0A69
		private void OnWhereClausesListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateDeleteButton();
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000F1A71 File Offset: 0x000F0A71
		private void UpdateDeleteButton()
		{
			this._removeButton.Enabled = (this._whereClausesListView.SelectedItems.Count > 0);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000F1A94 File Offset: 0x000F0A94
		private void UpdateAddButtonEnabled()
		{
			if (!(this._columnsComboBox.SelectedItem is SqlDataSourceConfigureFilterForm.ColumnItem))
			{
				this._addButton.Enabled = false;
				return;
			}
			SqlDataSourceConfigureFilterForm.OperatorItem operatorItem = this._operatorsComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.OperatorItem;
			if (operatorItem == null)
			{
				this._addButton.Enabled = false;
				return;
			}
			SqlDataSourceConfigureFilterForm.ParameterEditor parameterEditor = this._sourceComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.ParameterEditor;
			this._addButton.Enabled = (!operatorItem.IsBinary ^ (parameterEditor != null && parameterEditor.HasCompleteInformation));
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000F1B18 File Offset: 0x000F0B18
		private void UpdateOperators()
		{
			if (this._columnsComboBox.SelectedItem == null)
			{
				this._operatorsComboBox.SelectedItem = -1;
				this._operatorsComboBox.Items.Clear();
				this._operatorsComboBox.Enabled = false;
				this._operatorLabel.Enabled = false;
				this.UpdateParameter();
				return;
			}
			this._operatorsComboBox.Enabled = true;
			this._operatorLabel.Enabled = true;
			this._operatorsComboBox.Items.Clear();
			this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} = {1}", "=", true));
			this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} < {1}", "<", true));
			this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} > {1}", ">", true));
			this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} <= {1}", "<=", true));
			this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} >= {1}", ">=", true));
			this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} <> {1}", "<>", true));
			SqlDataSourceConfigureFilterForm.ColumnItem columnItem = (SqlDataSourceConfigureFilterForm.ColumnItem)this._columnsComboBox.SelectedItem;
			DesignerDataColumn designerDataColumn = columnItem.DesignerDataColumn;
			if (designerDataColumn.Nullable)
			{
				this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} IS NULL", "IS NULL", false));
				this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} IS NOT NULL", "IS NOT NULL", false));
			}
			DbType dataType = designerDataColumn.DataType;
			if (dataType == DbType.String || dataType == DbType.AnsiString || dataType == DbType.AnsiStringFixedLength || dataType == DbType.StringFixedLength)
			{
				this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} LIKE '%' + {1} + '%'", "LIKE", true));
				this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("{0} NOT LIKE '%' + {1} + '%'", "NOT LIKE", true));
				this._operatorsComboBox.Items.Add(new SqlDataSourceConfigureFilterForm.OperatorItem("CONTAINS({0}, {1})", "CONTAINS", true));
			}
			this._operatorsComboBox.InvalidateDropDownWidth();
			this._operatorsComboBox.SelectedIndex = 0;
			this.UpdateParameter();
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000F1D5C File Offset: 0x000F0D5C
		private void UpdateExpression()
		{
			SqlDataSourceConfigureFilterForm.ParameterEditor parameterEditor = this._sourceComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.ParameterEditor;
			if (this._operatorsComboBox.SelectedItem == null || parameterEditor == null)
			{
				this._expressionTextBox.Text = string.Empty;
				this._valueTextBox.Text = string.Empty;
				return;
			}
			SqlDataSourceFilterClause currentFilterClause = this.GetCurrentFilterClause();
			if (currentFilterClause != null)
			{
				this._expressionTextBox.Text = currentFilterClause.ToString();
			}
			else
			{
				this._expressionTextBox.Text = string.Empty;
			}
			if (parameterEditor.Parameter == null)
			{
				this._valueTextBox.Text = string.Empty;
				return;
			}
			bool flag;
			string parameterExpression = ParameterEditorUserControl.GetParameterExpression(this._sqlDataSourceDesigner.Component.Site, parameterEditor.Parameter, (SqlDataSource)this._sqlDataSourceDesigner.Component, out flag);
			if (flag)
			{
				this._valueTextBox.Text = string.Empty;
				return;
			}
			this._valueTextBox.Text = parameterExpression;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000F1E48 File Offset: 0x000F0E48
		private void UpdateParameter()
		{
			SqlDataSourceConfigureFilterForm.OperatorItem operatorItem = this._operatorsComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.OperatorItem;
			if (operatorItem != null && operatorItem.IsBinary)
			{
				this._expressionLabel.Enabled = true;
				this._expressionTextBox.Enabled = true;
				this._valueLabel.Enabled = true;
				this._valueTextBox.Enabled = true;
				this._propertiesGroupBox.Enabled = true;
				this._sourceLabel.Enabled = true;
				this._sourceComboBox.Enabled = true;
			}
			else
			{
				this._expressionLabel.Enabled = false;
				this._expressionTextBox.Enabled = false;
				this._valueLabel.Enabled = false;
				this._valueTextBox.Enabled = false;
				this._propertiesGroupBox.Enabled = false;
				this._sourceLabel.Enabled = false;
				this._sourceComboBox.Enabled = false;
				this._sourceComboBox.SelectedItem = null;
			}
			foreach (SqlDataSourceConfigureFilterForm.ParameterEditor parameterEditor in SqlDataSourceConfigureFilterForm._parameterEditors.Values)
			{
				parameterEditor.Visible = false;
			}
			SqlDataSourceConfigureFilterForm.ParameterEditor parameterEditor2 = this._sourceComboBox.SelectedItem as SqlDataSourceConfigureFilterForm.ParameterEditor;
			if (parameterEditor2 != null)
			{
				parameterEditor2.Visible = true;
				parameterEditor2.Initialize();
				this._propertiesPanel.Visible = true;
			}
			else
			{
				this._propertiesPanel.Visible = false;
			}
			this.UpdateExpression();
			this.UpdateAddButtonEnabled();
		}

		// Token: 0x04001DAA RID: 7594
		private static IDictionary<Type, SqlDataSourceConfigureFilterForm.ParameterEditor> _parameterEditors;

		// Token: 0x04001DAB RID: 7595
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001DAC RID: 7596
		private SqlDataSourceTableQuery _tableQuery;

		// Token: 0x020004B4 RID: 1204
		private sealed class ColumnItem
		{
			// Token: 0x06002BA6 RID: 11174 RVA: 0x000F1FB4 File Offset: 0x000F0FB4
			public ColumnItem(DesignerDataColumn designerDataColumn)
			{
				this._designerDataColumn = designerDataColumn;
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000F1FC3 File Offset: 0x000F0FC3
			public DesignerDataColumn DesignerDataColumn
			{
				get
				{
					return this._designerDataColumn;
				}
			}

			// Token: 0x06002BA8 RID: 11176 RVA: 0x000F1FCB File Offset: 0x000F0FCB
			public override string ToString()
			{
				return this._designerDataColumn.Name;
			}

			// Token: 0x04001DAD RID: 7597
			private DesignerDataColumn _designerDataColumn;
		}

		// Token: 0x020004B5 RID: 1205
		private sealed class OperatorItem
		{
			// Token: 0x06002BA9 RID: 11177 RVA: 0x000F1FD8 File Offset: 0x000F0FD8
			public OperatorItem(string operatorFormat, string operatorName, bool isBinary)
			{
				this._operatorName = operatorName;
				this._operatorFormat = operatorFormat;
				this._isBinary = isBinary;
			}

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000F1FF5 File Offset: 0x000F0FF5
			public bool IsBinary
			{
				get
				{
					return this._isBinary;
				}
			}

			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000F1FFD File Offset: 0x000F0FFD
			public string OperatorFormat
			{
				get
				{
					return this._operatorFormat;
				}
			}

			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000F2005 File Offset: 0x000F1005
			public string OperatorName
			{
				get
				{
					return this._operatorName;
				}
			}

			// Token: 0x06002BAD RID: 11181 RVA: 0x000F200D File Offset: 0x000F100D
			public override string ToString()
			{
				return this._operatorName;
			}

			// Token: 0x04001DAE RID: 7598
			private string _operatorName;

			// Token: 0x04001DAF RID: 7599
			private bool _isBinary;

			// Token: 0x04001DB0 RID: 7600
			private string _operatorFormat;
		}

		// Token: 0x020004B6 RID: 1206
		private sealed class FilterClauseItem : ListViewItem
		{
			// Token: 0x06002BAE RID: 11182 RVA: 0x000F2015 File Offset: 0x000F1015
			public FilterClauseItem(IServiceProvider serviceProvider, SqlDataSourceTableQuery tableQuery, SqlDataSourceFilterClause filterClause, SqlDataSource sqlDataSource)
			{
				this._filterClause = filterClause;
				this._tableQuery = tableQuery;
				this._serviceProvider = serviceProvider;
				this._sqlDataSource = sqlDataSource;
			}

			// Token: 0x1700082D RID: 2093
			// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000F203A File Offset: 0x000F103A
			public SqlDataSourceFilterClause FilterClause
			{
				get
				{
					return this._filterClause;
				}
			}

			// Token: 0x06002BB0 RID: 11184 RVA: 0x000F2044 File Offset: 0x000F1044
			public void Refresh()
			{
				base.SubItems.Clear();
				base.Text = this._filterClause.ToString();
				ListView listView = base.ListView;
				IServiceProvider serviceProvider = null;
				if (listView != null)
				{
					serviceProvider = ((SqlDataSourceConfigureFilterForm)listView.Parent).ServiceProvider;
				}
				string text;
				if (this._filterClause.Parameter == null)
				{
					text = string.Empty;
				}
				else
				{
					bool flag;
					text = ParameterEditorUserControl.GetParameterExpression(serviceProvider, this._filterClause.Parameter, this._sqlDataSource, out flag);
					if (flag)
					{
						text = string.Empty;
					}
				}
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
				listViewSubItem.Text = text;
				base.SubItems.Add(listViewSubItem);
			}

			// Token: 0x04001DB1 RID: 7601
			private SqlDataSourceFilterClause _filterClause;

			// Token: 0x04001DB2 RID: 7602
			private SqlDataSourceTableQuery _tableQuery;

			// Token: 0x04001DB3 RID: 7603
			private IServiceProvider _serviceProvider;

			// Token: 0x04001DB4 RID: 7604
			private SqlDataSource _sqlDataSource;
		}

		// Token: 0x020004B7 RID: 1207
		private abstract class ParameterEditor : System.Windows.Forms.Panel
		{
			// Token: 0x06002BB1 RID: 11185 RVA: 0x000F20DF File Offset: 0x000F10DF
			protected ParameterEditor(IServiceProvider serviceProvider)
			{
				this._serviceProvider = serviceProvider;
			}

			// Token: 0x1700082E RID: 2094
			// (get) Token: 0x06002BB2 RID: 11186
			public abstract string EditorName { get; }

			// Token: 0x1700082F RID: 2095
			// (get) Token: 0x06002BB3 RID: 11187
			public abstract bool HasCompleteInformation { get; }

			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x06002BB4 RID: 11188
			public abstract Parameter Parameter { get; }

			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000F20EE File Offset: 0x000F10EE
			protected IServiceProvider ServiceProvider
			{
				get
				{
					return this._serviceProvider;
				}
			}

			// Token: 0x14000042 RID: 66
			// (add) Token: 0x06002BB6 RID: 11190 RVA: 0x000F20F6 File Offset: 0x000F10F6
			// (remove) Token: 0x06002BB7 RID: 11191 RVA: 0x000F2109 File Offset: 0x000F1109
			public event EventHandler ParameterChanged
			{
				add
				{
					base.Events.AddHandler(SqlDataSourceConfigureFilterForm.ParameterEditor.EventParameterChanged, value);
				}
				remove
				{
					base.Events.RemoveHandler(SqlDataSourceConfigureFilterForm.ParameterEditor.EventParameterChanged, value);
				}
			}

			// Token: 0x06002BB8 RID: 11192
			public abstract void Initialize();

			// Token: 0x06002BB9 RID: 11193 RVA: 0x000F211C File Offset: 0x000F111C
			protected void OnParameterChanged()
			{
				EventHandler eventHandler = base.Events[SqlDataSourceConfigureFilterForm.ParameterEditor.EventParameterChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}

			// Token: 0x06002BBA RID: 11194 RVA: 0x000F214E File Offset: 0x000F114E
			public override string ToString()
			{
				return this.EditorName;
			}

			// Token: 0x04001DB5 RID: 7605
			protected const int ControlWidth = 220;

			// Token: 0x04001DB6 RID: 7606
			private static readonly object EventParameterChanged = new object();

			// Token: 0x04001DB7 RID: 7607
			private IServiceProvider _serviceProvider;
		}

		// Token: 0x020004B8 RID: 1208
		private sealed class StaticParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BBC RID: 11196 RVA: 0x000F2164 File Offset: 0x000F1164
			public StaticParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 0);
				this._defaultValueLabel.Name = "StaticDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 10;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_StaticParameterEditor_ValueLabel");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 23);
				this._defaultValueTextBox.Name = "StaticDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 20;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x17000832 RID: 2098
			// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000F22A8 File Offset: 0x000F12A8
			public override string EditorName
			{
				get
				{
					return "None";
				}
			}

			// Token: 0x17000833 RID: 2099
			// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000F22AF File Offset: 0x000F12AF
			public override bool HasCompleteInformation
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000834 RID: 2100
			// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000F22B2 File Offset: 0x000F12B2
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BC0 RID: 11200 RVA: 0x000F22BA File Offset: 0x000F12BA
			public override void Initialize()
			{
				this._parameter = new Parameter();
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BC1 RID: 11201 RVA: 0x000F22D7 File Offset: 0x000F12D7
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001DB8 RID: 7608
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DB9 RID: 7609
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DBA RID: 7610
			private Parameter _parameter;
		}

		// Token: 0x020004B9 RID: 1209
		private sealed class CookieParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BC2 RID: 11202 RVA: 0x000F22F8 File Offset: 0x000F12F8
			public CookieParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._cookieNameLabel = new System.Windows.Forms.Label();
				this._cookieNameTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._cookieNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._cookieNameLabel.Location = new Point(0, 0);
				this._cookieNameLabel.Name = "CookieNameLabel";
				this._cookieNameLabel.Size = new Size(220, 16);
				this._cookieNameLabel.TabIndex = 10;
				this._cookieNameLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_CookieParameterEditor_CookieNameLabel");
				this._cookieNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._cookieNameTextBox.Location = new Point(0, 23);
				this._cookieNameTextBox.Name = "CookieNameTextBox";
				this._cookieNameTextBox.Size = new Size(220, 20);
				this._cookieNameTextBox.TabIndex = 20;
				this._cookieNameTextBox.TextChanged += this.OnCookieNameTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "CookieDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "CookieDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._cookieNameLabel);
				base.Controls.Add(this._cookieNameTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x17000835 RID: 2101
			// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x000F2548 File Offset: 0x000F1548
			public override string EditorName
			{
				get
				{
					return "Cookie";
				}
			}

			// Token: 0x17000836 RID: 2102
			// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000F254F File Offset: 0x000F154F
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.CookieName.Length > 0;
				}
			}

			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000F2564 File Offset: 0x000F1564
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BC6 RID: 11206 RVA: 0x000F256C File Offset: 0x000F156C
			public override void Initialize()
			{
				this._parameter = new CookieParameter();
				this._cookieNameTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BC7 RID: 11207 RVA: 0x000F2599 File Offset: 0x000F1599
			private void OnCookieNameTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.CookieName = this._cookieNameTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x06002BC8 RID: 11208 RVA: 0x000F25B7 File Offset: 0x000F15B7
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x04001DBB RID: 7611
			private System.Windows.Forms.Label _cookieNameLabel;

			// Token: 0x04001DBC RID: 7612
			private System.Windows.Forms.TextBox _cookieNameTextBox;

			// Token: 0x04001DBD RID: 7613
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DBE RID: 7614
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DBF RID: 7615
			private CookieParameter _parameter;
		}

		// Token: 0x020004BA RID: 1210
		private sealed class ControlParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BC9 RID: 11209 RVA: 0x000F25D0 File Offset: 0x000F15D0
			public ControlParameterEditor(IServiceProvider serviceProvider, Control control) : base(serviceProvider)
			{
				this._control = control;
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._controlIDLabel = new System.Windows.Forms.Label();
				this._controlIDComboBox = new AutoSizeComboBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._controlIDLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._controlIDLabel.Location = new Point(0, 0);
				this._controlIDLabel.Name = "ControlIDLabel";
				this._controlIDLabel.Size = new Size(220, 16);
				this._controlIDLabel.TabIndex = 10;
				this._controlIDLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ControlParameterEditor_ControlIDLabel");
				this._controlIDComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._controlIDComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				this._controlIDComboBox.Location = new Point(0, 23);
				this._controlIDComboBox.Name = "ControlIDComboBox";
				this._controlIDComboBox.Size = new Size(220, 20);
				this._controlIDComboBox.Sorted = true;
				this._controlIDComboBox.TabIndex = 20;
				this._controlIDComboBox.SelectedIndexChanged += this.OnControlIDComboBoxSelectedIndexChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "ControlDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "ControlDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._controlIDLabel);
				base.Controls.Add(this._controlIDComboBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				if (base.ServiceProvider != null)
				{
					IDesignerHost designerHost = (IDesignerHost)base.ServiceProvider.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						ParameterEditorUserControl.ControlItem[] controlItems = ParameterEditorUserControl.ControlItem.GetControlItems(designerHost, this._control);
						foreach (ParameterEditorUserControl.ControlItem item in controlItems)
						{
							this._controlIDComboBox.Items.Add(item);
						}
						this._controlIDComboBox.InvalidateDropDownWidth();
					}
				}
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000F28A8 File Offset: 0x000F18A8
			public override string EditorName
			{
				get
				{
					return "Control";
				}
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06002BCB RID: 11211 RVA: 0x000F28AF File Offset: 0x000F18AF
			public override bool HasCompleteInformation
			{
				get
				{
					return this._controlIDComboBox.SelectedItem != null;
				}
			}

			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000F28C2 File Offset: 0x000F18C2
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BCD RID: 11213 RVA: 0x000F28CA File Offset: 0x000F18CA
			public override void Initialize()
			{
				this._parameter = new ControlParameter();
				this._controlIDComboBox.SelectedItem = null;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BCE RID: 11214 RVA: 0x000F28F4 File Offset: 0x000F18F4
			private void OnControlIDComboBoxSelectedIndexChanged(object s, EventArgs e)
			{
				ParameterEditorUserControl.ControlItem controlItem = this._controlIDComboBox.SelectedItem as ParameterEditorUserControl.ControlItem;
				if (controlItem == null)
				{
					this._parameter.ControlID = string.Empty;
					this._parameter.PropertyName = string.Empty;
				}
				else
				{
					this._parameter.ControlID = controlItem.ControlID;
					this._parameter.PropertyName = controlItem.PropertyName;
				}
				base.OnParameterChanged();
			}

			// Token: 0x06002BCF RID: 11215 RVA: 0x000F295F File Offset: 0x000F195F
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x04001DC0 RID: 7616
			private System.Windows.Forms.Label _controlIDLabel;

			// Token: 0x04001DC1 RID: 7617
			private AutoSizeComboBox _controlIDComboBox;

			// Token: 0x04001DC2 RID: 7618
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DC3 RID: 7619
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DC4 RID: 7620
			private ControlParameter _parameter;

			// Token: 0x04001DC5 RID: 7621
			private Control _control;
		}

		// Token: 0x020004BB RID: 1211
		private sealed class FormParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BD0 RID: 11216 RVA: 0x000F2978 File Offset: 0x000F1978
			public FormParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._formFieldLabel = new System.Windows.Forms.Label();
				this._formFieldTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._formFieldLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._formFieldLabel.Location = new Point(0, 0);
				this._formFieldLabel.Name = "FormFieldLabel";
				this._formFieldLabel.Size = new Size(220, 16);
				this._formFieldLabel.TabIndex = 10;
				this._formFieldLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_FormParameterEditor_FormFieldLabel");
				this._formFieldTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._formFieldTextBox.Location = new Point(0, 23);
				this._formFieldTextBox.Name = "FormFieldTextBox";
				this._formFieldTextBox.Size = new Size(220, 20);
				this._formFieldTextBox.TabIndex = 20;
				this._formFieldTextBox.TextChanged += this.OnFormFieldTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "FormDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "FormDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._formFieldLabel);
				base.Controls.Add(this._formFieldTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000F2BC8 File Offset: 0x000F1BC8
			public override string EditorName
			{
				get
				{
					return "Form";
				}
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000F2BCF File Offset: 0x000F1BCF
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.FormField.Length > 0;
				}
			}

			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000F2BE4 File Offset: 0x000F1BE4
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BD4 RID: 11220 RVA: 0x000F2BEC File Offset: 0x000F1BEC
			public override void Initialize()
			{
				this._parameter = new FormParameter();
				this._formFieldTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BD5 RID: 11221 RVA: 0x000F2C19 File Offset: 0x000F1C19
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x06002BD6 RID: 11222 RVA: 0x000F2C31 File Offset: 0x000F1C31
			private void OnFormFieldTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.FormField = this._formFieldTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001DC6 RID: 7622
			private System.Windows.Forms.Label _formFieldLabel;

			// Token: 0x04001DC7 RID: 7623
			private System.Windows.Forms.TextBox _formFieldTextBox;

			// Token: 0x04001DC8 RID: 7624
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DC9 RID: 7625
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DCA RID: 7626
			private FormParameter _parameter;
		}

		// Token: 0x020004BC RID: 1212
		private sealed class SessionParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BD7 RID: 11223 RVA: 0x000F2C50 File Offset: 0x000F1C50
			public SessionParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._sessionFieldLabel = new System.Windows.Forms.Label();
				this._sessionFieldTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._sessionFieldLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._sessionFieldLabel.Location = new Point(0, 0);
				this._sessionFieldLabel.Name = "SessionFieldLabel";
				this._sessionFieldLabel.Size = new Size(220, 16);
				this._sessionFieldLabel.TabIndex = 10;
				this._sessionFieldLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_SessionParameterEditor_SessionFieldLabel");
				this._sessionFieldTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._sessionFieldTextBox.Location = new Point(0, 23);
				this._sessionFieldTextBox.Name = "SessionFieldTextBox";
				this._sessionFieldTextBox.Size = new Size(220, 20);
				this._sessionFieldTextBox.TabIndex = 20;
				this._sessionFieldTextBox.TextChanged += this.OnSessionFieldTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "SessionDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "SessionDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._sessionFieldLabel);
				base.Controls.Add(this._sessionFieldTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x1700083E RID: 2110
			// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000F2EA0 File Offset: 0x000F1EA0
			public override string EditorName
			{
				get
				{
					return "Session";
				}
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x000F2EA7 File Offset: 0x000F1EA7
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.SessionField.Length > 0;
				}
			}

			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000F2EBC File Offset: 0x000F1EBC
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BDB RID: 11227 RVA: 0x000F2EC4 File Offset: 0x000F1EC4
			public override void Initialize()
			{
				this._parameter = new SessionParameter();
				this._sessionFieldTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BDC RID: 11228 RVA: 0x000F2EF1 File Offset: 0x000F1EF1
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x06002BDD RID: 11229 RVA: 0x000F2F09 File Offset: 0x000F1F09
			private void OnSessionFieldTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.SessionField = this._sessionFieldTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001DCB RID: 7627
			private System.Windows.Forms.Label _sessionFieldLabel;

			// Token: 0x04001DCC RID: 7628
			private System.Windows.Forms.TextBox _sessionFieldTextBox;

			// Token: 0x04001DCD RID: 7629
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DCE RID: 7630
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DCF RID: 7631
			private SessionParameter _parameter;
		}

		// Token: 0x020004BD RID: 1213
		private sealed class QueryStringParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BDE RID: 11230 RVA: 0x000F2F28 File Offset: 0x000F1F28
			public QueryStringParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._queryStringFieldLabel = new System.Windows.Forms.Label();
				this._queryStringFieldTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._queryStringFieldLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._queryStringFieldLabel.Location = new Point(0, 0);
				this._queryStringFieldLabel.Name = "QueryStringFieldLabel";
				this._queryStringFieldLabel.Size = new Size(220, 16);
				this._queryStringFieldLabel.TabIndex = 10;
				this._queryStringFieldLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_QueryStringParameterEditor_QueryStringFieldLabel");
				this._queryStringFieldTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._queryStringFieldTextBox.Location = new Point(0, 23);
				this._queryStringFieldTextBox.Name = "QueryStringFieldTextBox";
				this._queryStringFieldTextBox.Size = new Size(220, 20);
				this._queryStringFieldTextBox.TabIndex = 20;
				this._queryStringFieldTextBox.TextChanged += this.OnQueryStringFieldTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "QueryStringDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "QueryStringDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._queryStringFieldLabel);
				base.Controls.Add(this._queryStringFieldTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000F3178 File Offset: 0x000F2178
			public override string EditorName
			{
				get
				{
					return "QueryString";
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000F317F File Offset: 0x000F217F
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.QueryStringField.Length > 0;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000F3194 File Offset: 0x000F2194
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BE2 RID: 11234 RVA: 0x000F319C File Offset: 0x000F219C
			public override void Initialize()
			{
				this._parameter = new QueryStringParameter();
				this._queryStringFieldTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BE3 RID: 11235 RVA: 0x000F31C9 File Offset: 0x000F21C9
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x06002BE4 RID: 11236 RVA: 0x000F31E1 File Offset: 0x000F21E1
			private void OnQueryStringFieldTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.QueryStringField = this._queryStringFieldTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001DD0 RID: 7632
			private System.Windows.Forms.Label _queryStringFieldLabel;

			// Token: 0x04001DD1 RID: 7633
			private System.Windows.Forms.TextBox _queryStringFieldTextBox;

			// Token: 0x04001DD2 RID: 7634
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DD3 RID: 7635
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DD4 RID: 7636
			private QueryStringParameter _parameter;
		}

		// Token: 0x020004BE RID: 1214
		private sealed class ProfileParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x06002BE5 RID: 11237 RVA: 0x000F3200 File Offset: 0x000F2200
			public ProfileParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._propertyNameLabel = new System.Windows.Forms.Label();
				this._propertyNameTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._propertyNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._propertyNameLabel.Location = new Point(0, 0);
				this._propertyNameLabel.Name = "ProfilePropertyNameLabel";
				this._propertyNameLabel.Size = new Size(220, 16);
				this._propertyNameLabel.TabIndex = 10;
				this._propertyNameLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ProfileParameterEditor_PropertyNameLabel");
				this._propertyNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._propertyNameTextBox.Location = new Point(0, 23);
				this._propertyNameTextBox.Name = "ProfilePropertyNameTextBox";
				this._propertyNameTextBox.Size = new Size(220, 20);
				this._propertyNameTextBox.TabIndex = 20;
				this._propertyNameTextBox.TextChanged += this.OnPropertyNameTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "ProfileDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "ProfileDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._propertyNameLabel);
				base.Controls.Add(this._propertyNameTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000F3450 File Offset: 0x000F2450
			public override string EditorName
			{
				get
				{
					return "Profile";
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000F3457 File Offset: 0x000F2457
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.PropertyName.Length > 0;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000F346C File Offset: 0x000F246C
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x06002BE9 RID: 11241 RVA: 0x000F3474 File Offset: 0x000F2474
			public override void Initialize()
			{
				this._parameter = new ProfileParameter();
				this._propertyNameTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x06002BEA RID: 11242 RVA: 0x000F34A1 File Offset: 0x000F24A1
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x06002BEB RID: 11243 RVA: 0x000F34B9 File Offset: 0x000F24B9
			private void OnPropertyNameTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.PropertyName = this._propertyNameTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001DD5 RID: 7637
			private System.Windows.Forms.Label _propertyNameLabel;

			// Token: 0x04001DD6 RID: 7638
			private System.Windows.Forms.TextBox _propertyNameTextBox;

			// Token: 0x04001DD7 RID: 7639
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001DD8 RID: 7640
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001DD9 RID: 7641
			private ProfileParameter _parameter;
		}
	}
}
