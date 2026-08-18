using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200017A RID: 378
	internal interface IParallelPartitionable<T>
	{
		// Token: 0x06000DE2 RID: 3554
		QueryOperatorEnumerator<T, int>[] GetPartitions(int partitionCount);
	}
}
