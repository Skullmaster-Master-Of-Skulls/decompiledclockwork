using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000935 RID: 2357
	internal class InterruptibleTimer
	{
		// Token: 0x06005A8D RID: 23181 RVA: 0x0014CA20 File Offset: 0x0014AC20
		public InterruptibleTimer(TimeSpan defaultInterval, WaitCallback callback, object state)
		{
			if (callback == null)
			{
				throw Fx.AssertAndThrow("Argument callback cannot be null.");
			}
			this.defaultInterval = defaultInterval;
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06005A8E RID: 23182 RVA: 0x0014CA56 File Offset: 0x0014AC56
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x0014CA60 File Offset: 0x0014AC60
		public void Abort()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.aborted = true;
				if (this.set)
				{
					this.timer.Cancel();
					this.set = false;
				}
			}
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x0014CABC File Offset: 0x0014ACBC
		public bool Cancel()
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.aborted)
				{
					result = false;
				}
				else if (this.set)
				{
					this.timer.Cancel();
					this.set = false;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x0014CB24 File Offset: 0x0014AD24
		private void OnTimerElapsed()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.aborted)
				{
					return;
				}
				this.set = false;
			}
			this.callback(this.state);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0014CB80 File Offset: 0x0014AD80
		private static void OnTimerElapsed(object state)
		{
			InterruptibleTimer interruptibleTimer = (InterruptibleTimer)state;
			interruptibleTimer.OnTimerElapsed();
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x0014CB9A File Offset: 0x0014AD9A
		public void Set()
		{
			this.Set(this.defaultInterval);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x0014CBA8 File Offset: 0x0014ADA8
		public void Set(TimeSpan interval)
		{
			this.InternalSet(interval, false);
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0014CBB2 File Offset: 0x0014ADB2
		public void SetIfNotSet()
		{
			this.InternalSet(this.defaultInterval, true);
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x0014CBC4 File Offset: 0x0014ADC4
		private void InternalSet(TimeSpan interval, bool ifNotSet)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!this.aborted && (!ifNotSet || !this.set))
				{
					if (this.timer == null)
					{
						this.timer = new IOThreadTimer(InterruptibleTimer.onTimerElapsed, this, true);
					}
					this.timer.Set(interval);
					this.set = true;
				}
			}
		}

		// Token: 0x040036A0 RID: 13984
		private WaitCallback callback;

		// Token: 0x040036A1 RID: 13985
		private bool aborted;

		// Token: 0x040036A2 RID: 13986
		private TimeSpan defaultInterval;

		// Token: 0x040036A3 RID: 13987
		private static Action<object> onTimerElapsed = new Action<object>(InterruptibleTimer.OnTimerElapsed);

		// Token: 0x040036A4 RID: 13988
		private bool set;

		// Token: 0x040036A5 RID: 13989
		private object state;

		// Token: 0x040036A6 RID: 13990
		private object thisLock = new object();

		// Token: 0x040036A7 RID: 13991
		private IOThreadTimer timer;
	}
}
