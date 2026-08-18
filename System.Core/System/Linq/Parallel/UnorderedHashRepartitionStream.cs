using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000192 RID: 402
	internal class UnorderedHashRepartitionStream<TInputOutput, THashKey, TIgnoreKey> : HashRepartitionStream<TInputOutput, THashKey, int>
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x0003274C File Offset: 0x0003094C
		internal UnorderedHashRepartitionStream(PartitionedStream<TInputOutput, TIgnoreKey> inputStream, Func<TInputOutput, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TInputOutput> elementComparer, CancellationToken cancellationToken) : base(inputStream.PartitionCount, Util.GetDefaultComparer<int>(), keyComparer, elementComparer)
		{
			QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, int>[] partitions = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>[inputStream.PartitionCount];
			this.m_partitions = partitions;
			CountdownEvent barrier = new CountdownEvent(inputStream.PartitionCount);
			ListChunk<Pair<TInputOutput, THashKey>>[,] valueExchangeMatrix = new ListChunk<Pair<TInputOutput, THashKey>>[inputStream.PartitionCount, inputStream.PartitionCount];
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				this.m_partitions[i] = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>(inputStream[i], inputStream.PartitionCount, i, keySelector, this, barrier, valueExchangeMatrix, cancellationToken);
			}
		}
	}
}
