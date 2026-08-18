using System;
using System.Collections.Generic;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000342 RID: 834
	internal sealed class Pair<L, R>
	{
		// Token: 0x0600315B RID: 12635 RVA: 0x000C290D File Offset: 0x000C0B0D
		internal Pair(L left, R right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000C2923 File Offset: 0x000C0B23
		internal KeyValuePair<L, R> GetKVP()
		{
			return new KeyValuePair<L, R>(this.Left, this.Right);
		}

		// Token: 0x04001570 RID: 5488
		internal L Left;

		// Token: 0x04001571 RID: 5489
		internal R Right;
	}
}
