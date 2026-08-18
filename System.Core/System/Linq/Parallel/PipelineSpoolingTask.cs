using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F3 RID: 499
	internal class PipelineSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x000388FB File Offset: 0x00036AFB
		internal PipelineSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, AsynchronousChannel<TInputOutput> destination) : base(taskIndex, groupState)
		{
			this.m_source = source;
			this.m_destination = destination;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00038914 File Offset: 0x00036B14
		protected override void SpoolingWork()
		{
			TInputOutput item = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source = this.m_source;
			AsynchronousChannel<TInputOutput> destination = this.m_destination;
			CancellationToken mergedCancellationToken = this.m_groupState.CancellationState.MergedCancellationToken;
			while (source.MoveNext(ref item, ref tignoreKey) && !mergedCancellationToken.IsCancellationRequested)
			{
				destination.Enqueue(item);
			}
			destination.FlushBuffers();
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00038975 File Offset: 0x00036B75
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			if (this.m_destination != null)
			{
				this.m_destination.SetDone();
			}
			this.m_source.Dispose();
		}

		// Token: 0x04000922 RID: 2338
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> m_source;

		// Token: 0x04000923 RID: 2339
		private AsynchronousChannel<TInputOutput> m_destination;
	}
}
