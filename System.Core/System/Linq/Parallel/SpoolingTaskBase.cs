using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001F5 RID: 501
	internal abstract class SpoolingTaskBase : QueryTask
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x000389ED File Offset: 0x00036BED
		protected SpoolingTaskBase(int taskIndex, QueryTaskGroupState groupState) : base(taskIndex, groupState)
		{
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000389F8 File Offset: 0x00036BF8
		protected override void Work()
		{
			try
			{
				this.SpoolingWork();
			}
			catch (Exception ex)
			{
				OperationCanceledException ex2 = ex as OperationCanceledException;
				if (ex2 == null || !(ex2.CancellationToken == this.m_groupState.CancellationState.MergedCancellationToken) || !this.m_groupState.CancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					this.m_groupState.CancellationState.InternalCancellationTokenSource.Cancel();
					throw;
				}
			}
			finally
			{
				this.SpoolingFinally();
			}
		}

		// Token: 0x0600100A RID: 4106
		protected abstract void SpoolingWork();

		// Token: 0x0600100B RID: 4107 RVA: 0x00038A8C File Offset: 0x00036C8C
		protected virtual void SpoolingFinally()
		{
		}
	}
}
