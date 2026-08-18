using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x020000CB RID: 203
	internal class IdleTimeoutMonitor
	{
		// Token: 0x06000DD8 RID: 3544 RVA: 0x000271A4 File Offset: 0x000253A4
		internal IdleTimeoutMonitor(TimeSpan timeout)
		{
			this._idleTimeout = timeout;
			this._timer = new Timer(new TimerCallback(this.TimerCompletionCallback), null, this._timerPeriod, this._timerPeriod);
			this._lastEvent = DateTime.UtcNow;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000271FC File Offset: 0x000253FC
		internal void Stop()
		{
			if (this._timer != null)
			{
				lock (this)
				{
					if (this._timer != null)
					{
						((IDisposable)this._timer).Dispose();
						this._timer = null;
					}
				}
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00027254 File Offset: 0x00025454
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00027294 File Offset: 0x00025494
		internal DateTime LastEvent
		{
			get
			{
				DateTime lastEvent;
				lock (this)
				{
					lastEvent = this._lastEvent;
				}
				return lastEvent;
			}
			set
			{
				lock (this)
				{
					this._lastEvent = value;
				}
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000272D0 File Offset: 0x000254D0
		private void TimerCompletionCallback(object state)
		{
			HttpApplicationFactory.TrimApplicationInstances(false);
			if (this._idleTimeout == TimeSpan.MaxValue)
			{
				return;
			}
			if (HostingEnvironment.ShutdownInitiated)
			{
				return;
			}
			if (HostingEnvironment.BusyCount != 0)
			{
				return;
			}
			if (DateTime.UtcNow <= this.LastEvent.Add(this._idleTimeout))
			{
				return;
			}
			if (Debugger.IsAttached)
			{
				return;
			}
			HttpRuntime.SetShutdownReason(ApplicationShutdownReason.IdleTimeout, SR.GetString("Hosting_Env_IdleTimeout"));
			HostingEnvironment.InitiateShutdownWithoutDemand();
		}

		// Token: 0x0400051F RID: 1311
		private TimeSpan _idleTimeout;

		// Token: 0x04000520 RID: 1312
		private DateTime _lastEvent;

		// Token: 0x04000521 RID: 1313
		private Timer _timer;

		// Token: 0x04000522 RID: 1314
		private readonly TimeSpan _timerPeriod = new TimeSpan(0, 0, 30);
	}
}
