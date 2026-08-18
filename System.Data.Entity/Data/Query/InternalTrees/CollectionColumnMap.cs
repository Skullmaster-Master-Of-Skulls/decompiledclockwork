using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A4 RID: 164
	internal abstract class CollectionColumnMap : ColumnMap
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x0003622E File Offset: 0x0003442E
		internal CollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys) : base(type, name)
		{
			this.m_element = elementMap;
			this.m_keys = (keys ?? new SimpleColumnMap[0]);
			this.m_foreignKeys = (foreignKeys ?? new SimpleColumnMap[0]);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00036263 File Offset: 0x00034463
		internal SimpleColumnMap[] ForeignKeys
		{
			get
			{
				return this.m_foreignKeys;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0003626B File Offset: 0x0003446B
		internal SimpleColumnMap[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00036273 File Offset: 0x00034473
		internal ColumnMap Element
		{
			get
			{
				return this.m_element;
			}
		}

		// Token: 0x040008BF RID: 2239
		private readonly ColumnMap m_element;

		// Token: 0x040008C0 RID: 2240
		private readonly SimpleColumnMap[] m_foreignKeys;

		// Token: 0x040008C1 RID: 2241
		private readonly SimpleColumnMap[] m_keys;
	}
}
