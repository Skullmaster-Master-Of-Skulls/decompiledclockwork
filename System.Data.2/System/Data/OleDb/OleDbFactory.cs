using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x02000251 RID: 593
	public sealed class OleDbFactory : DbProviderFactory
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x00100C94 File Offset: 0x00100094
		private OleDbFactory()
		{
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00100CA8 File Offset: 0x001000A8
		public override DbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00100CBC File Offset: 0x001000BC
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OleDbCommandBuilder();
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00100CD0 File Offset: 0x001000D0
		public override DbConnection CreateConnection()
		{
			return new OleDbConnection();
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00100CE4 File Offset: 0x001000E4
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OleDbConnectionStringBuilder();
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00100CF8 File Offset: 0x001000F8
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00100D0C File Offset: 0x0010010C
		public override DbParameter CreateParameter()
		{
			return new OleDbParameter();
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00100D20 File Offset: 0x00100120
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OleDbPermission(state);
		}

		// Token: 0x0400160C RID: 5644
		public static readonly OleDbFactory Instance = new OleDbFactory();
	}
}
