using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004D2 RID: 1234
	internal class CallbackClosure
	{
		// Token: 0x0600266E RID: 9838 RVA: 0x0009C400 File Offset: 0x0009B400
		internal CallbackClosure(ExecutionContext context, AsyncCallback callback)
		{
			if (callback != null)
			{
				this.savedCallback = callback;
				this.savedContext = context;
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0009C419 File Offset: 0x0009B419
		internal bool IsCompatible(AsyncCallback callback)
		{
			return callback != null && this.savedCallback != null && object.Equals(this.savedCallback, callback);
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x0009C439 File Offset: 0x0009B439
		internal AsyncCallback AsyncCallback
		{
			get
			{
				return this.savedCallback;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0009C441 File Offset: 0x0009B441
		internal ExecutionContext Context
		{
			get
			{
				return this.savedContext;
			}
		}

		// Token: 0x040025F5 RID: 9717
		private AsyncCallback savedCallback;

		// Token: 0x040025F6 RID: 9718
		private ExecutionContext savedContext;
	}
}
