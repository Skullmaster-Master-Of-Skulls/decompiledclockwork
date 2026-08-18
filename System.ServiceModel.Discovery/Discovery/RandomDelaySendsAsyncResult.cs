using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000045 RID: 69
	internal abstract class RandomDelaySendsAsyncResult : AsyncResult
	{
		// Token: 0x0600035A RID: 858 RVA: 0x000098D4 File Offset: 0x00007AD4
		protected RandomDelaySendsAsyncResult(int numSends, TimeSpan maxDelay, AsyncCallback callback, object state) : this(numSends, maxDelay, null, callback, state)
		{
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000098E2 File Offset: 0x00007AE2
		protected RandomDelaySendsAsyncResult(int numSends, TimeSpan maxDelay, ICommunicationObject channel, AsyncCallback callback, object state) : this(numSends, maxDelay, channel, null, callback, state)
		{
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000098F4 File Offset: 0x00007AF4
		protected RandomDelaySendsAsyncResult(int numSends, TimeSpan maxDelay, ICommunicationObject channel, Random random, AsyncCallback callback, object state) : base(callback, state)
		{
			this.onTimerCallback = new Action<object>(this.OnTimer);
			this.onSendCompletedCallback = Fx.ThunkCallback(new AsyncCallback(this.OnSendCompleted));
			this.channel = channel;
			if (this.channel != null)
			{
				this.onCloseCompletedCallback = Fx.ThunkCallback(new AsyncCallback(this.OnCloseCompleted));
			}
			this.numSends = numSends;
			this.maxDelay = maxDelay;
			this.completesCounter = 0L;
			this.sendCompletesCounter = 0L;
			this.cancelled = false;
			this.thisLock = new object();
			if (maxDelay != TimeSpan.Zero)
			{
				this.delaysInTicks = new long[numSends];
				Random randomGenerator = (random != null) ? random : new Random();
				for (int i = 0; i < this.numSends; i++)
				{
					this.delaysInTicks[i] = RandomDelaySendsAsyncResult.RandomDelay(randomGenerator, maxDelay.Ticks);
				}
				Array.Sort<long>(this.delaysInTicks);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000099E4 File Offset: 0x00007BE4
		public void Start(TimeSpan timeout)
		{
			if (this.cancelled)
			{
				return;
			}
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.timeoutHelper.RemainingTime();
			if (this.maxDelay == TimeSpan.Zero)
			{
				this.StartZeroDelay();
				return;
			}
			this.StartSchedule();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009A31 File Offset: 0x00007C31
		private void StartSchedule()
		{
			this.currentSendIndex = -1;
			this.timer = new IOThreadTimer(this.onTimerCallback, this, false);
			this.startTicks = Ticks.Now;
			this.Schedule(0);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009A60 File Offset: 0x00007C60
		private void StartZeroDelay()
		{
			this.currentSendIndex = 0;
			while (this.currentSendIndex < this.numSends)
			{
				IAsyncResult asyncResult = this.OnBeginSend(this.currentSendIndex, this.timeoutHelper.RemainingTime(), this.onSendCompletedCallback, null);
				if (asyncResult.CompletedSynchronously)
				{
					this.OnEndSend(asyncResult);
					if (Interlocked.Increment(ref this.sendCompletesCounter) == (long)this.numSends)
					{
						this.CompleteSends(true);
					}
				}
				this.currentSendIndex++;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009ADB File Offset: 0x00007CDB
		private void Schedule(int index)
		{
			if (index < this.numSends)
			{
				this.timer.SetAt(this.startTicks + this.delaysInTicks[index]);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009B00 File Offset: 0x00007D00
		private void StartSend(int index)
		{
			Exception ex = null;
			bool flag = false;
			try
			{
				IAsyncResult asyncResult = this.OnBeginSend(index, this.timeoutHelper.RemainingTime(), this.onSendCompletedCallback, null);
				if (asyncResult.CompletedSynchronously)
				{
					flag = true;
					this.OnEndSend(asyncResult);
				}
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null)
			{
				this.CallCompleteOnce(false, ex);
				return;
			}
			if (flag && Interlocked.Increment(ref this.sendCompletesCounter) == (long)this.numSends)
			{
				this.CompleteSends(false);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009B88 File Offset: 0x00007D88
		private void OnTimer(object state)
		{
			int num = Interlocked.Increment(ref this.currentSendIndex);
			this.StartSend(num);
			this.Schedule(num + 1);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00009BB4 File Offset: 0x00007DB4
		private void OnSendCompleted(IAsyncResult result)
		{
			Exception ex = null;
			if (!result.CompletedSynchronously)
			{
				try
				{
					this.OnEndSend(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallCompleteOnce(false, ex);
					return;
				}
				if (Interlocked.Increment(ref this.sendCompletesCounter) == (long)this.numSends)
				{
					this.CompleteSends(false);
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009C1C File Offset: 0x00007E1C
		private void CompleteSends(bool sendsCompletedSynchronously)
		{
			Exception ex = null;
			bool flag = false;
			if (this.channel != null && !base.IsCompleted)
			{
				try
				{
					IAsyncResult asyncResult = this.channel.BeginClose(this.timeoutHelper.RemainingTime(), this.onCloseCompletedCallback, null);
					if (asyncResult.CompletedSynchronously)
					{
						this.channel.EndClose(asyncResult);
						flag = true;
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallCompleteOnce(false, ex);
				}
				if (flag)
				{
					this.CallCompleteOnce(sendsCompletedSynchronously, null);
					return;
				}
			}
			else
			{
				this.CallCompleteOnce(sendsCompletedSynchronously, null);
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009CB4 File Offset: 0x00007EB4
		private void OnCloseCompleted(IAsyncResult result)
		{
			Exception ex = null;
			if (!result.CompletedSynchronously)
			{
				try
				{
					this.channel.EndClose(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallCompleteOnce(false, ex);
				}
				this.CallCompleteOnce(false, null);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009D0C File Offset: 0x00007F0C
		private void CallCompleteOnce(bool completedSynchronously, Exception e)
		{
			if (Interlocked.Increment(ref this.completesCounter) == 1L)
			{
				if (e != null)
				{
					this.Cancel();
				}
				base.Complete(completedSynchronously, e);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00009D2E File Offset: 0x00007F2E
		private void CompleteOnCancel()
		{
			if (Interlocked.Increment(ref this.completesCounter) == 1L)
			{
				base.Complete(false, new OperationCanceledException());
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009D4C File Offset: 0x00007F4C
		public void Cancel()
		{
			if (!this.cancelled)
			{
				bool flag = false;
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.cancelled)
					{
						flag = true;
						this.cancelled = true;
					}
				}
				if (flag)
				{
					if (this.timer != null)
					{
						this.timer.Cancel();
					}
					if (this.channel != null)
					{
						this.channel.Abort();
					}
					this.CompleteOnCancel();
				}
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public static long RandomDelay(Random randomGenerator, long maxTicks)
		{
			double num = (double)maxTicks;
			return (long)(num * randomGenerator.NextDouble());
		}

		// Token: 0x0600036A RID: 874
		protected abstract IAsyncResult OnBeginSend(int index, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600036B RID: 875
		protected abstract void OnEndSend(IAsyncResult result);

		// Token: 0x040000DA RID: 218
		private readonly ICommunicationObject channel;

		// Token: 0x040000DB RID: 219
		private IOThreadTimer timer;

		// Token: 0x040000DC RID: 220
		private TimeoutHelper timeoutHelper;

		// Token: 0x040000DD RID: 221
		private TimeSpan maxDelay;

		// Token: 0x040000DE RID: 222
		private long startTicks;

		// Token: 0x040000DF RID: 223
		private long[] delaysInTicks;

		// Token: 0x040000E0 RID: 224
		private int numSends;

		// Token: 0x040000E1 RID: 225
		private Action<object> onTimerCallback;

		// Token: 0x040000E2 RID: 226
		private AsyncCallback onSendCompletedCallback;

		// Token: 0x040000E3 RID: 227
		private AsyncCallback onCloseCompletedCallback;

		// Token: 0x040000E4 RID: 228
		private int currentSendIndex;

		// Token: 0x040000E5 RID: 229
		private long completesCounter;

		// Token: 0x040000E6 RID: 230
		private long sendCompletesCounter;

		// Token: 0x040000E7 RID: 231
		private bool cancelled;

		// Token: 0x040000E8 RID: 232
		private object thisLock;
	}
}
