using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x0200073F RID: 1855
	public class DropCreateDatabaseIfModelChanges<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x060053F8 RID: 21496 RVA: 0x001707A6 File Offset: 0x0016E9A6
		static DropCreateDatabaseIfModelChanges()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x001707BC File Offset: 0x0016E9BC
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			DatabaseExistenceState databaseExistenceState = new DatabaseTableChecker().AnyModelTableExists(context.InternalContext);
			if (databaseExistenceState == DatabaseExistenceState.Exists)
			{
				if (context.Database.CompatibleWithModel(true))
				{
					return;
				}
				context.Database.Delete();
				databaseExistenceState = DatabaseExistenceState.DoesNotExist;
			}
			context.Database.Create(databaseExistenceState);
			this.Seed(context);
			context.SaveChanges();
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x00170844 File Offset: 0x0016EA44
		protected virtual void Seed(TContext context)
		{
		}
	}
}
