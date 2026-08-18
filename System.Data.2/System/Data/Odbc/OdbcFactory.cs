using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x0200029E RID: 670
	public sealed class OdbcFactory : DbProviderFactory
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x00110D80 File Offset: 0x00110180
		private OdbcFactory()
		{
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x00110D94 File Offset: 0x00110194
		public override DbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00110DA8 File Offset: 0x001101A8
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OdbcCommandBuilder();
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x00110DBC File Offset: 0x001101BC
		public override DbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x00110DD0 File Offset: 0x001101D0
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OdbcConnectionStringBuilder();
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00110DE4 File Offset: 0x001101E4
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00110DF8 File Offset: 0x001101F8
		public override DbParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00110E0C File Offset: 0x0011020C
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OdbcPermission(state);
		}

		// Token: 0x04001AAA RID: 6826
		public static readonly OdbcFactory Instance = new OdbcFactory();
	}
}
