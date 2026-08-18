using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x0200024E RID: 590
	internal class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x0600166B RID: 5739 RVA: 0x00074184 File Offset: 0x00072384
		internal MultiAsyncResult(object context, AsyncCallback callback, object state) : base(context, state, callback)
		{
			this.context = context;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00074196 File Offset: 0x00072396
		internal object Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0007419E File Offset: 0x0007239E
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000741A6 File Offset: 0x000723A6
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000741AE File Offset: 0x000723AE
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000741BD File Offset: 0x000723BD
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this.outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000741D9 File Offset: 0x000723D9
		private void Increment()
		{
			Interlocked.Increment(ref this.outstanding);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000741E7 File Offset: 0x000723E7
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x000741F0 File Offset: 0x000723F0
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x04001742 RID: 5954
		private int outstanding;

		// Token: 0x04001743 RID: 5955
		private object context;
	}
}
