using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006FD RID: 1789
	internal class MigrationAssembly
	{
		// Token: 0x060048A3 RID: 18595 RVA: 0x0015DC49 File Offset: 0x0015BE49
		public static string CreateMigrationId(string migrationName)
		{
			return UtcNowGenerator.UtcNowAsMigrationIdTimestamp() + "_" + migrationName;
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x0015DC5B File Offset: 0x0015BE5B
		public static string CreateBootstrapMigrationId()
		{
			return new string('0', 15) + "_" + Strings.BootstrapMigration;
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0015DC75 File Offset: 0x0015BE75
		protected MigrationAssembly()
		{
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x0015DD24 File Offset: 0x0015BF24
		public MigrationAssembly(Assembly migrationsAssembly, string migrationsNamespace)
		{
			this._migrations = (from t in migrationsAssembly.GetAccessibleTypes()
			where t.IsSubclassOf(typeof(DbMigration)) && typeof(IMigrationMetadata).IsAssignableFrom(t) && t.GetPublicConstructor(new Type[0]) != null && !t.IsAbstract() && !t.IsGenericType() && t.Namespace == migrationsNamespace
			select (IMigrationMetadata)Activator.CreateInstance(t) into mm
			where !string.IsNullOrWhiteSpace(mm.Id) && mm.Id.IsValidMigrationId()
			orderby mm.Id
			select mm).ToList<IMigrationMetadata>();
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x0015DDDB File Offset: 0x0015BFDB
		public virtual IEnumerable<string> MigrationIds
		{
			get
			{
				return (from t in this._migrations
				select t.Id).ToList<string>();
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0015DE17 File Offset: 0x0015C017
		public virtual string UniquifyName(string migrationName)
		{
			return (from m in this._migrations
			select m.GetType().Name).Uniquify(migrationName);
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0015DE64 File Offset: 0x0015C064
		public virtual DbMigration GetMigration(string migrationId)
		{
			DbMigration dbMigration = (DbMigration)this._migrations.SingleOrDefault((IMigrationMetadata m) => m.Id.StartsWith(migrationId, StringComparison.Ordinal));
			if (dbMigration != null)
			{
				dbMigration.Reset();
			}
			return dbMigration;
		}

		// Token: 0x04001AFC RID: 6908
		private readonly IList<IMigrationMetadata> _migrations;
	}
}
