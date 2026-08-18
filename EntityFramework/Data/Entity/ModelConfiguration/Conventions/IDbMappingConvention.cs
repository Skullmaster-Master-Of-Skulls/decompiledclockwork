using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F2 RID: 2034
	internal interface IDbMappingConvention : IConvention
	{
		// Token: 0x06005C3B RID: 23611
		void Apply(DbDatabaseMapping databaseMapping);
	}
}
