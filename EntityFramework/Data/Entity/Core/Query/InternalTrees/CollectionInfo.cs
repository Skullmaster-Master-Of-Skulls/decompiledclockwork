using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D2 RID: 1490
	internal class CollectionInfo
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x0011846E File Offset: 0x0011666E
		internal Var CollectionVar
		{
			get
			{
				return this.m_collectionVar;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x00118476 File Offset: 0x00116676
		internal ColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x0011847E File Offset: 0x0011667E
		internal VarList FlattenedElementVars
		{
			get
			{
				return this.m_flattenedElementVars;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x00118486 File Offset: 0x00116686
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x0011848E File Offset: 0x0011668E
		internal List<SortKey> SortKeys
		{
			get
			{
				return this.m_sortKeys;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x00118496 File Offset: 0x00116696
		internal object DiscriminatorValue
		{
			get
			{
				return this.m_discriminatorValue;
			}
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x0011849E File Offset: 0x0011669E
		internal CollectionInfo(Var collectionVar, ColumnMap columnMap, VarList flattenedElementVars, VarVec keys, List<SortKey> sortKeys, object discriminatorValue)
		{
			this.m_collectionVar = collectionVar;
			this.m_columnMap = columnMap;
			this.m_flattenedElementVars = flattenedElementVars;
			this.m_keys = keys;
			this.m_sortKeys = sortKeys;
			this.m_discriminatorValue = discriminatorValue;
		}

		// Token: 0x0400165D RID: 5725
		private readonly Var m_collectionVar;

		// Token: 0x0400165E RID: 5726
		private readonly ColumnMap m_columnMap;

		// Token: 0x0400165F RID: 5727
		private readonly VarList m_flattenedElementVars;

		// Token: 0x04001660 RID: 5728
		private readonly VarVec m_keys;

		// Token: 0x04001661 RID: 5729
		private readonly List<SortKey> m_sortKeys;

		// Token: 0x04001662 RID: 5730
		private readonly object m_discriminatorValue;
	}
}
