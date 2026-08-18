using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x02000732 RID: 1842
	public class CreateDatabaseIfNotExists<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x06005341 RID: 21313 RVA: 0x0016EF2C File Offset: 0x0016D12C
		static CreateDatabaseIfNotExists()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0016EF44 File Offset: 0x0016D144
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			DatabaseExistenceState databaseExistenceState = new DatabaseTableChecker().AnyModelTableExists(context.InternalContext);
			if (databaseExistenceState == DatabaseExistenceState.Exists)
			{
				if (!context.Database.CompatibleWithModel(false, databaseExistenceState))
				{
					throw Error.DatabaseInitializationStrategy_ModelMismatch(context.GetType().Name);
				}
			}
			else
			{
				context.Database.Create(databaseExistenceState);
				this.Seed(context);
				context.SaveChanges();
			}
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0016EFCF File Offset: 0x0016D1CF
		protected virtual void Seed(TContext context)
		{
		}
	}
}
