using System;

namespace System.Data.Common
{
	// Token: 0x020002FE RID: 766
	public abstract class DbTransaction : MarshalByRefObject, IDbTransaction, IDisposable
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0012F864 File Offset: 0x0012EC64
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x0012F878 File Offset: 0x0012EC78
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060030BE RID: 12478
		protected abstract DbConnection DbConnection { get; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060030BF RID: 12479
		public abstract IsolationLevel IsolationLevel { get; }

		// Token: 0x060030C0 RID: 12480
		public abstract void Commit();

		// Token: 0x060030C1 RID: 12481 RVA: 0x0012F88C File Offset: 0x0012EC8C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0012F8A0 File Offset: 0x0012ECA0
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060030C3 RID: 12483
		public abstract void Rollback();
	}
}
