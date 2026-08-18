using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000383 RID: 899
	public class SystemInformation
	{
		// Token: 0x06003AA2 RID: 15010 RVA: 0x00002843 File Offset: 0x00000A43
		private SystemInformation()
		{
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x00102778 File Offset: 0x00100978
		public static bool DragFullWindows
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(38, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x00102798 File Offset: 0x00100998
		public static bool HighContrast
		{
			get
			{
				SystemInformation.EnsureSystemEvents();
				if (SystemInformation.systemEventsDirty)
				{
					NativeMethods.HIGHCONTRAST_I highcontrast_I = default(NativeMethods.HIGHCONTRAST_I);
					highcontrast_I.cbSize = Marshal.SizeOf(highcontrast_I);
					highcontrast_I.dwFlags = 0;
					highcontrast_I.lpszDefaultScheme = IntPtr.Zero;
					bool flag = UnsafeNativeMethods.SystemParametersInfo(66, highcontrast_I.cbSize, ref highcontrast_I, 0);
					if (flag)
					{
						SystemInformation.highContrast = ((highcontrast_I.dwFlags & 1) != 0);
					}
					else
					{
						SystemInformation.highContrast = false;
					}
					SystemInformation.systemEventsDirty = false;
				}
				return SystemInformation.highContrast;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x00102818 File Offset: 0x00100A18
		public static int MouseWheelScrollLines
		{
			get
			{
				if (SystemInformation.NativeMouseWheelSupport)
				{
					int result = 0;
					UnsafeNativeMethods.SystemParametersInfo(104, 0, ref result, 0);
					return result;
				}
				IntPtr intPtr = IntPtr.Zero;
				intPtr = UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
				if (intPtr != IntPtr.Zero)
				{
					int msg = SafeNativeMethods.RegisterWindowMessage("MSH_SCROLL_LINES_MSG");
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(null, intPtr), msg, 0, 0);
					if (num != 0)
					{
						return num;
					}
				}
				return 3;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x00102885 File Offset: 0x00100A85
		public static Size PrimaryMonitorSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(0), UnsafeNativeMethods.GetSystemMetrics(1));
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x00102898 File Offset: 0x00100A98
		public static int VerticalScrollBarWidth
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(2);
			}
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x001028A0 File Offset: 0x00100AA0
		public static int GetVerticalScrollBarWidthForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return UnsafeNativeMethods.TryGetSystemMetricsForDpi(2, (uint)dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(2);
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x001028B7 File Offset: 0x00100AB7
		public static int HorizontalScrollBarHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(3);
			}
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x001028BF File Offset: 0x00100ABF
		public static int GetHorizontalScrollBarHeightForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return UnsafeNativeMethods.TryGetSystemMetricsForDpi(3, (uint)dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(3);
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x001028D6 File Offset: 0x00100AD6
		public static int CaptionHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(4);
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x001028DE File Offset: 0x00100ADE
		public static Size BorderSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(5), UnsafeNativeMethods.GetSystemMetrics(6));
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x001028F1 File Offset: 0x00100AF1
		public static Size GetBorderSizeForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return new Size(UnsafeNativeMethods.TryGetSystemMetricsForDpi(5, (uint)dpi), UnsafeNativeMethods.TryGetSystemMetricsForDpi(6, (uint)dpi));
			}
			return SystemInformation.BorderSize;
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x00102913 File Offset: 0x00100B13
		public static Size FixedFrameBorderSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(7), UnsafeNativeMethods.GetSystemMetrics(8));
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x00102926 File Offset: 0x00100B26
		public static int VerticalScrollBarThumbHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(9);
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x0010292F File Offset: 0x00100B2F
		public static int HorizontalScrollBarThumbWidth
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(10);
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x00102938 File Offset: 0x00100B38
		public static Size IconSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(11), UnsafeNativeMethods.GetSystemMetrics(12));
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x0010294D File Offset: 0x00100B4D
		public static Size CursorSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(13), UnsafeNativeMethods.GetSystemMetrics(14));
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x00102962 File Offset: 0x00100B62
		public static Font MenuFont
		{
			get
			{
				return SystemInformation.GetMenuFontHelper(0U, false);
			}
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x0010296B File Offset: 0x00100B6B
		public static Font GetMenuFontForDpi(int dpi)
		{
			return SystemInformation.GetMenuFontHelper((uint)dpi, DpiHelper.EnableDpiChangedMessageHandling);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x00102978 File Offset: 0x00100B78
		private static Font GetMenuFontHelper(uint dpi, bool useDpi)
		{
			Font result = null;
			NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
			bool flag;
			if (useDpi)
			{
				flag = UnsafeNativeMethods.TrySystemParametersInfoForDpi(41, nonclientmetrics.cbSize, nonclientmetrics, 0, dpi);
			}
			else
			{
				flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
			}
			if (flag && nonclientmetrics.lfMenuFont != null)
			{
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					result = Font.FromLogFont(nonclientmetrics.lfMenuFont);
				}
				catch
				{
					result = Control.DefaultFont;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return result;
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x00102A04 File Offset: 0x00100C04
		public static int MenuHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(15);
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x00102A0D File Offset: 0x00100C0D
		public static PowerStatus PowerStatus
		{
			get
			{
				if (SystemInformation.powerStatus == null)
				{
					SystemInformation.powerStatus = new PowerStatus();
				}
				return SystemInformation.powerStatus;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x00102A28 File Offset: 0x00100C28
		public static Rectangle WorkingArea
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.SystemParametersInfo(48, 0, ref rect, 0);
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x00102A66 File Offset: 0x00100C66
		public static int KanjiWindowHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(18);
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x00102A6F File Offset: 0x00100C6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool MousePresent
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(19) != 0;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x00102A7B File Offset: 0x00100C7B
		public static int VerticalScrollBarArrowHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(20);
			}
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x00102A84 File Offset: 0x00100C84
		public static int VerticalScrollBarArrowHeightForDpi(int dpi)
		{
			return UnsafeNativeMethods.TryGetSystemMetricsForDpi(21, (uint)dpi);
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x00102A8E File Offset: 0x00100C8E
		public static int HorizontalScrollBarArrowWidth
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(21);
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x00102A97 File Offset: 0x00100C97
		public static int GetHorizontalScrollBarArrowWidthForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return UnsafeNativeMethods.TryGetSystemMetricsForDpi(21, (uint)dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(21);
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x00102AB0 File Offset: 0x00100CB0
		public static bool DebugOS
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				return UnsafeNativeMethods.GetSystemMetrics(22) != 0;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x00102AC6 File Offset: 0x00100CC6
		public static bool MouseButtonsSwapped
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(23) != 0;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x00102AD2 File Offset: 0x00100CD2
		public static Size MinimumWindowSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(28), UnsafeNativeMethods.GetSystemMetrics(29));
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x00102AE7 File Offset: 0x00100CE7
		public static Size CaptionButtonSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(30), UnsafeNativeMethods.GetSystemMetrics(31));
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x00102AFC File Offset: 0x00100CFC
		public static Size FrameBorderSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(32), UnsafeNativeMethods.GetSystemMetrics(33));
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x00102B11 File Offset: 0x00100D11
		public static Size MinWindowTrackSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(34), UnsafeNativeMethods.GetSystemMetrics(35));
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x00102B26 File Offset: 0x00100D26
		public static Size DoubleClickSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(36), UnsafeNativeMethods.GetSystemMetrics(37));
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x00102B3B File Offset: 0x00100D3B
		public static int DoubleClickTime
		{
			get
			{
				return SafeNativeMethods.GetDoubleClickTime();
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x00102B42 File Offset: 0x00100D42
		public static Size IconSpacingSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(38), UnsafeNativeMethods.GetSystemMetrics(39));
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x00102B57 File Offset: 0x00100D57
		public static bool RightAlignedMenus
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(40) != 0;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x00102B63 File Offset: 0x00100D63
		public static bool PenWindows
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(41) != 0;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x00102B6F File Offset: 0x00100D6F
		public static bool DbcsEnabled
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(42) != 0;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00102B7B File Offset: 0x00100D7B
		public static int MouseButtons
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(43);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x00102B84 File Offset: 0x00100D84
		public static bool Secure
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				return UnsafeNativeMethods.GetSystemMetrics(44) != 0;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x00102B9A File Offset: 0x00100D9A
		public static Size Border3DSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(45), UnsafeNativeMethods.GetSystemMetrics(46));
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x00102BAF File Offset: 0x00100DAF
		public static Size MinimizedWindowSpacingSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(47), UnsafeNativeMethods.GetSystemMetrics(48));
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x00102BC4 File Offset: 0x00100DC4
		public static Size SmallIconSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(49), UnsafeNativeMethods.GetSystemMetrics(50));
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x00102BD9 File Offset: 0x00100DD9
		public static int ToolWindowCaptionHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(51);
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x00102BE2 File Offset: 0x00100DE2
		public static Size ToolWindowCaptionButtonSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(52), UnsafeNativeMethods.GetSystemMetrics(53));
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x00102BF7 File Offset: 0x00100DF7
		public static Size MenuButtonSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(54), UnsafeNativeMethods.GetSystemMetrics(55));
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x00102C0C File Offset: 0x00100E0C
		public static ArrangeStartingPosition ArrangeStartingPosition
		{
			get
			{
				ArrangeStartingPosition arrangeStartingPosition = ArrangeStartingPosition.BottomRight | ArrangeStartingPosition.Hide | ArrangeStartingPosition.TopLeft;
				int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(56);
				return arrangeStartingPosition & (ArrangeStartingPosition)systemMetrics;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x00102C28 File Offset: 0x00100E28
		public static ArrangeDirection ArrangeDirection
		{
			get
			{
				ArrangeDirection arrangeDirection = ArrangeDirection.Down;
				int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(56);
				return arrangeDirection & (ArrangeDirection)systemMetrics;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x00102C42 File Offset: 0x00100E42
		public static Size MinimizedWindowSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(57), UnsafeNativeMethods.GetSystemMetrics(58));
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00102C57 File Offset: 0x00100E57
		public static Size MaxWindowTrackSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(59), UnsafeNativeMethods.GetSystemMetrics(60));
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x00102C6C File Offset: 0x00100E6C
		public static Size PrimaryMonitorMaximizedWindowSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(61), UnsafeNativeMethods.GetSystemMetrics(62));
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x00102C81 File Offset: 0x00100E81
		public static bool Network
		{
			get
			{
				return (UnsafeNativeMethods.GetSystemMetrics(63) & 1) != 0;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x00102C8F File Offset: 0x00100E8F
		public static bool TerminalServerSession
		{
			get
			{
				return (UnsafeNativeMethods.GetSystemMetrics(4096) & 1) != 0;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x00102CA0 File Offset: 0x00100EA0
		public static BootMode BootMode
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				return (BootMode)UnsafeNativeMethods.GetSystemMetrics(67);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x00102CB3 File Offset: 0x00100EB3
		public static Size DragSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(68), UnsafeNativeMethods.GetSystemMetrics(69));
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x00102CC8 File Offset: 0x00100EC8
		public static bool ShowSounds
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(70) != 0;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x00102CD4 File Offset: 0x00100ED4
		public static Size MenuCheckSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(71), UnsafeNativeMethods.GetSystemMetrics(72));
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x00102CE9 File Offset: 0x00100EE9
		public static bool MidEastEnabled
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(74) != 0;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x00102CF5 File Offset: 0x00100EF5
		private static bool MultiMonitorSupport
		{
			get
			{
				if (!SystemInformation.checkMultiMonitorSupport)
				{
					SystemInformation.multiMonitorSupport = (UnsafeNativeMethods.GetSystemMetrics(80) != 0);
					SystemInformation.checkMultiMonitorSupport = true;
				}
				return SystemInformation.multiMonitorSupport;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x00102D18 File Offset: 0x00100F18
		public static bool NativeMouseWheelSupport
		{
			get
			{
				if (!SystemInformation.checkNativeMouseWheelSupport)
				{
					SystemInformation.nativeMouseWheelSupport = (UnsafeNativeMethods.GetSystemMetrics(75) != 0);
					SystemInformation.checkNativeMouseWheelSupport = true;
				}
				return SystemInformation.nativeMouseWheelSupport;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x00102D3C File Offset: 0x00100F3C
		public static bool MouseWheelPresent
		{
			get
			{
				bool result = false;
				if (!SystemInformation.NativeMouseWheelSupport)
				{
					IntPtr value = IntPtr.Zero;
					value = UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
					if (value != IntPtr.Zero)
					{
						result = true;
					}
				}
				else
				{
					result = (UnsafeNativeMethods.GetSystemMetrics(75) != 0);
				}
				return result;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x00102D88 File Offset: 0x00100F88
		public static Rectangle VirtualScreen
		{
			get
			{
				if (SystemInformation.MultiMonitorSupport)
				{
					return new Rectangle(UnsafeNativeMethods.GetSystemMetrics(76), UnsafeNativeMethods.GetSystemMetrics(77), UnsafeNativeMethods.GetSystemMetrics(78), UnsafeNativeMethods.GetSystemMetrics(79));
				}
				Size primaryMonitorSize = SystemInformation.PrimaryMonitorSize;
				return new Rectangle(0, 0, primaryMonitorSize.Width, primaryMonitorSize.Height);
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x00102DD9 File Offset: 0x00100FD9
		public static int MonitorCount
		{
			get
			{
				if (SystemInformation.MultiMonitorSupport)
				{
					return UnsafeNativeMethods.GetSystemMetrics(80);
				}
				return 1;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x00102DEB File Offset: 0x00100FEB
		public static bool MonitorsSameDisplayFormat
		{
			get
			{
				return !SystemInformation.MultiMonitorSupport || UnsafeNativeMethods.GetSystemMetrics(81) != 0;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x00102E00 File Offset: 0x00101000
		public static string ComputerName
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				UnsafeNativeMethods.GetComputerName(stringBuilder, new int[]
				{
					stringBuilder.Capacity
				});
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x00102E3E File Offset: 0x0010103E
		public static string UserDomainName
		{
			get
			{
				return Environment.UserDomainName;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x00102E48 File Offset: 0x00101048
		public static bool UserInteractive
		{
			get
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					IntPtr intPtr = IntPtr.Zero;
					intPtr = UnsafeNativeMethods.GetProcessWindowStation();
					if (intPtr != IntPtr.Zero && SystemInformation.processWinStation != intPtr)
					{
						SystemInformation.isUserInteractive = true;
						int num = 0;
						NativeMethods.USEROBJECTFLAGS userobjectflags = new NativeMethods.USEROBJECTFLAGS();
						if (UnsafeNativeMethods.GetUserObjectInformation(new HandleRef(null, intPtr), 1, userobjectflags, Marshal.SizeOf(userobjectflags), ref num) && (userobjectflags.dwFlags & 1) == 0)
						{
							SystemInformation.isUserInteractive = false;
						}
						SystemInformation.processWinStation = intPtr;
					}
				}
				else
				{
					SystemInformation.isUserInteractive = true;
				}
				return SystemInformation.isUserInteractive;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x00102ED4 File Offset: 0x001010D4
		public static string UserName
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				UnsafeNativeMethods.GetUserName(stringBuilder, new int[]
				{
					stringBuilder.Capacity
				});
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x00102F12 File Offset: 0x00101112
		private static void EnsureSystemEvents()
		{
			if (!SystemInformation.systemEventsAttached)
			{
				SystemEvents.UserPreferenceChanged += SystemInformation.OnUserPreferenceChanged;
				SystemInformation.systemEventsAttached = true;
			}
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x00102F32 File Offset: 0x00101132
		private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			SystemInformation.systemEventsDirty = true;
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x00102F3C File Offset: 0x0010113C
		public static bool IsDropShadowEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4132, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x00102F6C File Offset: 0x0010116C
		public static bool IsFlatMenuEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4130, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x00102F9C File Offset: 0x0010119C
		public static bool IsFontSmoothingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(74, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x00102FBC File Offset: 0x001011BC
		public static int FontSmoothingContrast
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int result = 0;
					UnsafeNativeMethods.SystemParametersInfo(8204, 0, ref result, 0);
					return result;
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x00102FF8 File Offset: 0x001011F8
		public static int FontSmoothingType
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int result = 0;
					UnsafeNativeMethods.SystemParametersInfo(8202, 0, ref result, 0);
					return result;
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x00103034 File Offset: 0x00101234
		public static int IconHorizontalSpacing
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(13, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x00103050 File Offset: 0x00101250
		public static int IconVerticalSpacing
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(24, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x0010306C File Offset: 0x0010126C
		public static bool IsIconTitleWrappingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(25, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x0010308C File Offset: 0x0010128C
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4106, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x001030B0 File Offset: 0x001012B0
		public static int KeyboardDelay
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(22, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x001030CC File Offset: 0x001012CC
		public static bool IsKeyboardPreferred
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(68, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x001030EC File Offset: 0x001012EC
		public static int KeyboardSpeed
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(10, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x00103108 File Offset: 0x00101308
		public static Size MouseHoverSize
		{
			get
			{
				int height = 0;
				int width = 0;
				UnsafeNativeMethods.SystemParametersInfo(100, 0, ref height, 0);
				UnsafeNativeMethods.SystemParametersInfo(98, 0, ref width, 0);
				return new Size(width, height);
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x00103138 File Offset: 0x00101338
		public static int MouseHoverTime
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(102, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x00103154 File Offset: 0x00101354
		public static int MouseSpeed
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(112, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x00103170 File Offset: 0x00101370
		public static bool IsSnapToDefaultEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(95, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x00103190 File Offset: 0x00101390
		public static LeftRightAlignment PopupMenuAlignment
		{
			get
			{
				bool flag = false;
				UnsafeNativeMethods.SystemParametersInfo(27, 0, ref flag, 0);
				if (flag)
				{
					return LeftRightAlignment.Left;
				}
				return LeftRightAlignment.Right;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x001031B4 File Offset: 0x001013B4
		public static bool IsMenuFadeEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4114, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x001031F0 File Offset: 0x001013F0
		public static int MenuShowDelay
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(106, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x0010320C File Offset: 0x0010140C
		public static bool IsComboBoxAnimationEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4100, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x00103230 File Offset: 0x00101430
		public static bool IsTitleBarGradientEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4104, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x00103254 File Offset: 0x00101454
		public static bool IsHotTrackingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4110, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x00103278 File Offset: 0x00101478
		public static bool IsListBoxSmoothScrollingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4102, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x0010329C File Offset: 0x0010149C
		public static bool IsMenuAnimationEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4098, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x001032C0 File Offset: 0x001014C0
		public static bool IsSelectionFadeEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4116, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x001032FC File Offset: 0x001014FC
		public static bool IsToolTipAnimationEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4118, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x00103338 File Offset: 0x00101538
		public static bool UIEffectsEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4158, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x00103374 File Offset: 0x00101574
		public static bool IsActiveWindowTrackingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4096, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x00103398 File Offset: 0x00101598
		public static int ActiveWindowTrackingDelay
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(8194, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x001033B8 File Offset: 0x001015B8
		public static bool IsMinimizeRestoreAnimationEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(72, 0, ref num, 0);
				return num != 0;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x001033D8 File Offset: 0x001015D8
		public static int BorderMultiplierFactor
		{
			get
			{
				int result = 0;
				UnsafeNativeMethods.SystemParametersInfo(5, 0, ref result, 0);
				return result;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x001033F3 File Offset: 0x001015F3
		public static int CaretBlinkTime
		{
			get
			{
				return (int)SafeNativeMethods.GetCaretBlinkTime();
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x001033FC File Offset: 0x001015FC
		public static int CaretWidth
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int result = 0;
					UnsafeNativeMethods.SystemParametersInfo(8198, 0, ref result, 0);
					return result;
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x00103443 File Offset: 0x00101643
		public static int MouseWheelScrollDelta
		{
			get
			{
				return 120;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x00103447 File Offset: 0x00101647
		public static int VerticalFocusThickness
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					return UnsafeNativeMethods.GetSystemMetrics(84);
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x0010346C File Offset: 0x0010166C
		public static int HorizontalFocusThickness
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					return UnsafeNativeMethods.GetSystemMetrics(83);
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x00103491 File Offset: 0x00101691
		public static int VerticalResizeBorderThickness
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(33);
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x0010349A File Offset: 0x0010169A
		public static int HorizontalResizeBorderThickness
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(32);
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x001034A4 File Offset: 0x001016A4
		public static ScreenOrientation ScreenOrientation
		{
			get
			{
				ScreenOrientation result = ScreenOrientation.Angle0;
				NativeMethods.DEVMODE devmode = default(NativeMethods.DEVMODE);
				devmode.dmSize = (short)Marshal.SizeOf(typeof(NativeMethods.DEVMODE));
				devmode.dmDriverExtra = 0;
				try
				{
					SafeNativeMethods.EnumDisplaySettings(null, -1, ref devmode);
					if ((devmode.dmFields & 128) > 0)
					{
						result = devmode.dmDisplayOrientation;
					}
				}
				catch
				{
				}
				return result;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003B12 RID: 15122 RVA: 0x00103510 File Offset: 0x00101710
		public static int SizingBorderWidth
		{
			get
			{
				NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
				bool flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
				if (flag && nonclientmetrics.iBorderWidth > 0)
				{
					return nonclientmetrics.iBorderWidth;
				}
				return 0;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x00103548 File Offset: 0x00101748
		public static Size SmallCaptionButtonSize
		{
			get
			{
				NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
				bool flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
				if (flag && nonclientmetrics.iSmCaptionHeight > 0 && nonclientmetrics.iSmCaptionWidth > 0)
				{
					return new Size(nonclientmetrics.iSmCaptionWidth, nonclientmetrics.iSmCaptionHeight);
				}
				return Size.Empty;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x00103598 File Offset: 0x00101798
		public static Size MenuBarButtonSize
		{
			get
			{
				NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
				bool flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
				if (flag && nonclientmetrics.iMenuHeight > 0 && nonclientmetrics.iMenuWidth > 0)
				{
					return new Size(nonclientmetrics.iMenuWidth, nonclientmetrics.iMenuHeight);
				}
				return Size.Empty;
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x001035E8 File Offset: 0x001017E8
		internal static bool InLockedTerminalSession()
		{
			bool result = false;
			if (SystemInformation.TerminalServerSession)
			{
				IntPtr intPtr = SafeNativeMethods.OpenInputDesktop(0, false, 256);
				if (intPtr == IntPtr.Zero)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					result = (lastWin32Error == 5);
				}
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.CloseDesktop(intPtr);
				}
			}
			return result;
		}

		// Token: 0x0400232F RID: 9007
		private static bool checkMultiMonitorSupport = false;

		// Token: 0x04002330 RID: 9008
		private static bool multiMonitorSupport = false;

		// Token: 0x04002331 RID: 9009
		private static bool checkNativeMouseWheelSupport = false;

		// Token: 0x04002332 RID: 9010
		private static bool nativeMouseWheelSupport = true;

		// Token: 0x04002333 RID: 9011
		private static bool highContrast = false;

		// Token: 0x04002334 RID: 9012
		private static bool systemEventsAttached = false;

		// Token: 0x04002335 RID: 9013
		private static bool systemEventsDirty = true;

		// Token: 0x04002336 RID: 9014
		private static IntPtr processWinStation = IntPtr.Zero;

		// Token: 0x04002337 RID: 9015
		private static bool isUserInteractive = false;

		// Token: 0x04002338 RID: 9016
		private static PowerStatus powerStatus = null;

		// Token: 0x04002339 RID: 9017
		private const int DefaultMouseWheelScrollLines = 3;
	}
}
