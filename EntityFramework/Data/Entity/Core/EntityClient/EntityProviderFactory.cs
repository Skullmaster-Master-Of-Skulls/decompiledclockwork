using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x0200033E RID: 830
	[SuppressMessage("Microsoft.Usage", "CA2302", Justification = "We don't expect serviceType to be an Embedded Interop Types.")]
	public sealed class EntityProviderFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06001D9D RID: 7581 RVA: 0x0008EF61 File Offset: 0x0008D161
		private EntityProviderFactory()
		{
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0008EF69 File Offset: 0x0008D169
		public override DbCommand CreateCommand()
		{
			return new EntityCommand();
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0008EF70 File Offset: 0x0008D170
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0008EF77 File Offset: 0x0008D177
		public override DbConnection CreateConnection()
		{
			return new EntityConnection();
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0008EF7E File Offset: 0x0008D17E
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new EntityConnectionStringBuilder();
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0008EF85 File Offset: 0x0008D185
		public override DbDataAdapter CreateDataAdapter()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0008EF8C File Offset: 0x0008D18C
		public override DbParameter CreateParameter()
		{
			return new EntityParameter();
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0008EF93 File Offset: 0x0008D193
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0008EF9A File Offset: 0x0008D19A
		object IServiceProvider.GetService(Type serviceType)
		{
			if (!(serviceType == typeof(DbProviderServices)))
			{
				return null;
			}
			return EntityProviderServices.Instance;
		}

		// Token: 0x04000A19 RID: 2585
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EntityProviderFactory implements the singleton pattern and it's stateless.  This is needed in order to work with DbProviderFactories.")]
		public static readonly EntityProviderFactory Instance = new EntityProviderFactory();
	}
}
