using System;
using System.Collections.Generic;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D5 RID: 213
	internal abstract class SortBaseOp : RelOp
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		internal SortBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003C17A File Offset: 0x0003A37A
		internal SortBaseOp(OpType opType, List<SortKey> sortKeys) : this(opType)
		{
			this.m_keys = sortKeys;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0003C18A File Offset: 0x0003A38A
		internal List<SortKey> Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x04000978 RID: 2424
		private List<SortKey> m_keys;
	}
}
