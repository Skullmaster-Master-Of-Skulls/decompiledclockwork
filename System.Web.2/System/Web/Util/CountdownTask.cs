using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Util
{
	// Token: 0x020001D1 RID: 465
	internal sealed class CountdownTask
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x000499F5 File Offset: 0x00047BF5
		public CountdownTask(int initialCount)
		{
			this.AddCount(initialCount);
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00049A0F File Offset: 0x00047C0F
		public int CurrentCount
		{
			get
			{
				return this._pendingCount;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00049A17 File Offset: 0x00047C17
		public Task Task
		{
			get
			{
				return this._tcs.Task;
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00049A24 File Offset: 0x00047C24
		private void AddCount(int delta)
		{
			if (Interlocked.Add(ref this._pendingCount, delta) == 0)
			{
				bool flag = this._tcs.TrySetResult(null);
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00049A4E File Offset: 0x00047C4E
		public void MarkOperationCompleted()
		{
			this.AddCount(-1);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00049A57 File Offset: 0x00047C57
		public void MarkOperationPending()
		{
			this.AddCount(1);
		}

		// Token: 0x04001713 RID: 5907
		private int _pendingCount;

		// Token: 0x04001714 RID: 5908
		private readonly TaskCompletionSource<object> _tcs = new TaskCompletionSource<object>();
	}
}
