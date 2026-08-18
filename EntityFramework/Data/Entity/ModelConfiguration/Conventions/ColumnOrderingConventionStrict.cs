using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000728 RID: 1832
	public class ColumnOrderingConventionStrict : ColumnOrderingConvention
	{
		// Token: 0x06004B5D RID: 19293 RVA: 0x0016195C File Offset: 0x0015FB5C
		protected override void ValidateColumns(EntityType table, string tableName)
		{
			bool flag = (from c in table.Properties
			select c.GetOrder() into o
			where o != null
			group o by o).Any((IGrouping<int?, int?> g) => g.Count<int?>() > 1);
			if (flag)
			{
				throw Error.DuplicateConfiguredColumnOrder(tableName);
			}
		}
	}
}
