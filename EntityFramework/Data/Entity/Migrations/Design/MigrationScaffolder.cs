using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020006D4 RID: 1748
	public class MigrationScaffolder
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x0014C477 File Offset: 0x0014A677
		public MigrationScaffolder(DbMigrationsConfiguration migrationsConfiguration)
		{
			Check.NotNull<DbMigrationsConfiguration>(migrationsConfiguration, "migrationsConfiguration");
			this._migrator = new DbMigrator(migrationsConfiguration);
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x0014C497 File Offset: 0x0014A697
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x0014C4B8 File Offset: 0x0014A6B8
		public string Namespace
		{
			get
			{
				if (!this._namespaceSpecified)
				{
					return this._migrator.Configuration.MigrationsNamespace;
				}
				return this._namespace;
			}
			set
			{
				this._namespaceSpecified = (this._migrator.Configuration.MigrationsNamespace != value);
				this._namespace = value;
			}
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0014C4DD File Offset: 0x0014A6DD
		public virtual ScaffoldedMigration Scaffold(string migrationName)
		{
			Check.NotEmpty(migrationName, "migrationName");
			return this._migrator.Scaffold(migrationName, this.Namespace, false);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x0014C4FE File Offset: 0x0014A6FE
		public virtual ScaffoldedMigration Scaffold(string migrationName, bool ignoreChanges)
		{
			Check.NotEmpty(migrationName, "migrationName");
			return this._migrator.Scaffold(migrationName, this.Namespace, ignoreChanges);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x0014C51F File Offset: 0x0014A71F
		public virtual ScaffoldedMigration ScaffoldInitialCreate()
		{
			return this._migrator.ScaffoldInitialCreate(this.Namespace);
		}

		// Token: 0x040019B6 RID: 6582
		private readonly DbMigrator _migrator;

		// Token: 0x040019B7 RID: 6583
		private string _namespace;

		// Token: 0x040019B8 RID: 6584
		private bool _namespaceSpecified;
	}
}
