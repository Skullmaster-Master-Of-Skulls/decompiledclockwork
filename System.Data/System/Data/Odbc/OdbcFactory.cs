using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x020001EE RID: 494
	public sealed class OdbcFactory : DbProviderFactory
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x00263B58 File Offset: 0x00262F58
		private OdbcFactory()
		{
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00263B78 File Offset: 0x00262F78
		public override DbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00263B98 File Offset: 0x00262F98
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OdbcCommandBuilder();
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00263BB8 File Offset: 0x00262FB8
		public override DbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00263BD8 File Offset: 0x00262FD8
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OdbcConnectionStringBuilder();
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00263BF8 File Offset: 0x00262FF8
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00263C18 File Offset: 0x00263018
		public override DbParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00263C38 File Offset: 0x00263038
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OdbcPermission(state);
		}

		// Token: 0x0400101E RID: 4126
		public static readonly OdbcFactory Instance = new OdbcFactory();
	}
}
