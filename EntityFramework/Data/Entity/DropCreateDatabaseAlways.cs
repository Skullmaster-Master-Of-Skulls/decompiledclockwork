using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x0200073E RID: 1854
	public class DropCreateDatabaseAlways<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x060053F4 RID: 21492 RVA: 0x0017072F File Offset: 0x0016E92F
		static DropCreateDatabaseAlways()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00170748 File Offset: 0x0016E948
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			context.Database.Delete();
			context.Database.Create(DatabaseExistenceState.DoesNotExist);
			this.Seed(context);
			context.SaveChanges();
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0017079C File Offset: 0x0016E99C
		protected virtual void Seed(TContext context)
		{
		}
	}
}
