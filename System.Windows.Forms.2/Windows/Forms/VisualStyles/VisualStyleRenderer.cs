using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000456 RID: 1110
	public sealed class VisualStyleRenderer
	{
		// Token: 0x06004D82 RID: 19842 RVA: 0x001402CE File Offset: 0x0013E4CE
		static VisualStyleRenderer()
		{
			SystemEvents.UserPreferenceChanging += VisualStyleRenderer.OnUserPreferenceChanging;
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x001402FF File Offset: 0x0013E4FF
		private static bool AreClientAreaVisualStylesSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState & VisualStyleState.ClientAreaEnabled) == VisualStyleState.ClientAreaEnabled;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x00140314 File Offset: 0x0013E514
		public static bool IsSupported
		{
			get
			{
				bool flag = VisualStyleRenderer.AreClientAreaVisualStylesSupported;
				if (flag)
				{
					IntPtr handle = VisualStyleRenderer.GetHandle("BUTTON", false);
					flag = (handle != IntPtr.Zero);
				}
				return flag;
			}
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x00140343 File Offset: 0x0013E543
		public static bool IsElementDefined(VisualStyleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return VisualStyleRenderer.IsCombinationDefined(element.ClassName, element.Part);
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x00140364 File Offset: 0x0013E564
		internal static bool IsCombinationDefined(string className, int part)
		{
			bool flag = false;
			if (!VisualStyleRenderer.IsSupported)
			{
				if (!VisualStyleInformation.IsEnabledByUser)
				{
					throw new InvalidOperationException(SR.GetString("VisualStyleNotActive"));
				}
				throw new InvalidOperationException(SR.GetString("VisualStylesDisabledInClientArea"));
			}
			else
			{
				if (className == null)
				{
					throw new ArgumentNullException("className");
				}
				IntPtr handle = VisualStyleRenderer.GetHandle(className, false);
				if (handle != IntPtr.Zero)
				{
					flag = (part == 0 || SafeNativeMethods.IsThemePartDefined(new HandleRef(null, handle), part, 0));
				}
				if (!flag)
				{
					using (VisualStyleRenderer.ThemeHandle themeHandle = VisualStyleRenderer.ThemeHandle.Create(className, false))
					{
						if (themeHandle != null)
						{
							flag = SafeNativeMethods.IsThemePartDefined(new HandleRef(null, themeHandle.NativeHandle), part, 0);
						}
						if (flag)
						{
							VisualStyleRenderer.RefreshCache();
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x00140424 File Offset: 0x0013E624
		public VisualStyleRenderer(VisualStyleElement element) : this(element.ClassName, element.Part, element.State)
		{
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x0014043E File Offset: 0x0013E63E
		public VisualStyleRenderer(string className, int part, int state)
		{
			if (!VisualStyleRenderer.IsCombinationDefined(className, part))
			{
				throw new ArgumentException(SR.GetString("VisualStylesInvalidCombination"));
			}
			this._class = className;
			this.part = part;
			this.state = state;
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x00140474 File Offset: 0x0013E674
		public string Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x0014047C File Offset: 0x0013E67C
		public int Part
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00140484 File Offset: 0x0013E684
		public int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x0014048C File Offset: 0x0013E68C
		public IntPtr Handle
		{
			get
			{
				if (VisualStyleRenderer.IsSupported)
				{
					return VisualStyleRenderer.GetHandle(this._class);
				}
				if (!VisualStyleInformation.IsEnabledByUser)
				{
					throw new InvalidOperationException(SR.GetString("VisualStyleNotActive"));
				}
				throw new InvalidOperationException(SR.GetString("VisualStylesDisabledInClientArea"));
			}
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x001404C7 File Offset: 0x0013E6C7
		public void SetParameters(VisualStyleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.SetParameters(element.ClassName, element.Part, element.State);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x001404EF File Offset: 0x0013E6EF
		public void SetParameters(string className, int part, int state)
		{
			if (!VisualStyleRenderer.IsCombinationDefined(className, part))
			{
				throw new ArgumentException(SR.GetString("VisualStylesInvalidCombination"));
			}
			this._class = className;
			this.part = part;
			this.state = state;
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0014051F File Offset: 0x0013E71F
		public void DrawBackground(IDeviceContext dc, Rectangle bounds)
		{
			this.DrawBackground(dc, bounds, IntPtr.Zero);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x00140530 File Offset: 0x0013E730
		internal void DrawBackground(IDeviceContext dc, Rectangle bounds, IntPtr hWnd)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return;
			}
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				if (IntPtr.Zero != hWnd)
				{
					using (VisualStyleRenderer.ThemeHandle themeHandle = VisualStyleRenderer.ThemeHandle.Create(this._class, true, new HandleRef(null, hWnd)))
					{
						this.lastHResult = SafeNativeMethods.DrawThemeBackground(new HandleRef(this, themeHandle.NativeHandle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), null);
						return;
					}
				}
				this.lastHResult = SafeNativeMethods.DrawThemeBackground(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), null);
			}
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x00140630 File Offset: 0x0013E830
		public void DrawBackground(IDeviceContext dc, Rectangle bounds, Rectangle clipRectangle)
		{
			this.DrawBackground(dc, bounds, clipRectangle, IntPtr.Zero);
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x00140640 File Offset: 0x0013E840
		internal void DrawBackground(IDeviceContext dc, Rectangle bounds, Rectangle clipRectangle, IntPtr hWnd)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return;
			}
			if (clipRectangle.Width < 0 || clipRectangle.Height < 0)
			{
				return;
			}
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				if (IntPtr.Zero != hWnd)
				{
					using (VisualStyleRenderer.ThemeHandle themeHandle = VisualStyleRenderer.ThemeHandle.Create(this._class, true, new HandleRef(null, hWnd)))
					{
						this.lastHResult = SafeNativeMethods.DrawThemeBackground(new HandleRef(this, themeHandle.NativeHandle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), new NativeMethods.COMRECT(clipRectangle));
						return;
					}
				}
				this.lastHResult = SafeNativeMethods.DrawThemeBackground(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), new NativeMethods.COMRECT(clipRectangle));
			}
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x00140760 File Offset: 0x0013E960
		public Rectangle DrawEdge(IDeviceContext dc, Rectangle bounds, Edges edges, EdgeStyle style, EdgeEffects effects)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!ClientUtils.IsEnumValid_Masked(edges, (int)edges, 31U))
			{
				throw new InvalidEnumArgumentException("edges", (int)edges, typeof(Edges));
			}
			if (!ClientUtils.IsEnumValid_NotSequential(style, (int)style, new int[]
			{
				5,
				10,
				6,
				9
			}))
			{
				throw new InvalidEnumArgumentException("style", (int)style, typeof(EdgeStyle));
			}
			if (!ClientUtils.IsEnumValid_Masked(effects, (int)effects, 55296U))
			{
				throw new InvalidEnumArgumentException("effects", (int)effects, typeof(EdgeEffects));
			}
			NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.DrawThemeEdge(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), (int)style, (int)(edges | (Edges)effects | (Edges)8192), comrect);
			}
			return Rectangle.FromLTRB(comrect.left, comrect.top, comrect.right, comrect.bottom);
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x001408A0 File Offset: 0x0013EAA0
		public void DrawImage(Graphics g, Rectangle bounds, Image image)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return;
			}
			g.DrawImage(image, bounds);
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x001408DC File Offset: 0x0013EADC
		public void DrawImage(Graphics g, Rectangle bounds, ImageList imageList, int imageIndex)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (imageList == null)
			{
				throw new ArgumentNullException("imageList");
			}
			if (imageIndex < 0 || imageIndex >= imageList.Images.Count)
			{
				throw new ArgumentOutOfRangeException("imageIndex", SR.GetString("InvalidArgument", new object[]
				{
					"imageIndex",
					imageIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return;
			}
			g.DrawImage(imageList.Images[imageIndex], bounds);
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00140974 File Offset: 0x0013EB74
		public void DrawParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (childControl == null)
			{
				throw new ArgumentNullException("childControl");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return;
			}
			if (childControl.Handle != IntPtr.Zero)
			{
				using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
				{
					HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
					this.lastHResult = SafeNativeMethods.DrawThemeParentBackground(new HandleRef(this, childControl.Handle), hdc, new NativeMethods.COMRECT(bounds));
				}
			}
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x00140A24 File Offset: 0x0013EC24
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw)
		{
			this.DrawText(dc, bounds, textToDraw, false);
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00140A30 File Offset: 0x0013EC30
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled)
		{
			this.DrawText(dc, bounds, textToDraw, drawDisabled, TextFormatFlags.HorizontalCenter);
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x00140A40 File Offset: 0x0013EC40
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return;
			}
			int dwTextFlags = drawDisabled ? 1 : 0;
			if (!string.IsNullOrEmpty(textToDraw))
			{
				using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
				{
					HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
					this.lastHResult = SafeNativeMethods.DrawThemeText(new HandleRef(this, this.Handle), hdc, this.part, this.state, textToDraw, textToDraw.Length, (int)flags, dwTextFlags, new NativeMethods.COMRECT(bounds));
				}
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x00140AF8 File Offset: 0x0013ECF8
		public Rectangle GetBackgroundContentRectangle(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeBackgroundContentRect(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), comrect);
			}
			return Rectangle.FromLTRB(comrect.left, comrect.top, comrect.right, comrect.bottom);
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00140BBC File Offset: 0x0013EDBC
		public Rectangle GetBackgroundExtent(IDeviceContext dc, Rectangle contentBounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (contentBounds.Width < 0 || contentBounds.Height < 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeBackgroundExtent(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(contentBounds), comrect);
			}
			return Rectangle.FromLTRB(comrect.left, comrect.top, comrect.right, comrect.bottom);
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00140C80 File Offset: 0x0013EE80
		[SuppressUnmanagedCodeSecurity]
		public Region GetBackgroundRegion(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (bounds.Width < 0 || bounds.Height < 0)
			{
				return null;
			}
			IntPtr zero = IntPtr.Zero;
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeBackgroundRegion(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), ref zero);
			}
			if (zero == IntPtr.Zero)
			{
				return null;
			}
			Region result = Region.FromHrgn(zero);
			SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, zero));
			return result;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00140D48 File Offset: 0x0013EF48
		public bool GetBoolean(BooleanProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 2201, 2213))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(BooleanProperty));
			}
			bool result = false;
			this.lastHResult = SafeNativeMethods.GetThemeBool(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, ref result);
			return result;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x00140DAC File Offset: 0x0013EFAC
		public Color GetColor(ColorProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 3801, 3823))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(ColorProperty));
			}
			int win32Color = 0;
			this.lastHResult = SafeNativeMethods.GetThemeColor(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, ref win32Color);
			return ColorTranslator.FromWin32(win32Color);
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00140E18 File Offset: 0x0013F018
		public int GetEnumValue(EnumProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 4001, 4015))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(EnumProperty));
			}
			int result = 0;
			this.lastHResult = SafeNativeMethods.GetThemeEnumValue(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, ref result);
			return result;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00140E7C File Offset: 0x0013F07C
		public string GetFilename(FilenameProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 3001, 3008))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(FilenameProperty));
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			this.lastHResult = SafeNativeMethods.GetThemeFilename(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x00140EF4 File Offset: 0x0013F0F4
		public Font GetFont(IDeviceContext dc, FontProperty prop)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 2601, 2601))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(FontProperty));
			}
			NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeFont(new HandleRef(this, this.Handle), hdc, this.part, this.state, (int)prop, logfont);
			}
			Font result = null;
			if (NativeMethods.Succeeded(this.lastHResult))
			{
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					result = Font.FromLogFont(logfont);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00140FEC File Offset: 0x0013F1EC
		public int GetInteger(IntegerProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 2401, 2424))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(IntegerProperty));
			}
			int result = 0;
			this.lastHResult = SafeNativeMethods.GetThemeInt(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, ref result);
			return result;
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x00141050 File Offset: 0x0013F250
		public Size GetPartSize(IDeviceContext dc, ThemeSizeType type)
		{
			return this.GetPartSize(dc, type, IntPtr.Zero);
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00141060 File Offset: 0x0013F260
		internal Size GetPartSize(IDeviceContext dc, ThemeSizeType type, IntPtr hWnd)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!ClientUtils.IsEnumValid(type, (int)type, 0, 2))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(ThemeSizeType));
			}
			NativeMethods.SIZE size = new NativeMethods.SIZE();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				if (DpiHelper.EnableDpiChangedMessageHandling && IntPtr.Zero != hWnd)
				{
					using (VisualStyleRenderer.ThemeHandle themeHandle = VisualStyleRenderer.ThemeHandle.Create(this._class, true, new HandleRef(null, hWnd)))
					{
						this.lastHResult = SafeNativeMethods.GetThemePartSize(new HandleRef(this, themeHandle.NativeHandle), hdc, this.part, this.state, null, type, size);
						goto IL_EC;
					}
				}
				this.lastHResult = SafeNativeMethods.GetThemePartSize(new HandleRef(this, this.Handle), hdc, this.part, this.state, null, type, size);
			}
			IL_EC:
			return new Size(size.cx, size.cy);
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00141188 File Offset: 0x0013F388
		public Size GetPartSize(IDeviceContext dc, Rectangle bounds, ThemeSizeType type)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!ClientUtils.IsEnumValid(type, (int)type, 0, 2))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(ThemeSizeType));
			}
			NativeMethods.SIZE size = new NativeMethods.SIZE();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemePartSize(new HandleRef(this, this.Handle), hdc, this.part, this.state, new NativeMethods.COMRECT(bounds), type, size);
			}
			return new Size(size.cx, size.cy);
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x0014124C File Offset: 0x0013F44C
		public Point GetPoint(PointProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 3401, 3408))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(PointProperty));
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			this.lastHResult = SafeNativeMethods.GetThemePosition(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, point);
			return new Point(point.x, point.y);
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x001412C4 File Offset: 0x0013F4C4
		public Padding GetMargins(IDeviceContext dc, MarginProperty prop)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 3601, 3603))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(MarginProperty));
			}
			NativeMethods.MARGINS margins = default(NativeMethods.MARGINS);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hDC = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeMargins(new HandleRef(this, this.Handle), hDC, this.part, this.state, (int)prop, ref margins);
			}
			return new Padding(margins.cxLeftWidth, margins.cyTopHeight, margins.cxRightWidth, margins.cyBottomHeight);
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0014139C File Offset: 0x0013F59C
		public string GetString(StringProperty prop)
		{
			if (!ClientUtils.IsEnumValid(prop, (int)prop, 3201, 3201))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(StringProperty));
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			this.lastHResult = SafeNativeMethods.GetThemeString(new HandleRef(this, this.Handle), this.part, this.state, (int)prop, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00141414 File Offset: 0x0013F614
		public Rectangle GetTextExtent(IDeviceContext dc, string textToDraw, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(textToDraw))
			{
				throw new ArgumentNullException("textToDraw");
			}
			NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeTextExtent(new HandleRef(this, this.Handle), hdc, this.part, this.state, textToDraw, textToDraw.Length, (int)flags, null, comrect);
			}
			return Rectangle.FromLTRB(comrect.left, comrect.top, comrect.right, comrect.bottom);
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x001414D4 File Offset: 0x0013F6D4
		public Rectangle GetTextExtent(IDeviceContext dc, Rectangle bounds, string textToDraw, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(textToDraw))
			{
				throw new ArgumentNullException("textToDraw");
			}
			NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeTextExtent(new HandleRef(this, this.Handle), hdc, this.part, this.state, textToDraw, textToDraw.Length, (int)flags, new NativeMethods.COMRECT(bounds), comrect);
			}
			return Rectangle.FromLTRB(comrect.left, comrect.top, comrect.right, comrect.bottom);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x0014159C File Offset: 0x0013F79C
		public TextMetrics GetTextMetrics(IDeviceContext dc)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			TextMetrics result = default(TextMetrics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.GetThemeTextMetrics(new HandleRef(this, this.Handle), hdc, this.part, this.state, ref result);
			}
			return result;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x00141628 File Offset: 0x0013F828
		public HitTestCode HitTestBackground(IDeviceContext dc, Rectangle backgroundRectangle, Point pt, HitTestOptions options)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			int result = 0;
			NativeMethods.POINTSTRUCT ptTest = new NativeMethods.POINTSTRUCT(pt.X, pt.Y);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.HitTestThemeBackground(new HandleRef(this, this.Handle), hdc, this.part, this.state, (int)options, new NativeMethods.COMRECT(backgroundRectangle), NativeMethods.NullHandleRef, ptTest, ref result);
			}
			return (HitTestCode)result;
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x001416D0 File Offset: 0x0013F8D0
		public HitTestCode HitTestBackground(Graphics g, Rectangle backgroundRectangle, Region region, Point pt, HitTestOptions options)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr hrgn = region.GetHrgn(g);
			return this.HitTestBackground(g, backgroundRectangle, hrgn, pt, options);
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x00141700 File Offset: 0x0013F900
		public HitTestCode HitTestBackground(IDeviceContext dc, Rectangle backgroundRectangle, IntPtr hRgn, Point pt, HitTestOptions options)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			int result = 0;
			NativeMethods.POINTSTRUCT ptTest = new NativeMethods.POINTSTRUCT(pt.X, pt.Y);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform))
			{
				HandleRef hdc = new HandleRef(windowsGraphicsWrapper, windowsGraphicsWrapper.WindowsGraphics.DeviceContext.Hdc);
				this.lastHResult = SafeNativeMethods.HitTestThemeBackground(new HandleRef(this, this.Handle), hdc, this.part, this.state, (int)options, new NativeMethods.COMRECT(backgroundRectangle), new HandleRef(this, hRgn), ptTest, ref result);
			}
			return (HitTestCode)result;
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x001417AC File Offset: 0x0013F9AC
		public bool IsBackgroundPartiallyTransparent()
		{
			return SafeNativeMethods.IsThemeBackgroundPartiallyTransparent(new HandleRef(this, this.Handle), this.part, this.state);
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x001417CB File Offset: 0x0013F9CB
		public int LastHResult
		{
			get
			{
				return this.lastHResult;
			}
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x001417D3 File Offset: 0x0013F9D3
		private static void CreateThemeHandleHashtable()
		{
			VisualStyleRenderer.themeHandles = new Hashtable(VisualStyleRenderer.numberOfPossibleClasses);
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x001417E4 File Offset: 0x0013F9E4
		private static void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs ea)
		{
			if (ea.Category == UserPreferenceCategory.VisualStyle)
			{
				VisualStyleRenderer.globalCacheVersion += 1L;
			}
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x00141800 File Offset: 0x0013FA00
		private static void RefreshCache()
		{
			if (VisualStyleRenderer.themeHandles != null)
			{
				string[] array = new string[VisualStyleRenderer.themeHandles.Keys.Count];
				VisualStyleRenderer.themeHandles.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					VisualStyleRenderer.ThemeHandle themeHandle = (VisualStyleRenderer.ThemeHandle)VisualStyleRenderer.themeHandles[text];
					if (themeHandle != null)
					{
						themeHandle.Dispose();
					}
					if (VisualStyleRenderer.AreClientAreaVisualStylesSupported)
					{
						themeHandle = VisualStyleRenderer.ThemeHandle.Create(text, false);
						if (themeHandle != null)
						{
							VisualStyleRenderer.themeHandles[text] = themeHandle;
						}
					}
				}
			}
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x0014188C File Offset: 0x0013FA8C
		private static IntPtr GetHandle(string className)
		{
			return VisualStyleRenderer.GetHandle(className, true);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x00141898 File Offset: 0x0013FA98
		private static IntPtr GetHandle(string className, bool throwExceptionOnFail)
		{
			if (VisualStyleRenderer.themeHandles == null)
			{
				VisualStyleRenderer.CreateThemeHandleHashtable();
			}
			if (VisualStyleRenderer.threadCacheVersion != VisualStyleRenderer.globalCacheVersion)
			{
				VisualStyleRenderer.RefreshCache();
				VisualStyleRenderer.threadCacheVersion = VisualStyleRenderer.globalCacheVersion;
			}
			VisualStyleRenderer.ThemeHandle themeHandle;
			if (!VisualStyleRenderer.themeHandles.Contains(className))
			{
				themeHandle = VisualStyleRenderer.ThemeHandle.Create(className, throwExceptionOnFail);
				if (themeHandle == null)
				{
					return IntPtr.Zero;
				}
				VisualStyleRenderer.themeHandles.Add(className, themeHandle);
			}
			else
			{
				themeHandle = (VisualStyleRenderer.ThemeHandle)VisualStyleRenderer.themeHandles[className];
			}
			return themeHandle.NativeHandle;
		}

		// Token: 0x0400325F RID: 12895
		private const TextFormatFlags AllGraphicsProperties = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform;

		// Token: 0x04003260 RID: 12896
		internal const int EdgeAdjust = 8192;

		// Token: 0x04003261 RID: 12897
		private string _class;

		// Token: 0x04003262 RID: 12898
		private int part;

		// Token: 0x04003263 RID: 12899
		private int state;

		// Token: 0x04003264 RID: 12900
		private int lastHResult;

		// Token: 0x04003265 RID: 12901
		private static int numberOfPossibleClasses = VisualStyleElement.Count;

		// Token: 0x04003266 RID: 12902
		[ThreadStatic]
		private static Hashtable themeHandles = null;

		// Token: 0x04003267 RID: 12903
		[ThreadStatic]
		private static long threadCacheVersion = 0L;

		// Token: 0x04003268 RID: 12904
		private static long globalCacheVersion = 0L;

		// Token: 0x0200084E RID: 2126
		private class ThemeHandle : IDisposable
		{
			// Token: 0x0600707C RID: 28796 RVA: 0x0019C3DE File Offset: 0x0019A5DE
			private ThemeHandle(IntPtr hTheme)
			{
				this._hTheme = hTheme;
			}

			// Token: 0x17001883 RID: 6275
			// (get) Token: 0x0600707D RID: 28797 RVA: 0x0019C3F8 File Offset: 0x0019A5F8
			public IntPtr NativeHandle
			{
				get
				{
					return this._hTheme;
				}
			}

			// Token: 0x0600707E RID: 28798 RVA: 0x0019C400 File Offset: 0x0019A600
			public static VisualStyleRenderer.ThemeHandle Create(string className, bool throwExceptionOnFail)
			{
				return VisualStyleRenderer.ThemeHandle.Create(className, throwExceptionOnFail, new HandleRef(null, IntPtr.Zero));
			}

			// Token: 0x0600707F RID: 28799 RVA: 0x0019C414 File Offset: 0x0019A614
			internal static VisualStyleRenderer.ThemeHandle Create(string className, bool throwExceptionOnFail, HandleRef hWndRef)
			{
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					intPtr = SafeNativeMethods.OpenThemeData(hWndRef, className);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					if (throwExceptionOnFail)
					{
						throw new InvalidOperationException(SR.GetString("VisualStyleHandleCreationFailed"), ex);
					}
					return null;
				}
				if (!(intPtr == IntPtr.Zero))
				{
					return new VisualStyleRenderer.ThemeHandle(intPtr);
				}
				if (throwExceptionOnFail)
				{
					throw new InvalidOperationException(SR.GetString("VisualStyleHandleCreationFailed"));
				}
				return null;
			}

			// Token: 0x06007080 RID: 28800 RVA: 0x0019C490 File Offset: 0x0019A690
			public void Dispose()
			{
				if (this._hTheme != IntPtr.Zero)
				{
					SafeNativeMethods.CloseThemeData(new HandleRef(null, this._hTheme));
					this._hTheme = IntPtr.Zero;
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x0019C4C8 File Offset: 0x0019A6C8
			~ThemeHandle()
			{
				this.Dispose();
			}

			// Token: 0x0400437F RID: 17279
			private IntPtr _hTheme = IntPtr.Zero;
		}
	}
}
