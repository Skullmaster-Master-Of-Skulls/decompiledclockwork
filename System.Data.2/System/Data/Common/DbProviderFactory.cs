using System;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x020002FA RID: 762
	public abstract class DbProviderFactory
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x0012EE40 File Offset: 0x0012E240
		public virtual bool CanCreateDataSourceEnumerator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0012EE50 File Offset: 0x0012E250
		public virtual DbCommand CreateCommand()
		{
			return null;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x0012EE60 File Offset: 0x0012E260
		public virtual DbCommandBuilder CreateCommandBuilder()
		{
			return null;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x0012EE70 File Offset: 0x0012E270
		public virtual DbConnection CreateConnection()
		{
			return null;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0012EE80 File Offset: 0x0012E280
		public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return null;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0012EE90 File Offset: 0x0012E290
		public virtual DbDataAdapter CreateDataAdapter()
		{
			return null;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0012EEA0 File Offset: 0x0012E2A0
		public virtual DbParameter CreateParameter()
		{
			return null;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0012EEB0 File Offset: 0x0012E2B0
		public virtual CodeAccessPermission CreatePermission(PermissionState state)
		{
			return null;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0012EEC0 File Offset: 0x0012E2C0
		public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return null;
		}
	}
}
