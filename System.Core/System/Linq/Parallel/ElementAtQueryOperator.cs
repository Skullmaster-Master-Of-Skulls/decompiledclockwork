using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D0 RID: 464
	internal sealed class ElementAtQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x000363C8 File Offset: 0x000345C8
		internal ElementAtQueryOperator(IEnumerable<TSource> child, int index) : base(child)
		{
			this.m_index = index;
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this.m_prematureMerge = true;
				this.m_limitsParallelism = (ordinalIndexState != OrdinalIndexState.Shuffled);
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003640C File Offset: 0x0003460C
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, false);
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00036430 File Offset: 0x00034630
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, int> partitionedStream;
			if (this.m_prematureMerge)
			{
				partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TSource, int>)inputStream;
			}
			Shared<bool> resultFoundFlag = new Shared<bool>(false);
			PartitionedStream<TSource, int> partitionedStream2 = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new ElementAtQueryOperator<TSource>.ElementAtQueryOperatorEnumerator(partitionedStream[i], this.m_index, resultFoundFlag, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000364C3 File Offset: 0x000346C3
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000364CA File Offset: 0x000346CA
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_limitsParallelism;
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000364D4 File Offset: 0x000346D4
		internal bool Aggregate(out TSource result, bool withDefaultValue)
		{
			if (this.LimitsParallelism && base.SpecifiedQuerySettings.WithDefaults().ExecutionMode.Value != ParallelExecutionMode.ForceParallelism)
			{
				CancellationState cancellationState = base.SpecifiedQuerySettings.CancellationState;
				if (withDefaultValue)
				{
					IEnumerable<TSource> source = base.Child.AsSequentialQuery(cancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source2 = CancellableEnumerable.Wrap<TSource>(source, cancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TSource>(source2, cancellationState).ElementAtOrDefault(this.m_index);
				}
				else
				{
					IEnumerable<TSource> source3 = base.Child.AsSequentialQuery(cancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source4 = CancellableEnumerable.Wrap<TSource>(source3, cancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TSource>(source4, cancellationState).ElementAt(this.m_index);
				}
				return true;
			}
			using (IEnumerator<TSource> enumerator = base.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)))
			{
				if (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					result = tsource;
					return true;
				}
			}
			result = default(TSource);
			return false;
		}

		// Token: 0x040008C3 RID: 2243
		private readonly int m_index;

		// Token: 0x040008C4 RID: 2244
		private readonly bool m_prematureMerge;

		// Token: 0x040008C5 RID: 2245
		private readonly bool m_limitsParallelism;

		// Token: 0x020003F2 RID: 1010
		private class ElementAtQueryOperatorEnumerator : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06001E1D RID: 7709 RVA: 0x0006BCB1 File Offset: 0x00069EB1
			internal ElementAtQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, int> source, int index, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_index = index;
				this.m_resultFoundFlag = resultFoundFlag;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E1E RID: 7710 RVA: 0x0006BCD8 File Offset: 0x00069ED8
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				int num = 0;
				while (this.m_source.MoveNext(ref currentElement, ref currentKey))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					if (this.m_resultFoundFlag.Value)
					{
						break;
					}
					if (currentKey == this.m_index)
					{
						this.m_resultFoundFlag.Value = true;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001E1F RID: 7711 RVA: 0x0006BD33 File Offset: 0x00069F33
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011D4 RID: 4564
			private QueryOperatorEnumerator<TSource, int> m_source;

			// Token: 0x040011D5 RID: 4565
			private int m_index;

			// Token: 0x040011D6 RID: 4566
			private Shared<bool> m_resultFoundFlag;

			// Token: 0x040011D7 RID: 4567
			private CancellationToken m_cancellationToken;
		}
	}
}
