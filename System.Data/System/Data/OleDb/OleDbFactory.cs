using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x02000229 RID: 553
	public sealed class OleDbFactory : DbProviderFactory
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x0027B848 File Offset: 0x0027AC48
		private OleDbFactory()
		{
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0027B868 File Offset: 0x0027AC68
		public override DbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0027B888 File Offset: 0x0027AC88
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OleDbCommandBuilder();
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0027B8A8 File Offset: 0x0027ACA8
		public override DbConnection CreateConnection()
		{
			return new OleDbConnection();
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0027B8C8 File Offset: 0x0027ACC8
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OleDbConnectionStringBuilder();
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0027B8E8 File Offset: 0x0027ACE8
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0027B908 File Offset: 0x0027AD08
		public override DbParameter CreateParameter()
		{
			return new OleDbParameter();
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0027B928 File Offset: 0x0027AD28
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OleDbPermission(state);
		}

		// Token: 0x040012F7 RID: 4855
		public static readonly OleDbFactory Instance = new OleDbFactory();
	}
}
