using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.EntityClient
{
	// Token: 0x02000123 RID: 291
	public sealed class EntityProviderFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06000FAD RID: 4013 RVA: 0x00041A5E File Offset: 0x0003FC5E
		private EntityProviderFactory()
		{
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00041A66 File Offset: 0x0003FC66
		public override DbCommand CreateCommand()
		{
			return new EntityCommand();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00041A6D File Offset: 0x0003FC6D
		public override DbConnection CreateConnection()
		{
			return new EntityConnection();
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00041A74 File Offset: 0x0003FC74
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new EntityConnectionStringBuilder();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbDataAdapter CreateDataAdapter()
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003F4F1 File Offset: 0x0003D6F1
		public override DbParameter CreateParameter()
		{
			return new EntityParameter();
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00013A81 File Offset: 0x00011C81
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00041A7C File Offset: 0x0003FC7C
		object IServiceProvider.GetService(Type serviceType)
		{
			object result = null;
			if (serviceType == typeof(DbProviderServices))
			{
				result = EntityProviderServices.Instance;
			}
			else if (serviceType == typeof(IEntityAdapter))
			{
				result = new EntityAdapter();
			}
			return result;
		}

		// Token: 0x04000A2F RID: 2607
		public static readonly EntityProviderFactory Instance = new EntityProviderFactory();
	}
}
