using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006CF RID: 1743
	[DebuggerStepThrough]
	public abstract class MigratorBase
	{
		// Token: 0x06004579 RID: 17785 RVA: 0x00146E44 File Offset: 0x00145044
		protected MigratorBase(MigratorBase innerMigrator)
		{
			if (innerMigrator == null)
			{
				this._this = this;
				return;
			}
			this._this = innerMigrator;
			MigratorBase migratorBase = innerMigrator;
			while (migratorBase._this != innerMigrator)
			{
				migratorBase = migratorBase._this;
			}
			migratorBase._this = this;
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x00146E84 File Offset: 0x00145084
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual IEnumerable<string> GetPendingMigrations()
		{
			return this._this.GetPendingMigrations();
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x00146E91 File Offset: 0x00145091
		public virtual DbMigrationsConfiguration Configuration
		{
			get
			{
				return this._this.Configuration;
			}
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x00146E9E File Offset: 0x0014509E
		public void Update()
		{
			this.Update(null);
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x00146EA7 File Offset: 0x001450A7
		public virtual void Update(string targetMigration)
		{
			this._this.Update(targetMigration);
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x00146EB5 File Offset: 0x001450B5
		internal virtual string GetMigrationId(string migration)
		{
			return this._this.GetMigrationId(migration);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x00146EC3 File Offset: 0x001450C3
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual IEnumerable<string> GetLocalMigrations()
		{
			return this._this.GetLocalMigrations();
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x00146ED0 File Offset: 0x001450D0
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual IEnumerable<string> GetDatabaseMigrations()
		{
			return this._this.GetDatabaseMigrations();
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x00146EDD File Offset: 0x001450DD
		internal virtual void AutoMigrate(string migrationId, VersionedModel sourceModel, VersionedModel targetModel, bool downgrading)
		{
			this._this.AutoMigrate(migrationId, sourceModel, targetModel, downgrading);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x00146EEF File Offset: 0x001450EF
		internal virtual void ApplyMigration(DbMigration migration, DbMigration lastMigration)
		{
			this._this.ApplyMigration(migration, lastMigration);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x00146EFE File Offset: 0x001450FE
		internal virtual void EnsureDatabaseExists(Action mustSucceedToKeepDatabase)
		{
			this._this.EnsureDatabaseExists(mustSucceedToKeepDatabase);
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00146F0C File Offset: 0x0014510C
		internal virtual void RevertMigration(string migrationId, DbMigration migration, XDocument targetModel)
		{
			this._this.RevertMigration(migrationId, migration, targetModel);
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x00146F1C File Offset: 0x0014511C
		internal virtual void SeedDatabase()
		{
			this._this.SeedDatabase();
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x00146F29 File Offset: 0x00145129
		internal virtual void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements)
		{
			this._this.ExecuteStatements(migrationStatements);
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x00146F37 File Offset: 0x00145137
		internal virtual IEnumerable<MigrationStatement> GenerateStatements(IList<MigrationOperation> operations, string migrationId)
		{
			return this._this.GenerateStatements(operations, migrationId);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x00146F46 File Offset: 0x00145146
		internal virtual IEnumerable<DbQueryCommandTree> CreateDiscoveryQueryTrees()
		{
			return this._this.CreateDiscoveryQueryTrees();
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x00146F53 File Offset: 0x00145153
		internal virtual void ExecuteSql(MigrationStatement migrationStatement, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			this._this.ExecuteSql(migrationStatement, connection, transaction, interceptionContext);
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x00146F65 File Offset: 0x00145165
		internal virtual void Upgrade(IEnumerable<string> pendingMigrations, string targetMigrationId, string lastMigrationId)
		{
			this._this.Upgrade(pendingMigrations, targetMigrationId, lastMigrationId);
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x00146F75 File Offset: 0x00145175
		internal virtual void Downgrade(IEnumerable<string> pendingMigrations)
		{
			this._this.Downgrade(pendingMigrations);
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00146F83 File Offset: 0x00145183
		internal virtual void UpgradeHistory(IEnumerable<MigrationOperation> upgradeOperations)
		{
			this._this.UpgradeHistory(upgradeOperations);
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x00146F91 File Offset: 0x00145191
		internal virtual string TargetDatabase
		{
			get
			{
				return this._this.TargetDatabase;
			}
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x00146F9E File Offset: 0x0014519E
		internal virtual bool HistoryExists()
		{
			return this._this.HistoryExists();
		}

		// Token: 0x0400197C RID: 6524
		private MigratorBase _this;
	}
}
