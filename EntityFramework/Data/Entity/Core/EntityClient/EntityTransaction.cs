using System;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x0200033F RID: 831
	public class EntityTransaction : DbTransaction
	{
		// Token: 0x06001DA7 RID: 7591 RVA: 0x0008EFC1 File Offset: 0x0008D1C1
		internal EntityTransaction()
		{
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0008EFC9 File Offset: 0x0008D1C9
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Object is in fact passed to property of the class and gets Disposed properly in the Dispose() method.")]
		internal EntityTransaction(EntityConnection connection, DbTransaction storeTransaction)
		{
			this._connection = connection;
			this._storeTransaction = storeTransaction;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0008EFDF File Offset: 0x0008D1DF
		public new virtual EntityConnection Connection
		{
			get
			{
				return (EntityConnection)this.DbConnection;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0008EFEC File Offset: 0x0008D1EC
		protected override DbConnection DbConnection
		{
			get
			{
				if (((this._storeTransaction != null) ? DbInterception.Dispatch.Transaction.GetConnection(this._storeTransaction, this.InterceptionContext) : null) == null)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0008F01E File Offset: 0x0008D21E
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (this._storeTransaction == null)
				{
					return (IsolationLevel)0;
				}
				return DbInterception.Dispatch.Transaction.GetIsolationLevel(this._storeTransaction, this.InterceptionContext);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0008F045 File Offset: 0x0008D245
		public virtual DbTransaction StoreTransaction
		{
			get
			{
				return this._storeTransaction;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0008F055 File Offset: 0x0008D255
		private DbInterceptionContext InterceptionContext
		{
			get
			{
				return DbInterceptionContext.Combine(from c in this._connection.AssociatedContexts
				select c.InterceptionContext);
			}
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0008F08C File Offset: 0x0008D28C
		public override void Commit()
		{
			try
			{
				if (this._storeTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Commit(this._storeTransaction, this.InterceptionContext);
				}
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType() && !(ex is CommitFailedException))
				{
					throw new EntityException(Strings.EntityClient_ProviderSpecificError("Commit"), ex);
				}
				throw;
			}
			this.ClearCurrentTransaction();
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0008F0F8 File Offset: 0x0008D2F8
		public override void Rollback()
		{
			try
			{
				if (this._storeTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Rollback(this._storeTransaction, this.InterceptionContext);
				}
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ProviderSpecificError("Rollback"), ex);
				}
				throw;
			}
			this.ClearCurrentTransaction();
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0008F15C File Offset: 0x0008D35C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearCurrentTransaction();
				if (this._storeTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Dispose(this._storeTransaction, this.InterceptionContext);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0008F191 File Offset: 0x0008D391
		private void ClearCurrentTransaction()
		{
			if (this._connection != null && this._connection.CurrentTransaction == this)
			{
				this._connection.ClearCurrentTransaction();
			}
		}

		// Token: 0x04000A1A RID: 2586
		private readonly EntityConnection _connection;

		// Token: 0x04000A1B RID: 2587
		private readonly DbTransaction _storeTransaction;
	}
}
