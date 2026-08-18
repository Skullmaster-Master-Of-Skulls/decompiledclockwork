using System;
using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200010D RID: 269
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SqlDataSourceConnectionStringEditor : ConnectionStringEditor
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0003BB20 File Offset: 0x00039D20
		protected override string GetProviderName(object instance)
		{
			SqlDataSource sqlDataSource = instance as SqlDataSource;
			if (sqlDataSource != null)
			{
				return sqlDataSource.ProviderName;
			}
			return string.Empty;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003BB44 File Offset: 0x00039D44
		protected override void SetProviderName(object instance, DesignerDataConnection connection)
		{
			SqlDataSource sqlDataSource = instance as SqlDataSource;
			if (sqlDataSource != null)
			{
				if (connection.IsConfigured)
				{
					ExpressionEditor expressionEditor = ExpressionEditor.GetExpressionEditor(typeof(ConnectionStringsExpressionBuilder), sqlDataSource.Site);
					if (expressionEditor != null)
					{
						string expressionPrefix = expressionEditor.ExpressionPrefix;
						ExpressionBindingCollection expressions = ((IExpressionsAccessor)sqlDataSource).Expressions;
						expressions.Add(new ExpressionBinding("ProviderName", typeof(string), expressionPrefix, connection.Name + ".ProviderName"));
						return;
					}
				}
				else
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(sqlDataSource)["ProviderName"];
					propertyDescriptor.SetValue(sqlDataSource, connection.ProviderName);
				}
			}
		}
	}
}
