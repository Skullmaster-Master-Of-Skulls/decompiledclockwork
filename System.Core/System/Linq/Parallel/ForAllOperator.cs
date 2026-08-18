using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D2 RID: 466
	internal sealed class ForAllOperator<TInput> : UnaryQueryOperator<TInput, TInput>
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x00036708 File Offset: 0x00034908
		internal ForAllOperator(IEnumerable<TInput> child, Action<TInput> elementAction) : base(child)
		{
			this.m_elementAction = elementAction;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00036718 File Offset: 0x00034918
		internal void RunSynchronously()
		{
			Shared<bool> topLevelDisposedFlag = new Shared<bool>(false);
			CancellationTokenSource topLevelCancellationTokenSource = new CancellationTokenSource();
			QuerySettings querySettings = base.SpecifiedQuerySettings.WithPerExecutionSettings(topLevelCancellationTokenSource, topLevelDisposedFlag).WithDefaults();
			QueryLifecycle.LogicalQueryExecutionBegin(querySettings.QueryId);
			IEnumerator<TInput> openedEnumerator = base.GetOpenedEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true, true, querySettings);
			querySettings.CleanStateAtQueryEnd();
			QueryLifecycle.LogicalQueryExecutionEnd(querySettings.QueryId);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003677C File Offset: 0x0003497C
		internal override QueryResults<TInput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TInput, TInput>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000367A0 File Offset: 0x000349A0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TInput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInput, int> partitionedStream = new PartitionedStream<TInput, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ForAllOperator<TInput>.ForAllEnumerator<TKey>(inputStream[i], this.m_elementAction, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000367F9 File Offset: 0x000349F9
		internal override IEnumerable<TInput> AsSequentialQuery(CancellationToken token)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00036800 File Offset: 0x00034A00
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008C8 RID: 2248
		private readonly Action<TInput> m_elementAction;

		// Token: 0x020003F5 RID: 1013
		private class ForAllEnumerator<TKey> : QueryOperatorEnumerator<TInput, int>
		{
			// Token: 0x06001E24 RID: 7716 RVA: 0x0006BF00 File Offset: 0x0006A100
			internal ForAllEnumerator(QueryOperatorEnumerator<TInput, TKey> source, Action<TInput> elementAction, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_elementAction = elementAction;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E25 RID: 7717 RVA: 0x0006BF20 File Offset: 0x0006A120
			internal override bool MoveNext(ref TInput currentElement, ref int currentKey)
			{
				TInput obj = default(TInput);
				TKey tkey = default(TKey);
				int num = 0;
				while (this.m_source.MoveNext(ref obj, ref tkey))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					this.m_elementAction(obj);
				}
				return false;
			}

			// Token: 0x06001E26 RID: 7718 RVA: 0x0006BF74 File Offset: 0x0006A174
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011E2 RID: 4578
			private readonly QueryOperatorEnumerator<TInput, TKey> m_source;

			// Token: 0x040011E3 RID: 4579
			private readonly Action<TInput> m_elementAction;

			// Token: 0x040011E4 RID: 4580
			private CancellationToken m_cancellationToken;
		}
	}
}
