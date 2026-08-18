using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001DF RID: 479
	internal sealed class ReverseQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x000373A0 File Offset: 0x000355A0
		internal ReverseQueryOperator(IEnumerable<TSource> child) : base(child)
		{
			if (base.Child.OrdinalIndexState == OrdinalIndexState.Indexible)
			{
				base.SetOrdinalIndexState(OrdinalIndexState.Indexible);
				return;
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000373C8 File Offset: 0x000355C8
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, TKey> partitionedStream = new PartitionedStream<TSource, TKey>(partitionCount, new ReverseComparer<TKey>(inputStream.KeyComparer), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ReverseQueryOperator<TSource>.ReverseQueryOperatorEnumerator<TKey>(inputStream[i], settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00037424 File Offset: 0x00035624
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, false);
			return ReverseQueryOperator<TSource>.ReverseQueryOperatorResults.NewResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00037448 File Offset: 0x00035648
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TSource> source = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(token), token);
			return source.Reverse<TSource>();
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003746E File Offset: 0x0003566E
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x020003FF RID: 1023
		private class ReverseQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, TKey>
		{
			// Token: 0x06001E3F RID: 7743 RVA: 0x0006C3A4 File Offset: 0x0006A5A4
			internal ReverseQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E40 RID: 7744 RVA: 0x0006C3BC File Offset: 0x0006A5BC
			internal override bool MoveNext(ref TSource currentElement, ref TKey currentKey)
			{
				if (this.m_buffer == null)
				{
					this.m_bufferIndex = new Shared<int>(0);
					this.m_buffer = new List<Pair<TSource, TKey>>();
					TSource first = default(TSource);
					TKey second = default(TKey);
					int num = 0;
					while (this.m_source.MoveNext(ref first, ref second))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						this.m_buffer.Add(new Pair<TSource, TKey>(first, second));
						this.m_bufferIndex.Value++;
					}
				}
				Shared<int> bufferIndex = this.m_bufferIndex;
				int num2 = bufferIndex.Value - 1;
				bufferIndex.Value = num2;
				if (num2 >= 0)
				{
					currentElement = this.m_buffer[this.m_bufferIndex.Value].First;
					currentKey = this.m_buffer[this.m_bufferIndex.Value].Second;
					return true;
				}
				return false;
			}

			// Token: 0x06001E41 RID: 7745 RVA: 0x0006C4AD File Offset: 0x0006A6AD
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001203 RID: 4611
			private readonly QueryOperatorEnumerator<TSource, TKey> m_source;

			// Token: 0x04001204 RID: 4612
			private readonly CancellationToken m_cancellationToken;

			// Token: 0x04001205 RID: 4613
			private List<Pair<TSource, TKey>> m_buffer;

			// Token: 0x04001206 RID: 4614
			private Shared<int> m_bufferIndex;
		}

		// Token: 0x02000400 RID: 1024
		private class ReverseQueryOperatorResults : UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults
		{
			// Token: 0x06001E42 RID: 7746 RVA: 0x0006C4BA File Offset: 0x0006A6BA
			public static QueryResults<TSource> NewResults(QueryResults<TSource> childQueryResults, ReverseQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new ReverseQueryOperator<TSource>.ReverseQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06001E43 RID: 7747 RVA: 0x0006C4D7 File Offset: 0x0006A6D7
			private ReverseQueryOperatorResults(QueryResults<TSource> childQueryResults, ReverseQueryOperator<TSource> op, QuerySettings settings, bool preferStriping) : base(childQueryResults, op, settings, preferStriping)
			{
				this.m_count = this.m_childQueryResults.ElementsCount;
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0006C4F5 File Offset: 0x0006A6F5
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001E45 RID: 7749 RVA: 0x0006C4F8 File Offset: 0x0006A6F8
			internal override int ElementsCount
			{
				get
				{
					return this.m_count;
				}
			}

			// Token: 0x06001E46 RID: 7750 RVA: 0x0006C500 File Offset: 0x0006A700
			internal override TSource GetElement(int index)
			{
				return this.m_childQueryResults.GetElement(this.m_count - index - 1);
			}

			// Token: 0x04001207 RID: 4615
			private int m_count;
		}
	}
}
