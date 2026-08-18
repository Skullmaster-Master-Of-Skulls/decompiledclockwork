using System;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x0200011E RID: 286
	public sealed class OperationCounter
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000775 RID: 1909 RVA: 0x000144C0 File Offset: 0x000126C0
		// (remove) Token: 0x06000776 RID: 1910 RVA: 0x000144F8 File Offset: 0x000126F8
		public event EventHandler Completed;

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001452D File Offset: 0x0001272D
		public int Count
		{
			get
			{
				return Thread.VolatileRead(ref this._count);
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001453C File Offset: 0x0001273C
		private int AddAndExecuteCallbackIfCompleted(int value)
		{
			int num = Interlocked.Add(ref this._count, value);
			if (num == 0)
			{
				this.OnCompleted();
			}
			return num;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00014560 File Offset: 0x00012760
		public int Decrement()
		{
			return this.AddAndExecuteCallbackIfCompleted(-1);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00014569 File Offset: 0x00012769
		public int Decrement(int value)
		{
			return this.AddAndExecuteCallbackIfCompleted(-value);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00014573 File Offset: 0x00012773
		public int Increment()
		{
			return this.AddAndExecuteCallbackIfCompleted(1);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001457C File Offset: 0x0001277C
		public int Increment(int value)
		{
			return this.AddAndExecuteCallbackIfCompleted(value);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00014588 File Offset: 0x00012788
		private void OnCompleted()
		{
			EventHandler completed = this.Completed;
			if (completed != null)
			{
				completed(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000213 RID: 531
		private int _count;
	}
}
