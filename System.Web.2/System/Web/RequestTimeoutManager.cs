using System;
using System.Collections;
using System.Threading;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000F0 RID: 240
	internal class RequestTimeoutManager
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x00029454 File Offset: 0x00027654
		internal RequestTimeoutManager()
		{
			this._requestCount = 0;
			this._lists = new DoubleLinkList[13];
			for (int i = 0; i < this._lists.Length; i++)
			{
				this._lists[i] = new DoubleLinkList();
			}
			this._currentList = 0;
			this._inProgressLock = 0;
			this._timer = new Timer(new TimerCallback(this.TimerCompletionCallback), null, this._timerPeriod, this._timerPeriod);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000294DC File Offset: 0x000276DC
		internal void Stop()
		{
			if (this._timer != null)
			{
				((IDisposable)this._timer).Dispose();
				this._timer = null;
			}
			while (this._inProgressLock != 0)
			{
				Thread.Sleep(100);
			}
			if (this._requestCount > 0)
			{
				this.CancelTimedOutRequests(DateTime.UtcNow.AddYears(1));
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00029531 File Offset: 0x00027731
		private void TimerCompletionCallback(object state)
		{
			if (this._requestCount > 0)
			{
				this.CancelTimedOutRequests(DateTime.UtcNow);
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00029548 File Offset: 0x00027748
		private void CancelTimedOutRequests(DateTime now)
		{
			if (Interlocked.CompareExchange(ref this._inProgressLock, 1, 0) != 0)
			{
				return;
			}
			ArrayList arrayList = new ArrayList(this._requestCount);
			for (int i = 0; i < this._lists.Length; i++)
			{
				DoubleLinkList obj = this._lists[i];
				lock (obj)
				{
					DoubleLinkListEnumerator enumerator = this._lists[i].GetEnumerator();
					while (enumerator.MoveNext())
					{
						arrayList.Add(enumerator.GetDoubleLink());
					}
				}
			}
			int count = arrayList.Count;
			for (int j = 0; j < count; j++)
			{
				((RequestTimeoutManager.RequestTimeoutEntry)arrayList[j]).TimeoutIfNeeded(now);
			}
			Interlocked.Exchange(ref this._inProgressLock, 0);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00029618 File Offset: 0x00027818
		internal void Add(HttpContext context)
		{
			if (context.TimeoutLink != null)
			{
				((RequestTimeoutManager.RequestTimeoutEntry)context.TimeoutLink).IncrementCount();
				return;
			}
			RequestTimeoutManager.RequestTimeoutEntry requestTimeoutEntry = new RequestTimeoutManager.RequestTimeoutEntry(context);
			int currentList = this._currentList;
			this._currentList = currentList + 1;
			int num = currentList;
			if (num >= this._lists.Length)
			{
				num = 0;
				this._currentList = 0;
			}
			requestTimeoutEntry.AddToList(this._lists[num]);
			Interlocked.Increment(ref this._requestCount);
			context.TimeoutLink = requestTimeoutEntry;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002968C File Offset: 0x0002788C
		internal void Remove(HttpContext context)
		{
			RequestTimeoutManager.RequestTimeoutEntry requestTimeoutEntry = (RequestTimeoutManager.RequestTimeoutEntry)context.TimeoutLink;
			if (requestTimeoutEntry != null)
			{
				if (requestTimeoutEntry.DecrementCount() != 0)
				{
					return;
				}
				requestTimeoutEntry.RemoveFromList();
				Interlocked.Decrement(ref this._requestCount);
			}
			context.TimeoutLink = null;
		}

		// Token: 0x0400059B RID: 1435
		private int _requestCount;

		// Token: 0x0400059C RID: 1436
		private DoubleLinkList[] _lists;

		// Token: 0x0400059D RID: 1437
		private int _currentList;

		// Token: 0x0400059E RID: 1438
		private int _inProgressLock;

		// Token: 0x0400059F RID: 1439
		private readonly TimeSpan _timerPeriod = new TimeSpan(0, 0, 15);

		// Token: 0x040005A0 RID: 1440
		private Timer _timer;

		// Token: 0x020008EA RID: 2282
		private class RequestTimeoutEntry : DoubleLink
		{
			// Token: 0x06006862 RID: 26722 RVA: 0x00173E76 File Offset: 0x00172076
			internal RequestTimeoutEntry(HttpContext context)
			{
				this._context = context;
				this._count = 1;
			}

			// Token: 0x06006863 RID: 26723 RVA: 0x00173E8C File Offset: 0x0017208C
			internal void AddToList(DoubleLinkList list)
			{
				lock (list)
				{
					list.InsertTail(this);
					this._list = list;
				}
			}

			// Token: 0x06006864 RID: 26724 RVA: 0x00173ED0 File Offset: 0x001720D0
			internal void RemoveFromList()
			{
				if (this._list != null)
				{
					DoubleLinkList list = this._list;
					lock (list)
					{
						base.Remove();
						this._list = null;
					}
				}
			}

			// Token: 0x06006865 RID: 26725 RVA: 0x00173F20 File Offset: 0x00172120
			internal void TimeoutIfNeeded(DateTime now)
			{
				Thread thread = this._context.MustTimeout(now);
				if (thread != null)
				{
					this.RemoveFromList();
					thread.Abort(new HttpApplication.CancelModuleException(true));
				}
			}

			// Token: 0x06006866 RID: 26726 RVA: 0x00173F4F File Offset: 0x0017214F
			internal void IncrementCount()
			{
				Interlocked.Increment(ref this._count);
			}

			// Token: 0x06006867 RID: 26727 RVA: 0x00173F5D File Offset: 0x0017215D
			internal int DecrementCount()
			{
				return Interlocked.Decrement(ref this._count);
			}

			// Token: 0x04003657 RID: 13911
			private HttpContext _context;

			// Token: 0x04003658 RID: 13912
			private DoubleLinkList _list;

			// Token: 0x04003659 RID: 13913
			private int _count;
		}
	}
}
