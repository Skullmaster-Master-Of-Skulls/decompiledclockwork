using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001C7 RID: 455
	internal struct QuerySettings
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00035826 File Offset: 0x00033A26
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0003582E File Offset: 0x00033A2E
		internal CancellationState CancellationState
		{
			get
			{
				return this.m_cancellationState;
			}
			set
			{
				this.m_cancellationState = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00035837 File Offset: 0x00033A37
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0003583F File Offset: 0x00033A3F
		internal TaskScheduler TaskScheduler
		{
			get
			{
				return this.m_taskScheduler;
			}
			set
			{
				this.m_taskScheduler = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00035848 File Offset: 0x00033A48
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x00035850 File Offset: 0x00033A50
		internal int? DegreeOfParallelism
		{
			get
			{
				return this.m_degreeOfParallelism;
			}
			set
			{
				this.m_degreeOfParallelism = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00035859 File Offset: 0x00033A59
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00035861 File Offset: 0x00033A61
		internal ParallelExecutionMode? ExecutionMode
		{
			get
			{
				return this.m_executionMode;
			}
			set
			{
				this.m_executionMode = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003586A File Offset: 0x00033A6A
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00035872 File Offset: 0x00033A72
		internal ParallelMergeOptions? MergeOptions
		{
			get
			{
				return this.m_mergeOptions;
			}
			set
			{
				this.m_mergeOptions = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003587B File Offset: 0x00033A7B
		internal int QueryId
		{
			get
			{
				return this.m_queryId;
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00035883 File Offset: 0x00033A83
		internal QuerySettings(TaskScheduler taskScheduler, int? degreeOfParallelism, CancellationToken externalCancellationToken, ParallelExecutionMode? executionMode, ParallelMergeOptions? mergeOptions)
		{
			this.m_taskScheduler = taskScheduler;
			this.m_degreeOfParallelism = degreeOfParallelism;
			this.m_cancellationState = new CancellationState(externalCancellationToken);
			this.m_executionMode = executionMode;
			this.m_mergeOptions = mergeOptions;
			this.m_queryId = -1;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000358B8 File Offset: 0x00033AB8
		internal QuerySettings Merge(QuerySettings settings2)
		{
			if (this.TaskScheduler != null && settings2.TaskScheduler != null)
			{
				throw new InvalidOperationException(SR.GetString("ParallelQuery_DuplicateTaskScheduler"));
			}
			if (this.DegreeOfParallelism != null && settings2.DegreeOfParallelism != null)
			{
				throw new InvalidOperationException(SR.GetString("ParallelQuery_DuplicateDOP"));
			}
			if (this.CancellationState.ExternalCancellationToken.CanBeCanceled && settings2.CancellationState.ExternalCancellationToken.CanBeCanceled)
			{
				throw new InvalidOperationException(SR.GetString("ParallelQuery_DuplicateWithCancellation"));
			}
			if (this.ExecutionMode != null && settings2.ExecutionMode != null)
			{
				throw new InvalidOperationException(SR.GetString("ParallelQuery_DuplicateExecutionMode"));
			}
			if (this.MergeOptions != null && settings2.MergeOptions != null)
			{
				throw new InvalidOperationException(SR.GetString("ParallelQuery_DuplicateMergeOptions"));
			}
			TaskScheduler taskScheduler = (this.TaskScheduler == null) ? settings2.TaskScheduler : this.TaskScheduler;
			int? degreeOfParallelism = (this.DegreeOfParallelism != null) ? this.DegreeOfParallelism : settings2.DegreeOfParallelism;
			CancellationToken externalCancellationToken = this.CancellationState.ExternalCancellationToken.CanBeCanceled ? this.CancellationState.ExternalCancellationToken : settings2.CancellationState.ExternalCancellationToken;
			ParallelExecutionMode? executionMode = (this.ExecutionMode != null) ? this.ExecutionMode : settings2.ExecutionMode;
			ParallelMergeOptions? mergeOptions = (this.MergeOptions != null) ? this.MergeOptions : settings2.MergeOptions;
			return new QuerySettings(taskScheduler, degreeOfParallelism, externalCancellationToken, executionMode, mergeOptions);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00035A67 File Offset: 0x00033C67
		internal QuerySettings WithPerExecutionSettings()
		{
			return this.WithPerExecutionSettings(new CancellationTokenSource(), new Shared<bool>(false));
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00035A7C File Offset: 0x00033C7C
		internal QuerySettings WithPerExecutionSettings(CancellationTokenSource topLevelCancellationTokenSource, Shared<bool> topLevelDisposedFlag)
		{
			QuerySettings result = new QuerySettings(this.TaskScheduler, this.DegreeOfParallelism, this.CancellationState.ExternalCancellationToken, this.ExecutionMode, this.MergeOptions);
			result.CancellationState.InternalCancellationTokenSource = topLevelCancellationTokenSource;
			result.CancellationState.MergedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(result.CancellationState.InternalCancellationTokenSource.Token, result.CancellationState.ExternalCancellationToken);
			result.CancellationState.TopLevelDisposedFlag = topLevelDisposedFlag;
			result.m_queryId = PlinqEtwProvider.NextQueryId();
			return result;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00035B08 File Offset: 0x00033D08
		internal QuerySettings WithDefaults()
		{
			QuerySettings result = this;
			if (result.TaskScheduler == null)
			{
				result.TaskScheduler = TaskScheduler.Default;
			}
			if (result.DegreeOfParallelism == null)
			{
				result.DegreeOfParallelism = new int?(Scheduling.GetDefaultDegreeOfParallelism());
			}
			if (result.ExecutionMode == null)
			{
				result.ExecutionMode = new ParallelExecutionMode?(ParallelExecutionMode.Default);
			}
			if (result.MergeOptions == null)
			{
				result.MergeOptions = new ParallelMergeOptions?(ParallelMergeOptions.Default);
			}
			ParallelMergeOptions? mergeOptions = result.MergeOptions;
			ParallelMergeOptions parallelMergeOptions = ParallelMergeOptions.Default;
			if (mergeOptions.GetValueOrDefault() == parallelMergeOptions & mergeOptions != null)
			{
				result.MergeOptions = new ParallelMergeOptions?(ParallelMergeOptions.AutoBuffered);
			}
			return result;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00035BC0 File Offset: 0x00033DC0
		internal static QuerySettings Empty
		{
			get
			{
				return new QuerySettings(null, null, default(CancellationToken), null, null);
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00035BF7 File Offset: 0x00033DF7
		public void CleanStateAtQueryEnd()
		{
			this.m_cancellationState.MergedCancellationTokenSource.Dispose();
		}

		// Token: 0x040008AD RID: 2221
		private TaskScheduler m_taskScheduler;

		// Token: 0x040008AE RID: 2222
		private int? m_degreeOfParallelism;

		// Token: 0x040008AF RID: 2223
		private CancellationState m_cancellationState;

		// Token: 0x040008B0 RID: 2224
		private ParallelExecutionMode? m_executionMode;

		// Token: 0x040008B1 RID: 2225
		private ParallelMergeOptions? m_mergeOptions;

		// Token: 0x040008B2 RID: 2226
		private int m_queryId;
	}
}
