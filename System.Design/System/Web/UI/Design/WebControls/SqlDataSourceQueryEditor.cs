using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004D8 RID: 1240
	internal sealed class SqlDataSourceQueryEditor : UITypeEditor
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x000FABC0 File Offset: 0x000F9BC0
		private bool EditQueryChangeCallback(object context)
		{
			SqlDataSource sqlDataSource = (SqlDataSource)((Pair)context).First;
			DataSourceOperation operation = (DataSourceOperation)((Pair)context).Second;
			IServiceProvider site = sqlDataSource.Site;
			IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
			SqlDataSourceDesigner sqlDataSourceDesigner = (SqlDataSourceDesigner)designerHost.GetDesigner(sqlDataSource);
			ParameterCollection originalParameters = null;
			string command = string.Empty;
			SqlDataSourceCommandType commandType = SqlDataSourceCommandType.Text;
			switch (operation)
			{
			case DataSourceOperation.Delete:
				originalParameters = sqlDataSource.DeleteParameters;
				command = sqlDataSource.DeleteCommand;
				commandType = sqlDataSource.DeleteCommandType;
				break;
			case DataSourceOperation.Insert:
				originalParameters = sqlDataSource.InsertParameters;
				command = sqlDataSource.InsertCommand;
				commandType = sqlDataSource.InsertCommandType;
				break;
			case DataSourceOperation.Select:
				originalParameters = sqlDataSource.SelectParameters;
				command = sqlDataSource.SelectCommand;
				commandType = sqlDataSource.SelectCommandType;
				break;
			case DataSourceOperation.Update:
				originalParameters = sqlDataSource.UpdateParameters;
				command = sqlDataSource.UpdateCommand;
				commandType = sqlDataSource.UpdateCommandType;
				break;
			}
			SqlDataSourceQueryEditorForm sqlDataSourceQueryEditorForm = new SqlDataSourceQueryEditorForm(site, sqlDataSourceDesigner, sqlDataSource.ProviderName, sqlDataSourceDesigner.ConnectionString, operation, commandType, command, originalParameters);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(site, sqlDataSourceQueryEditorForm);
			if (dialogResult == DialogResult.OK)
			{
				PropertyDescriptor propertyDescriptor = null;
				switch (operation)
				{
				case DataSourceOperation.Delete:
					propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["DeleteCommand"];
					break;
				case DataSourceOperation.Insert:
					propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["InsertCommand"];
					break;
				case DataSourceOperation.Select:
					propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["SelectCommand"];
					break;
				case DataSourceOperation.Update:
					propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["UpdateCommand"];
					break;
				}
				if (propertyDescriptor != null)
				{
					propertyDescriptor.ResetValue(sqlDataSource);
					propertyDescriptor.SetValue(sqlDataSource, sqlDataSourceQueryEditorForm.Command);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000FAD64 File Offset: 0x000F9D64
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ControlDesigner.InvokeTransactedChange((IComponent)context.Instance, new TransactedChangeCallback(this.EditQueryChangeCallback), new Pair(context.Instance, value), SR.GetString("SqlDataSourceDesigner_EditQueryTransactionDescription"));
			return value;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000FAD99 File Offset: 0x000F9D99
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
