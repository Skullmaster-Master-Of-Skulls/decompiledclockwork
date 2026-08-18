using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000198 RID: 408
	internal class HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput> : QueryOperatorEnumerator<TOutput, TLeftKey>
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x00032E59 File Offset: 0x00031059
		internal HashJoinQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TLeftInput, THashKey>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TRightInput, THashKey>, int> rightSource, Func<TLeftInput, TRightInput, TOutput> singleResultSelector, Func<TLeftInput, IEnumerable<TRightInput>, TOutput> groupResultSelector, IEqualityComparer<THashKey> keyComparer, CancellationToken cancellationToken)
		{
			this.m_leftSource = leftSource;
			this.m_rightSource = rightSource;
			this.m_singleResultSelector = singleResultSelector;
			this.m_groupResultSelector = groupResultSelector;
			this.m_keyComparer = keyComparer;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00032E90 File Offset: 0x00031090
		internal override bool MoveNext(ref TOutput currentElement, ref TLeftKey currentKey)
		{
			HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables mutables = this.m_mutables;
			if (mutables == null)
			{
				mutables = (this.m_mutables = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables());
				mutables.m_rightHashLookup = new HashLookup<THashKey, Pair<TRightInput, ListChunk<TRightInput>>>(this.m_keyComparer);
				Pair<TRightInput, THashKey> pair = default(Pair<TRightInput, THashKey>);
				int num = 0;
				int num2 = 0;
				while (this.m_rightSource.MoveNext(ref pair, ref num))
				{
					if ((num2++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					TRightInput first = pair.First;
					THashKey second = pair.Second;
					if (second != null)
					{
						Pair<TRightInput, ListChunk<TRightInput>> value = default(Pair<TRightInput, ListChunk<TRightInput>>);
						if (!mutables.m_rightHashLookup.TryGetValue(second, ref value))
						{
							value = new Pair<TRightInput, ListChunk<TRightInput>>(first, null);
							if (this.m_groupResultSelector != null)
							{
								value.Second = new ListChunk<TRightInput>(2);
								value.Second.Add(first);
							}
							mutables.m_rightHashLookup.Add(second, value);
						}
						else
						{
							if (value.Second == null)
							{
								value.Second = new ListChunk<TRightInput>(2);
								mutables.m_rightHashLookup[second] = value;
							}
							value.Second.Add(first);
						}
					}
				}
			}
			ListChunk<TRightInput> currentRightMatches = mutables.m_currentRightMatches;
			if (currentRightMatches != null && mutables.m_currentRightMatchesIndex == currentRightMatches.Count)
			{
				ListChunk<TRightInput> listChunk = mutables.m_currentRightMatches = currentRightMatches.Next;
				mutables.m_currentRightMatchesIndex = 0;
			}
			if (mutables.m_currentRightMatches == null)
			{
				Pair<TLeftInput, THashKey> pair2 = default(Pair<TLeftInput, THashKey>);
				TLeftKey tleftKey = default(TLeftKey);
				while (this.m_leftSource.MoveNext(ref pair2, ref tleftKey))
				{
					HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables mutables2 = mutables;
					int outputLoopCount = mutables2.m_outputLoopCount;
					mutables2.m_outputLoopCount = outputLoopCount + 1;
					if ((outputLoopCount & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					Pair<TRightInput, ListChunk<TRightInput>> pair3 = default(Pair<TRightInput, ListChunk<TRightInput>>);
					TLeftInput first2 = pair2.First;
					THashKey second2 = pair2.Second;
					if (second2 != null && mutables.m_rightHashLookup.TryGetValue(second2, ref pair3) && this.m_singleResultSelector != null)
					{
						mutables.m_currentRightMatches = pair3.Second;
						mutables.m_currentRightMatchesIndex = 0;
						currentElement = this.m_singleResultSelector(first2, pair3.First);
						currentKey = tleftKey;
						if (pair3.Second != null)
						{
							mutables.m_currentLeft = first2;
							mutables.m_currentLeftKey = tleftKey;
						}
						return true;
					}
					if (this.m_groupResultSelector != null)
					{
						IEnumerable<TRightInput> enumerable = pair3.Second;
						if (enumerable == null)
						{
							enumerable = ParallelEnumerable.Empty<TRightInput>();
						}
						currentElement = this.m_groupResultSelector(first2, enumerable);
						currentKey = tleftKey;
						return true;
					}
				}
				return false;
			}
			currentElement = this.m_singleResultSelector(mutables.m_currentLeft, mutables.m_currentRightMatches.m_chunk[mutables.m_currentRightMatchesIndex]);
			currentKey = mutables.m_currentLeftKey;
			mutables.m_currentRightMatchesIndex++;
			return true;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003314D File Offset: 0x0003134D
		protected override void Dispose(bool disposing)
		{
			this.m_leftSource.Dispose();
			this.m_rightSource.Dispose();
		}

		// Token: 0x04000871 RID: 2161
		private readonly QueryOperatorEnumerator<Pair<TLeftInput, THashKey>, TLeftKey> m_leftSource;

		// Token: 0x04000872 RID: 2162
		private readonly QueryOperatorEnumerator<Pair<TRightInput, THashKey>, int> m_rightSource;

		// Token: 0x04000873 RID: 2163
		private readonly Func<TLeftInput, TRightInput, TOutput> m_singleResultSelector;

		// Token: 0x04000874 RID: 2164
		private readonly Func<TLeftInput, IEnumerable<TRightInput>, TOutput> m_groupResultSelector;

		// Token: 0x04000875 RID: 2165
		private readonly IEqualityComparer<THashKey> m_keyComparer;

		// Token: 0x04000876 RID: 2166
		private readonly CancellationToken m_cancellationToken;

		// Token: 0x04000877 RID: 2167
		private HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables m_mutables;

		// Token: 0x020003C0 RID: 960
		private class Mutables
		{
			// Token: 0x0400114B RID: 4427
			internal TLeftInput m_currentLeft;

			// Token: 0x0400114C RID: 4428
			internal TLeftKey m_currentLeftKey;

			// Token: 0x0400114D RID: 4429
			internal HashLookup<THashKey, Pair<TRightInput, ListChunk<TRightInput>>> m_rightHashLookup;

			// Token: 0x0400114E RID: 4430
			internal ListChunk<TRightInput> m_currentRightMatches;

			// Token: 0x0400114F RID: 4431
			internal int m_currentRightMatchesIndex;

			// Token: 0x04001150 RID: 4432
			internal int m_outputLoopCount;
		}
	}
}
