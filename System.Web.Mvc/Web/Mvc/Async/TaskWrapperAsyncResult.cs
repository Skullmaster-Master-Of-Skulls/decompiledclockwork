using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Mvc.Async
{
	// Token: 0x0200005D RID: 93
	internal sealed class TaskWrapperAsyncResult : IAsyncResult
	{
		// Token: 0x0600026E RID: 622 RVA: 0x000087AE File Offset: 0x000069AE
		internal TaskWrapperAsyncResult(Task task, object asyncState, Action cleanupThunk = null)
		{
			this.Task = task;
			this.AsyncState = asyncState;
			this.CleanupThunk = cleanupThunk;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000087CB File Offset: 0x000069CB
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000087D3 File Offset: 0x000069D3
		public object AsyncState { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000087DC File Offset: 0x000069DC
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.Task).AsyncWaitHandle;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000087E9 File Offset: 0x000069E9
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000087F1 File Offset: 0x000069F1
		public Action CleanupThunk { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000087FC File Offset: 0x000069FC
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000882C File Offset: 0x00006A2C
		public bool CompletedSynchronously
		{
			get
			{
				bool? completedSynchronously = this._completedSynchronously;
				if (completedSynchronously == null)
				{
					return ((IAsyncResult)this.Task).CompletedSynchronously;
				}
				return completedSynchronously.GetValueOrDefault();
			}
			internal set
			{
				this._completedSynchronously = new bool?(value);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000883A File Offset: 0x00006A3A
		public bool IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.Task).IsCompleted;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00008847 File Offset: 0x00006A47
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000884F File Offset: 0x00006A4F
		internal Task Task { get; private set; }

		// Token: 0x0400007A RID: 122
		private bool? _completedSynchronously;
	}
}
