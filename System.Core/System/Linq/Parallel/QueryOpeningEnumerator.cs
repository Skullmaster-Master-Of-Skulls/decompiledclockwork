using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019F RID: 415
	internal class QueryOpeningEnumerator<TOutput> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x00033A01 File Offset: 0x00031C01
		internal QueryOpeningEnumerator(QueryOperator<TOutput> queryOperator, ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			this.m_queryOperator = queryOperator;
			this.m_mergeOptions = mergeOptions;
			this.m_suppressOrderPreservation = suppressOrderPreservation;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00033A35 File Offset: 0x00031C35
		public TOutput Current
		{
			get
			{
				if (this.m_openedQueryEnumerator == null)
				{
					throw new InvalidOperationException(SR.GetString("PLINQ_CommonEnumerator_Current_NotStarted"));
				}
				return this.m_openedQueryEnumerator.Current;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00033A5C File Offset: 0x00031C5C
		public void Dispose()
		{
			this.m_topLevelDisposedFlag.Value = true;
			this.m_topLevelCancellationTokenSource.Cancel();
			if (this.m_openedQueryEnumerator != null)
			{
				this.m_openedQueryEnumerator.Dispose();
				this.m_querySettings.CleanStateAtQueryEnd();
			}
			QueryLifecycle.LogicalQueryExecutionEnd(this.m_querySettings.QueryId);
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00033AAE File Offset: 0x00031CAE
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TOutput>)this).Current;
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00033ABC File Offset: 0x00031CBC
		public bool MoveNext()
		{
			if (this.m_topLevelDisposedFlag.Value)
			{
				throw new ObjectDisposedException("enumerator", SR.GetString("PLINQ_DisposeRequested"));
			}
			if (this.m_openedQueryEnumerator == null)
			{
				this.OpenQuery();
			}
			bool result = this.m_openedQueryEnumerator.MoveNext();
			if ((this.m_moveNextIteration & 63) == 0)
			{
				CancellationState.ThrowWithStandardMessageIfCanceled(this.m_querySettings.CancellationState.ExternalCancellationToken);
			}
			this.m_moveNextIteration++;
			return result;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00033B34 File Offset: 0x00031D34
		private void OpenQuery()
		{
			if (this.m_hasQueryOpeningFailed)
			{
				throw new InvalidOperationException(SR.GetString("PLINQ_EnumerationPreviouslyFailed"));
			}
			try
			{
				this.m_querySettings = this.m_queryOperator.SpecifiedQuerySettings.WithPerExecutionSettings(this.m_topLevelCancellationTokenSource, this.m_topLevelDisposedFlag).WithDefaults();
				QueryLifecycle.LogicalQueryExecutionBegin(this.m_querySettings.QueryId);
				this.m_openedQueryEnumerator = this.m_queryOperator.GetOpenedEnumerator(this.m_mergeOptions, this.m_suppressOrderPreservation, false, this.m_querySettings);
				CancellationState.ThrowWithStandardMessageIfCanceled(this.m_querySettings.CancellationState.ExternalCancellationToken);
			}
			catch
			{
				this.m_hasQueryOpeningFailed = true;
				throw;
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00033BEC File Offset: 0x00031DEC
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400088E RID: 2190
		private readonly QueryOperator<TOutput> m_queryOperator;

		// Token: 0x0400088F RID: 2191
		private IEnumerator<TOutput> m_openedQueryEnumerator;

		// Token: 0x04000890 RID: 2192
		private QuerySettings m_querySettings;

		// Token: 0x04000891 RID: 2193
		private readonly ParallelMergeOptions? m_mergeOptions;

		// Token: 0x04000892 RID: 2194
		private readonly bool m_suppressOrderPreservation;

		// Token: 0x04000893 RID: 2195
		private int m_moveNextIteration;

		// Token: 0x04000894 RID: 2196
		private bool m_hasQueryOpeningFailed;

		// Token: 0x04000895 RID: 2197
		private readonly Shared<bool> m_topLevelDisposedFlag = new Shared<bool>(false);

		// Token: 0x04000896 RID: 2198
		private readonly CancellationTokenSource m_topLevelCancellationTokenSource = new CancellationTokenSource();
	}
}
