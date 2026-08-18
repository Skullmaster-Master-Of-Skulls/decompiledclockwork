using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200014F RID: 335
	public abstract class TableExistenceChecker
	{
		// Token: 0x06000AFE RID: 2814
		public abstract bool AnyModelTableExistsInDatabase(ObjectContext context, DbConnection connection, IEnumerable<EntitySet> modelTables, string edmMetadataContextTableName);

		// Token: 0x06000AFF RID: 2815 RVA: 0x000377D0 File Offset: 0x000359D0
		protected virtual string GetTableName(EntitySet modelTable)
		{
			if (!modelTable.MetadataProperties.Contains("Table") || modelTable.MetadataProperties["Table"].Value == null)
			{
				return modelTable.Name;
			}
			return (string)modelTable.MetadataProperties["Table"].Value;
		}
	}
}
