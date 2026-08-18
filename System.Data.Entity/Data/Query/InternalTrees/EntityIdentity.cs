using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A7 RID: 167
	internal abstract class EntityIdentity
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x00036316 File Offset: 0x00034516
		internal EntityIdentity(SimpleColumnMap[] keyColumns)
		{
			this.m_keys = keyColumns;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00036325 File Offset: 0x00034525
		internal SimpleColumnMap[] Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x040008C4 RID: 2244
		private readonly SimpleColumnMap[] m_keys;
	}
}
