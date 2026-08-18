using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity
{
	// Token: 0x0200027C RID: 636
	public class DbContextTransaction : IDisposable
	{
		// Token: 0x06001658 RID: 5720 RVA: 0x0006C2BE File Offset: 0x0006A4BE
		internal DbContextTransaction(EntityConnection connection)
		{
			this._connection = connection;
			this.EnsureOpenConnection();
			this._entityTransaction = this._connection.BeginTransaction();
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0006C2E4 File Offset: 0x0006A4E4
		internal DbContextTransaction(EntityConnection connection, IsolationLevel isolationLevel)
		{
			this._connection = connection;
			this.EnsureOpenConnection();
			this._entityTransaction = this._connection.BeginTransaction(isolationLevel);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0006C30B File Offset: 0x0006A50B
		internal DbContextTransaction(EntityTransaction transaction)
		{
			this._connection = transaction.Connection;
			this.EnsureOpenConnection();
			this._entityTransaction = transaction;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0006C32C File Offset: 0x0006A52C
		private void EnsureOpenConnection()
		{
			if (ConnectionState.Open != this._connection.State)
			{
				this._connection.Open();
				this._shouldCloseConnection = true;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0006C34E File Offset: 0x0006A54E
		public DbTransaction UnderlyingTransaction
		{
			get
			{
				return this._entityTransaction.StoreTransaction;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0006C35B File Offset: 0x0006A55B
		public void Commit()
		{
			this._entityTransaction.Commit();
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0006C368 File Offset: 0x0006A568
		public void Rollback()
		{
			this._entityTransaction.Rollback();
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0006C375 File Offset: 0x0006A575
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0006C384 File Offset: 0x0006A584
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				this._connection.ClearCurrentTransaction();
				this._entityTransaction.Dispose();
				if (this._shouldCloseConnection && this._connection.State != ConnectionState.Closed)
				{
					this._connection.Close();
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006C3D9 File Offset: 0x0006A5D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0006C3E1 File Offset: 0x0006A5E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0006C3EA File Offset: 0x0006A5EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006C3F2 File Offset: 0x0006A5F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040007EA RID: 2026
		private readonly EntityConnection _connection;

		// Token: 0x040007EB RID: 2027
		private readonly EntityTransaction _entityTransaction;

		// Token: 0x040007EC RID: 2028
		private bool _shouldCloseConnection;

		// Token: 0x040007ED RID: 2029
		private bool _isDisposed;
	}
}
