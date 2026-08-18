using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000354 RID: 852
	public class Screen
	{
		// Token: 0x0600379A RID: 14234 RVA: 0x000F7CDF File Offset: 0x000F5EDF
		internal Screen(IntPtr monitor) : this(monitor, IntPtr.Zero)
		{
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000F7CF0 File Offset: 0x000F5EF0
		internal Screen(IntPtr monitor, IntPtr hdc)
		{
			IntPtr intPtr = hdc;
			if (!Screen.multiMonitorSupport || monitor == (IntPtr)(-1163005939))
			{
				this.bounds = SystemInformation.VirtualScreen;
				this.primary = true;
				this.deviceName = "DISPLAY";
			}
			else
			{
				NativeMethods.MONITORINFOEX monitorinfoex = new NativeMethods.MONITORINFOEX();
				SafeNativeMethods.GetMonitorInfo(new HandleRef(null, monitor), monitorinfoex);
				this.bounds = Rectangle.FromLTRB(monitorinfoex.rcMonitor.left, monitorinfoex.rcMonitor.top, monitorinfoex.rcMonitor.right, monitorinfoex.rcMonitor.bottom);
				this.primary = ((monitorinfoex.dwFlags & 1) != 0);
				this.deviceName = new string(monitorinfoex.szDevice);
				this.deviceName = this.deviceName.TrimEnd(new char[1]);
				if (hdc == IntPtr.Zero)
				{
					intPtr = UnsafeNativeMethods.CreateDC(this.deviceName);
				}
			}
			this.hmonitor = monitor;
			this.bitDepth = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, intPtr), 12);
			this.bitDepth *= UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, intPtr), 14);
			if (hdc != intPtr)
			{
				UnsafeNativeMethods.DeleteDC(new HandleRef(null, intPtr));
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x000F7E3C File Offset: 0x000F603C
		public static Screen[] AllScreens
		{
			get
			{
				if (Screen.screens == null)
				{
					if (Screen.multiMonitorSupport)
					{
						Screen.MonitorEnumCallback monitorEnumCallback = new Screen.MonitorEnumCallback();
						NativeMethods.MonitorEnumProc lpfnEnum = new NativeMethods.MonitorEnumProc(monitorEnumCallback.Callback);
						SafeNativeMethods.EnumDisplayMonitors(NativeMethods.NullHandleRef, null, lpfnEnum, IntPtr.Zero);
						if (monitorEnumCallback.screens.Count > 0)
						{
							Screen[] array = new Screen[monitorEnumCallback.screens.Count];
							monitorEnumCallback.screens.CopyTo(array, 0);
							Screen.screens = array;
						}
						else
						{
							Screen.screens = new Screen[]
							{
								new Screen((IntPtr)(-1163005939))
							};
						}
					}
					else
					{
						Screen.screens = new Screen[]
						{
							Screen.PrimaryScreen
						};
					}
					SystemEvents.DisplaySettingsChanging += Screen.OnDisplaySettingsChanging;
				}
				return Screen.screens;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000F7EFC File Offset: 0x000F60FC
		public int BitsPerPixel
		{
			get
			{
				return this.bitDepth;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000F7F04 File Offset: 0x000F6104
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000F7F0C File Offset: 0x000F610C
		public string DeviceName
		{
			get
			{
				return this.deviceName;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000F7F14 File Offset: 0x000F6114
		public bool Primary
		{
			get
			{
				return this.primary;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000F7F1C File Offset: 0x000F611C
		public static Screen PrimaryScreen
		{
			get
			{
				if (Screen.multiMonitorSupport)
				{
					Screen[] allScreens = Screen.AllScreens;
					for (int i = 0; i < allScreens.Length; i++)
					{
						if (allScreens[i].primary)
						{
							return allScreens[i];
						}
					}
					return null;
				}
				return new Screen((IntPtr)(-1163005939), IntPtr.Zero);
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000F7F68 File Offset: 0x000F6168
		public Rectangle WorkingArea
		{
			get
			{
				if (this.currentDesktopChangedCount != Screen.DesktopChangedCount)
				{
					Interlocked.Exchange(ref this.currentDesktopChangedCount, Screen.DesktopChangedCount);
					if (!Screen.multiMonitorSupport || this.hmonitor == (IntPtr)(-1163005939))
					{
						this.workingArea = SystemInformation.WorkingArea;
					}
					else
					{
						NativeMethods.MONITORINFOEX monitorinfoex = new NativeMethods.MONITORINFOEX();
						SafeNativeMethods.GetMonitorInfo(new HandleRef(null, this.hmonitor), monitorinfoex);
						this.workingArea = Rectangle.FromLTRB(monitorinfoex.rcWork.left, monitorinfoex.rcWork.top, monitorinfoex.rcWork.right, monitorinfoex.rcWork.bottom);
					}
				}
				return this.workingArea;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000F8018 File Offset: 0x000F6218
		private static int DesktopChangedCount
		{
			get
			{
				if (Screen.desktopChangedCount == -1)
				{
					object obj = Screen.syncLock;
					lock (obj)
					{
						if (Screen.desktopChangedCount == -1)
						{
							SystemEvents.UserPreferenceChanged += Screen.OnUserPreferenceChanged;
							Screen.desktopChangedCount = 0;
						}
					}
				}
				return Screen.desktopChangedCount;
			}
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000F8080 File Offset: 0x000F6280
		public override bool Equals(object obj)
		{
			if (obj is Screen)
			{
				Screen screen = (Screen)obj;
				if (this.hmonitor == screen.hmonitor)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000F80B4 File Offset: 0x000F62B4
		public static Screen FromPoint(Point point)
		{
			if (Screen.multiMonitorSupport)
			{
				NativeMethods.POINTSTRUCT pt = new NativeMethods.POINTSTRUCT(point.X, point.Y);
				return new Screen(SafeNativeMethods.MonitorFromPoint(pt, 2));
			}
			return new Screen((IntPtr)(-1163005939));
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000F80FC File Offset: 0x000F62FC
		public static Screen FromRectangle(Rectangle rect)
		{
			if (Screen.multiMonitorSupport)
			{
				NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(rect.X, rect.Y, rect.Width, rect.Height);
				return new Screen(SafeNativeMethods.MonitorFromRect(ref rect2, 2));
			}
			return new Screen((IntPtr)(-1163005939), IntPtr.Zero);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000F8154 File Offset: 0x000F6354
		public static Screen FromControl(Control control)
		{
			return Screen.FromHandleInternal(control.Handle);
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000F8161 File Offset: 0x000F6361
		public static Screen FromHandle(IntPtr hwnd)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			return Screen.FromHandleInternal(hwnd);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000F8173 File Offset: 0x000F6373
		internal static Screen FromHandleInternal(IntPtr hwnd)
		{
			if (Screen.multiMonitorSupport)
			{
				return new Screen(SafeNativeMethods.MonitorFromWindow(new HandleRef(null, hwnd), 2));
			}
			return new Screen((IntPtr)(-1163005939), IntPtr.Zero);
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000F81A3 File Offset: 0x000F63A3
		public static Rectangle GetWorkingArea(Point pt)
		{
			return Screen.FromPoint(pt).WorkingArea;
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000F81B0 File Offset: 0x000F63B0
		public static Rectangle GetWorkingArea(Rectangle rect)
		{
			return Screen.FromRectangle(rect).WorkingArea;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000F81BD File Offset: 0x000F63BD
		public static Rectangle GetWorkingArea(Control ctl)
		{
			return Screen.FromControl(ctl).WorkingArea;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000F81CA File Offset: 0x000F63CA
		public static Rectangle GetBounds(Point pt)
		{
			return Screen.FromPoint(pt).Bounds;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000F81D7 File Offset: 0x000F63D7
		public static Rectangle GetBounds(Rectangle rect)
		{
			return Screen.FromRectangle(rect).Bounds;
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000F81E4 File Offset: 0x000F63E4
		public static Rectangle GetBounds(Control ctl)
		{
			return Screen.FromControl(ctl).Bounds;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000F81F1 File Offset: 0x000F63F1
		public override int GetHashCode()
		{
			return (int)this.hmonitor;
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000F81FE File Offset: 0x000F63FE
		private static void OnDisplaySettingsChanging(object sender, EventArgs e)
		{
			SystemEvents.DisplaySettingsChanging -= Screen.OnDisplaySettingsChanging;
			Screen.screens = null;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000F8217 File Offset: 0x000F6417
		private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Desktop)
			{
				Interlocked.Increment(ref Screen.desktopChangedCount);
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000F8230 File Offset: 0x000F6430
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				"[Bounds=",
				this.bounds.ToString(),
				" WorkingArea=",
				this.WorkingArea.ToString(),
				" Primary=",
				this.primary.ToString(),
				" DeviceName=",
				this.deviceName
			});
		}

		// Token: 0x04002161 RID: 8545
		private readonly IntPtr hmonitor;

		// Token: 0x04002162 RID: 8546
		private readonly Rectangle bounds;

		// Token: 0x04002163 RID: 8547
		private Rectangle workingArea = Rectangle.Empty;

		// Token: 0x04002164 RID: 8548
		private readonly bool primary;

		// Token: 0x04002165 RID: 8549
		private readonly string deviceName;

		// Token: 0x04002166 RID: 8550
		private readonly int bitDepth;

		// Token: 0x04002167 RID: 8551
		private static object syncLock = new object();

		// Token: 0x04002168 RID: 8552
		private static int desktopChangedCount = -1;

		// Token: 0x04002169 RID: 8553
		private int currentDesktopChangedCount = -1;

		// Token: 0x0400216A RID: 8554
		private const int PRIMARY_MONITOR = -1163005939;

		// Token: 0x0400216B RID: 8555
		private const int MONITOR_DEFAULTTONULL = 0;

		// Token: 0x0400216C RID: 8556
		private const int MONITOR_DEFAULTTOPRIMARY = 1;

		// Token: 0x0400216D RID: 8557
		private const int MONITOR_DEFAULTTONEAREST = 2;

		// Token: 0x0400216E RID: 8558
		private const int MONITORINFOF_PRIMARY = 1;

		// Token: 0x0400216F RID: 8559
		private static bool multiMonitorSupport = UnsafeNativeMethods.GetSystemMetrics(80) != 0;

		// Token: 0x04002170 RID: 8560
		private static Screen[] screens;

		// Token: 0x020007DE RID: 2014
		private class MonitorEnumCallback
		{
			// Token: 0x06006DD4 RID: 28116 RVA: 0x00193171 File Offset: 0x00191371
			public virtual bool Callback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lparam)
			{
				this.screens.Add(new Screen(monitor, hdc));
				return true;
			}

			// Token: 0x040042BA RID: 17082
			public ArrayList screens = new ArrayList();
		}
	}
}
