using System;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x02000142 RID: 322
	public abstract class DbProviderFactory
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00241BD8 File Offset: 0x00240FD8
		public virtual bool CanCreateDataSourceEnumerator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00241BE8 File Offset: 0x00240FE8
		public virtual DbCommand CreateCommand()
		{
			return null;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00241BF8 File Offset: 0x00240FF8
		public virtual DbCommandBuilder CreateCommandBuilder()
		{
			return null;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00241C08 File Offset: 0x00241008
		public virtual DbConnection CreateConnection()
		{
			return null;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00241C18 File Offset: 0x00241018
		public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return null;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00241C28 File Offset: 0x00241028
		public virtual DbDataAdapter CreateDataAdapter()
		{
			return null;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00241C38 File Offset: 0x00241038
		public virtual DbParameter CreateParameter()
		{
			return null;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00241C48 File Offset: 0x00241048
		public virtual CodeAccessPermission CreatePermission(PermissionState state)
		{
			return null;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00241C58 File Offset: 0x00241058
		public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return null;
		}
	}
}
