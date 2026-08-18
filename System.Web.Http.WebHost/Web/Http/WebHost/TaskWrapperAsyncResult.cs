using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000029 RID: 41
	internal sealed class TaskWrapperAsyncResult : IAsyncResult
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00006C4D File Offset: 0x00004E4D
		public TaskWrapperAsyncResult(Task task, object asyncState)
		{
			if (task == null)
			{
				throw Error.ArgumentNull("task");
			}
			this.Task = task;
			this.AsyncState = asyncState;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006C71 File Offset: 0x00004E71
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006C79 File Offset: 0x00004E79
		public object AsyncState { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006C82 File Offset: 0x00004E82
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.Task).AsyncWaitHandle;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006C90 File Offset: 0x00004E90
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006CC0 File Offset: 0x00004EC0
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006CCE File Offset: 0x00004ECE
		public bool IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.Task).IsCompleted;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006CDB File Offset: 0x00004EDB
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006CE3 File Offset: 0x00004EE3
		public Task Task { get; private set; }

		// Token: 0x0400004E RID: 78
		private bool? _completedSynchronously;
	}
}
