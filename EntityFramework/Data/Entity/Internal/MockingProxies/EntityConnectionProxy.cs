using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.MockingProxies
{
	// Token: 0x020006C5 RID: 1733
	internal class EntityConnectionProxy
	{
		// Token: 0x060044D5 RID: 17621 RVA: 0x00144E03 File Offset: 0x00143003
		protected EntityConnectionProxy()
		{
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00144E0B File Offset: 0x0014300B
		public EntityConnectionProxy(EntityConnection entityConnection)
		{
			this._entityConnection = entityConnection;
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x00144E1A File Offset: 0x0014301A
		public static implicit operator EntityConnection(EntityConnectionProxy proxy)
		{
			return proxy._entityConnection;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x00144E22 File Offset: 0x00143022
		public virtual DbConnection StoreConnection
		{
			get
			{
				return this._entityConnection.StoreConnection;
			}
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x00144E2F File Offset: 0x0014302F
		public virtual void Dispose()
		{
			this._entityConnection.Dispose();
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x00144E3C File Offset: 0x0014303C
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual EntityConnectionProxy CreateNew(DbConnection storeConnection)
		{
			EntityConnection entityConnection = new EntityConnection(this._entityConnection.GetMetadataWorkspace(), storeConnection);
			EntityTransaction currentTransaction = this._entityConnection.CurrentTransaction;
			if (currentTransaction != null && DbInterception.Dispatch.Transaction.GetConnection(currentTransaction.StoreTransaction, this._entityConnection.InterceptionContext) == storeConnection)
			{
				entityConnection.UseStoreTransaction(currentTransaction.StoreTransaction);
			}
			return new EntityConnectionProxy(entityConnection);
		}

		// Token: 0x04001960 RID: 6496
		private readonly EntityConnection _entityConnection;
	}
}
