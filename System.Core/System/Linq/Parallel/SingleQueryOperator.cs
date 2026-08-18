using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E2 RID: 482
	internal sealed class SingleQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000FAF RID: 4015 RVA: 0x00037804 File Offset: 0x00035A04
		internal SingleQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate) : base(child)
		{
			this.m_predicate = predicate;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00037814 File Offset: 0x00035A14
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, false);
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00037838 File Offset: 0x00035A38
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			Shared<int> totalElementCount = new Shared<int>(0);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new SingleQueryOperator<TSource>.SingleQueryOperatorEnumerator<TKey>(inputStream[i], this.m_predicate, totalElementCount);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003788D File Offset: 0x00035A8D
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00037894 File Offset: 0x00035A94
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008EB RID: 2283
		private readonly Func<TSource, bool> m_predicate;

		// Token: 0x02000405 RID: 1029
		private class SingleQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06001E55 RID: 7765 RVA: 0x0006C92E File Offset: 0x0006AB2E
			internal SingleQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, Shared<int> totalElementCount)
			{
				this.m_source = source;
				this.m_predicate = predicate;
				this.m_totalElementCount = totalElementCount;
			}

			// Token: 0x06001E56 RID: 7766 RVA: 0x0006C94C File Offset: 0x0006AB4C
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (!this.m_alreadySearched)
				{
					bool flag = false;
					TSource tsource = default(TSource);
					TKey tkey = default(TKey);
					while (this.m_source.MoveNext(ref tsource, ref tkey))
					{
						if (this.m_predicate == null || this.m_predicate(tsource))
						{
							Interlocked.Increment(ref this.m_totalElementCount.Value);
							currentElement = tsource;
							currentKey = 0;
							if (flag)
							{
								this.m_yieldExtra = true;
								break;
							}
							flag = true;
						}
						if (Volatile.Read(ref this.m_totalElementCount.Value) > 1)
						{
							break;
						}
					}
					this.m_alreadySearched = true;
					return flag;
				}
				if (this.m_yieldExtra)
				{
					this.m_yieldExtra = false;
					currentElement = default(TSource);
					currentKey = 0;
					return true;
				}
				return false;
			}

			// Token: 0x06001E57 RID: 7767 RVA: 0x0006C9FD File Offset: 0x0006ABFD
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001218 RID: 4632
			private QueryOperatorEnumerator<TSource, TKey> m_source;

			// Token: 0x04001219 RID: 4633
			private Func<TSource, bool> m_predicate;

			// Token: 0x0400121A RID: 4634
			private bool m_alreadySearched;

			// Token: 0x0400121B RID: 4635
			private bool m_yieldExtra;

			// Token: 0x0400121C RID: 4636
			private Shared<int> m_totalElementCount;
		}
	}
}
