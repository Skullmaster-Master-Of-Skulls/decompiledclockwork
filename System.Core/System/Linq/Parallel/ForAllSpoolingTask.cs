using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001F4 RID: 500
	internal class ForAllSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06001005 RID: 4101 RVA: 0x0003899B File Offset: 0x00036B9B
		internal ForAllSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source) : base(taskIndex, groupState)
		{
			this.m_source = source;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000389AC File Offset: 0x00036BAC
		protected override void SpoolingWork()
		{
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			while (this.m_source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000389DA File Offset: 0x00036BDA
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			this.m_source.Dispose();
		}

		// Token: 0x04000924 RID: 2340
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> m_source;
	}
}
