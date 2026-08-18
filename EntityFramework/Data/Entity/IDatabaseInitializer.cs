using System;

namespace System.Data.Entity
{
	// Token: 0x0200019B RID: 411
	public interface IDatabaseInitializer<in TContext> where TContext : DbContext
	{
		// Token: 0x06000E0B RID: 3595
		void InitializeDatabase(TContext context);
	}
}
