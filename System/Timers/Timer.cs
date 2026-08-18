using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Timers
{
	// Token: 0x02000737 RID: 1847
	[DefaultProperty("Interval")]
	[DefaultEvent("Elapsed")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Timer : Component, ISupportInitialize
	{
		// Token: 0x0600385A RID: 14426 RVA: 0x000EDB24 File Offset: 0x000ECB24
		public Timer()
		{
			this.interval = 100.0;
			this.enabled = false;
			this.autoReset = true;
			this.initializing = false;
			this.delayedEnable = false;
			this.callback = new TimerCallback(this.MyTimerCallback);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000EDB74 File Offset: 0x000ECB74
		public Timer(double interval) : this()
		{
			if (interval <= 0.0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"interval",
					interval
				}));
			}
			int num = (int)Math.Ceiling(interval);
			if (num < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"interval",
					interval
				}));
			}
			this.interval = interval;
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000EDBF8 File Offset: 0x000ECBF8
		// (set) Token: 0x0600385D RID: 14429 RVA: 0x000EDC00 File Offset: 0x000ECC00
		[TimersDescription("TimerAutoReset")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AutoReset
		{
			get
			{
				return this.autoReset;
			}
			set
			{
				if (base.DesignMode)
				{
					this.autoReset = value;
					return;
				}
				if (this.autoReset != value)
				{
					this.autoReset = value;
					if (this.timer != null)
					{
						this.UpdateTimer();
					}
				}
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000EDC30 File Offset: 0x000ECC30
		// (set) Token: 0x0600385F RID: 14431 RVA: 0x000EDC38 File Offset: 0x000ECC38
		[TimersDescription("TimerEnabled")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (base.DesignMode)
				{
					this.delayedEnable = value;
					this.enabled = value;
					return;
				}
				if (this.initializing)
				{
					this.delayedEnable = value;
					return;
				}
				if (this.enabled != value)
				{
					if (!value)
					{
						if (this.timer != null)
						{
							this.cookie = null;
							this.timer.Dispose();
							this.timer = null;
						}
						this.enabled = value;
						return;
					}
					this.enabled = value;
					if (this.timer == null)
					{
						if (this.disposed)
						{
							throw new ObjectDisposedException(base.GetType().Name);
						}
						int num = (int)Math.Ceiling(this.interval);
						this.cookie = new object();
						this.timer = new Timer(this.callback, this.cookie, num, this.autoReset ? num : -1);
						return;
					}
					else
					{
						this.UpdateTimer();
					}
				}
			}
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000EDD10 File Offset: 0x000ECD10
		private void UpdateTimer()
		{
			int num = (int)Math.Ceiling(this.interval);
			this.timer.Change(num, this.autoReset ? num : -1);
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000EDD43 File Offset: 0x000ECD43
		// (set) Token: 0x06003862 RID: 14434 RVA: 0x000EDD4C File Offset: 0x000ECD4C
		[RecommendedAsConfigurable(true)]
		[DefaultValue(100.0)]
		[Category("Behavior")]
		[TimersDescription("TimerInterval")]
		public double Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException(SR.GetString("TimerInvalidInterval", new object[]
					{
						value,
						0
					}));
				}
				this.interval = value;
				if (this.timer != null)
				{
					this.UpdateTimer();
				}
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06003863 RID: 14435 RVA: 0x000EDDA4 File Offset: 0x000ECDA4
		// (remove) Token: 0x06003864 RID: 14436 RVA: 0x000EDDBD File Offset: 0x000ECDBD
		[TimersDescription("TimerIntervalElapsed")]
		[Category("Behavior")]
		public event ElapsedEventHandler Elapsed
		{
			add
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Combine(this.onIntervalElapsed, value);
			}
			remove
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Remove(this.onIntervalElapsed, value);
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000EDDEE File Offset: 0x000ECDEE
		// (set) Token: 0x06003865 RID: 14437 RVA: 0x000EDDD6 File Offset: 0x000ECDD6
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (base.DesignMode)
				{
					this.enabled = true;
				}
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000EDDF8 File Offset: 0x000ECDF8
		// (set) Token: 0x06003868 RID: 14440 RVA: 0x000EDE52 File Offset: 0x000ECE52
		[Browsable(false)]
		[TimersDescription("TimerSynchronizingObject")]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000EDE5B File Offset: 0x000ECE5B
		public void BeginInit()
		{
			this.Close();
			this.initializing = true;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000EDE6A File Offset: 0x000ECE6A
		public void Close()
		{
			this.initializing = false;
			this.delayedEnable = false;
			this.enabled = false;
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000EDE9B File Offset: 0x000ECE9B
		protected override void Dispose(bool disposing)
		{
			this.Close();
			this.disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000EDEB1 File Offset: 0x000ECEB1
		public void EndInit()
		{
			this.initializing = false;
			this.Enabled = this.delayedEnable;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000EDEC6 File Offset: 0x000ECEC6
		public void Start()
		{
			this.Enabled = true;
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000EDECF File Offset: 0x000ECECF
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000EDED8 File Offset: 0x000ECED8
		private void MyTimerCallback(object state)
		{
			if (state != this.cookie)
			{
				return;
			}
			if (!this.autoReset)
			{
				this.enabled = false;
			}
			Timer.FILE_TIME file_TIME = default(Timer.FILE_TIME);
			Timer.GetSystemTimeAsFileTime(ref file_TIME);
			ElapsedEventArgs elapsedEventArgs = new ElapsedEventArgs(file_TIME.ftTimeLow, file_TIME.ftTimeHigh);
			try
			{
				ElapsedEventHandler elapsedEventHandler = this.onIntervalElapsed;
				if (elapsedEventHandler != null)
				{
					if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
					{
						this.SynchronizingObject.BeginInvoke(elapsedEventHandler, new object[]
						{
							this,
							elapsedEventArgs
						});
					}
					else
					{
						elapsedEventHandler(this, elapsedEventArgs);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06003870 RID: 14448
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		internal static extern void GetSystemTimeAsFileTime(ref Timer.FILE_TIME lpSystemTimeAsFileTime);

		// Token: 0x04003243 RID: 12867
		private double interval;

		// Token: 0x04003244 RID: 12868
		private bool enabled;

		// Token: 0x04003245 RID: 12869
		private bool initializing;

		// Token: 0x04003246 RID: 12870
		private bool delayedEnable;

		// Token: 0x04003247 RID: 12871
		private ElapsedEventHandler onIntervalElapsed;

		// Token: 0x04003248 RID: 12872
		private bool autoReset;

		// Token: 0x04003249 RID: 12873
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400324A RID: 12874
		private bool disposed;

		// Token: 0x0400324B RID: 12875
		private Timer timer;

		// Token: 0x0400324C RID: 12876
		private TimerCallback callback;

		// Token: 0x0400324D RID: 12877
		private object cookie;

		// Token: 0x02000738 RID: 1848
		internal struct FILE_TIME
		{
			// Token: 0x0400324E RID: 12878
			internal int ftTimeLow;

			// Token: 0x0400324F RID: 12879
			internal int ftTimeHigh;
		}
	}
}
