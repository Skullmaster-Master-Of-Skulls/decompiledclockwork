using System;

namespace System.Web.Hosting
{
	// Token: 0x020007AA RID: 1962
	internal class FlushAsyncResult : AsyncResultBase
	{
		// Token: 0x06005D20 RID: 23840 RVA: 0x0014300A File Offset: 0x0014120A
		internal FlushAsyncResult(AsyncCallback cb, object state) : base(cb, state)
		{
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x00143014 File Offset: 0x00141214
		internal override void Complete(int bytesSent, int hresult, IntPtr pAsyncCompletionContext, bool synchronous)
		{
			base.Complete(hresult, synchronous);
		}
	}
}
