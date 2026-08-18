using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal.MockingProxies;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006B7 RID: 1719
	internal class ClonedObjectContext : IDisposable
	{
		// Token: 0x06004477 RID: 17527 RVA: 0x0014410B File Offset: 0x0014230B
		protected ClonedObjectContext()
		{
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x00144114 File Offset: 0x00142314
		public ClonedObjectContext(ObjectContextProxy objectContext, DbConnection connection, string connectionString, bool transferLoadedAssemblies = true)
		{
			if (connection == null || connection.State != ConnectionState.Open)
			{
				connection = DbProviderServices.GetProviderFactory(objectContext.Connection.StoreConnection).CreateConnection();
				DbInterception.Dispatch.Connection.SetConnectionString(connection, new DbConnectionPropertyInterceptionContext<string>().WithValue(connectionString));
				this._connectionCloned = true;
			}
			this._clonedEntityConnection = objectContext.Connection.CreateNew(connection);
			this._objectContext = objectContext.CreateNew(this._clonedEntityConnection);
			this._objectContext.CopyContextOptions(objectContext);
			if (!string.IsNullOrWhiteSpace(objectContext.DefaultContainerName))
			{
				this._objectContext.DefaultContainerName = objectContext.DefaultContainerName;
			}
			if (transferLoadedAssemblies)
			{
				this.TransferLoadedAssemblies(objectContext);
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x001441C5 File Offset: 0x001423C5
		public virtual ObjectContextProxy ObjectContext
		{
			get
			{
				return this._objectContext;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x001441CD File Offset: 0x001423CD
		public virtual DbConnection Connection
		{
			get
			{
				return this._objectContext.Connection.StoreConnection;
			}
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x00144228 File Offset: 0x00142428
		private void TransferLoadedAssemblies(ObjectContextProxy source)
		{
			IEnumerable<GlobalItem> objectItemCollection = source.GetObjectItemCollection();
			IEnumerable<Assembly> enumerable = (from i in objectItemCollection
			where i is EntityType || i is ComplexType
			select source.GetClrType((StructuralType)i).Assembly()).Union(from i in objectItemCollection.OfType<EnumType>()
			select source.GetClrType(i).Assembly()).Distinct<Assembly>();
			foreach (Assembly assembly in enumerable)
			{
				this._objectContext.LoadFromAssembly(assembly);
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x001442EC File Offset: 0x001424EC
		public void Dispose()
		{
			if (this._objectContext != null)
			{
				ObjectContextProxy objectContext = this._objectContext;
				DbConnection connection = this.Connection;
				this._objectContext = null;
				objectContext.Dispose();
				this._clonedEntityConnection.Dispose();
				if (this._connectionCloned)
				{
					DbInterception.Dispatch.Connection.Dispose(connection, new DbInterceptionContext());
				}
			}
		}

		// Token: 0x04001936 RID: 6454
		private ObjectContextProxy _objectContext;

		// Token: 0x04001937 RID: 6455
		private readonly bool _connectionCloned;

		// Token: 0x04001938 RID: 6456
		private readonly EntityConnectionProxy _clonedEntityConnection;
	}
}
