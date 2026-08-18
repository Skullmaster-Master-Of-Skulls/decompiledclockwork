using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003A8 RID: 936
	[DefaultProperty("Interval")]
	[DefaultEvent("Tick")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionTimer")]
	public class Timer : Component
	{
		// Token: 0x06003CFA RID: 15610 RVA: 0x00109358 File Offset: 0x00107558
		public Timer()
		{
			this.interval = 100;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x00109373 File Offset: 0x00107573
		public Timer(IContainer container) : this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x00109390 File Offset: 0x00107590
		// (set) Token: 0x06003CFD RID: 15613 RVA: 0x00109398 File Offset: 0x00107598
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x140002E9 RID: 745
		// (add) Token: 0x06003CFE RID: 15614 RVA: 0x001093A1 File Offset: 0x001075A1
		// (remove) Token: 0x06003CFF RID: 15615 RVA: 0x001093BA File Offset: 0x001075BA
		[SRCategory("CatBehavior")]
		[SRDescription("TimerTimerDescr")]
		public event EventHandler Tick
		{
			add
			{
				this.onTimer = (EventHandler)Delegate.Combine(this.onTimer, value);
			}
			remove
			{
				this.onTimer = (EventHandler)Delegate.Remove(this.onTimer, value);
			}
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x001093D3 File Offset: 0x001075D3
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.timerWindow != null)
				{
					this.timerWindow.StopTimer();
				}
				this.Enabled = false;
			}
			this.timerWindow = null;
			base.Dispose(disposing);
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x00109400 File Offset: 0x00107600
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x0010941C File Offset: 0x0010761C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TimerEnabledDescr")]
		public virtual bool Enabled
		{
			get
			{
				if (this.timerWindow == null)
				{
					return this.enabled;
				}
				return this.timerWindow.IsTimerRunning;
			}
			set
			{
				object obj = this.syncObj;
				lock (obj)
				{
					if (this.enabled != value)
					{
						this.enabled = value;
						if (!base.DesignMode)
						{
							if (value)
							{
								if (this.timerWindow == null)
								{
									this.timerWindow = new Timer.TimerNativeWindow(this);
								}
								this.timerRoot = GCHandle.Alloc(this);
								this.timerWindow.StartTimer(this.interval);
							}
							else
							{
								if (this.timerWindow != null)
								{
									this.timerWindow.StopTimer();
								}
								if (this.timerRoot.IsAllocated)
								{
									this.timerRoot.Free();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x001094D0 File Offset: 0x001076D0
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x001094D8 File Offset: 0x001076D8
		[SRCategory("CatBehavior")]
		[DefaultValue(100)]
		[SRDescription("TimerIntervalDescr")]
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				object obj = this.syncObj;
				lock (obj)
				{
					if (value < 1)
					{
						throw new ArgumentOutOfRangeException("Interval", SR.GetString("TimerInvalidInterval", new object[]
						{
							value,
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.interval != value)
					{
						this.interval = value;
						if (this.Enabled && !base.DesignMode && this.timerWindow != null)
						{
							this.timerWindow.RestartTimer(value);
						}
					}
				}
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x00109580 File Offset: 0x00107780
		protected virtual void OnTick(EventArgs e)
		{
			if (this.onTimer != null)
			{
				this.onTimer(this, e);
			}
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x00109597 File Offset: 0x00107797
		public void Start()
		{
			this.Enabled = true;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x001095A0 File Offset: 0x001077A0
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x001095AC File Offset: 0x001077AC
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Interval: " + this.Interval.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x04002400 RID: 9216
		private int interval;

		// Token: 0x04002401 RID: 9217
		private bool enabled;

		// Token: 0x04002402 RID: 9218
		internal EventHandler onTimer;

		// Token: 0x04002403 RID: 9219
		private GCHandle timerRoot;

		// Token: 0x04002404 RID: 9220
		private Timer.TimerNativeWindow timerWindow;

		// Token: 0x04002405 RID: 9221
		private object userData;

		// Token: 0x04002406 RID: 9222
		private object syncObj = new object();

		// Token: 0x020007F4 RID: 2036
		private class TimerNativeWindow : NativeWindow
		{
			// Token: 0x06006E77 RID: 28279 RVA: 0x00195165 File Offset: 0x00193365
			internal TimerNativeWindow(Timer owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006E78 RID: 28280 RVA: 0x00195174 File Offset: 0x00193374
			~TimerNativeWindow()
			{
				this.StopTimer();
			}

			// Token: 0x1700181E RID: 6174
			// (get) Token: 0x06006E79 RID: 28281 RVA: 0x001951A0 File Offset: 0x001933A0
			public bool IsTimerRunning
			{
				get
				{
					return this._timerID != 0 && base.Handle != IntPtr.Zero;
				}
			}

			// Token: 0x06006E7A RID: 28282 RVA: 0x001951BC File Offset: 0x001933BC
			private bool EnsureHandle()
			{
				if (base.Handle == IntPtr.Zero)
				{
					CreateParams createParams = new CreateParams();
					createParams.Style = 0;
					createParams.ExStyle = 0;
					createParams.ClassStyle = 0;
					createParams.Caption = base.GetType().Name;
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						createParams.Parent = (IntPtr)NativeMethods.HWND_MESSAGE;
					}
					this.CreateHandle(createParams);
				}
				return base.Handle != IntPtr.Zero;
			}

			// Token: 0x06006E7B RID: 28283 RVA: 0x0019523C File Offset: 0x0019343C
			private bool GetInvokeRequired(IntPtr hWnd)
			{
				if (hWnd != IntPtr.Zero)
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, hWnd), out num);
					int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
					return windowThreadProcessId != currentThreadId;
				}
				return false;
			}

			// Token: 0x06006E7C RID: 28284 RVA: 0x00195274 File Offset: 0x00193474
			public void RestartTimer(int newInterval)
			{
				this.StopTimer(false, IntPtr.Zero);
				this.StartTimer(newInterval);
			}

			// Token: 0x06006E7D RID: 28285 RVA: 0x0019528C File Offset: 0x0019348C
			public void StartTimer(int interval)
			{
				if (this._timerID == 0 && !this._stoppingTimer && this.EnsureHandle())
				{
					this._timerID = (int)SafeNativeMethods.SetTimer(new HandleRef(this, base.Handle), Timer.TimerNativeWindow.TimerID++, interval, IntPtr.Zero);
				}
			}

			// Token: 0x06006E7E RID: 28286 RVA: 0x001952E0 File Offset: 0x001934E0
			public void StopTimer()
			{
				this.StopTimer(true, IntPtr.Zero);
			}

			// Token: 0x06006E7F RID: 28287 RVA: 0x001952F0 File Offset: 0x001934F0
			public void StopTimer(bool destroyHwnd, IntPtr hWnd)
			{
				if (hWnd == IntPtr.Zero)
				{
					hWnd = base.Handle;
				}
				if (this.GetInvokeRequired(hWnd))
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, hWnd), 16, 0, 0);
					return;
				}
				lock (this)
				{
					if (!this._stoppingTimer && !(hWnd == IntPtr.Zero) && UnsafeNativeMethods.IsWindow(new HandleRef(this, hWnd)))
					{
						if (this._timerID != 0)
						{
							try
							{
								this._stoppingTimer = true;
								SafeNativeMethods.KillTimer(new HandleRef(this, hWnd), this._timerID);
							}
							finally
							{
								this._timerID = 0;
								this._stoppingTimer = false;
							}
						}
						if (destroyHwnd)
						{
							base.DestroyHandle();
						}
					}
				}
			}

			// Token: 0x06006E80 RID: 28288 RVA: 0x001953C4 File Offset: 0x001935C4
			public override void DestroyHandle()
			{
				this.StopTimer(false, IntPtr.Zero);
				base.DestroyHandle();
			}

			// Token: 0x06006E81 RID: 28289 RVA: 0x0003BADD File Offset: 0x00039CDD
			protected override void OnThreadException(Exception e)
			{
				Application.OnThreadException(e);
			}

			// Token: 0x06006E82 RID: 28290 RVA: 0x001953D8 File Offset: 0x001935D8
			public override void ReleaseHandle()
			{
				this.StopTimer(false, IntPtr.Zero);
				base.ReleaseHandle();
			}

			// Token: 0x06006E83 RID: 28291 RVA: 0x001953EC File Offset: 0x001935EC
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 275)
				{
					if ((int)((long)m.WParam) == this._timerID)
					{
						this._owner.OnTick(EventArgs.Empty);
						return;
					}
				}
				else if (m.Msg == 16)
				{
					this.StopTimer(true, m.HWnd);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x040042E4 RID: 17124
			private Timer _owner;

			// Token: 0x040042E5 RID: 17125
			private int _timerID;

			// Token: 0x040042E6 RID: 17126
			private static int TimerID = 1;

			// Token: 0x040042E7 RID: 17127
			private bool _stoppingTimer;
		}
	}
}
