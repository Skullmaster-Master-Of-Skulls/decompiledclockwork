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
	// Token: 0x02000118 RID: 280
	internal partial class SqlDataSourceRefreshSchemaForm : DesignerForm
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x000402BC File Offset: 0x0003E4BC
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000404D4 File Offset: 0x0003E6D4
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.RefreshSchema";
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00040A20 File Offset: 0x0003EC20
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

		// Token: 0x06000A31 RID: 2609 RVA: 0x00040B50 File Offset: 0x0003ED50
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

		// Token: 0x06000A32 RID: 2610 RVA: 0x00040C48 File Offset: 0x0003EE48
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

		// Token: 0x0400061B RID: 1563
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x0400061C RID: 1564
		private SqlDataSource _sqlDataSource;

		// Token: 0x0400061D RID: 1565
		private string _connectionString;

		// Token: 0x0400061E RID: 1566
		private string _providerName;

		// Token: 0x0400061F RID: 1567
		private string _selectCommand;

		// Token: 0x04000620 RID: 1568
		private SqlDataSourceCommandType _selectCommandType;

		// Token: 0x02000449 RID: 1097
		private sealed class ParameterItem
		{
			// Token: 0x06002919 RID: 10521 RVA: 0x000F9A20 File Offset: 0x000F7C20
			public ParameterItem(Parameter p)
			{
				this._name = p.Name;
				this._dbType = p.DbType;
				this._type = p.Type;
				this._defaultValue = p.DefaultValue;
			}

			// Token: 0x170008AE RID: 2222
			// (get) Token: 0x0600291A RID: 10522 RVA: 0x000F9A58 File Offset: 0x000F7C58
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170008AF RID: 2223
			// (get) Token: 0x0600291B RID: 10523 RVA: 0x000F9A60 File Offset: 0x000F7C60
			// (set) Token: 0x0600291C RID: 10524 RVA: 0x000F9A68 File Offset: 0x000F7C68
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

			// Token: 0x170008B0 RID: 2224
			// (get) Token: 0x0600291D RID: 10525 RVA: 0x000F9A71 File Offset: 0x000F7C71
			// (set) Token: 0x0600291E RID: 10526 RVA: 0x000F9A79 File Offset: 0x000F7C79
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

			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x0600291F RID: 10527 RVA: 0x000F9A82 File Offset: 0x000F7C82
			// (set) Token: 0x06002920 RID: 10528 RVA: 0x000F9A8A File Offset: 0x000F7C8A
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

			// Token: 0x04001D1C RID: 7452
			private string _name;

			// Token: 0x04001D1D RID: 7453
			private DbType _dbType;

			// Token: 0x04001D1E RID: 7454
			private TypeCode _type;

			// Token: 0x04001D1F RID: 7455
			private string _defaultValue;
		}

		// Token: 0x0200044A RID: 1098
		private sealed class TypeCodeComparer : IComparer
		{
			// Token: 0x06002921 RID: 10529 RVA: 0x000F9A93 File Offset: 0x000F7C93
			int IComparer.Compare(object x, object y)
			{
				return string.Compare(Enum.GetName(typeof(TypeCode), x), Enum.GetName(typeof(TypeCode), y), StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0200044B RID: 1099
		private sealed class DbTypeComparer : IComparer
		{
			// Token: 0x06002923 RID: 10531 RVA: 0x000F9ABB File Offset: 0x000F7CBB
			int IComparer.Compare(object x, object y)
			{
				return string.Compare(Enum.GetName(typeof(DbType), x), Enum.GetName(typeof(DbType), y), StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
