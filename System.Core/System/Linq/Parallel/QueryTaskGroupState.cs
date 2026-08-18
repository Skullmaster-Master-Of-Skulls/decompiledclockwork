using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001EF RID: 495
	internal class QueryTaskGroupState
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00038564 File Offset: 0x00036764
		internal QueryTaskGroupState(CancellationState cancellationState, int queryId)
		{
			this.m_cancellationState = cancellationState;
			this.m_queryId = queryId;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003857A File Offset: 0x0003677A
		internal bool IsAlreadyEnded
		{
			get
			{
				return this.m_alreadyEnded == 1;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00038585 File Offset: 0x00036785
		internal CancellationState CancellationState
		{
			get
			{
				return this.m_cancellationState;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0003858D File Offset: 0x0003678D
		internal int QueryId
		{
			get
			{
				return this.m_queryId;
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00038595 File Offset: 0x00036795
		internal void QueryBegin(Task rootTask)
		{
			this.m_rootTask = rootTask;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000385A0 File Offset: 0x000367A0
		internal void QueryEnd(bool userInitiatedDispose)
		{
			if (Interlocked.Exchange(ref this.m_alreadyEnded, 1) == 0)
			{
				try
				{
					this.m_rootTask.Wait();
				}
				catch (AggregateException ex)
				{
					AggregateException ex2 = ex.Flatten();
					bool flag = true;
					for (int i = 0; i < ex2.InnerExceptions.Count; i++)
					{
						OperationCanceledException ex3 = ex2.InnerExceptions[i] as OperationCanceledException;
						if (ex3 == null || !ex3.CancellationToken.IsCancellationRequested || ex3.CancellationToken != this.m_cancellationState.ExternalCancellationToken)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						throw ex2;
					}
				}
				finally
				{
					this.m_rootTask.Dispose();
				}
				if (this.m_cancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					if (!this.m_cancellationState.TopLevelDisposedFlag.Value)
					{
						CancellationState.ThrowWithStandardMessageIfCanceled(this.m_cancellationState.ExternalCancellationToken);
					}
					if (!userInitiatedDispose)
					{
						throw new ObjectDisposedException("enumerator", SR.GetString("PLINQ_DisposeRequested"));
					}
				}
			}
		}

		// Token: 0x04000916 RID: 2326
		private Task m_rootTask;

		// Token: 0x04000917 RID: 2327
		private int m_alreadyEnded;

		// Token: 0x04000918 RID: 2328
		private CancellationState m_cancellationState;

		// Token: 0x04000919 RID: 2329
		private int m_queryId;
	}
}
