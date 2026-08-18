using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Sql;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006BF RID: 1727
	internal class DatabaseCreator
	{
		// Token: 0x060044AF RID: 17583 RVA: 0x00144684 File Offset: 0x00142884
		public DatabaseCreator() : this(DbConfiguration.DependencyResolver)
		{
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x00144691 File Offset: 0x00142891
		public DatabaseCreator(IDbDependencyResolver resolver)
		{
			this._resolver = resolver;
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x001446A0 File Offset: 0x001428A0
		public virtual void CreateDatabase(InternalContext internalContext, Func<DbMigrationsConfiguration, DbContext, MigratorBase> createMigrator, ObjectContext objectContext)
		{
			if (internalContext.CodeFirstModel != null && this._resolver.GetService(internalContext.ProviderName) != null)
			{
				createMigrator(internalContext.MigrationsConfiguration, internalContext.Owner).Update();
			}
			else
			{
				internalContext.DatabaseOperations.Create(objectContext);
				internalContext.SaveMetadataToDatabase();
			}
			internalContext.MarkDatabaseInitialized();
		}

		// Token: 0x0400194E RID: 6478
		private readonly IDbDependencyResolver _resolver;
	}
}
