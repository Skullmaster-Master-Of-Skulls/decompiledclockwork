using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004D9 RID: 1241
	internal partial class SqlDataSourceQueryEditorForm : DesignerForm
	{
		// Token: 0x06002C90 RID: 11408 RVA: 0x000FADA4 File Offset: 0x000F9DA4
		public SqlDataSourceQueryEditorForm(IServiceProvider serviceProvider, SqlDataSourceDesigner sqlDataSourceDesigner, string providerName, string connectionString, DataSourceOperation operation, SqlDataSourceCommandType commandType, string command, IList originalParameters) : base(serviceProvider)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this.InitializeComponent();
			this.InitializeUI();
			if (string.IsNullOrEmpty(providerName))
			{
				providerName = "System.Data.SqlClient";
			}
			this._dataConnection = new DesignerDataConnection(string.Empty, providerName, connectionString);
			this._commandType = commandType;
			this._commandTextBox.Text = command;
			this._originalParameters = originalParameters;
			string text = Enum.GetName(typeof(DataSourceOperation), operation).ToUpperInvariant();
			this._commandLabel.Text = SR.GetString("SqlDataSourceQueryEditorForm_CommandLabel", new object[]
			{
				text
			});
			ArrayList arrayList = new ArrayList(originalParameters.Count);
			sqlDataSourceDesigner.CopyList(originalParameters, arrayList);
			this._parameterEditorUserControl.AddParameters((Parameter[])arrayList.ToArray(typeof(Parameter)));
			this._commandTextBox.Select(0, 0);
			switch (operation)
			{
			case DataSourceOperation.Delete:
				this._queryBuilderMode = QueryBuilderMode.Delete;
				return;
			case DataSourceOperation.Insert:
				this._queryBuilderMode = QueryBuilderMode.Insert;
				return;
			case DataSourceOperation.Select:
				this._queryBuilderMode = QueryBuilderMode.Select;
				return;
			case DataSourceOperation.Update:
				this._queryBuilderMode = QueryBuilderMode.Update;
				return;
			default:
				return;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000FAEC3 File Offset: 0x000F9EC3
		public string Command
		{
			get
			{
				return this._commandTextBox.Text;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000FAED0 File Offset: 0x000F9ED0
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.QueryEditor";
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000FB264 File Offset: 0x000FA264
		private void InitializeUI()
		{
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this._inferParametersButton.Text = SR.GetString("SqlDataSourceQueryEditorForm_InferParametersButton");
			this._queryBuilderButton.Text = SR.GetString("SqlDataSourceQueryEditorForm_QueryBuilderButton");
			this.Text = SR.GetString("SqlDataSourceQueryEditorForm_Caption");
			this._dataEnvironment = (IDataEnvironment)base.ServiceProvider.GetService(typeof(IDataEnvironment));
			this._queryBuilderButton.Enabled = (this._dataEnvironment != null);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000FB30C File Offset: 0x000FA30C
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000FB31C File Offset: 0x000FA31C
		private void OnInferParametersButtonClick(object sender, EventArgs e)
		{
			if (this._commandTextBox.Text.Trim().Length == 0)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("SqlDataSourceQueryEditorForm_InferNeedsCommand"));
				return;
			}
			Parameter[] array = this._sqlDataSourceDesigner.InferParameterNames(this._dataConnection, this._commandTextBox.Text, this._commandType);
			if (array != null)
			{
				Parameter[] parameters = this._parameterEditorUserControl.GetParameters();
				StringCollection stringCollection = new StringCollection();
				foreach (Parameter parameter in parameters)
				{
					stringCollection.Add(parameter.Name);
				}
				bool flag = true;
				try
				{
					DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this._dataConnection.ProviderName);
					flag = SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory);
				}
				catch
				{
				}
				if (flag)
				{
					List<Parameter> list = new List<Parameter>();
					foreach (Parameter parameter2 in array)
					{
						if (!stringCollection.Contains(parameter2.Name))
						{
							list.Add(parameter2);
						}
						else
						{
							stringCollection.Remove(parameter2.Name);
						}
					}
					this._parameterEditorUserControl.AddParameters(list.ToArray());
					return;
				}
				List<Parameter> list2 = new List<Parameter>();
				foreach (Parameter item in array)
				{
					list2.Add(item);
				}
				foreach (Parameter parameter3 in parameters)
				{
					Parameter parameter4 = null;
					foreach (Parameter parameter5 in list2)
					{
						if (parameter5.Direction == parameter3.Direction)
						{
							parameter4 = parameter5;
							break;
						}
					}
					if (parameter4 != null)
					{
						list2.Remove(parameter4);
					}
				}
				this._parameterEditorUserControl.AddParameters(list2.ToArray());
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000FB50C File Offset: 0x000FA50C
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			this._sqlDataSourceDesigner.CopyList(this._parameterEditorUserControl.GetParameters(), this._originalParameters);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000FB538 File Offset: 0x000FA538
		private void OnQueryBuilderButtonClick(object sender, EventArgs e)
		{
			if (this._dataConnection.ConnectionString == null || this._dataConnection.ConnectionString.Trim().Length == 0)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("SqlDataSourceQueryEditorForm_QueryBuilderNeedsConnectionString"));
				return;
			}
			string text = this._dataEnvironment.BuildQuery(this, this._dataConnection, this._queryBuilderMode, this._commandTextBox.Text);
			if (text != null && text.Length > 0)
			{
				this._commandTextBox.Text = text;
			}
			this._commandTextBox.Focus();
			this._commandTextBox.Select(0, 0);
		}

		// Token: 0x04001E68 RID: 7784
		private QueryBuilderMode _queryBuilderMode;

		// Token: 0x04001E69 RID: 7785
		private IDataEnvironment _dataEnvironment;

		// Token: 0x04001E6A RID: 7786
		private SqlDataSourceCommandType _commandType;

		// Token: 0x04001E6B RID: 7787
		private DesignerDataConnection _dataConnection;

		// Token: 0x04001E6C RID: 7788
		private IList _originalParameters;
	}
}
