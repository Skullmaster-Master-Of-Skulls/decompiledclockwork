using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E7 RID: 1511
	internal abstract class EntityIdentity
	{
		// Token: 0x06003C0A RID: 15370 RVA: 0x00118A7F File Offset: 0x00116C7F
		internal EntityIdentity(SimpleColumnMap[] keyColumns)
		{
			this.m_keys = keyColumns;
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x00118A8E File Offset: 0x00116C8E
		internal SimpleColumnMap[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x04001682 RID: 5762
		private readonly SimpleColumnMap[] m_keys;
	}
}
