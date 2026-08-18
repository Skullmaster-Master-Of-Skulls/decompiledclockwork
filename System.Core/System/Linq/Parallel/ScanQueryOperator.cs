using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000195 RID: 405
	internal sealed class ScanQueryOperator<TElement> : QueryOperator<TElement>
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x00032A28 File Offset: 0x00030C28
		internal ScanQueryOperator(IEnumerable<TElement> data) : base(false, QuerySettings.Empty)
		{
			ParallelEnumerableWrapper<TElement> parallelEnumerableWrapper = data as ParallelEnumerableWrapper<TElement>;
			if (parallelEnumerableWrapper != null)
			{
				data = parallelEnumerableWrapper.WrappedEnumerable;
			}
			this.m_data = data;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00032A5A File Offset: 0x00030C5A
		public IEnumerable<TElement> Data
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00032A64 File Offset: 0x00030C64
		internal override QueryResults<TElement> Open(QuerySettings settings, bool preferStriping)
		{
			IList<TElement> list = this.m_data as IList<TElement>;
			if (list != null)
			{
				return new ListQueryResults<TElement>(list, settings.DegreeOfParallelism.GetValueOrDefault(), preferStriping);
			}
			return new ScanQueryOperator<TElement>.ScanEnumerableQueryOperatorResults(this.m_data, settings);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00032AA3 File Offset: 0x00030CA3
		internal override IEnumerator<TElement> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			return this.m_data.GetEnumerator();
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00032AB0 File Offset: 0x00030CB0
		internal override IEnumerable<TElement> AsSequentialQuery(CancellationToken token)
		{
			return this.m_data;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00032AB8 File Offset: 0x00030CB8
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				if (!(this.m_data is IList<TElement>))
				{
					return OrdinalIndexState.Correct;
				}
				return OrdinalIndexState.Indexible;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00032ACA File Offset: 0x00030CCA
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400086B RID: 2155
		private readonly IEnumerable<TElement> m_data;

		// Token: 0x020003BD RID: 957
		private class ScanEnumerableQueryOperatorResults : QueryResults<TElement>
		{
			// Token: 0x06001D72 RID: 7538 RVA: 0x0006909B File Offset: 0x0006729B
			internal ScanEnumerableQueryOperatorResults(IEnumerable<TElement> data, QuerySettings settings)
			{
				this.m_data = data;
				this.m_settings = settings;
			}

			// Token: 0x06001D73 RID: 7539 RVA: 0x000690B4 File Offset: 0x000672B4
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TElement> recipient)
			{
				PartitionedStream<TElement, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TElement>(this.m_data, this.m_settings.DegreeOfParallelism.Value, false);
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x0400113D RID: 4413
			private IEnumerable<TElement> m_data;

			// Token: 0x0400113E RID: 4414
			private QuerySettings m_settings;
		}
	}
}
