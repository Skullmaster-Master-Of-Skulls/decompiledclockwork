using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006FE RID: 1790
	public class MigratorLoggingDecorator : MigratorBase
	{
		// Token: 0x060048AF RID: 18607 RVA: 0x0015DEA5 File Offset: 0x0015C0A5
		public MigratorLoggingDecorator(MigratorBase innerMigrator, MigrationsLogger logger) : base(innerMigrator)
		{
			Check.NotNull<MigratorBase>(innerMigrator, "innerMigrator");
			Check.NotNull<MigrationsLogger>(logger, "logger");
			this._logger = logger;
			this._logger.Verbose(Strings.LoggingTargetDatabase(base.TargetDatabase));
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0015DEE3 File Offset: 0x0015C0E3
		internal override void AutoMigrate(string migrationId, VersionedModel sourceModel, VersionedModel targetModel, bool downgrading)
		{
			this._logger.Info(downgrading ? Strings.LoggingRevertAutoMigrate(migrationId) : Strings.LoggingAutoMigrate(migrationId));
			base.AutoMigrate(migrationId, sourceModel, targetModel, downgrading);
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0015DF34 File Offset: 0x0015C134
		internal override void ExecuteSql(MigrationStatement migrationStatement, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			this._logger.Verbose(migrationStatement.Sql);
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(connection);
			if (providerServices != null)
			{
				providerServices.RegisterInfoMessageHandler(connection, delegate(string message)
				{
					if (!string.Equals(message, this._lastInfoMessage, StringComparison.OrdinalIgnoreCase))
					{
						this._logger.Warning(message);
						this._lastInfoMessage = message;
					}
				});
			}
			base.ExecuteSql(migrationStatement, connection, transaction, interceptionContext);
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0015DF84 File Offset: 0x0015C184
		internal override void Upgrade(IEnumerable<string> pendingMigrations, string targetMigrationId, string lastMigrationId)
		{
			int num = pendingMigrations.Count<string>();
			this._logger.Info((num > 0) ? Strings.LoggingPendingMigrations(num, pendingMigrations.Join(null, ", ")) : (string.IsNullOrWhiteSpace(targetMigrationId) ? Strings.LoggingNoExplicitMigrations : Strings.LoggingAlreadyAtTarget(targetMigrationId)));
			base.Upgrade(pendingMigrations, targetMigrationId, lastMigrationId);
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0015DFE0 File Offset: 0x0015C1E0
		internal override void Downgrade(IEnumerable<string> pendingMigrations)
		{
			IEnumerable<string> enumerable = pendingMigrations.Take(pendingMigrations.Count<string>() - 1);
			this._logger.Info(Strings.LoggingPendingMigrationsDown(enumerable.Count<string>(), enumerable.Join(null, ", ")));
			base.Downgrade(pendingMigrations);
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0015E02A File Offset: 0x0015C22A
		internal override void ApplyMigration(DbMigration migration, DbMigration lastMigration)
		{
			this._logger.Info(Strings.LoggingApplyMigration(((IMigrationMetadata)migration).Id));
			base.ApplyMigration(migration, lastMigration);
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0015E04F File Offset: 0x0015C24F
		internal override void RevertMigration(string migrationId, DbMigration migration, XDocument targetModel)
		{
			this._logger.Info(Strings.LoggingRevertMigration(migrationId));
			base.RevertMigration(migrationId, migration, targetModel);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0015E06B File Offset: 0x0015C26B
		internal override void SeedDatabase()
		{
			this._logger.Info(Strings.LoggingSeedingDatabase);
			base.SeedDatabase();
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0015E083 File Offset: 0x0015C283
		internal override void UpgradeHistory(IEnumerable<MigrationOperation> upgradeOperations)
		{
			this._logger.Info(Strings.UpgradingHistoryTable);
			base.UpgradeHistory(upgradeOperations);
		}

		// Token: 0x04001B02 RID: 6914
		private readonly MigrationsLogger _logger;

		// Token: 0x04001B03 RID: 6915
		private string _lastInfoMessage;
	}
}
