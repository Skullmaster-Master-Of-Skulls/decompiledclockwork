using System;
using System.Collections.Generic;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C2 RID: 194
	internal class CollectionInfo
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003BDA3 File Offset: 0x00039FA3
		internal Var CollectionVar
		{
			get
			{
				return this.m_collectionVar;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0003BDAB File Offset: 0x00039FAB
		internal ColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0003BDB3 File Offset: 0x00039FB3
		internal VarList FlattenedElementVars
		{
			get
			{
				return this.m_flattenedElementVars;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0003BDBB File Offset: 0x00039FBB
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0003BDC3 File Offset: 0x00039FC3
		internal List<SortKey> SortKeys
		{
			get
			{
				return this.m_sortKeys;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0003BDCB File Offset: 0x00039FCB
		internal object DiscriminatorValue
		{
			get
			{
				return this.m_discriminatorValue;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0003BDD3 File Offset: 0x00039FD3
		internal CollectionInfo(Var collectionVar, ColumnMap columnMap, VarList flattenedElementVars, VarVec keys, List<SortKey> sortKeys, object discriminatorValue)
		{
			this.m_collectionVar = collectionVar;
			this.m_columnMap = columnMap;
			this.m_flattenedElementVars = flattenedElementVars;
			this.m_keys = keys;
			this.m_sortKeys = sortKeys;
			this.m_discriminatorValue = discriminatorValue;
		}

		// Token: 0x04000953 RID: 2387
		private Var m_collectionVar;

		// Token: 0x04000954 RID: 2388
		private ColumnMap m_columnMap;

		// Token: 0x04000955 RID: 2389
		private VarList m_flattenedElementVars;

		// Token: 0x04000956 RID: 2390
		private VarVec m_keys;

		// Token: 0x04000957 RID: 2391
		private List<SortKey> m_sortKeys;

		// Token: 0x04000958 RID: 2392
		private object m_discriminatorValue;
	}
}
