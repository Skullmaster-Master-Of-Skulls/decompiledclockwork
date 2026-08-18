using System;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000124 RID: 292
	internal sealed class SimpleAsyncResult : IAsyncResult
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00014D70 File Offset: 0x00012F70
		public SimpleAsyncResult(object asyncState)
		{
			this._asyncState = asyncState;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00014D7F File Offset: 0x00012F7F
		public object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00014D87 File Offset: 0x00012F87
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00014D8A File Offset: 0x00012F8A
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00014D92 File Offset: 0x00012F92
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00014D9C File Offset: 0x00012F9C
		public void MarkCompleted(bool completedSynchronously, AsyncCallback callback)
		{
			this._completedSynchronously = completedSynchronously;
			this._isCompleted = true;
			if (callback != null)
			{
				callback(this);
			}
		}

		// Token: 0x04000228 RID: 552
		private readonly object _asyncState;

		// Token: 0x04000229 RID: 553
		private bool _completedSynchronously;

		// Token: 0x0400022A RID: 554
		private volatile bool _isCompleted;
	}
}
