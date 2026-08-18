using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000190 RID: 400
	public class DbMappingView
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x0003D078 File Offset: 0x0003B278
		public DbMappingView(string entitySql)
		{
			Check.NotEmpty(entitySql, "entitySql");
			this._entitySql = entitySql;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003D093 File Offset: 0x0003B293
		public string EntitySql
		{
			get
			{
				return this._entitySql;
			}
		}

		// Token: 0x040003AE RID: 942
		private readonly string _entitySql;
	}
}
