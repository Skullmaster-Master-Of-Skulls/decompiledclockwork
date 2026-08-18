using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020001AE RID: 430
	public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x000B1D28 File Offset: 0x000B1128
		private SqlClientFactory()
		{
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x000B1D3C File Offset: 0x000B113C
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000B1D4C File Offset: 0x000B114C
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000B1D60 File Offset: 0x000B1160
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000B1D74 File Offset: 0x000B1174
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000B1D88 File Offset: 0x000B1188
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000B1D9C File Offset: 0x000B119C
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000B1DB0 File Offset: 0x000B11B0
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000B1DC4 File Offset: 0x000B11C4
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000B1DD8 File Offset: 0x000B11D8
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000B1DEC File Offset: 0x000B11EC
		object IServiceProvider.GetService(Type serviceType)
		{
			object result = null;
			if (serviceType == GreenMethods.SystemDataCommonDbProviderServices_Type)
			{
				result = GreenMethods.SystemDataSqlClientSqlProviderServices_Instance();
			}
			return result;
		}

		// Token: 0x04000EFC RID: 3836
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
