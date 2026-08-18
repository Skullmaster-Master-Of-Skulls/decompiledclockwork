using System;
using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004C5 RID: 1221
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SqlDataSourceConnectionStringEditor : ConnectionStringEditor
	{
		// Token: 0x06002C2F RID: 11311 RVA: 0x000F79D4 File Offset: 0x000F69D4
		protected override string GetProviderName(object instance)
		{
			SqlDataSource sqlDataSource = instance as SqlDataSource;
			if (sqlDataSource != null)
			{
				return sqlDataSource.ProviderName;
			}
			return string.Empty;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000F79F8 File Offset: 0x000F69F8
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
