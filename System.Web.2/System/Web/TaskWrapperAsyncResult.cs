using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x0200004E RID: 78
	internal sealed class TaskWrapperAsyncResult : IAsyncResult
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x000078F1 File Offset: 0x00005AF1
		internal TaskWrapperAsyncResult(Task task, object asyncState)
		{
			this.Task = task;
			this.AsyncState = asyncState;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00007907 File Offset: 0x00005B07
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000790F File Offset: 0x00005B0F
		public object AsyncState { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00007918 File Offset: 0x00005B18
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.Task).AsyncWaitHandle;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00007925 File Offset: 0x00005B25
		public bool CompletedSynchronously
		{
			get
			{
				return this._forceCompletedSynchronously || ((IAsyncResult)this.Task).CompletedSynchronously;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000793C File Offset: 0x00005B3C
		public bool IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.Task).IsCompleted;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00007949 File Offset: 0x00005B49
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00007951 File Offset: 0x00005B51
		internal Task Task { get; private set; }

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000795A File Offset: 0x00005B5A
		internal void ForceCompletedSynchronously()
		{
			this._forceCompletedSynchronously = true;
		}

		// Token: 0x04000150 RID: 336
		private bool _forceCompletedSynchronously;
	}
}
