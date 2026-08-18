using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000188 RID: 392
	internal struct Producer<TKey>
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x00031C51 File Offset: 0x0002FE51
		internal Producer(TKey maxKey, int producerIndex)
		{
			this.MaxKey = maxKey;
			this.ProducerIndex = producerIndex;
		}

		// Token: 0x04000842 RID: 2114
		internal readonly TKey MaxKey;

		// Token: 0x04000843 RID: 2115
		internal readonly int ProducerIndex;
	}
}
