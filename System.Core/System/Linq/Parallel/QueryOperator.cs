using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001C4 RID: 452
	internal abstract class QueryOperator<TOutput> : ParallelQuery<TOutput>
	{
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003548A File Offset: 0x0003368A
		internal QueryOperator(QuerySettings settings) : this(false, settings)
		{
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00035494 File Offset: 0x00033694
		internal QueryOperator(bool isOrdered, QuerySettings settings) : base(settings)
		{
			this.m_outputOrdered = isOrdered;
		}

		// Token: 0x06000EF3 RID: 3827
		internal abstract QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping);

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000354A4 File Offset: 0x000336A4
		public override IEnumerator<TOutput> GetEnumerator()
		{
			return this.GetEnumerator(null, false);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000354C1 File Offset: 0x000336C1
		public IEnumerator<TOutput> GetEnumerator(ParallelMergeOptions? mergeOptions)
		{
			return this.GetEnumerator(mergeOptions, false);
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x000354CB File Offset: 0x000336CB
		internal bool OutputOrdered
		{
			get
			{
				return this.m_outputOrdered;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000354D3 File Offset: 0x000336D3
		internal virtual IEnumerator<TOutput> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			return new QueryOpeningEnumerator<TOutput>(this, mergeOptions, suppressOrderPreservation);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000354E0 File Offset: 0x000336E0
		internal IEnumerator<TOutput> GetOpenedEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrder, bool forEffect, QuerySettings querySettings)
		{
			if (querySettings.ExecutionMode.Value == ParallelExecutionMode.Default && this.LimitsParallelism)
			{
				IEnumerable<TOutput> source = this.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
				return ExceptionAggregator.WrapEnumerable<TOutput>(source, querySettings.CancellationState).GetEnumerator();
			}
			QueryResults<TOutput> queryResults = this.GetQueryResults(querySettings);
			if (mergeOptions == null)
			{
				mergeOptions = querySettings.MergeOptions;
			}
			if (querySettings.CancellationState.MergedCancellationToken.IsCancellationRequested)
			{
				if (querySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(querySettings.CancellationState.ExternalCancellationToken);
				}
				throw new OperationCanceledException();
			}
			else
			{
				bool outputOrdered = this.OutputOrdered && !suppressOrder;
				PartitionedStreamMerger<TOutput> partitionedStreamMerger = new PartitionedStreamMerger<TOutput>(forEffect, mergeOptions.GetValueOrDefault(), querySettings.TaskScheduler, outputOrdered, querySettings.CancellationState, querySettings.QueryId);
				queryResults.GivePartitionedStream(partitionedStreamMerger);
				if (forEffect)
				{
					return null;
				}
				return partitionedStreamMerger.MergeExecutor.GetEnumerator();
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000355D6 File Offset: 0x000337D6
		private QueryResults<TOutput> GetQueryResults(QuerySettings querySettings)
		{
			return this.Open(querySettings, false);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000355E0 File Offset: 0x000337E0
		internal TOutput[] ExecuteAndGetResultsAsArray()
		{
			QuerySettings querySettings = base.SpecifiedQuerySettings.WithPerExecutionSettings().WithDefaults();
			QueryLifecycle.LogicalQueryExecutionBegin(querySettings.QueryId);
			TOutput[] result;
			try
			{
				if (querySettings.ExecutionMode.Value == ParallelExecutionMode.Default && this.LimitsParallelism)
				{
					IEnumerable<TOutput> source = this.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TOutput> source2 = CancellableEnumerable.Wrap<TOutput>(source, querySettings.CancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TOutput>(source2, querySettings.CancellationState).ToArray<TOutput>();
				}
				else
				{
					QueryResults<TOutput> queryResults = this.GetQueryResults(querySettings);
					if (queryResults.IsIndexible && this.OutputOrdered)
					{
						ArrayMergeHelper<TOutput> arrayMergeHelper = new ArrayMergeHelper<TOutput>(base.SpecifiedQuerySettings, queryResults);
						arrayMergeHelper.Execute();
						TOutput[] resultsAsArray = arrayMergeHelper.GetResultsAsArray();
						querySettings.CleanStateAtQueryEnd();
						result = resultsAsArray;
					}
					else
					{
						PartitionedStreamMerger<TOutput> partitionedStreamMerger = new PartitionedStreamMerger<TOutput>(false, ParallelMergeOptions.FullyBuffered, querySettings.TaskScheduler, this.OutputOrdered, querySettings.CancellationState, querySettings.QueryId);
						queryResults.GivePartitionedStream(partitionedStreamMerger);
						TOutput[] resultsAsArray2 = partitionedStreamMerger.MergeExecutor.GetResultsAsArray();
						querySettings.CleanStateAtQueryEnd();
						result = resultsAsArray2;
					}
				}
			}
			finally
			{
				QueryLifecycle.LogicalQueryExecutionEnd(querySettings.QueryId);
			}
			return result;
		}

		// Token: 0x06000EFB RID: 3835
		internal abstract IEnumerable<TOutput> AsSequentialQuery(CancellationToken token);

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000EFC RID: 3836
		internal abstract bool LimitsParallelism { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000EFD RID: 3837
		internal abstract OrdinalIndexState OrdinalIndexState { get; }

		// Token: 0x06000EFE RID: 3838 RVA: 0x00035718 File Offset: 0x00033918
		internal static ListQueryResults<TOutput> ExecuteAndCollectResults<TKey>(PartitionedStream<TOutput, TKey> openedChild, int partitionCount, bool outputOrdered, bool useStriping, QuerySettings settings)
		{
			TaskScheduler taskScheduler = settings.TaskScheduler;
			MergeExecutor<TOutput> mergeExecutor = MergeExecutor<TOutput>.Execute<TKey>(openedChild, false, ParallelMergeOptions.FullyBuffered, taskScheduler, outputOrdered, settings.CancellationState, settings.QueryId);
			return new ListQueryResults<TOutput>(mergeExecutor.GetResultsAsArray(), partitionCount, useStriping);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00035754 File Offset: 0x00033954
		internal static QueryOperator<TOutput> AsQueryOperator(IEnumerable<TOutput> source)
		{
			QueryOperator<TOutput> queryOperator = source as QueryOperator<TOutput>;
			if (queryOperator == null)
			{
				OrderedParallelQuery<TOutput> orderedParallelQuery = source as OrderedParallelQuery<TOutput>;
				if (orderedParallelQuery != null)
				{
					queryOperator = orderedParallelQuery.SortOperator;
				}
				else
				{
					queryOperator = new ScanQueryOperator<TOutput>(source);
				}
			}
			return queryOperator;
		}

		// Token: 0x040008AC RID: 2220
		protected bool m_outputOrdered;
	}
}
