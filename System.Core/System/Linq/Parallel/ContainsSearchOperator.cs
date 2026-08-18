using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CD RID: 461
	internal sealed class ContainsSearchOperator<TInput> : UnaryQueryOperator<TInput, bool>
	{
		// Token: 0x06000F47 RID: 3911 RVA: 0x0003607A File Offset: 0x0003427A
		internal ContainsSearchOperator(IEnumerable<TInput> child, TInput searchValue, IEqualityComparer<TInput> comparer) : base(child)
		{
			this.m_searchValue = searchValue;
			if (comparer == null)
			{
				this.m_comparer = EqualityComparer<TInput>.Default;
				return;
			}
			this.m_comparer = comparer;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000360A0 File Offset: 0x000342A0
		internal bool Aggregate()
		{
			using (IEnumerator<bool> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000360F0 File Offset: 0x000342F0
		internal override QueryResults<bool> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TInput, bool>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00036114 File Offset: 0x00034314
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<bool> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<bool, int> partitionedStream = new PartitionedStream<bool, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			Shared<bool> resultFoundFlag = new Shared<bool>(false);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ContainsSearchOperator<TInput>.ContainsSearchOperatorEnumerator<TKey>(inputStream[i], this.m_searchValue, this.m_comparer, i, resultFoundFlag, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003617C File Offset: 0x0003437C
		internal override IEnumerable<bool> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00036183 File Offset: 0x00034383
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008BF RID: 2239
		private readonly TInput m_searchValue;

		// Token: 0x040008C0 RID: 2240
		private readonly IEqualityComparer<TInput> m_comparer;

		// Token: 0x020003EE RID: 1006
		private class ContainsSearchOperatorEnumerator<TKey> : QueryOperatorEnumerator<bool, int>
		{
			// Token: 0x06001E11 RID: 7697 RVA: 0x0006B8E2 File Offset: 0x00069AE2
			internal ContainsSearchOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, TInput searchValue, IEqualityComparer<TInput> comparer, int partitionIndex, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_searchValue = searchValue;
				this.m_comparer = comparer;
				this.m_partitionIndex = partitionIndex;
				this.m_resultFoundFlag = resultFoundFlag;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E12 RID: 7698 RVA: 0x0006B918 File Offset: 0x00069B18
			internal override bool MoveNext(ref bool currentElement, ref int currentKey)
			{
				if (this.m_resultFoundFlag.Value)
				{
					return false;
				}
				TInput x = default(TInput);
				TKey tkey = default(TKey);
				if (this.m_source.MoveNext(ref x, ref tkey))
				{
					currentElement = false;
					currentKey = this.m_partitionIndex;
					int num = 0;
					for (;;)
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (this.m_resultFoundFlag.Value)
						{
							break;
						}
						if (this.m_comparer.Equals(x, this.m_searchValue))
						{
							goto Block_5;
						}
						if (!this.m_source.MoveNext(ref x, ref tkey))
						{
							return true;
						}
					}
					return false;
					Block_5:
					this.m_resultFoundFlag.Value = true;
					currentElement = true;
					return true;
				}
				return false;
			}

			// Token: 0x06001E13 RID: 7699 RVA: 0x0006B9BF File Offset: 0x00069BBF
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011BD RID: 4541
			private readonly QueryOperatorEnumerator<TInput, TKey> m_source;

			// Token: 0x040011BE RID: 4542
			private readonly TInput m_searchValue;

			// Token: 0x040011BF RID: 4543
			private readonly IEqualityComparer<TInput> m_comparer;

			// Token: 0x040011C0 RID: 4544
			private readonly int m_partitionIndex;

			// Token: 0x040011C1 RID: 4545
			private readonly Shared<bool> m_resultFoundFlag;

			// Token: 0x040011C2 RID: 4546
			private CancellationToken m_cancellationToken;
		}
	}
}
