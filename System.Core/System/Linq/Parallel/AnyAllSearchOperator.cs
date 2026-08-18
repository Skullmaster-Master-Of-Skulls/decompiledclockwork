using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CA RID: 458
	internal sealed class AnyAllSearchOperator<TInput> : UnaryQueryOperator<TInput, bool>
	{
		// Token: 0x06000F36 RID: 3894 RVA: 0x00035D00 File Offset: 0x00033F00
		internal AnyAllSearchOperator(IEnumerable<TInput> child, bool qualification, Func<TInput, bool> predicate) : base(child)
		{
			this.m_qualification = qualification;
			this.m_predicate = predicate;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00035D18 File Offset: 0x00033F18
		internal bool Aggregate()
		{
			using (IEnumerator<bool> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == this.m_qualification)
					{
						return this.m_qualification;
					}
				}
			}
			return !this.m_qualification;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00035D7C File Offset: 0x00033F7C
		internal override QueryResults<bool> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TInput, bool>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00035DA0 File Offset: 0x00033FA0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<bool> recipient, bool preferStriping, QuerySettings settings)
		{
			Shared<bool> resultFoundFlag = new Shared<bool>(false);
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<bool, int> partitionedStream = new PartitionedStream<bool, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new AnyAllSearchOperator<TInput>.AnyAllSearchOperatorEnumerator<TKey>(inputStream[i], this.m_qualification, this.m_predicate, i, resultFoundFlag, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00035E08 File Offset: 0x00034008
		internal override IEnumerable<bool> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00035E0F File Offset: 0x0003400F
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008B8 RID: 2232
		private readonly Func<TInput, bool> m_predicate;

		// Token: 0x040008B9 RID: 2233
		private readonly bool m_qualification;

		// Token: 0x020003EA RID: 1002
		private class AnyAllSearchOperatorEnumerator<TKey> : QueryOperatorEnumerator<bool, int>
		{
			// Token: 0x06001E04 RID: 7684 RVA: 0x0006B649 File Offset: 0x00069849
			internal AnyAllSearchOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, bool qualification, Func<TInput, bool> predicate, int partitionIndex, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_qualification = qualification;
				this.m_predicate = predicate;
				this.m_partitionIndex = partitionIndex;
				this.m_resultFoundFlag = resultFoundFlag;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E05 RID: 7685 RVA: 0x0006B680 File Offset: 0x00069880
			internal override bool MoveNext(ref bool currentElement, ref int currentKey)
			{
				if (this.m_resultFoundFlag.Value)
				{
					return false;
				}
				TInput arg = default(TInput);
				TKey tkey = default(TKey);
				if (this.m_source.MoveNext(ref arg, ref tkey))
				{
					currentElement = !this.m_qualification;
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
						if (this.m_predicate(arg) == this.m_qualification)
						{
							goto Block_5;
						}
						if (!this.m_source.MoveNext(ref arg, ref tkey))
						{
							return true;
						}
					}
					return false;
					Block_5:
					this.m_resultFoundFlag.Value = true;
					currentElement = this.m_qualification;
					return true;
				}
				return false;
			}

			// Token: 0x06001E06 RID: 7686 RVA: 0x0006B734 File Offset: 0x00069934
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011AF RID: 4527
			private readonly QueryOperatorEnumerator<TInput, TKey> m_source;

			// Token: 0x040011B0 RID: 4528
			private readonly Func<TInput, bool> m_predicate;

			// Token: 0x040011B1 RID: 4529
			private readonly bool m_qualification;

			// Token: 0x040011B2 RID: 4530
			private readonly int m_partitionIndex;

			// Token: 0x040011B3 RID: 4531
			private readonly Shared<bool> m_resultFoundFlag;

			// Token: 0x040011B4 RID: 4532
			private readonly CancellationToken m_cancellationToken;
		}
	}
}
