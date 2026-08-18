using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200018B RID: 395
	internal class HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey> : QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, int>
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x00031D24 File Offset: 0x0002FF24
		internal HashRepartitionEnumerator(QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, int partitionCount, int partitionIndex, Func<TInputOutput, THashKey> keySelector, HashRepartitionStream<TInputOutput, THashKey, int> repartitionStream, CountdownEvent barrier, ListChunk<Pair<TInputOutput, THashKey>>[,] valueExchangeMatrix, CancellationToken cancellationToken)
		{
			this.m_source = source;
			this.m_partitionCount = partitionCount;
			this.m_partitionIndex = partitionIndex;
			this.m_keySelector = keySelector;
			this.m_repartitionStream = repartitionStream;
			this.m_barrier = barrier;
			this.m_valueExchangeMatrix = valueExchangeMatrix;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00031D74 File Offset: 0x0002FF74
		internal override bool MoveNext(ref Pair<TInputOutput, THashKey> currentElement, ref int currentKey)
		{
			if (this.m_partitionCount != 1)
			{
				HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables mutables = this.m_mutables;
				if (mutables == null)
				{
					mutables = (this.m_mutables = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables());
				}
				if (mutables.m_currentBufferIndex == -1)
				{
					this.EnumerateAndRedistributeElements();
				}
				while (mutables.m_currentBufferIndex < this.m_partitionCount)
				{
					if (mutables.m_currentBuffer != null)
					{
						HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables mutables2 = mutables;
						int num = mutables2.m_currentIndex + 1;
						mutables2.m_currentIndex = num;
						if (num < mutables.m_currentBuffer.Count)
						{
							currentElement = mutables.m_currentBuffer.m_chunk[mutables.m_currentIndex];
							return true;
						}
						mutables.m_currentIndex = -1;
						mutables.m_currentBuffer = mutables.m_currentBuffer.Next;
					}
					else
					{
						if (mutables.m_currentBufferIndex == this.m_partitionIndex)
						{
							this.m_barrier.Wait(this.m_cancellationToken);
							mutables.m_currentBufferIndex = -1;
						}
						mutables.m_currentBufferIndex++;
						mutables.m_currentIndex = -1;
						if (mutables.m_currentBufferIndex == this.m_partitionIndex)
						{
							mutables.m_currentBufferIndex++;
						}
						if (mutables.m_currentBufferIndex < this.m_partitionCount)
						{
							mutables.m_currentBuffer = this.m_valueExchangeMatrix[mutables.m_currentBufferIndex, this.m_partitionIndex];
						}
					}
				}
				return false;
			}
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			TInputOutput tinputOutput = default(TInputOutput);
			if (this.m_source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
				currentElement = new Pair<TInputOutput, THashKey>(tinputOutput, (this.m_keySelector == null) ? default(THashKey) : this.m_keySelector(tinputOutput));
				return true;
			}
			return false;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00031F04 File Offset: 0x00030104
		private void EnumerateAndRedistributeElements()
		{
			HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables mutables = this.m_mutables;
			ListChunk<Pair<TInputOutput, THashKey>>[] array = new ListChunk<Pair<TInputOutput, THashKey>>[this.m_partitionCount];
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			int num = 0;
			while (this.m_source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this.m_cancellationToken);
				}
				THashKey thashKey = default(THashKey);
				int num2;
				if (this.m_keySelector != null)
				{
					thashKey = this.m_keySelector(tinputOutput);
					num2 = this.m_repartitionStream.GetHashCode(thashKey) % this.m_partitionCount;
				}
				else
				{
					num2 = this.m_repartitionStream.GetHashCode(tinputOutput) % this.m_partitionCount;
				}
				ListChunk<Pair<TInputOutput, THashKey>> listChunk = array[num2];
				if (listChunk == null)
				{
					listChunk = (array[num2] = new ListChunk<Pair<TInputOutput, THashKey>>(128));
				}
				listChunk.Add(new Pair<TInputOutput, THashKey>(tinputOutput, thashKey));
			}
			for (int i = 0; i < this.m_partitionCount; i++)
			{
				this.m_valueExchangeMatrix[this.m_partitionIndex, i] = array[i];
			}
			this.m_barrier.Signal();
			mutables.m_currentBufferIndex = this.m_partitionIndex;
			mutables.m_currentBuffer = array[this.m_partitionIndex];
			mutables.m_currentIndex = -1;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00032035 File Offset: 0x00030235
		protected override void Dispose(bool disposed)
		{
			if (this.m_barrier != null)
			{
				if (this.m_mutables == null || this.m_mutables.m_currentBufferIndex == -1)
				{
					this.m_barrier.Signal();
					this.m_barrier = null;
				}
				this.m_source.Dispose();
			}
		}

		// Token: 0x04000847 RID: 2119
		private const int ENUMERATION_NOT_STARTED = -1;

		// Token: 0x04000848 RID: 2120
		private readonly int m_partitionCount;

		// Token: 0x04000849 RID: 2121
		private readonly int m_partitionIndex;

		// Token: 0x0400084A RID: 2122
		private readonly Func<TInputOutput, THashKey> m_keySelector;

		// Token: 0x0400084B RID: 2123
		private readonly HashRepartitionStream<TInputOutput, THashKey, int> m_repartitionStream;

		// Token: 0x0400084C RID: 2124
		private readonly ListChunk<Pair<TInputOutput, THashKey>>[,] m_valueExchangeMatrix;

		// Token: 0x0400084D RID: 2125
		private readonly QueryOperatorEnumerator<TInputOutput, TIgnoreKey> m_source;

		// Token: 0x0400084E RID: 2126
		private CountdownEvent m_barrier;

		// Token: 0x0400084F RID: 2127
		private readonly CancellationToken m_cancellationToken;

		// Token: 0x04000850 RID: 2128
		private HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables m_mutables;

		// Token: 0x020003B1 RID: 945
		private class Mutables
		{
			// Token: 0x06001D4F RID: 7503 RVA: 0x00068530 File Offset: 0x00066730
			internal Mutables()
			{
				this.m_currentBufferIndex = -1;
			}

			// Token: 0x0400110B RID: 4363
			internal int m_currentBufferIndex;

			// Token: 0x0400110C RID: 4364
			internal ListChunk<Pair<TInputOutput, THashKey>> m_currentBuffer;

			// Token: 0x0400110D RID: 4365
			internal int m_currentIndex;
		}
	}
}
