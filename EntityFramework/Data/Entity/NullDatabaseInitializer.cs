using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x020002CD RID: 717
	public class NullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x06001952 RID: 6482 RVA: 0x0007E5B3 File Offset: 0x0007C7B3
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
		}
	}
}
