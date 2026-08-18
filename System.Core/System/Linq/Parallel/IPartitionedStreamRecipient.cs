using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200018D RID: 397
	internal interface IPartitionedStreamRecipient<TElement>
	{
		// Token: 0x06000E23 RID: 3619
		void Receive<TKey>(PartitionedStream<TElement, TKey> partitionedStream);
	}
}
