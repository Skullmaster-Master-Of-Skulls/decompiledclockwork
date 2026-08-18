using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006FF RID: 1791
	public class MigratorScriptingDecorator : MigratorBase
	{
		// Token: 0x060048B9 RID: 18617 RVA: 0x0015E09C File Offset: 0x0015C29C
		public MigratorScriptingDecorator(MigratorBase innerMigrator) : base(innerMigrator)
		{
			Check.NotNull<MigratorBase>(innerMigrator, "innerMigrator");
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x0015E0EC File Offset: 0x0015C2EC
		public string ScriptUpdate(string sourceMigration, string targetMigration)
		{
			this._sqlBuilder.Clear();
			if (string.IsNullOrWhiteSpace(sourceMigration))
			{
				this.Update(targetMigration);
			}
			else
			{
				if (sourceMigration.IsAutomaticMigration())
				{
					throw Error.AutoNotValidForScriptWindows(sourceMigration);
				}
				string sourceMigrationId = this.GetMigrationId(sourceMigration);
				IEnumerable<string> enumerable = from m in this.GetLocalMigrations()
				where string.CompareOrdinal(m, sourceMigrationId) > 0
				select m;
				string targetMigrationId = null;
				if (!string.IsNullOrWhiteSpace(targetMigration))
				{
					if (targetMigration.IsAutomaticMigration())
					{
						throw Error.AutoNotValidForScriptWindows(targetMigration);
					}
					targetMigrationId = this.GetMigrationId(targetMigration);
					if (string.CompareOrdinal(sourceMigrationId, targetMigrationId) > 0)
					{
						throw Error.DownScriptWindowsNotSupported();
					}
					enumerable = from m in enumerable
					where string.CompareOrdinal(m, targetMigrationId) <= 0
					select m;
				}
				this._updateDatabaseOperation = ((sourceMigration == "0") ? new UpdateDatabaseOperation(base.CreateDiscoveryQueryTrees().ToList<DbQueryCommandTree>()) : null);
				this.Upgrade(enumerable, targetMigrationId, sourceMigrationId);
				if (this._updateDatabaseOperation != null)
				{
					this.ExecuteStatements(base.GenerateStatements(new UpdateDatabaseOperation[]
					{
						this._updateDatabaseOperation
					}, null));
				}
			}
			return this._sqlBuilder.ToString();
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0015E21C File Offset: 0x0015C41C
		internal override IEnumerable<MigrationStatement> GenerateStatements(IList<MigrationOperation> operations, string migrationId)
		{
			if (this._updateDatabaseOperation == null)
			{
				return base.GenerateStatements(operations, migrationId);
			}
			this._updateDatabaseOperation.AddMigration(migrationId, operations);
			return Enumerable.Empty<MigrationStatement>();
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0015E241 File Offset: 0x0015C441
		internal override void EnsureDatabaseExists(Action mustSucceedToKeepDatabase)
		{
			mustSucceedToKeepDatabase();
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0015E249 File Offset: 0x0015C449
		internal override void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements)
		{
			MigratorScriptingDecorator.BuildSqlScript(migrationStatements, this._sqlBuilder);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0015E258 File Offset: 0x0015C458
		internal static void BuildSqlScript(IEnumerable<MigrationStatement> migrationStatements, StringBuilder sqlBuilder)
		{
			foreach (MigrationStatement migrationStatement in migrationStatements)
			{
				if (!string.IsNullOrWhiteSpace(migrationStatement.Sql))
				{
					if (!string.IsNullOrWhiteSpace(migrationStatement.BatchTerminator) && sqlBuilder.Length > 0)
					{
						sqlBuilder.AppendLine(migrationStatement.BatchTerminator);
						sqlBuilder.AppendLine();
					}
					sqlBuilder.AppendLine(migrationStatement.Sql);
				}
			}
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0015E2E0 File Offset: 0x0015C4E0
		internal override void SeedDatabase()
		{
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0015E2E2 File Offset: 0x0015C4E2
		internal override bool HistoryExists()
		{
			return false;
		}

		// Token: 0x04001B04 RID: 6916
		private readonly StringBuilder _sqlBuilder = new StringBuilder();

		// Token: 0x04001B05 RID: 6917
		private UpdateDatabaseOperation _updateDatabaseOperation;
	}
}
