using System;
using System.Collections.Generic;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000765 RID: 1893
	internal class DbContextTypesInitializersPair : Tuple<Dictionary<Type, List<string>>, Action<DbContext>>
	{
		// Token: 0x06005554 RID: 21844 RVA: 0x00172F44 File Offset: 0x00171144
		public DbContextTypesInitializersPair(Dictionary<Type, List<string>> entityTypeToPropertyNameMap, Action<DbContext> setsInitializer) : base(entityTypeToPropertyNameMap, setsInitializer)
		{
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06005555 RID: 21845 RVA: 0x00172F4E File Offset: 0x0017114E
		public Dictionary<Type, List<string>> EntityTypeToPropertyNameMap
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06005556 RID: 21846 RVA: 0x00172F56 File Offset: 0x00171156
		public Action<DbContext> SetsInitializer
		{
			get
			{
				return base.Item2;
			}
		}
	}
}
