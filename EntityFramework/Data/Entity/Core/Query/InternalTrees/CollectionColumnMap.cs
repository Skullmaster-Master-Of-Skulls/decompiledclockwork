using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D1 RID: 1489
	internal abstract class CollectionColumnMap : ColumnMap
	{
		// Token: 0x06003B9D RID: 15261 RVA: 0x00118421 File Offset: 0x00116621
		internal CollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys) : base(type, name)
		{
			this.m_element = elementMap;
			this.m_keys = (keys ?? new SimpleColumnMap[0]);
			this.m_foreignKeys = (foreignKeys ?? new SimpleColumnMap[0]);
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x00118456 File Offset: 0x00116656
		internal SimpleColumnMap[] ForeignKeys
		{
			get
			{
				return this.m_foreignKeys;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x0011845E File Offset: 0x0011665E
		internal SimpleColumnMap[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x00118466 File Offset: 0x00116666
		internal ColumnMap Element
		{
			get
			{
				return this.m_element;
			}
		}

		// Token: 0x0400165A RID: 5722
		private readonly ColumnMap m_element;

		// Token: 0x0400165B RID: 5723
		private readonly SimpleColumnMap[] m_foreignKeys;

		// Token: 0x0400165C RID: 5724
		private readonly SimpleColumnMap[] m_keys;
	}
}
