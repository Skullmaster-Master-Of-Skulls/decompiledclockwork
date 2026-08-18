using System;
using System.Data.Entity.Migrations.Model;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x02000282 RID: 642
	public interface IDbMigration
	{
		// Token: 0x060016A6 RID: 5798
		void AddOperation(MigrationOperation migrationOperation);
	}
}
