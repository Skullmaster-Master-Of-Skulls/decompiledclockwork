using System;
using System.Data.Common;

namespace System.Data.EntityClient
{
	// Token: 0x02000125 RID: 293
	public sealed class EntityTransaction : DbTransaction
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x00041BC2 File Offset: 0x0003FDC2
		internal EntityTransaction(EntityConnection connection, DbTransaction storeTransaction)
		{
			this._connection = connection;
			this._storeTransaction = storeTransaction;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00041BD8 File Offset: 0x0003FDD8
		public new EntityConnection Connection
		{
			get
			{
				if (this._storeTransaction.Connection == null)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00041BD8 File Offset: 0x0003FDD8
		protected override DbConnection DbConnection
		{
			get
			{
				if (this._storeTransaction.Connection == null)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00041BEF File Offset: 0x0003FDEF
		public override IsolationLevel IsolationLevel
		{
			get
			{
				return this._storeTransaction.IsolationLevel;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00041BFC File Offset: 0x0003FDFC
		internal DbTransaction StoreTransaction
		{
			get
			{
				return this._storeTransaction;
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00041C04 File Offset: 0x0003FE04
		public override void Commit()
		{
			try
			{
				this._storeTransaction.Commit();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.Provider("Commit", ex);
				}
				throw;
			}
			this.ClearCurrentTransaction();
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00041C4C File Offset: 0x0003FE4C
		public override void Rollback()
		{
			try
			{
				this._storeTransaction.Rollback();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.Provider("Rollback", ex);
				}
				throw;
			}
			this.ClearCurrentTransaction();
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00041C94 File Offset: 0x0003FE94
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearCurrentTransaction();
				this._storeTransaction.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00041CB1 File Offset: 0x0003FEB1
		private void ClearCurrentTransaction()
		{
			if (this._connection.CurrentTransaction == this)
			{
				this._connection.ClearCurrentTransaction();
			}
		}

		// Token: 0x04000A31 RID: 2609
		private EntityConnection _connection;

		// Token: 0x04000A32 RID: 2610
		private DbTransaction _storeTransaction;
	}
}
