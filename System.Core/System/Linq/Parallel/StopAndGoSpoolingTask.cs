using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F2 RID: 498
	internal class StopAndGoSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x0003885A File Offset: 0x00036A5A
		internal StopAndGoSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, SynchronousChannel<TInputOutput> destination) : base(taskIndex, groupState)
		{
			this.m_source = source;
			this.m_destination = destination;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00038874 File Offset: 0x00036A74
		protected override void SpoolingWork()
		{
			TInputOutput item = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source = this.m_source;
			SynchronousChannel<TInputOutput> destination = this.m_destination;
			CancellationToken mergedCancellationToken = this.m_groupState.CancellationState.MergedCancellationToken;
			destination.Init();
			while (source.MoveNext(ref item, ref tignoreKey) && !mergedCancellationToken.IsCancellationRequested)
			{
				destination.Enqueue(item);
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000388D5 File Offset: 0x00036AD5
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			if (this.m_destination != null)
			{
				this.m_destination.SetDone();
			}
			this.m_source.Dispose();
		}

		// Token: 0x04000920 RID: 2336
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> m_source;

		// Token: 0x04000921 RID: 2337
		private SynchronousChannel<TInputOutput> m_destination;
	}
}
