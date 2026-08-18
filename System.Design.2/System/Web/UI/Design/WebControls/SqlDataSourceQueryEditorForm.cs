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
	// Token: 0x02000117 RID: 279
	internal partial class SqlDataSourceQueryEditorForm : DesignerForm
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x0003FAA0 File Offset: 0x0003DCA0
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0003FBBB File Offset: 0x0003DDBB
		public string Command
		{
			get
			{
				return this._commandTextBox.Text;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0003FBC8 File Offset: 0x0003DDC8
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.QueryEditor";
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0003FF5C File Offset: 0x0003E15C
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

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00040004 File Offset: 0x0003E204
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

		// Token: 0x06000A2B RID: 2603 RVA: 0x000401F4 File Offset: 0x0003E3F4
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			this._sqlDataSourceDesigner.CopyList(this._parameterEditorUserControl.GetParameters(), this._originalParameters);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00040220 File Offset: 0x0003E420
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

		// Token: 0x0400060E RID: 1550
		private QueryBuilderMode _queryBuilderMode;

		// Token: 0x0400060F RID: 1551
		private IDataEnvironment _dataEnvironment;

		// Token: 0x04000610 RID: 1552
		private SqlDataSourceCommandType _commandType;

		// Token: 0x04000611 RID: 1553
		private DesignerDataConnection _dataConnection;

		// Token: 0x04000612 RID: 1554
		private IList _originalParameters;
	}
}
