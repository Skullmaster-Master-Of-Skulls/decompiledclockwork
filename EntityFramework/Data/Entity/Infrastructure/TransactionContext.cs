using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000199 RID: 409
	public class TransactionContext : DbContext
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003D8EF File Offset: 0x0003BAEF
		public TransactionContext(DbConnection existingConnection) : base(existingConnection, false)
		{
			base.Configuration.ValidateOnSaveEnabled = false;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0003D905 File Offset: 0x0003BB05
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0003D90D File Offset: 0x0003BB0D
		public virtual IDbSet<TransactionRow> Transactions { get; set; }

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003D916 File Offset: 0x0003BB16
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TransactionRow>().ToTable("__TransactionHistory");
		}

		// Token: 0x040003B8 RID: 952
		private const string _defaultTableName = "__TransactionHistory";
	}
}
