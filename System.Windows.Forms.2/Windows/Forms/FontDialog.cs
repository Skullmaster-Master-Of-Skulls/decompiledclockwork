using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200025C RID: 604
	[DefaultEvent("Apply")]
	[DefaultProperty("Font")]
	[SRDescription("DescriptionFontDialog")]
	public class FontDialog : CommonDialog
	{
		// Token: 0x060025DD RID: 9693 RVA: 0x000AFCFB File Offset: 0x000ADEFB
		public FontDialog()
		{
			this.Reset();
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x000B0081 File Offset: 0x000AE281
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x000B0091 File Offset: 0x000AE291
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowSimulationsDescr")]
		public bool AllowSimulations
		{
			get
			{
				return !this.GetOption(4096);
			}
			set
			{
				this.SetOption(4096, !value);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x000B00A2 File Offset: 0x000AE2A2
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x000B00B2 File Offset: 0x000AE2B2
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowVectorFontsDescr")]
		public bool AllowVectorFonts
		{
			get
			{
				return !this.GetOption(2048);
			}
			set
			{
				this.SetOption(2048, !value);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000B00C3 File Offset: 0x000AE2C3
		// (set) Token: 0x060025E3 RID: 9699 RVA: 0x000B00D3 File Offset: 0x000AE2D3
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowVerticalFontsDescr")]
		public bool AllowVerticalFonts
		{
			get
			{
				return !this.GetOption(16777216);
			}
			set
			{
				this.SetOption(16777216, !value);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000B00E4 File Offset: 0x000AE2E4
		// (set) Token: 0x060025E5 RID: 9701 RVA: 0x000B00F4 File Offset: 0x000AE2F4
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowScriptChangeDescr")]
		public bool AllowScriptChange
		{
			get
			{
				return !this.GetOption(4194304);
			}
			set
			{
				this.SetOption(4194304, !value);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000B0105 File Offset: 0x000AE305
		// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000B0126 File Offset: 0x000AE326
		[SRCategory("CatData")]
		[SRDescription("FnDcolorDescr")]
		[DefaultValue(typeof(Color), "Black")]
		public Color Color
		{
			get
			{
				if (this.usingDefaultIndirectColor)
				{
					return ColorTranslator.FromWin32(ColorTranslator.ToWin32(this.color));
				}
				return this.color;
			}
			set
			{
				if (!value.IsEmpty)
				{
					this.color = value;
					this.usingDefaultIndirectColor = false;
					return;
				}
				this.color = SystemColors.ControlText;
				this.usingDefaultIndirectColor = true;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000B0152 File Offset: 0x000AE352
		// (set) Token: 0x060025E9 RID: 9705 RVA: 0x000B015F File Offset: 0x000AE35F
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDfixedPitchOnlyDescr")]
		public bool FixedPitchOnly
		{
			get
			{
				return this.GetOption(16384);
			}
			set
			{
				this.SetOption(16384, value);
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000B0170 File Offset: 0x000AE370
		// (set) Token: 0x060025EB RID: 9707 RVA: 0x000B01ED File Offset: 0x000AE3ED
		[SRCategory("CatData")]
		[SRDescription("FnDfontDescr")]
		public Font Font
		{
			get
			{
				Font font = this.font;
				if (font == null)
				{
					font = Control.DefaultFont;
				}
				float sizeInPoints = font.SizeInPoints;
				if (this.minSize != 0 && sizeInPoints < (float)this.MinSize)
				{
					font = new Font(font.FontFamily, (float)this.MinSize, font.Style, GraphicsUnit.Point);
				}
				if (this.maxSize != 0 && sizeInPoints > (float)this.MaxSize)
				{
					font = new Font(font.FontFamily, (float)this.MaxSize, font.Style, GraphicsUnit.Point);
				}
				return font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000B01F6 File Offset: 0x000AE3F6
		// (set) Token: 0x060025ED RID: 9709 RVA: 0x000B0203 File Offset: 0x000AE403
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDfontMustExistDescr")]
		public bool FontMustExist
		{
			get
			{
				return this.GetOption(65536);
			}
			set
			{
				this.SetOption(65536, value);
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000B0211 File Offset: 0x000AE411
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x000B0219 File Offset: 0x000AE419
		[SRCategory("CatData")]
		[DefaultValue(0)]
		[SRDescription("FnDmaxSizeDescr")]
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.maxSize = value;
				if (this.maxSize > 0 && this.maxSize < this.minSize)
				{
					this.minSize = this.maxSize;
				}
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000B024C File Offset: 0x000AE44C
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x000B0254 File Offset: 0x000AE454
		[SRCategory("CatData")]
		[DefaultValue(0)]
		[SRDescription("FnDminSizeDescr")]
		public int MinSize
		{
			get
			{
				return this.minSize;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.minSize = value;
				if (this.maxSize > 0 && this.maxSize < this.minSize)
				{
					this.maxSize = this.minSize;
				}
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000B0287 File Offset: 0x000AE487
		protected int Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000B028F File Offset: 0x000AE48F
		// (set) Token: 0x060025F4 RID: 9716 RVA: 0x000B029C File Offset: 0x000AE49C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDscriptsOnlyDescr")]
		public bool ScriptsOnly
		{
			get
			{
				return this.GetOption(1024);
			}
			set
			{
				this.SetOption(1024, value);
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x000B02AA File Offset: 0x000AE4AA
		// (set) Token: 0x060025F6 RID: 9718 RVA: 0x000B02B7 File Offset: 0x000AE4B7
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDshowApplyDescr")]
		public bool ShowApply
		{
			get
			{
				return this.GetOption(512);
			}
			set
			{
				this.SetOption(512, value);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000B02C5 File Offset: 0x000AE4C5
		// (set) Token: 0x060025F8 RID: 9720 RVA: 0x000B02CD File Offset: 0x000AE4CD
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDshowColorDescr")]
		public bool ShowColor
		{
			get
			{
				return this.showColor;
			}
			set
			{
				this.showColor = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x000B02D6 File Offset: 0x000AE4D6
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x000B02E3 File Offset: 0x000AE4E3
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDshowEffectsDescr")]
		public bool ShowEffects
		{
			get
			{
				return this.GetOption(256);
			}
			set
			{
				this.SetOption(256, value);
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x000B02F1 File Offset: 0x000AE4F1
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x000B02FA File Offset: 0x000AE4FA
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDshowHelpDescr")]
		public bool ShowHelp
		{
			get
			{
				return this.GetOption(4);
			}
			set
			{
				this.SetOption(4, value);
			}
		}

		// Token: 0x1400019C RID: 412
		// (add) Token: 0x060025FD RID: 9725 RVA: 0x000B0304 File Offset: 0x000AE504
		// (remove) Token: 0x060025FE RID: 9726 RVA: 0x000B0317 File Offset: 0x000AE517
		[SRDescription("FnDapplyDescr")]
		public event EventHandler Apply
		{
			add
			{
				base.Events.AddHandler(FontDialog.EventApply, value);
			}
			remove
			{
				base.Events.RemoveHandler(FontDialog.EventApply, value);
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000B032A File Offset: 0x000AE52A
		internal bool GetOption(int option)
		{
			return (this.options & option) != 0;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000B0338 File Offset: 0x000AE538
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg != 272)
			{
				if (msg != 273 || (int)wparam != 1026)
				{
					goto IL_110;
				}
				NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
				UnsafeNativeMethods.SendMessage(new HandleRef(null, hWnd), 1025, 0, logfont);
				this.UpdateFont(logfont);
				int num = (int)UnsafeNativeMethods.SendDlgItemMessage(new HandleRef(null, hWnd), 1139, 327, IntPtr.Zero, IntPtr.Zero);
				if (num != -1)
				{
					this.UpdateColor((int)UnsafeNativeMethods.SendDlgItemMessage(new HandleRef(null, hWnd), 1139, 336, (IntPtr)num, IntPtr.Zero));
				}
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					this.OnApply(EventArgs.Empty);
					goto IL_110;
				}
				try
				{
					this.OnApply(EventArgs.Empty);
					goto IL_110;
				}
				catch (Exception t)
				{
					Application.OnThreadException(t);
					goto IL_110;
				}
			}
			if (!this.showColor)
			{
				IntPtr dlgItem = UnsafeNativeMethods.GetDlgItem(new HandleRef(null, hWnd), 1139);
				SafeNativeMethods.ShowWindow(new HandleRef(null, dlgItem), 0);
				dlgItem = UnsafeNativeMethods.GetDlgItem(new HandleRef(null, hWnd), 1091);
				SafeNativeMethods.ShowWindow(new HandleRef(null, dlgItem), 0);
			}
			IL_110:
			return base.HookProc(hWnd, msg, wparam, lparam);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000B0470 File Offset: 0x000AE670
		protected virtual void OnApply(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FontDialog.EventApply];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000B04A0 File Offset: 0x000AE6A0
		public override void Reset()
		{
			this.options = 257;
			this.font = null;
			this.color = SystemColors.ControlText;
			this.usingDefaultIndirectColor = true;
			this.showColor = false;
			this.minSize = 0;
			this.maxSize = 0;
			this.SetOption(262144, true);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000B04F2 File Offset: 0x000AE6F2
		private void ResetFont()
		{
			this.font = null;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000B04FC File Offset: 0x000AE6FC
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			NativeMethods.WndProc lpfnHook = new NativeMethods.WndProc(this.HookProc);
			NativeMethods.CHOOSEFONT choosefont = new NativeMethods.CHOOSEFONT();
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
			Graphics graphics = Graphics.FromHdcInternal(dc);
			IntSecurity.ObjectFromWin32Handle.Assert();
			try
			{
				this.Font.ToLogFont(logfont, graphics);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				graphics.Dispose();
			}
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			IntPtr intPtr = IntPtr.Zero;
			bool result;
			try
			{
				intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(NativeMethods.LOGFONT)));
				Marshal.StructureToPtr(logfont, intPtr, false);
				choosefont.lStructSize = Marshal.SizeOf(typeof(NativeMethods.CHOOSEFONT));
				choosefont.hwndOwner = hWndOwner;
				choosefont.hDC = IntPtr.Zero;
				choosefont.lpLogFont = intPtr;
				choosefont.Flags = (this.Options | 64 | 8);
				if (this.minSize > 0 || this.maxSize > 0)
				{
					choosefont.Flags |= 8192;
				}
				if (this.ShowColor || this.ShowEffects)
				{
					choosefont.rgbColors = ColorTranslator.ToWin32(this.color);
				}
				else
				{
					choosefont.rgbColors = ColorTranslator.ToWin32(SystemColors.ControlText);
				}
				choosefont.lpfnHook = lpfnHook;
				choosefont.hInstance = UnsafeNativeMethods.GetModuleHandle(null);
				choosefont.nSizeMin = this.minSize;
				if (this.maxSize == 0)
				{
					choosefont.nSizeMax = int.MaxValue;
				}
				else
				{
					choosefont.nSizeMax = this.maxSize;
				}
				if (!SafeNativeMethods.ChooseFont(choosefont))
				{
					result = false;
				}
				else
				{
					NativeMethods.LOGFONT logfont2 = (NativeMethods.LOGFONT)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(NativeMethods.LOGFONT));
					if (logfont2.lfFaceName != null && logfont2.lfFaceName.Length > 0)
					{
						logfont = logfont2;
						this.UpdateFont(logfont);
						this.UpdateColor(choosefont.rgbColors);
					}
					result = true;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000B0714 File Offset: 0x000AE914
		internal void SetOption(int option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000B0737 File Offset: 0x000AE937
		private bool ShouldSerializeFont()
		{
			return !this.Font.Equals(Control.DefaultFont);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000B074C File Offset: 0x000AE94C
		public override string ToString()
		{
			string str = base.ToString();
			return str + ",  Font: " + this.Font.ToString();
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000B0776 File Offset: 0x000AE976
		private void UpdateColor(int rgb)
		{
			if (ColorTranslator.ToWin32(this.color) != rgb)
			{
				this.color = ColorTranslator.FromOle(rgb);
				this.usingDefaultIndirectColor = false;
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000B079C File Offset: 0x000AE99C
		private void UpdateFont(NativeMethods.LOGFONT lf)
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			try
			{
				Font font = null;
				try
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						font = Font.FromLogFont(lf, dc);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.font = ControlPaint.FontInPoints(font);
				}
				finally
				{
					if (font != null)
					{
						font.Dispose();
					}
				}
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
		}

		// Token: 0x04000FC1 RID: 4033
		protected static readonly object EventApply = new object();

		// Token: 0x04000FC2 RID: 4034
		private const int defaultMinSize = 0;

		// Token: 0x04000FC3 RID: 4035
		private const int defaultMaxSize = 0;

		// Token: 0x04000FC4 RID: 4036
		private int options;

		// Token: 0x04000FC5 RID: 4037
		private Font font;

		// Token: 0x04000FC6 RID: 4038
		private Color color;

		// Token: 0x04000FC7 RID: 4039
		private int minSize;

		// Token: 0x04000FC8 RID: 4040
		private int maxSize;

		// Token: 0x04000FC9 RID: 4041
		private bool showColor;

		// Token: 0x04000FCA RID: 4042
		private bool usingDefaultIndirectColor;
	}
}
