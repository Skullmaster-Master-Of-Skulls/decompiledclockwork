using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000759 RID: 1881
	public interface IDbContextFactory<out TContext> where TContext : DbContext
	{
		// Token: 0x0600552B RID: 21803
		TContext Create();
	}
}
