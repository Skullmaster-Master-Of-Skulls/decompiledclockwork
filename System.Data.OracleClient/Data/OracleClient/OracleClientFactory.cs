using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OracleClient
{
	// Token: 0x02000066 RID: 102
	public sealed class OracleClientFactory : DbProviderFactory
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00069594 File Offset: 0x00068994
		private OracleClientFactory()
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000695B4 File Offset: 0x000689B4
		public override DbCommand CreateCommand()
		{
			return new OracleCommand();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000695D4 File Offset: 0x000689D4
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OracleCommandBuilder();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000695F4 File Offset: 0x000689F4
		public override DbConnection CreateConnection()
		{
			return new OracleConnection();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00069614 File Offset: 0x00068A14
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OracleConnectionStringBuilder();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00069634 File Offset: 0x00068A34
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OracleDataAdapter();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00069654 File Offset: 0x00068A54
		public override DbParameter CreateParameter()
		{
			return new OracleParameter();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00069674 File Offset: 0x00068A74
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OraclePermission(state);
		}

		// Token: 0x0400042C RID: 1068
		public static readonly OracleClientFactory Instance = new OracleClientFactory();
	}
}
