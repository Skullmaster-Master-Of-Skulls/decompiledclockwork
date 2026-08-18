using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000189 RID: 393
	internal class ProducerComparerInt : IComparer<Producer<int>>
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x00031C61 File Offset: 0x0002FE61
		public int Compare(Producer<int> x, Producer<int> y)
		{
			return y.MaxKey - x.MaxKey;
		}
	}
}
