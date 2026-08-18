using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
	// Token: 0x02000108 RID: 264
	internal partial class SqlDataSourceConfigureFilterForm : DesignerForm
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x00035C80 File Offset: 0x00033E80
		internal SqlDataSourceConfigureFilterForm(ISite site, IServiceProvider serviceProvider, SqlDataSource dataSource, TypeDescriptionProvider provider) : base(site)
		{
			this._serviceProvider = serviceProvider;
			this._dataSource = dataSource;
			this._provider = provider;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00035CA0 File Offset: 0x00033EA0
		public SqlDataSourceConfigureFilterForm(SqlDataSourceDesigner sqlDataSourceDesigner, SqlDataSourceTableQuery tableQuery) : base(sqlDataSourceDesigner.Component.Site)
		{
			this._dataSource = (SqlDataSource)sqlDataSourceDesigner.Component;
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._tableQuery = tableQuery.Clone();
			this.InitializeComponent();
			this.InitializeUI();
			SqlDataSourceConfigureFilterForm._parameterEditors = this.CreateParameterList();
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00035F24 File Offset: 0x00034124
		public new IServiceProvider ServiceProvider
		{
			get
			{
				return this._serviceProvider ?? base.ServiceProvider;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00035F36 File Offset: 0x00034136
		public TypeDescriptionProvider TypeDescriptionProvider
		{
			get
			{
				if (this._provider != null)
				{
					return this._provider;
				}
				if (this._dataSource != null)
				{
					return TypeDescriptor.GetProvider(this._dataSource);
				}
				return null;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00035F5C File Offset: 0x0003415C
		public IList<SqlDataSourceFilterClause> FilterClauses
		{
			get
			{
				return this._tableQuery.FilterClauses;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00035F69 File Offset: 0x00034169
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.ConfigureFilter";
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00035F70 File Offset: 0x00034170
		internal IDictionary<Type, SqlDataSourceConfigureFilterForm.ParameterEditor> CreateParameterList()
		{
			Dictionary<Type, SqlDataSourceConfigureFilterForm.ParameterEditor> dictionary = new Dictionary<Type, SqlDataSourceConfigureFilterForm.ParameterEditor>();
			dictionary.Add(typeof(Parameter), new SqlDataSourceConfigureFilterForm.StaticParameterEditor(this.ServiceProvider));
			dictionary.Add(typeof(ControlParameter), new SqlDataSourceConfigureFilterForm.ControlParameterEditor(this.ServiceProvider, this._dataSource));
			dictionary.Add(typeof(CookieParameter), new SqlDataSourceConfigureFilterForm.CookieParameterEditor(this.ServiceProvider));
			dictionary.Add(typeof(FormParameter), new SqlDataSourceConfigureFilterForm.FormParameterEditor(this.ServiceProvider));
			dictionary.Add(typeof(ProfileParameter), new SqlDataSourceConfigureFilterForm.ProfileParameterEditor(this.ServiceProvider));
			dictionary.Add(typeof(QueryStringParameter), new SqlDataSourceConfigureFilterForm.QueryStringParameterEditor(this.ServiceProvider));
			dictionary.Add(typeof(SessionParameter), new SqlDataSourceConfigureFilterForm.SessionParameterEditor(this.ServiceProvider));
			if (this.TypeDescriptionProvider.IsSupportedType(typeof(RouteParameter)))
			{
				dictionary.Add(typeof(RouteParameter), new SqlDataSourceConfigureFilterForm.RouteParameterEditor(this.ServiceProvider));
			}
			return dictionary;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00036B28 File Offset: 0x00034D28
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

		// Token: 0x0600095C RID: 2396 RVA: 0x00036C6C File Offset: 0x00034E6C
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

		// Token: 0x0600095D RID: 2397 RVA: 0x00036E2C File Offset: 0x0003502C
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

		// Token: 0x0600095E RID: 2398 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00036EC2 File Offset: 0x000350C2
		private void OnColumnsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateOperators();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000357ED File Offset: 0x000339ED
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00036ECA File Offset: 0x000350CA
		private void OnOperatorsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateParameter();
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00036ED2 File Offset: 0x000350D2
		private void OnParameterChanged(object sender, EventArgs e)
		{
			this.UpdateExpression();
			this.UpdateAddButtonEnabled();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00036EE0 File Offset: 0x000350E0
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

		// Token: 0x06000964 RID: 2404 RVA: 0x00036ECA File Offset: 0x000350CA
		private void OnSourceComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateParameter();
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00036FED File Offset: 0x000351ED
		private void OnWhereClausesListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateDeleteButton();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00036FF5 File Offset: 0x000351F5
		private void UpdateDeleteButton()
		{
			this._removeButton.Enabled = (this._whereClausesListView.SelectedItems.Count > 0);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00037018 File Offset: 0x00035218
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

		// Token: 0x06000968 RID: 2408 RVA: 0x0003709C File Offset: 0x0003529C
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

		// Token: 0x06000969 RID: 2409 RVA: 0x000372E0 File Offset: 0x000354E0
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

		// Token: 0x0600096A RID: 2410 RVA: 0x000373CC File Offset: 0x000355CC
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

		// Token: 0x04000589 RID: 1417
		private IServiceProvider _serviceProvider;

		// Token: 0x0400058A RID: 1418
		private TypeDescriptionProvider _provider;

		// Token: 0x0400058B RID: 1419
		private static IDictionary<Type, SqlDataSourceConfigureFilterForm.ParameterEditor> _parameterEditors;

		// Token: 0x0400058C RID: 1420
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x0400058D RID: 1421
		private SqlDataSourceTableQuery _tableQuery;

		// Token: 0x0400058E RID: 1422
		private SqlDataSource _dataSource;

		// Token: 0x02000430 RID: 1072
		private sealed class ColumnItem
		{
			// Token: 0x0600289F RID: 10399 RVA: 0x000F7B3C File Offset: 0x000F5D3C
			public ColumnItem(DesignerDataColumn designerDataColumn)
			{
				this._designerDataColumn = designerDataColumn;
			}

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x060028A0 RID: 10400 RVA: 0x000F7B4B File Offset: 0x000F5D4B
			public DesignerDataColumn DesignerDataColumn
			{
				get
				{
					return this._designerDataColumn;
				}
			}

			// Token: 0x060028A1 RID: 10401 RVA: 0x000F7B53 File Offset: 0x000F5D53
			public override string ToString()
			{
				return this._designerDataColumn.Name;
			}

			// Token: 0x04001CDE RID: 7390
			private DesignerDataColumn _designerDataColumn;
		}

		// Token: 0x02000431 RID: 1073
		private sealed class OperatorItem
		{
			// Token: 0x060028A2 RID: 10402 RVA: 0x000F7B60 File Offset: 0x000F5D60
			public OperatorItem(string operatorFormat, string operatorName, bool isBinary)
			{
				this._operatorName = operatorName;
				this._operatorFormat = operatorFormat;
				this._isBinary = isBinary;
			}

			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000F7B7D File Offset: 0x000F5D7D
			public bool IsBinary
			{
				get
				{
					return this._isBinary;
				}
			}

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000F7B85 File Offset: 0x000F5D85
			public string OperatorFormat
			{
				get
				{
					return this._operatorFormat;
				}
			}

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000F7B8D File Offset: 0x000F5D8D
			public string OperatorName
			{
				get
				{
					return this._operatorName;
				}
			}

			// Token: 0x060028A6 RID: 10406 RVA: 0x000F7B8D File Offset: 0x000F5D8D
			public override string ToString()
			{
				return this._operatorName;
			}

			// Token: 0x04001CDF RID: 7391
			private string _operatorName;

			// Token: 0x04001CE0 RID: 7392
			private bool _isBinary;

			// Token: 0x04001CE1 RID: 7393
			private string _operatorFormat;
		}

		// Token: 0x02000432 RID: 1074
		private sealed class FilterClauseItem : ListViewItem
		{
			// Token: 0x060028A7 RID: 10407 RVA: 0x000F7B95 File Offset: 0x000F5D95
			public FilterClauseItem(IServiceProvider serviceProvider, SqlDataSourceTableQuery tableQuery, SqlDataSourceFilterClause filterClause, SqlDataSource sqlDataSource)
			{
				this._filterClause = filterClause;
				this._tableQuery = tableQuery;
				this._serviceProvider = serviceProvider;
				this._sqlDataSource = sqlDataSource;
			}

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000F7BBA File Offset: 0x000F5DBA
			public SqlDataSourceFilterClause FilterClause
			{
				get
				{
					return this._filterClause;
				}
			}

			// Token: 0x060028A9 RID: 10409 RVA: 0x000F7BC4 File Offset: 0x000F5DC4
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

			// Token: 0x04001CE2 RID: 7394
			private SqlDataSourceFilterClause _filterClause;

			// Token: 0x04001CE3 RID: 7395
			private SqlDataSourceTableQuery _tableQuery;

			// Token: 0x04001CE4 RID: 7396
			private IServiceProvider _serviceProvider;

			// Token: 0x04001CE5 RID: 7397
			private SqlDataSource _sqlDataSource;
		}

		// Token: 0x02000433 RID: 1075
		internal abstract class ParameterEditor : System.Windows.Forms.Panel
		{
			// Token: 0x060028AA RID: 10410 RVA: 0x000F7C5D File Offset: 0x000F5E5D
			protected ParameterEditor(IServiceProvider serviceProvider)
			{
				this._serviceProvider = serviceProvider;
			}

			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x060028AB RID: 10411
			public abstract string EditorName { get; }

			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x060028AC RID: 10412
			public abstract bool HasCompleteInformation { get; }

			// Token: 0x1700088A RID: 2186
			// (get) Token: 0x060028AD RID: 10413
			public abstract Parameter Parameter { get; }

			// Token: 0x1700088B RID: 2187
			// (get) Token: 0x060028AE RID: 10414 RVA: 0x000F7C6C File Offset: 0x000F5E6C
			protected IServiceProvider ServiceProvider
			{
				get
				{
					return this._serviceProvider;
				}
			}

			// Token: 0x14000069 RID: 105
			// (add) Token: 0x060028AF RID: 10415 RVA: 0x000F7C74 File Offset: 0x000F5E74
			// (remove) Token: 0x060028B0 RID: 10416 RVA: 0x000F7C87 File Offset: 0x000F5E87
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

			// Token: 0x060028B1 RID: 10417
			public abstract void Initialize();

			// Token: 0x060028B2 RID: 10418 RVA: 0x000F7C9C File Offset: 0x000F5E9C
			protected void OnParameterChanged()
			{
				EventHandler eventHandler = base.Events[SqlDataSourceConfigureFilterForm.ParameterEditor.EventParameterChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x000F7CCE File Offset: 0x000F5ECE
			public override string ToString()
			{
				return this.EditorName;
			}

			// Token: 0x04001CE6 RID: 7398
			private static readonly object EventParameterChanged = new object();

			// Token: 0x04001CE7 RID: 7399
			protected const int ControlWidth = 220;

			// Token: 0x04001CE8 RID: 7400
			private IServiceProvider _serviceProvider;
		}

		// Token: 0x02000434 RID: 1076
		internal sealed class StaticParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028B5 RID: 10421 RVA: 0x000F7CE4 File Offset: 0x000F5EE4
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

			// Token: 0x1700088C RID: 2188
			// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000F7E28 File Offset: 0x000F6028
			public override string EditorName
			{
				get
				{
					return "None";
				}
			}

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x060028B7 RID: 10423 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool HasCompleteInformation
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000F7E2F File Offset: 0x000F602F
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028B9 RID: 10425 RVA: 0x000F7E37 File Offset: 0x000F6037
			public override void Initialize()
			{
				this._parameter = new Parameter();
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028BA RID: 10426 RVA: 0x000F7E54 File Offset: 0x000F6054
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001CE9 RID: 7401
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CEA RID: 7402
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CEB RID: 7403
			private Parameter _parameter;
		}

		// Token: 0x02000435 RID: 1077
		internal sealed class CookieParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028BB RID: 10427 RVA: 0x000F7E74 File Offset: 0x000F6074
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

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x060028BC RID: 10428 RVA: 0x000F80C4 File Offset: 0x000F62C4
			public override string EditorName
			{
				get
				{
					return "Cookie";
				}
			}

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x060028BD RID: 10429 RVA: 0x000F80CB File Offset: 0x000F62CB
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.CookieName.Length > 0;
				}
			}

			// Token: 0x17000891 RID: 2193
			// (get) Token: 0x060028BE RID: 10430 RVA: 0x000F80E0 File Offset: 0x000F62E0
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028BF RID: 10431 RVA: 0x000F80E8 File Offset: 0x000F62E8
			public override void Initialize()
			{
				this._parameter = new CookieParameter();
				this._cookieNameTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028C0 RID: 10432 RVA: 0x000F8115 File Offset: 0x000F6315
			private void OnCookieNameTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.CookieName = this._cookieNameTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x060028C1 RID: 10433 RVA: 0x000F8133 File Offset: 0x000F6333
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x04001CEC RID: 7404
			private System.Windows.Forms.Label _cookieNameLabel;

			// Token: 0x04001CED RID: 7405
			private System.Windows.Forms.TextBox _cookieNameTextBox;

			// Token: 0x04001CEE RID: 7406
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CEF RID: 7407
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CF0 RID: 7408
			private CookieParameter _parameter;
		}

		// Token: 0x02000436 RID: 1078
		internal sealed class ControlParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028C2 RID: 10434 RVA: 0x000F814C File Offset: 0x000F634C
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

			// Token: 0x17000892 RID: 2194
			// (get) Token: 0x060028C3 RID: 10435 RVA: 0x000F8421 File Offset: 0x000F6621
			public override string EditorName
			{
				get
				{
					return "Control";
				}
			}

			// Token: 0x17000893 RID: 2195
			// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000F8428 File Offset: 0x000F6628
			public override bool HasCompleteInformation
			{
				get
				{
					return this._controlIDComboBox.SelectedItem != null;
				}
			}

			// Token: 0x17000894 RID: 2196
			// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000F8438 File Offset: 0x000F6638
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028C6 RID: 10438 RVA: 0x000F8440 File Offset: 0x000F6640
			public override void Initialize()
			{
				this._parameter = new ControlParameter();
				this._controlIDComboBox.SelectedItem = null;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028C7 RID: 10439 RVA: 0x000F846C File Offset: 0x000F666C
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

			// Token: 0x060028C8 RID: 10440 RVA: 0x000F84D7 File Offset: 0x000F66D7
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x04001CF1 RID: 7409
			private System.Windows.Forms.Label _controlIDLabel;

			// Token: 0x04001CF2 RID: 7410
			private AutoSizeComboBox _controlIDComboBox;

			// Token: 0x04001CF3 RID: 7411
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CF4 RID: 7412
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CF5 RID: 7413
			private ControlParameter _parameter;

			// Token: 0x04001CF6 RID: 7414
			private Control _control;
		}

		// Token: 0x02000437 RID: 1079
		internal sealed class FormParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028C9 RID: 10441 RVA: 0x000F84F0 File Offset: 0x000F66F0
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

			// Token: 0x17000895 RID: 2197
			// (get) Token: 0x060028CA RID: 10442 RVA: 0x000F8740 File Offset: 0x000F6940
			public override string EditorName
			{
				get
				{
					return "Form";
				}
			}

			// Token: 0x17000896 RID: 2198
			// (get) Token: 0x060028CB RID: 10443 RVA: 0x000F8747 File Offset: 0x000F6947
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.FormField.Length > 0;
				}
			}

			// Token: 0x17000897 RID: 2199
			// (get) Token: 0x060028CC RID: 10444 RVA: 0x000F875C File Offset: 0x000F695C
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028CD RID: 10445 RVA: 0x000F8764 File Offset: 0x000F6964
			public override void Initialize()
			{
				this._parameter = new FormParameter();
				this._formFieldTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028CE RID: 10446 RVA: 0x000F8791 File Offset: 0x000F6991
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x060028CF RID: 10447 RVA: 0x000F87A9 File Offset: 0x000F69A9
			private void OnFormFieldTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.FormField = this._formFieldTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001CF7 RID: 7415
			private System.Windows.Forms.Label _formFieldLabel;

			// Token: 0x04001CF8 RID: 7416
			private System.Windows.Forms.TextBox _formFieldTextBox;

			// Token: 0x04001CF9 RID: 7417
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CFA RID: 7418
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001CFB RID: 7419
			private FormParameter _parameter;
		}

		// Token: 0x02000438 RID: 1080
		internal sealed class SessionParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028D0 RID: 10448 RVA: 0x000F87C8 File Offset: 0x000F69C8
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

			// Token: 0x17000898 RID: 2200
			// (get) Token: 0x060028D1 RID: 10449 RVA: 0x000F8A18 File Offset: 0x000F6C18
			public override string EditorName
			{
				get
				{
					return "Session";
				}
			}

			// Token: 0x17000899 RID: 2201
			// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000F8A1F File Offset: 0x000F6C1F
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.SessionField.Length > 0;
				}
			}

			// Token: 0x1700089A RID: 2202
			// (get) Token: 0x060028D3 RID: 10451 RVA: 0x000F8A34 File Offset: 0x000F6C34
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028D4 RID: 10452 RVA: 0x000F8A3C File Offset: 0x000F6C3C
			public override void Initialize()
			{
				this._parameter = new SessionParameter();
				this._sessionFieldTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028D5 RID: 10453 RVA: 0x000F8A69 File Offset: 0x000F6C69
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x060028D6 RID: 10454 RVA: 0x000F8A81 File Offset: 0x000F6C81
			private void OnSessionFieldTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.SessionField = this._sessionFieldTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001CFC RID: 7420
			private System.Windows.Forms.Label _sessionFieldLabel;

			// Token: 0x04001CFD RID: 7421
			private System.Windows.Forms.TextBox _sessionFieldTextBox;

			// Token: 0x04001CFE RID: 7422
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001CFF RID: 7423
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001D00 RID: 7424
			private SessionParameter _parameter;
		}

		// Token: 0x02000439 RID: 1081
		internal sealed class QueryStringParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028D7 RID: 10455 RVA: 0x000F8AA0 File Offset: 0x000F6CA0
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

			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000F8CF0 File Offset: 0x000F6EF0
			public override string EditorName
			{
				get
				{
					return "QueryString";
				}
			}

			// Token: 0x1700089C RID: 2204
			// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000F8CF7 File Offset: 0x000F6EF7
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.QueryStringField.Length > 0;
				}
			}

			// Token: 0x1700089D RID: 2205
			// (get) Token: 0x060028DA RID: 10458 RVA: 0x000F8D0C File Offset: 0x000F6F0C
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028DB RID: 10459 RVA: 0x000F8D14 File Offset: 0x000F6F14
			public override void Initialize()
			{
				this._parameter = new QueryStringParameter();
				this._queryStringFieldTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028DC RID: 10460 RVA: 0x000F8D41 File Offset: 0x000F6F41
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x060028DD RID: 10461 RVA: 0x000F8D59 File Offset: 0x000F6F59
			private void OnQueryStringFieldTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.QueryStringField = this._queryStringFieldTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001D01 RID: 7425
			private System.Windows.Forms.Label _queryStringFieldLabel;

			// Token: 0x04001D02 RID: 7426
			private System.Windows.Forms.TextBox _queryStringFieldTextBox;

			// Token: 0x04001D03 RID: 7427
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001D04 RID: 7428
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001D05 RID: 7429
			private QueryStringParameter _parameter;
		}

		// Token: 0x0200043A RID: 1082
		internal sealed class RouteParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028DE RID: 10462 RVA: 0x000F8D78 File Offset: 0x000F6F78
			public RouteParameterEditor(IServiceProvider serviceProvider) : base(serviceProvider)
			{
				base.SuspendLayout();
				base.Size = new Size(220, 44);
				this._routeKeyLabel = new System.Windows.Forms.Label();
				this._routeKeyTextBox = new System.Windows.Forms.TextBox();
				this._defaultValueLabel = new System.Windows.Forms.Label();
				this._defaultValueTextBox = new System.Windows.Forms.TextBox();
				this._routeKeyLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._routeKeyLabel.Location = new Point(0, 0);
				this._routeKeyLabel.Name = "RouteKeyLabel";
				this._routeKeyLabel.Size = new Size(220, 16);
				this._routeKeyLabel.TabIndex = 10;
				this._routeKeyLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_RouteParameterEditor_RouteKeyLabel");
				this._routeKeyTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._routeKeyTextBox.Location = new Point(0, 23);
				this._routeKeyTextBox.Name = "RouteKeyTextBox";
				this._routeKeyTextBox.Size = new Size(220, 20);
				this._routeKeyTextBox.TabIndex = 20;
				this._routeKeyTextBox.TextChanged += this.OnRouteKeyTextBoxTextChanged;
				this._defaultValueLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueLabel.Location = new Point(0, 48);
				this._defaultValueLabel.Name = "RouteDefaultValueLabel";
				this._defaultValueLabel.Size = new Size(220, 16);
				this._defaultValueLabel.TabIndex = 30;
				this._defaultValueLabel.Text = SR.GetString("SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue");
				this._defaultValueTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this._defaultValueTextBox.Location = new Point(0, 68);
				this._defaultValueTextBox.Name = "RouteDefaultValueTextBox";
				this._defaultValueTextBox.Size = new Size(220, 20);
				this._defaultValueTextBox.TabIndex = 40;
				this._defaultValueTextBox.TextChanged += this.OnDefaultValueTextBoxTextChanged;
				base.Controls.Add(this._routeKeyLabel);
				base.Controls.Add(this._routeKeyTextBox);
				base.Controls.Add(this._defaultValueLabel);
				base.Controls.Add(this._defaultValueTextBox);
				this.Dock = DockStyle.Fill;
				base.ResumeLayout();
			}

			// Token: 0x1700089E RID: 2206
			// (get) Token: 0x060028DF RID: 10463 RVA: 0x000F8FC8 File Offset: 0x000F71C8
			public override string EditorName
			{
				get
				{
					return "Route";
				}
			}

			// Token: 0x1700089F RID: 2207
			// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000F8FCF File Offset: 0x000F71CF
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.RouteKey.Length > 0;
				}
			}

			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000F8FE4 File Offset: 0x000F71E4
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028E2 RID: 10466 RVA: 0x000F8FEC File Offset: 0x000F71EC
			public override void Initialize()
			{
				this._parameter = new RouteParameter();
				this._routeKeyTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028E3 RID: 10467 RVA: 0x000F9019 File Offset: 0x000F7219
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x060028E4 RID: 10468 RVA: 0x000F9031 File Offset: 0x000F7231
			private void OnRouteKeyTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.RouteKey = this._routeKeyTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001D06 RID: 7430
			private System.Windows.Forms.Label _routeKeyLabel;

			// Token: 0x04001D07 RID: 7431
			private System.Windows.Forms.TextBox _routeKeyTextBox;

			// Token: 0x04001D08 RID: 7432
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001D09 RID: 7433
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001D0A RID: 7434
			private RouteParameter _parameter;
		}

		// Token: 0x0200043B RID: 1083
		internal sealed class ProfileParameterEditor : SqlDataSourceConfigureFilterForm.ParameterEditor
		{
			// Token: 0x060028E5 RID: 10469 RVA: 0x000F9050 File Offset: 0x000F7250
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

			// Token: 0x170008A1 RID: 2209
			// (get) Token: 0x060028E6 RID: 10470 RVA: 0x000F92A0 File Offset: 0x000F74A0
			public override string EditorName
			{
				get
				{
					return "Profile";
				}
			}

			// Token: 0x170008A2 RID: 2210
			// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000F92A7 File Offset: 0x000F74A7
			public override bool HasCompleteInformation
			{
				get
				{
					return this._parameter.PropertyName.Length > 0;
				}
			}

			// Token: 0x170008A3 RID: 2211
			// (get) Token: 0x060028E8 RID: 10472 RVA: 0x000F92BC File Offset: 0x000F74BC
			public override Parameter Parameter
			{
				get
				{
					return this._parameter;
				}
			}

			// Token: 0x060028E9 RID: 10473 RVA: 0x000F92C4 File Offset: 0x000F74C4
			public override void Initialize()
			{
				this._parameter = new ProfileParameter();
				this._propertyNameTextBox.Text = string.Empty;
				this._defaultValueTextBox.Text = string.Empty;
			}

			// Token: 0x060028EA RID: 10474 RVA: 0x000F92F1 File Offset: 0x000F74F1
			private void OnDefaultValueTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.DefaultValue = this._defaultValueTextBox.Text;
			}

			// Token: 0x060028EB RID: 10475 RVA: 0x000F9309 File Offset: 0x000F7509
			private void OnPropertyNameTextBoxTextChanged(object s, EventArgs e)
			{
				this._parameter.PropertyName = this._propertyNameTextBox.Text;
				base.OnParameterChanged();
			}

			// Token: 0x04001D0B RID: 7435
			private System.Windows.Forms.Label _propertyNameLabel;

			// Token: 0x04001D0C RID: 7436
			private System.Windows.Forms.TextBox _propertyNameTextBox;

			// Token: 0x04001D0D RID: 7437
			private System.Windows.Forms.Label _defaultValueLabel;

			// Token: 0x04001D0E RID: 7438
			private System.Windows.Forms.TextBox _defaultValueTextBox;

			// Token: 0x04001D0F RID: 7439
			private ProfileParameter _parameter;
		}
	}
}
