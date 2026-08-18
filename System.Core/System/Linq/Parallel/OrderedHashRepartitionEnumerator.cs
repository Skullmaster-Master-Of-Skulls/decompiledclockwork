using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200018E RID: 398
	internal class OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey> : QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, TOrderKey>
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x0003212C File Offset: 0x0003032C
		internal OrderedHashRepartitionEnumerator(QueryOperatorEnumerator<TInputOutput, TOrderKey> source, int partitionCount, int partitionIndex, Func<TInputOutput, THashKey> keySelector, OrderedHashRepartitionStream<TInputOutput, THashKey, TOrderKey> repartitionStream, CountdownEvent barrier, ListChunk<Pair<TInputOutput, THashKey>>[,] valueExchangeMatrix, ListChunk<TOrderKey>[,] keyExchangeMatrix, CancellationToken cancellationToken)
		{
			this.m_source = source;
			this.m_partitionCount = partitionCount;
			this.m_partitionIndex = partitionIndex;
			this.m_keySelector = keySelector;
			this.m_repartitionStream = repartitionStream;
			this.m_barrier = barrier;
			this.m_valueExchangeMatrix = valueExchangeMatrix;
			this.m_keyExchangeMatrix = keyExchangeMatrix;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00032184 File Offset: 0x00030384
		internal override bool MoveNext(ref Pair<TInputOutput, THashKey> currentElement, ref TOrderKey currentKey)
		{
			if (this.m_partitionCount != 1)
			{
				OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables = this.m_mutables;
				if (mutables == null)
				{
					mutables = (this.m_mutables = new OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables());
				}
				if (mutables.m_currentBufferIndex == -1)
				{
					this.EnumerateAndRedistributeElements();
				}
				while (mutables.m_currentBufferIndex < this.m_partitionCount)
				{
					if (mutables.m_currentBuffer != null)
					{
						OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables2 = mutables;
						int num = mutables2.m_currentIndex + 1;
						mutables2.m_currentIndex = num;
						if (num < mutables.m_currentBuffer.Count)
						{
							currentElement = mutables.m_currentBuffer.m_chunk[mutables.m_currentIndex];
							currentKey = mutables.m_currentKeyBuffer.m_chunk[mutables.m_currentIndex];
							return true;
						}
						mutables.m_currentIndex = -1;
						mutables.m_currentBuffer = mutables.m_currentBuffer.Next;
						mutables.m_currentKeyBuffer = mutables.m_currentKeyBuffer.Next;
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
							mutables.m_currentKeyBuffer = this.m_keyExchangeMatrix[mutables.m_currentBufferIndex, this.m_partitionIndex];
						}
					}
				}
				return false;
			}
			TInputOutput tinputOutput = default(TInputOutput);
			if (this.m_source.MoveNext(ref tinputOutput, ref currentKey))
			{
				currentElement = new Pair<TInputOutput, THashKey>(tinputOutput, (this.m_keySelector == null) ? default(THashKey) : this.m_keySelector(tinputOutput));
				return true;
			}
			return false;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00032358 File Offset: 0x00030558
		private void EnumerateAndRedistributeElements()
		{
			OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables = this.m_mutables;
			ListChunk<Pair<TInputOutput, THashKey>>[] array = new ListChunk<Pair<TInputOutput, THashKey>>[this.m_partitionCount];
			ListChunk<TOrderKey>[] array2 = new ListChunk<TOrderKey>[this.m_partitionCount];
			TInputOutput tinputOutput = default(TInputOutput);
			TOrderKey e = default(TOrderKey);
			int num = 0;
			while (this.m_source.MoveNext(ref tinputOutput, ref e))
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
				ListChunk<TOrderKey> listChunk2 = array2[num2];
				if (listChunk == null)
				{
					listChunk = (array[num2] = new ListChunk<Pair<TInputOutput, THashKey>>(128));
					listChunk2 = (array2[num2] = new ListChunk<TOrderKey>(128));
				}
				listChunk.Add(new Pair<TInputOutput, THashKey>(tinputOutput, thashKey));
				listChunk2.Add(e);
			}
			for (int i = 0; i < this.m_partitionCount; i++)
			{
				this.m_valueExchangeMatrix[this.m_partitionIndex, i] = array[i];
				this.m_keyExchangeMatrix[this.m_partitionIndex, i] = array2[i];
			}
			this.m_barrier.Signal();
			mutables.m_currentBufferIndex = this.m_partitionIndex;
			mutables.m_currentBuffer = array[this.m_partitionIndex];
			mutables.m_currentKeyBuffer = array2[this.m_partitionIndex];
			mutables.m_currentIndex = -1;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000324DA File Offset: 0x000306DA
		protected override void Dispose(bool disposing)
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

		// Token: 0x04000855 RID: 2133
		private const int ENUMERATION_NOT_STARTED = -1;

		// Token: 0x04000856 RID: 2134
		private readonly int m_partitionCount;

		// Token: 0x04000857 RID: 2135
		private readonly int m_partitionIndex;

		// Token: 0x04000858 RID: 2136
		private readonly Func<TInputOutput, THashKey> m_keySelector;

		// Token: 0x04000859 RID: 2137
		private readonly HashRepartitionStream<TInputOutput, THashKey, TOrderKey> m_repartitionStream;

		// Token: 0x0400085A RID: 2138
		private readonly ListChunk<Pair<TInputOutput, THashKey>>[,] m_valueExchangeMatrix;

		// Token: 0x0400085B RID: 2139
		private readonly ListChunk<TOrderKey>[,] m_keyExchangeMatrix;

		// Token: 0x0400085C RID: 2140
		private readonly QueryOperatorEnumerator<TInputOutput, TOrderKey> m_source;

		// Token: 0x0400085D RID: 2141
		private CountdownEvent m_barrier;

		// Token: 0x0400085E RID: 2142
		private readonly CancellationToken m_cancellationToken;

		// Token: 0x0400085F RID: 2143
		private OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables m_mutables;

		// Token: 0x020003B2 RID: 946
		private class Mutables
		{
			// Token: 0x06001D50 RID: 7504 RVA: 0x0006853F File Offset: 0x0006673F
			internal Mutables()
			{
				this.m_currentBufferIndex = -1;
			}

			// Token: 0x0400110E RID: 4366
			internal int m_currentBufferIndex;

			// Token: 0x0400110F RID: 4367
			internal ListChunk<Pair<TInputOutput, THashKey>> m_currentBuffer;

			// Token: 0x04001110 RID: 4368
			internal ListChunk<TOrderKey> m_currentKeyBuffer;

			// Token: 0x04001111 RID: 4369
			internal int m_currentIndex;
		}
	}
}
