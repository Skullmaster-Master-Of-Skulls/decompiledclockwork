using System;
using System.Collections;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004DA RID: 1242
	internal partial class SqlDataSourceRefreshSchemaForm : DesignerForm
	{
		// Token: 0x06002C99 RID: 11417 RVA: 0x000FB5D4 File Offset: 0x000FA5D4
		public SqlDataSourceRefreshSchemaForm(IServiceProvider serviceProvider, SqlDataSourceDesigner sqlDataSourceDesigner, ParameterCollection parameters) : base(serviceProvider)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this._sqlDataSource = (SqlDataSource)this._sqlDataSourceDesigner.Component;
			this._connectionString = this._sqlDataSourceDesigner.ConnectionString;
			this._providerName = this._sqlDataSourceDesigner.ProviderName;
			this._selectCommand = this._sqlDataSourceDesigner.SelectCommand;
			this._selectCommandType = this._sqlDataSource.SelectCommandType;
			this.InitializeComponent();
			this.InitializeUI();
			Array values = Enum.GetValues(typeof(TypeCode));
			Array.Sort(values, new SqlDataSourceRefreshSchemaForm.TypeCodeComparer());
			foreach (object obj in values)
			{
				TypeCode typeCode = (TypeCode)obj;
				((DataGridViewComboBoxColumn)this._parametersDataGridView.Columns[1]).Items.Add(typeCode);
			}
			Array values2 = Enum.GetValues(typeof(DbType));
			Array.Sort(values2, new SqlDataSourceRefreshSchemaForm.DbTypeComparer());
			foreach (object obj2 in values2)
			{
				DbType dbType = (DbType)obj2;
				((DataGridViewComboBoxColumn)this._parametersDataGridView.Columns[2]).Items.Add(dbType);
			}
			ArrayList arrayList = new ArrayList(parameters.Count);
			foreach (object obj3 in parameters)
			{
				Parameter p = (Parameter)obj3;
				arrayList.Add(new SqlDataSourceRefreshSchemaForm.ParameterItem(p));
			}
			this._parametersDataGridView.DataSource = arrayList;
			this._commandTextBox.Text = this._selectCommand;
			this._commandTextBox.Select(0, 0);
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000FB7F0 File Offset: 0x000FA7F0
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.RefreshSchema";
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000FBD3C File Offset: 0x000FAD3C
		private void InitializeUI()
		{
			this.Text = SR.GetString("SqlDataSourceRefreshSchemaForm_Title", new object[]
			{
				this._sqlDataSource.ID
			});
			this._helpLabel.Text = SR.GetString("SqlDataSourceRefreshSchemaForm_HelpLabel");
			this._commandLabel.Text = SR.GetString("SqlDataSource_General_PreviewLabel");
			this._parametersLabel.Text = SR.GetString("SqlDataSourceRefreshSchemaForm_ParametersLabel");
			this._parametersDataGridView.AccessibleName = SR.GetString("SqlDataSourceParameterValueEditorForm_ParametersGridAccessibleName");
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this._parametersDataGridView.Columns[0].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_ParameterColumnHeader");
			this._parametersDataGridView.Columns[1].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_TypeColumnHeader");
			this._parametersDataGridView.Columns[2].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_DbTypeColumnHeader");
			this._parametersDataGridView.Columns[3].HeaderText = SR.GetString("SqlDataSourceParameterValueEditorForm_ValueColumnHeader");
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000FBE70 File Offset: 0x000FAE70
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			ICollection collection = (ICollection)this._parametersDataGridView.DataSource;
			ParameterCollection parameterCollection = new ParameterCollection();
			foreach (object obj in collection)
			{
				SqlDataSourceRefreshSchemaForm.ParameterItem parameterItem = (SqlDataSourceRefreshSchemaForm.ParameterItem)obj;
				if (parameterItem.DbType == DbType.Object)
				{
					parameterCollection.Add(new Parameter(parameterItem.Name, parameterItem.Type, parameterItem.DefaultValue));
				}
				else
				{
					parameterCollection.Add(new Parameter(parameterItem.Name, parameterItem.DbType, parameterItem.DefaultValue));
				}
			}
			bool flag = this._sqlDataSourceDesigner.RefreshSchema(new DesignerDataConnection(string.Empty, this._providerName, this._connectionString), this._selectCommand, this._selectCommandType, parameterCollection, false);
			if (flag)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000FBF64 File Offset: 0x000FAF64
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				int num = (int)Math.Floor((double)(this._parametersDataGridView.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 2 * SystemInformation.Border3DSize.Width) / 4.5);
				this._parametersDataGridView.Columns[0].Width = (int)((double)num * 1.5);
				this._parametersDataGridView.Columns[1].Width = num;
				this._parametersDataGridView.Columns[2].Width = num;
				this._parametersDataGridView.Columns[3].Width = num;
				this._parametersDataGridView.AutoResizeColumnHeadersHeight();
				for (int i = 0; i < this._parametersDataGridView.Rows.Count; i++)
				{
					this._parametersDataGridView.AutoResizeRow(i, DataGridViewAutoSizeRowMode.AllCells);
				}
			}
		}

		// Token: 0x04001E75 RID: 7797
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001E76 RID: 7798
		private SqlDataSource _sqlDataSource;

		// Token: 0x04001E77 RID: 7799
		private string _connectionString;

		// Token: 0x04001E78 RID: 7800
		private string _providerName;

		// Token: 0x04001E79 RID: 7801
		private string _selectCommand;

		// Token: 0x04001E7A RID: 7802
		private SqlDataSourceCommandType _selectCommandType;

		// Token: 0x020004DB RID: 1243
		private sealed class ParameterItem
		{
			// Token: 0x06002C9F RID: 11423 RVA: 0x000FC059 File Offset: 0x000FB059
			public ParameterItem(Parameter p)
			{
				this._name = p.Name;
				this._dbType = p.DbType;
				this._type = p.Type;
				this._defaultValue = p.DefaultValue;
			}

			// Token: 0x1700085C RID: 2140
			// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000FC091 File Offset: 0x000FB091
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x1700085D RID: 2141
			// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000FC099 File Offset: 0x000FB099
			// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x000FC0A1 File Offset: 0x000FB0A1
			public DbType DbType
			{
				get
				{
					return this._dbType;
				}
				set
				{
					this._dbType = value;
				}
			}

			// Token: 0x1700085E RID: 2142
			// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000FC0AA File Offset: 0x000FB0AA
			// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x000FC0B2 File Offset: 0x000FB0B2
			public TypeCode Type
			{
				get
				{
					return this._type;
				}
				set
				{
					this._type = value;
				}
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000FC0BB File Offset: 0x000FB0BB
			// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x000FC0C3 File Offset: 0x000FB0C3
			public string DefaultValue
			{
				get
				{
					return this._defaultValue;
				}
				set
				{
					this._defaultValue = value;
				}
			}

			// Token: 0x04001E7B RID: 7803
			private string _name;

			// Token: 0x04001E7C RID: 7804
			private DbType _dbType;

			// Token: 0x04001E7D RID: 7805
			private TypeCode _type;

			// Token: 0x04001E7E RID: 7806
			private string _defaultValue;
		}

		// Token: 0x020004DC RID: 1244
		private sealed class TypeCodeComparer : IComparer
		{
			// Token: 0x06002CA7 RID: 11431 RVA: 0x000FC0CC File Offset: 0x000FB0CC
			int IComparer.Compare(object x, object y)
			{
				return string.Compare(Enum.GetName(typeof(TypeCode), x), Enum.GetName(typeof(TypeCode), y), StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x020004DD RID: 1245
		private sealed class DbTypeComparer : IComparer
		{
			// Token: 0x06002CA9 RID: 11433 RVA: 0x000FC0FC File Offset: 0x000FB0FC
			int IComparer.Compare(object x, object y)
			{
				return string.Compare(Enum.GetName(typeof(DbType), x), Enum.GetName(typeof(DbType), y), StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
