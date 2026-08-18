using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x020006C9 RID: 1737
	public class MigrateDatabaseToLatestVersion<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext> where TContext : DbContext where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
	{
		// Token: 0x060044EE RID: 17646 RVA: 0x0014520C File Offset: 0x0014340C
		static MigrateDatabaseToLatestVersion()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x00145222 File Offset: 0x00143422
		public MigrateDatabaseToLatestVersion() : this(false)
		{
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x0014522B File Offset: 0x0014342B
		public MigrateDatabaseToLatestVersion(bool useSuppliedContext) : this(useSuppliedContext, Activator.CreateInstance<TMigrationsConfiguration>())
		{
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x00145239 File Offset: 0x00143439
		public MigrateDatabaseToLatestVersion(bool useSuppliedContext, TMigrationsConfiguration configuration)
		{
			Check.NotNull<TMigrationsConfiguration>(configuration, "configuration");
			this._config = configuration;
			this._useSuppliedContext = useSuppliedContext;
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x00145260 File Offset: 0x00143460
		public MigrateDatabaseToLatestVersion(string connectionStringName)
		{
			Check.NotEmpty(connectionStringName, "connectionStringName");
			TMigrationsConfiguration tmigrationsConfiguration = Activator.CreateInstance<TMigrationsConfiguration>();
			tmigrationsConfiguration.TargetDatabase = new DbConnectionInfo(connectionStringName);
			this._config = tmigrationsConfiguration;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x001452A4 File Offset: 0x001434A4
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			DbMigrator dbMigrator = new DbMigrator(this._config, this._useSuppliedContext ? context : default(TContext));
			dbMigrator.Update();
		}

		// Token: 0x04001963 RID: 6499
		private readonly DbMigrationsConfiguration _config;

		// Token: 0x04001964 RID: 6500
		private readonly bool _useSuppliedContext;
	}
}
