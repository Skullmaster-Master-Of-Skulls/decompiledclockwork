using System;
using System.Collections;
using System.Threading;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x020000EF RID: 239
	internal class RequestQueue
	{
		// Token: 0x06000E68 RID: 3688 RVA: 0x00028FFC File Offset: 0x000271FC
		private static bool IsLocal(HttpWorkerRequest wr)
		{
			string remoteAddress = wr.GetRemoteAddress();
			return remoteAddress == "127.0.0.1" || remoteAddress == "::1" || (!string.IsNullOrEmpty(remoteAddress) && remoteAddress == wr.GetLocalAddress());
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00029048 File Offset: 0x00027248
		private void QueueRequest(HttpWorkerRequest wr, bool isLocal)
		{
			lock (this)
			{
				if (isLocal)
				{
					this._localQueue.Enqueue(wr);
				}
				else
				{
					this._externQueue.Enqueue(wr);
				}
				this._count++;
			}
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_QUEUED);
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_IN_APPLICATION_QUEUE);
			if (EtwTrace.IsTraceEnabled(4, 1))
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_REQ_QUEUED, wr);
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000290C8 File Offset: 0x000272C8
		private HttpWorkerRequest DequeueRequest(bool localOnly)
		{
			HttpWorkerRequest httpWorkerRequest = null;
			while (this._count > 0)
			{
				lock (this)
				{
					if (this._localQueue.Count > 0)
					{
						httpWorkerRequest = (HttpWorkerRequest)this._localQueue.Dequeue();
						this._count--;
					}
					else if (!localOnly && this._externQueue.Count > 0)
					{
						httpWorkerRequest = (HttpWorkerRequest)this._externQueue.Dequeue();
						this._count--;
					}
				}
				if (httpWorkerRequest == null)
				{
					break;
				}
				PerfCounters.DecrementGlobalCounter(GlobalPerfCounter.REQUESTS_QUEUED);
				PerfCounters.DecrementCounter(AppPerfCounter.REQUESTS_IN_APPLICATION_QUEUE);
				if (EtwTrace.IsTraceEnabled(4, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_REQ_DEQUEUED, httpWorkerRequest);
				}
				if (this.CheckClientConnected(httpWorkerRequest))
				{
					break;
				}
				HttpRuntime.RejectRequestNow(httpWorkerRequest, true);
				httpWorkerRequest = null;
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_DISCONNECTED);
				PerfCounters.IncrementCounter(AppPerfCounter.APP_REQUEST_DISCONNECTED);
			}
			return httpWorkerRequest;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000291B0 File Offset: 0x000273B0
		private bool CheckClientConnected(HttpWorkerRequest wr)
		{
			return !(DateTime.UtcNow - wr.GetStartTime() > this._clientConnectedTime) || wr.IsClientConnected();
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000291D8 File Offset: 0x000273D8
		internal RequestQueue(int minExternFreeThreads, int minLocalFreeThreads, int queueLimit, TimeSpan clientConnectedTime)
		{
			this._minExternFreeThreads = minExternFreeThreads;
			this._minLocalFreeThreads = minLocalFreeThreads;
			this._queueLimit = queueLimit;
			this._clientConnectedTime = clientConnectedTime;
			this._workItemCallback = new WaitCallback(this.WorkItemCallback);
			this._timer = new Timer(new TimerCallback(this.TimerCompletionCallback), null, this._timerPeriod, this._timerPeriod);
			this._iis6 = HostingEnvironment.IsUnderIIS6Process;
			int num;
			int num2;
			ThreadPool.GetMaxThreads(out num, out num2);
			UnsafeNativeMethods.SetMinRequestsExecutingToDetectDeadlock(num - minExternFreeThreads);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00029280 File Offset: 0x00027480
		internal HttpWorkerRequest GetRequestToExecute(HttpWorkerRequest wr)
		{
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			int num3;
			if (this._iis6)
			{
				num3 = num;
			}
			else
			{
				num3 = ((num2 > num) ? num : num2);
			}
			if (num3 >= this._minExternFreeThreads && this._count == 0)
			{
				return wr;
			}
			bool flag = RequestQueue.IsLocal(wr);
			if (flag && num3 >= this._minLocalFreeThreads && this._count == 0)
			{
				return wr;
			}
			if (this._count >= this._queueLimit)
			{
				HttpRuntime.RejectRequestNow(wr, false);
				return null;
			}
			this.QueueRequest(wr, flag);
			if (num3 >= this._minExternFreeThreads)
			{
				wr = this.DequeueRequest(false);
			}
			else if (num3 >= this._minLocalFreeThreads)
			{
				wr = this.DequeueRequest(true);
			}
			else
			{
				wr = null;
				this.ScheduleMoreWorkIfNeeded();
			}
			return wr;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002932C File Offset: 0x0002752C
		internal void ScheduleMoreWorkIfNeeded()
		{
			if (this._draining)
			{
				return;
			}
			if (this._count == 0)
			{
				return;
			}
			if (this._workItemCount >= 2)
			{
				return;
			}
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			if (num < this._minLocalFreeThreads)
			{
				return;
			}
			Interlocked.Increment(ref this._workItemCount);
			ThreadPool.QueueUserWorkItem(this._workItemCallback);
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00029380 File Offset: 0x00027580
		internal bool IsEmpty
		{
			get
			{
				return this._count == 0;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002938C File Offset: 0x0002758C
		private void WorkItemCallback(object state)
		{
			Interlocked.Decrement(ref this._workItemCount);
			if (this._draining)
			{
				return;
			}
			if (this._count == 0)
			{
				return;
			}
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			if (num < this._minLocalFreeThreads)
			{
				return;
			}
			HttpWorkerRequest httpWorkerRequest = this.DequeueRequest(num < this._minExternFreeThreads);
			if (httpWorkerRequest == null)
			{
				return;
			}
			this.ScheduleMoreWorkIfNeeded();
			HttpRuntime.ProcessRequestNow(httpWorkerRequest);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000293EA File Offset: 0x000275EA
		private void TimerCompletionCallback(object state)
		{
			this.ScheduleMoreWorkIfNeeded();
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000293F4 File Offset: 0x000275F4
		internal void Drain()
		{
			this._draining = true;
			if (this._timer != null)
			{
				((IDisposable)this._timer).Dispose();
				this._timer = null;
			}
			while (this._workItemCount > 0)
			{
				Thread.Sleep(100);
			}
			if (this._count == 0)
			{
				return;
			}
			for (;;)
			{
				HttpWorkerRequest httpWorkerRequest = this.DequeueRequest(false);
				if (httpWorkerRequest == null)
				{
					break;
				}
				HttpRuntime.RejectRequestNow(httpWorkerRequest, false);
			}
		}

		// Token: 0x0400058D RID: 1421
		private int _minExternFreeThreads;

		// Token: 0x0400058E RID: 1422
		private int _minLocalFreeThreads;

		// Token: 0x0400058F RID: 1423
		private int _queueLimit;

		// Token: 0x04000590 RID: 1424
		private TimeSpan _clientConnectedTime;

		// Token: 0x04000591 RID: 1425
		private bool _iis6;

		// Token: 0x04000592 RID: 1426
		private Queue _localQueue = new Queue();

		// Token: 0x04000593 RID: 1427
		private Queue _externQueue = new Queue();

		// Token: 0x04000594 RID: 1428
		private int _count;

		// Token: 0x04000595 RID: 1429
		private WaitCallback _workItemCallback;

		// Token: 0x04000596 RID: 1430
		private int _workItemCount;

		// Token: 0x04000597 RID: 1431
		private const int _workItemLimit = 2;

		// Token: 0x04000598 RID: 1432
		private bool _draining;

		// Token: 0x04000599 RID: 1433
		private readonly TimeSpan _timerPeriod = new TimeSpan(0, 0, 10);

		// Token: 0x0400059A RID: 1434
		private Timer _timer;
	}
}
