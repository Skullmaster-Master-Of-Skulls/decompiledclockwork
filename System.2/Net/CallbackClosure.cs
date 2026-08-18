using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A8 RID: 424
	internal class CallbackClosure
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x00059C95 File Offset: 0x00057E95
		internal CallbackClosure(ExecutionContext context, AsyncCallback callback)
		{
			if (callback != null)
			{
				this.savedCallback = callback;
				this.savedContext = context;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00059CAE File Offset: 0x00057EAE
		internal bool IsCompatible(AsyncCallback callback)
		{
			return callback != null && this.savedCallback != null && object.Equals(this.savedCallback, callback);
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00059CCE File Offset: 0x00057ECE
		internal AsyncCallback AsyncCallback
		{
			get
			{
				return this.savedCallback;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00059CD6 File Offset: 0x00057ED6
		internal ExecutionContext Context
		{
			get
			{
				return this.savedContext;
			}
		}

		// Token: 0x040013AC RID: 5036
		private AsyncCallback savedCallback;

		// Token: 0x040013AD RID: 5037
		private ExecutionContext savedContext;
	}
}
