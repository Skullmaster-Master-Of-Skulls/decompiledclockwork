using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CF RID: 463
	internal sealed class DistinctQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x0003626A File Offset: 0x0003446A
		internal DistinctQueryOperator(IEnumerable<TInputOutput> source, IEqualityComparer<TInputOutput> comparer) : base(source)
		{
			this.m_comparer = comparer;
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00036284 File Offset: 0x00034484
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> childQueryResults = base.Child.Open(settings, false);
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(childQueryResults, this, settings, false);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000362A8 File Offset: 0x000344A8
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TKey>(inputStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TKey>(inputStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00036318 File Offset: 0x00034518
		private void WrapPartitionedStreamHelper<TKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> hashStream, IPartitionedStreamRecipient<TInputOutput> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<TInputOutput, TKey> partitionedStream = new PartitionedStream<TInputOutput, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream[i] = new DistinctQueryOperator<TInputOutput>.OrderedDistinctQueryOperatorEnumerator<TKey>(hashStream[i], this.m_comparer, hashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream[i] = (QueryOperatorEnumerator<TInputOutput, TKey>)new DistinctQueryOperator<TInputOutput>.DistinctQueryOperatorEnumerator<TKey>(hashStream[i], this.m_comparer, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00036396 File Offset: 0x00034596
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003639C File Offset: 0x0003459C
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> source = CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token);
			return source.Distinct(this.m_comparer);
		}

		// Token: 0x040008C2 RID: 2242
		private readonly IEqualityComparer<TInputOutput> m_comparer;

		// Token: 0x020003F0 RID: 1008
		private class DistinctQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06001E17 RID: 7703 RVA: 0x0006BABC File Offset: 0x00069CBC
			internal DistinctQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> source, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_hashLookup = new Set<TInputOutput>(comparer);
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E18 RID: 7704 RVA: 0x0006BAE0 File Offset: 0x00069CE0
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				TKey tkey = default(TKey);
				Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				if (this.m_outputLoopCount == null)
				{
					this.m_outputLoopCount = new Shared<int>(0);
				}
				while (this.m_source.MoveNext(ref pair, ref tkey))
				{
					Shared<int> outputLoopCount = this.m_outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					if (this.m_hashLookup.Add(pair.First))
					{
						currentElement = pair.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001E19 RID: 7705 RVA: 0x0006BB6E File Offset: 0x00069D6E
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011CB RID: 4555
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> m_source;

			// Token: 0x040011CC RID: 4556
			private Set<TInputOutput> m_hashLookup;

			// Token: 0x040011CD RID: 4557
			private CancellationToken m_cancellationToken;

			// Token: 0x040011CE RID: 4558
			private Shared<int> m_outputLoopCount;
		}

		// Token: 0x020003F1 RID: 1009
		private class OrderedDistinctQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, TKey>
		{
			// Token: 0x06001E1A RID: 7706 RVA: 0x0006BB7B File Offset: 0x00069D7B
			internal OrderedDistinctQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> source, IEqualityComparer<TInputOutput> comparer, IComparer<TKey> keyComparer, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_keyComparer = keyComparer;
				this.m_hashLookup = new Dictionary<Wrapper<TInputOutput>, TKey>(new WrapperEqualityComparer<TInputOutput>(comparer));
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E1B RID: 7707 RVA: 0x0006BBB0 File Offset: 0x00069DB0
			internal override bool MoveNext(ref TInputOutput currentElement, ref TKey currentKey)
			{
				if (this.m_hashLookupEnumerator == null)
				{
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TKey tkey = default(TKey);
					int num = 0;
					while (this.m_source.MoveNext(ref pair, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair.First);
						TKey y;
						if (!this.m_hashLookup.TryGetValue(key, out y) || this.m_keyComparer.Compare(tkey, y) < 0)
						{
							this.m_hashLookup[key] = tkey;
						}
					}
					this.m_hashLookupEnumerator = this.m_hashLookup.GetEnumerator();
				}
				if (this.m_hashLookupEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, TKey> keyValuePair = this.m_hashLookupEnumerator.Current;
					currentElement = keyValuePair.Key.Value;
					currentKey = keyValuePair.Value;
					return true;
				}
				return false;
			}

			// Token: 0x06001E1C RID: 7708 RVA: 0x0006BC91 File Offset: 0x00069E91
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
				if (this.m_hashLookupEnumerator != null)
				{
					this.m_hashLookupEnumerator.Dispose();
				}
			}

			// Token: 0x040011CF RID: 4559
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> m_source;

			// Token: 0x040011D0 RID: 4560
			private Dictionary<Wrapper<TInputOutput>, TKey> m_hashLookup;

			// Token: 0x040011D1 RID: 4561
			private IComparer<TKey> m_keyComparer;

			// Token: 0x040011D2 RID: 4562
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, TKey>> m_hashLookupEnumerator;

			// Token: 0x040011D3 RID: 4563
			private CancellationToken m_cancellationToken;
		}
	}
}
