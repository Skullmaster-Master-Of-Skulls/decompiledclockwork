using System;

namespace System.Data.Common
{
	// Token: 0x02000147 RID: 327
	public abstract class DbTransaction : MarshalByRefObject, IDbTransaction, IDisposable
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00242738 File Offset: 0x00241B38
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00242758 File Offset: 0x00241B58
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600151C RID: 5404
		protected abstract DbConnection DbConnection { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600151D RID: 5405
		public abstract IsolationLevel IsolationLevel { get; }

		// Token: 0x0600151E RID: 5406
		public abstract void Commit();

		// Token: 0x0600151F RID: 5407 RVA: 0x00242778 File Offset: 0x00241B78
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00242798 File Offset: 0x00241B98
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001521 RID: 5409
		public abstract void Rollback();
	}
}
