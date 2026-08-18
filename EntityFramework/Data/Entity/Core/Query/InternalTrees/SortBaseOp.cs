using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E0 RID: 1504
	internal abstract class SortBaseOp : RelOp
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x001188EA File Offset: 0x00116AEA
		internal SortBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x001188F3 File Offset: 0x00116AF3
		internal SortBaseOp(OpType opType, List<SortKey> sortKeys) : this(opType)
		{
			this.m_keys = sortKeys;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x00118903 File Offset: 0x00116B03
		internal List<SortKey> Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x04001678 RID: 5752
		private readonly List<SortKey> m_keys;
	}
}
