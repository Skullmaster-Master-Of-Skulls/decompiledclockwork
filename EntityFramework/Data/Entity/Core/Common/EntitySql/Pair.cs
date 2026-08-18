using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000263 RID: 611
	internal sealed class Pair<L, R>
	{
		// Token: 0x060014F9 RID: 5369 RVA: 0x000630A4 File Offset: 0x000612A4
		internal Pair(L left, R right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000630BA File Offset: 0x000612BA
		internal KeyValuePair<L, R> GetKVP()
		{
			return new KeyValuePair<L, R>(this.Left, this.Right);
		}

		// Token: 0x04000744 RID: 1860
		internal L Left;

		// Token: 0x04000745 RID: 1861
		internal R Right;
	}
}
