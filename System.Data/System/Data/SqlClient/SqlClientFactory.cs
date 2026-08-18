using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020002BC RID: 700
	public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x0600235A RID: 9050 RVA: 0x00290AC8 File Offset: 0x0028FEC8
		private SqlClientFactory()
		{
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x00290AE8 File Offset: 0x0028FEE8
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00290AF8 File Offset: 0x0028FEF8
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00290B18 File Offset: 0x0028FF18
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00290B38 File Offset: 0x0028FF38
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00290B58 File Offset: 0x0028FF58
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00290B78 File Offset: 0x0028FF78
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00290B98 File Offset: 0x0028FF98
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00290BB8 File Offset: 0x0028FFB8
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00290BD8 File Offset: 0x0028FFD8
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x00290BF8 File Offset: 0x0028FFF8
		object IServiceProvider.GetService(Type serviceType)
		{
			object result = null;
			if (serviceType == GreenMethods.SystemDataCommonDbProviderServices_Type)
			{
				result = GreenMethods.SystemDataSqlClientSqlProviderServices_Instance();
			}
			return result;
		}

		// Token: 0x0400170D RID: 5901
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
