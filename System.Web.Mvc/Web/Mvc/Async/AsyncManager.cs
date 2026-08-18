using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x020000F0 RID: 240
	public class AsyncManager
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x00011C99 File Offset: 0x0000FE99
		public AsyncManager() : this(null)
		{
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public AsyncManager(SynchronizationContext syncContext)
		{
			this._syncContext = (syncContext ?? SynchronizationContextUtil.GetSynchronizationContext());
			this.OutstandingOperations = new OperationCounter();
			this.OutstandingOperations.Completed += delegate(object param0, EventArgs param1)
			{
				this.Finish();
			};
			this.Parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000635 RID: 1589 RVA: 0x00011D14 File Offset: 0x0000FF14
		// (remove) Token: 0x06000636 RID: 1590 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public event EventHandler Finished;

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00011D81 File Offset: 0x0000FF81
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00011D89 File Offset: 0x0000FF89
		public OperationCounter OutstandingOperations { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00011D92 File Offset: 0x0000FF92
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00011D9A File Offset: 0x0000FF9A
		public IDictionary<string, object> Parameters { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00011DA3 File Offset: 0x0000FFA3
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00011DAB File Offset: 0x0000FFAB
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value < -1)
				{
					throw Error.AsyncCommon_InvalidTimeout("value");
				}
				this._timeout = value;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		public virtual void Finish()
		{
			EventHandler finished = this.Finished;
			if (finished != null)
			{
				finished(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00011DE7 File Offset: 0x0000FFE7
		public virtual void Sync(Action action)
		{
			this._syncContext.Sync(action);
		}

		// Token: 0x040001C0 RID: 448
		private readonly SynchronizationContext _syncContext;

		// Token: 0x040001C1 RID: 449
		private int _timeout = 45000;
	}
}
