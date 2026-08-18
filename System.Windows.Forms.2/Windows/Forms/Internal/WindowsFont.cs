using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E6 RID: 1254
	internal sealed class WindowsFont : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x060051CA RID: 20938 RVA: 0x00153D80 File Offset: 0x00151F80
		private void CreateFont()
		{
			this.hFont = IntUnsafeNativeMethods.CreateFontIndirect(this.logFont);
			if (this.hFont == IntPtr.Zero)
			{
				this.logFont.lfFaceName = "Microsoft Sans Serif";
				this.logFont.lfOutPrecision = 7;
				this.hFont = IntUnsafeNativeMethods.CreateFontIndirect(this.logFont);
			}
			IntUnsafeNativeMethods.GetObject(new HandleRef(this, this.hFont), this.logFont);
			this.ownHandle = true;
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00153DFC File Offset: 0x00151FFC
		public WindowsFont(string faceName) : this(faceName, 8.25f, FontStyle.Regular, 1, WindowsFontQuality.Default)
		{
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00153E0D File Offset: 0x0015200D
		public WindowsFont(string faceName, float size) : this(faceName, size, FontStyle.Regular, 1, WindowsFontQuality.Default)
		{
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x00153E1A File Offset: 0x0015201A
		public WindowsFont(string faceName, float size, FontStyle style) : this(faceName, size, style, 1, WindowsFontQuality.Default)
		{
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00153E28 File Offset: 0x00152028
		public WindowsFont(string faceName, float size, FontStyle style, byte charSet, WindowsFontQuality fontQuality)
		{
			this.fontSize = -1f;
			base..ctor();
			this.logFont = new IntNativeMethods.LOGFONT();
			int num = (int)Math.Ceiling((double)((float)WindowsGraphicsCacheManager.MeasurementGraphics.DeviceContext.DpiY * size / 72f));
			this.logFont.lfHeight = -num;
			this.logFont.lfFaceName = ((faceName != null) ? faceName : "Microsoft Sans Serif");
			this.logFont.lfCharSet = charSet;
			this.logFont.lfOutPrecision = 4;
			this.logFont.lfQuality = (byte)fontQuality;
			this.logFont.lfWeight = (((style & FontStyle.Bold) == FontStyle.Bold) ? 700 : 400);
			this.logFont.lfItalic = (((style & FontStyle.Italic) == FontStyle.Italic) ? 1 : 0);
			this.logFont.lfUnderline = (((style & FontStyle.Underline) == FontStyle.Underline) ? 1 : 0);
			this.logFont.lfStrikeOut = (((style & FontStyle.Strikeout) == FontStyle.Strikeout) ? 1 : 0);
			this.style = style;
			this.CreateFont();
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00153F24 File Offset: 0x00152124
		private WindowsFont(IntNativeMethods.LOGFONT lf, bool createHandle)
		{
			this.fontSize = -1f;
			base..ctor();
			this.logFont = lf;
			if (this.logFont.lfFaceName == null)
			{
				this.logFont.lfFaceName = "Microsoft Sans Serif";
			}
			this.style = FontStyle.Regular;
			if (lf.lfWeight == 700)
			{
				this.style |= FontStyle.Bold;
			}
			if (lf.lfItalic == 1)
			{
				this.style |= FontStyle.Italic;
			}
			if (lf.lfUnderline == 1)
			{
				this.style |= FontStyle.Underline;
			}
			if (lf.lfStrikeOut == 1)
			{
				this.style |= FontStyle.Strikeout;
			}
			if (createHandle)
			{
				this.CreateFont();
			}
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00153FD6 File Offset: 0x001521D6
		public static WindowsFont FromFont(Font font)
		{
			return WindowsFont.FromFont(font, WindowsFontQuality.Default);
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x00153FE0 File Offset: 0x001521E0
		public static WindowsFont FromFont(Font font, WindowsFontQuality fontQuality)
		{
			string text = font.FontFamily.Name;
			if (text != null && text.Length > 1 && text[0] == '@')
			{
				text = text.Substring(1);
			}
			return new WindowsFont(text, font.SizeInPoints, font.Style, font.GdiCharSet, fontQuality);
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00154034 File Offset: 0x00152234
		public static WindowsFont FromHdc(IntPtr hdc)
		{
			IntPtr currentObject = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(null, hdc), 6);
			return WindowsFont.FromHfont(currentObject);
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00154055 File Offset: 0x00152255
		public static WindowsFont FromHfont(IntPtr hFont)
		{
			return WindowsFont.FromHfont(hFont, false);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x00154060 File Offset: 0x00152260
		public static WindowsFont FromHfont(IntPtr hFont, bool takeOwnership)
		{
			IntNativeMethods.LOGFONT logfont = new IntNativeMethods.LOGFONT();
			IntUnsafeNativeMethods.GetObject(new HandleRef(null, hFont), logfont);
			return new WindowsFont(logfont, false)
			{
				hFont = hFont,
				ownHandle = takeOwnership
			};
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x00154098 File Offset: 0x00152298
		~WindowsFont()
		{
			this.Dispose(false);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x001540C8 File Offset: 0x001522C8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x001540D4 File Offset: 0x001522D4
		internal void Dispose(bool disposing)
		{
			bool flag = false;
			if (this.ownHandle && (!this.ownedByCacheManager || !disposing) && (this.everOwnedByCacheManager || !disposing || !DeviceContexts.IsFontInUse(this)))
			{
				IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, this.hFont));
				this.hFont = IntPtr.Zero;
				this.ownHandle = false;
				flag = true;
			}
			if (disposing && (flag || !this.ownHandle))
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00154144 File Offset: 0x00152344
		public override bool Equals(object font)
		{
			WindowsFont windowsFont = font as WindowsFont;
			return windowsFont != null && (windowsFont == this || (this.Name == windowsFont.Name && this.LogFontHeight == windowsFont.LogFontHeight && this.Style == windowsFont.Style && this.CharSet == windowsFont.CharSet && this.Quality == windowsFont.Quality));
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x001541B0 File Offset: 0x001523B0
		public override int GetHashCode()
		{
			return (int)(((int)this.Style << 13 | this.Style >> 19) ^ (FontStyle)((int)this.CharSet << 26 | (int)((uint)this.CharSet >> 6)) ^ (FontStyle)((uint)this.Size << 7 | (uint)this.Size >> 25));
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x001541ED File Offset: 0x001523ED
		public object Clone()
		{
			return new WindowsFont(this.logFont, true);
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x001541FC File Offset: 0x001523FC
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}: Name={1}, Size={2} points, Height={3} pixels, Sytle={4}]", new object[]
			{
				base.GetType().Name,
				this.logFont.lfFaceName,
				this.Size,
				this.Height,
				this.Style
			});
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060051DC RID: 20956 RVA: 0x00154264 File Offset: 0x00152464
		public IntPtr Hfont
		{
			get
			{
				return this.hFont;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060051DD RID: 20957 RVA: 0x0015426C File Offset: 0x0015246C
		public bool Italic
		{
			get
			{
				return this.logFont.lfItalic == 1;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060051DE RID: 20958 RVA: 0x0015427C File Offset: 0x0015247C
		// (set) Token: 0x060051DF RID: 20959 RVA: 0x00154284 File Offset: 0x00152484
		public bool OwnedByCacheManager
		{
			get
			{
				return this.ownedByCacheManager;
			}
			set
			{
				if (value)
				{
					this.everOwnedByCacheManager = true;
				}
				this.ownedByCacheManager = value;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060051E0 RID: 20960 RVA: 0x00154297 File Offset: 0x00152497
		public WindowsFontQuality Quality
		{
			get
			{
				return (WindowsFontQuality)this.logFont.lfQuality;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x001542A4 File Offset: 0x001524A4
		public FontStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x001542AC File Offset: 0x001524AC
		public int Height
		{
			get
			{
				if (this.lineSpacing == 0)
				{
					WindowsGraphics measurementGraphics = WindowsGraphicsCacheManager.MeasurementGraphics;
					measurementGraphics.DeviceContext.SelectFont(this);
					IntNativeMethods.TEXTMETRIC textMetrics = measurementGraphics.GetTextMetrics();
					this.lineSpacing = textMetrics.tmHeight;
				}
				return this.lineSpacing;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x060051E3 RID: 20963 RVA: 0x001542ED File Offset: 0x001524ED
		public byte CharSet
		{
			get
			{
				return this.logFont.lfCharSet;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x001542FA File Offset: 0x001524FA
		public int LogFontHeight
		{
			get
			{
				return this.logFont.lfHeight;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x00154307 File Offset: 0x00152507
		public string Name
		{
			get
			{
				return this.logFont.lfFaceName;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x060051E6 RID: 20966 RVA: 0x00154314 File Offset: 0x00152514
		public float Size
		{
			get
			{
				if (this.fontSize < 0f)
				{
					WindowsGraphics measurementGraphics = WindowsGraphicsCacheManager.MeasurementGraphics;
					measurementGraphics.DeviceContext.SelectFont(this);
					IntNativeMethods.TEXTMETRIC textMetrics = measurementGraphics.GetTextMetrics();
					int num = (this.logFont.lfHeight > 0) ? textMetrics.tmHeight : (textMetrics.tmHeight - textMetrics.tmInternalLeading);
					this.fontSize = (float)num * 72f / (float)measurementGraphics.DeviceContext.DpiY;
				}
				return this.fontSize;
			}
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00154390 File Offset: 0x00152590
		public static WindowsFontQuality WindowsFontQualityFromTextRenderingHint(Graphics g)
		{
			if (g == null)
			{
				return WindowsFontQuality.Default;
			}
			switch (g.TextRenderingHint)
			{
			case TextRenderingHint.SingleBitPerPixelGridFit:
				return WindowsFontQuality.Proof;
			case TextRenderingHint.SingleBitPerPixel:
				return WindowsFontQuality.Draft;
			case TextRenderingHint.AntiAliasGridFit:
				return WindowsFontQuality.AntiAliased;
			case TextRenderingHint.AntiAlias:
				return WindowsFontQuality.AntiAliased;
			case TextRenderingHint.ClearTypeGridFit:
				if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1)
				{
					return WindowsFontQuality.ClearTypeNatural;
				}
				return WindowsFontQuality.ClearType;
			}
			return WindowsFontQuality.Default;
		}

		// Token: 0x040035DE RID: 13790
		private const int LogFontNameOffset = 28;

		// Token: 0x040035DF RID: 13791
		private IntPtr hFont;

		// Token: 0x040035E0 RID: 13792
		private float fontSize;

		// Token: 0x040035E1 RID: 13793
		private int lineSpacing;

		// Token: 0x040035E2 RID: 13794
		private bool ownHandle;

		// Token: 0x040035E3 RID: 13795
		private bool ownedByCacheManager;

		// Token: 0x040035E4 RID: 13796
		private bool everOwnedByCacheManager;

		// Token: 0x040035E5 RID: 13797
		private IntNativeMethods.LOGFONT logFont;

		// Token: 0x040035E6 RID: 13798
		private FontStyle style;

		// Token: 0x040035E7 RID: 13799
		private const string defaultFaceName = "Microsoft Sans Serif";

		// Token: 0x040035E8 RID: 13800
		private const float defaultFontSize = 8.25f;

		// Token: 0x040035E9 RID: 13801
		private const int defaultFontHeight = 13;
	}
}
