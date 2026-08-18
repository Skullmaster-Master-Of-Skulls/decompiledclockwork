using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001AB RID: 427
	public class UpdateDatabaseOperation : MigrationOperation
	{
		// Token: 0x06000E64 RID: 3684 RVA: 0x0003F10C File Offset: 0x0003D30C
		public UpdateDatabaseOperation(IList<DbQueryCommandTree> historyQueryTrees) : base(null)
		{
			Check.NotNull<IList<DbQueryCommandTree>>(historyQueryTrees, "historyQueryTrees");
			this._historyQueryTrees = historyQueryTrees;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003F133 File Offset: 0x0003D333
		public IList<DbQueryCommandTree> HistoryQueryTrees
		{
			get
			{
				return this._historyQueryTrees;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003F13B File Offset: 0x0003D33B
		public IList<UpdateDatabaseOperation.Migration> Migrations
		{
			get
			{
				return this._migrations;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003F143 File Offset: 0x0003D343
		public void AddMigration(string migrationId, IList<MigrationOperation> operations)
		{
			Check.NotEmpty(migrationId, "migrationId");
			Check.NotNull<IList<MigrationOperation>>(operations, "operations");
			this._migrations.Add(new UpdateDatabaseOperation.Migration(migrationId, operations));
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0003F16F File Offset: 0x0003D36F
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003E0 RID: 992
		private readonly IList<DbQueryCommandTree> _historyQueryTrees;

		// Token: 0x040003E1 RID: 993
		private readonly IList<UpdateDatabaseOperation.Migration> _migrations = new List<UpdateDatabaseOperation.Migration>();

		// Token: 0x020001AC RID: 428
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public class Migration
		{
			// Token: 0x06000E69 RID: 3689 RVA: 0x0003F172 File Offset: 0x0003D372
			internal Migration(string migrationId, IList<MigrationOperation> operations)
			{
				this._migrationId = migrationId;
				this._operations = operations;
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003F188 File Offset: 0x0003D388
			public string MigrationId
			{
				get
				{
					return this._migrationId;
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003F190 File Offset: 0x0003D390
			public IList<MigrationOperation> Operations
			{
				get
				{
					return this._operations;
				}
			}

			// Token: 0x040003E2 RID: 994
			private readonly string _migrationId;

			// Token: 0x040003E3 RID: 995
			private readonly IList<MigrationOperation> _operations;
		}
	}
}
