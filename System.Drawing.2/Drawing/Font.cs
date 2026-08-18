using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Internal;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing
{
	// Token: 0x0200003B RID: 59
	[TypeConverter(typeof(FontConverter))]
	[Editor("System.Drawing.Design.FontEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[Serializable]
	public sealed class Font : MarshalByRefObject, ICloneable, ISerializable, IDisposable
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x000192CC File Offset: 0x000174CC
		private void CreateNativeFont()
		{
			int num = SafeNativeMethods.Gdip.GdipCreateFont(new HandleRef(this, this.fontFamily.NativeFamily), this.fontSize, this.fontStyle, this.fontUnit, out this.nativeFont);
			if (num == 15)
			{
				throw new ArgumentException(SR.GetString("GdiplusFontStyleNotFound", new object[]
				{
					this.fontFamily.Name,
					this.fontStyle.ToString()
				}));
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00019350 File Offset: 0x00017550
		private Font(SerializationInfo info, StreamingContext context)
		{
			string familyName = null;
			float emSize = -1f;
			FontStyle style = FontStyle.Regular;
			GraphicsUnit unit = GraphicsUnit.Point;
			SingleConverter singleConverter = new SingleConverter();
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (string.Equals(enumerator.Name, "Name", StringComparison.OrdinalIgnoreCase))
				{
					familyName = (string)enumerator.Value;
				}
				else if (string.Equals(enumerator.Name, "Size", StringComparison.OrdinalIgnoreCase))
				{
					if (enumerator.Value is string)
					{
						emSize = (float)singleConverter.ConvertFrom(enumerator.Value);
					}
					else
					{
						emSize = (float)enumerator.Value;
					}
				}
				else if (string.Compare(enumerator.Name, "Style", true, CultureInfo.InvariantCulture) == 0)
				{
					style = (FontStyle)enumerator.Value;
				}
				else if (string.Compare(enumerator.Name, "Unit", true, CultureInfo.InvariantCulture) == 0)
				{
					unit = (GraphicsUnit)enumerator.Value;
				}
			}
			this.Initialize(familyName, emSize, style, unit, 1, Font.IsVerticalName(familyName));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00019470 File Offset: 0x00017670
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Name", string.IsNullOrEmpty(this.OriginalFontName) ? this.Name : this.OriginalFontName);
			si.AddValue("Size", this.Size);
			si.AddValue("Style", this.Style);
			si.AddValue("Unit", this.Unit);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000194E0 File Offset: 0x000176E0
		public Font(Font prototype, FontStyle newStyle)
		{
			this.originalFontName = prototype.OriginalFontName;
			this.Initialize(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, 1, false);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001952C File Offset: 0x0001772C
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
		{
			this.Initialize(family, emSize, style, unit, 1, false);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00019553 File Offset: 0x00017753
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
		{
			this.Initialize(family, emSize, style, unit, gdiCharSet, false);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001957B File Offset: 0x0001777B
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			this.Initialize(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000195A4 File Offset: 0x000177A4
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
		{
			this.Initialize(familyName, emSize, style, unit, gdiCharSet, Font.IsVerticalName(familyName));
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000195D4 File Offset: 0x000177D4
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			if (float.IsNaN(emSize) || float.IsInfinity(emSize) || emSize <= 0f)
			{
				throw new ArgumentException(SR.GetString("InvalidBoundArgument", new object[]
				{
					"emSize",
					emSize,
					0,
					"System.Single.MaxValue"
				}), "emSize");
			}
			this.Initialize(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001965D File Offset: 0x0001785D
		public Font(FontFamily family, float emSize, FontStyle style)
		{
			this.Initialize(family, emSize, style, GraphicsUnit.Point, 1, false);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00019683 File Offset: 0x00017883
		public Font(FontFamily family, float emSize, GraphicsUnit unit)
		{
			this.Initialize(family, emSize, FontStyle.Regular, unit, 1, false);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000196A9 File Offset: 0x000178A9
		public Font(FontFamily family, float emSize)
		{
			this.Initialize(family, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000196CF File Offset: 0x000178CF
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
		{
			this.Initialize(familyName, emSize, style, unit, 1, Font.IsVerticalName(familyName));
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000196FB File Offset: 0x000178FB
		public Font(string familyName, float emSize, FontStyle style)
		{
			this.Initialize(familyName, emSize, style, GraphicsUnit.Point, 1, Font.IsVerticalName(familyName));
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019726 File Offset: 0x00017926
		public Font(string familyName, float emSize, GraphicsUnit unit)
		{
			this.Initialize(familyName, emSize, FontStyle.Regular, unit, 1, Font.IsVerticalName(familyName));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019751 File Offset: 0x00017951
		public Font(string familyName, float emSize)
		{
			this.Initialize(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, Font.IsVerticalName(familyName));
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001977C File Offset: 0x0001797C
		private Font(IntPtr nativeFont, byte gdiCharSet, bool gdiVerticalFont)
		{
			float emSize = 0f;
			GraphicsUnit unit = GraphicsUnit.Point;
			FontStyle style = FontStyle.Regular;
			IntPtr zero = IntPtr.Zero;
			this.nativeFont = nativeFont;
			int num = SafeNativeMethods.Gdip.GdipGetFontUnit(new HandleRef(this, nativeFont), out unit);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipGetFontSize(new HandleRef(this, nativeFont), out emSize);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipGetFontStyle(new HandleRef(this, nativeFont), out style);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipGetFamily(new HandleRef(this, nativeFont), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetFontFamily(new FontFamily(zero));
			this.Initialize(this.fontFamily, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001983D File Offset: 0x00017A3D
		private void Initialize(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			this.originalFontName = familyName;
			this.SetFontFamily(new FontFamily(Font.StripVerticalName(familyName), true));
			this.Initialize(this.fontFamily, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001986C File Offset: 0x00017A6C
		private void Initialize(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			if (family == null)
			{
				throw new ArgumentNullException("family");
			}
			if (float.IsNaN(emSize) || float.IsInfinity(emSize) || emSize <= 0f)
			{
				throw new ArgumentException(SR.GetString("InvalidBoundArgument", new object[]
				{
					"emSize",
					emSize,
					0,
					"System.Single.MaxValue"
				}), "emSize");
			}
			this.fontSize = emSize;
			this.fontStyle = style;
			this.fontUnit = unit;
			this.gdiCharSet = gdiCharSet;
			this.gdiVerticalFont = gdiVerticalFont;
			if (this.fontFamily == null)
			{
				this.SetFontFamily(new FontFamily(family.NativeFamily));
			}
			if (this.nativeFont == IntPtr.Zero)
			{
				this.CreateNativeFont();
			}
			int num = SafeNativeMethods.Gdip.GdipGetFontSize(new HandleRef(this, this.nativeFont), out this.fontSize);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00019958 File Offset: 0x00017B58
		public static Font FromHfont(IntPtr hfont)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			SafeNativeMethods.LOGFONT logfont = new SafeNativeMethods.LOGFONT();
			SafeNativeMethods.GetObject(new HandleRef(null, hfont), logfont);
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			Font result;
			try
			{
				result = Font.FromLogFont(logfont, dc);
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
			return result;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000199BC File Offset: 0x00017BBC
		public static Font FromLogFont(object lf)
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			Font result;
			try
			{
				result = Font.FromLogFont(lf, dc);
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019A04 File Offset: 0x00017C04
		public static Font FromLogFont(object lf, IntPtr hdc)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr zero = IntPtr.Zero;
			int num;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				num = SafeNativeMethods.Gdip.GdipCreateFontFromLogfontA(new HandleRef(null, hdc), lf, out zero);
			}
			else
			{
				num = SafeNativeMethods.Gdip.GdipCreateFontFromLogfontW(new HandleRef(null, hdc), lf, out zero);
			}
			if (num == 16)
			{
				throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont_NoName"));
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			if (zero == IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont", new object[]
				{
					lf.ToString()
				}));
			}
			bool flag;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				flag = (Marshal.ReadByte(lf, 28) == 64);
			}
			else
			{
				flag = (Marshal.ReadInt16(lf, 28) == 64);
			}
			return new Font(zero, Marshal.ReadByte(lf, 23), flag);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00019ACC File Offset: 0x00017CCC
		public static Font FromHdc(IntPtr hdc)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateFontFromDC(new HandleRef(null, hdc), ref zero);
			if (num == 16)
			{
				throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont_NoName"));
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Font(zero, 0, false);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019B20 File Offset: 0x00017D20
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneFont(new HandleRef(this, this.nativeFont), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Font(zero, this.gdiCharSet, this.gdiVerticalFont);
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00019B65 File Offset: 0x00017D65
		internal IntPtr NativeFont
		{
			get
			{
				return this.nativeFont;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00019B6D File Offset: 0x00017D6D
		[Browsable(false)]
		public FontFamily FontFamily
		{
			get
			{
				return this.fontFamily;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019B75 File Offset: 0x00017D75
		private void SetFontFamily(FontFamily family)
		{
			this.fontFamily = family;
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			GC.SuppressFinalize(this.fontFamily);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019B94 File Offset: 0x00017D94
		~Font()
		{
			this.Dispose(false);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019BC4 File Offset: 0x00017DC4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019BD4 File Offset: 0x00017DD4
		private void Dispose(bool disposing)
		{
			if (this.nativeFont != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeleteFont(new HandleRef(this, this.nativeFont));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeFont = IntPtr.Zero;
				}
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019C3C File Offset: 0x00017E3C
		private static bool IsVerticalName(string familyName)
		{
			return familyName != null && familyName.Length > 0 && familyName[0] == '@';
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00019C57 File Offset: 0x00017E57
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Bold
		{
			get
			{
				return (this.Style & FontStyle.Bold) > FontStyle.Regular;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00019C64 File Offset: 0x00017E64
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte GdiCharSet
		{
			get
			{
				return this.gdiCharSet;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00019C6C File Offset: 0x00017E6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool GdiVerticalFont
		{
			get
			{
				return this.gdiVerticalFont;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00019C74 File Offset: 0x00017E74
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Italic
		{
			get
			{
				return (this.Style & FontStyle.Italic) > FontStyle.Regular;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00019C81 File Offset: 0x00017E81
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		public string Name
		{
			get
			{
				return this.FontFamily.Name;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00019C8E File Offset: 0x00017E8E
		[Browsable(false)]
		public string OriginalFontName
		{
			get
			{
				return this.originalFontName;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00019C96 File Offset: 0x00017E96
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Strikeout
		{
			get
			{
				return (this.Style & FontStyle.Strikeout) > FontStyle.Regular;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00019CA3 File Offset: 0x00017EA3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Underline
		{
			get
			{
				return (this.Style & FontStyle.Underline) > FontStyle.Regular;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00019CB0 File Offset: 0x00017EB0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			Font font = obj as Font;
			return font != null && (font.FontFamily.Equals(this.FontFamily) && font.GdiVerticalFont == this.GdiVerticalFont && font.GdiCharSet == this.GdiCharSet && font.Style == this.Style && font.Size == this.Size) && font.Unit == this.Unit;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00019D2A File Offset: 0x00017F2A
		public override int GetHashCode()
		{
			return (int)(((uint)this.fontStyle << 13 | this.fontStyle >> 19) ^ (FontStyle)((uint)this.fontUnit << 26 | this.fontUnit >> 6) ^ (FontStyle)((uint)this.fontSize << 7 | (uint)this.fontSize >> 25));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00019D67 File Offset: 0x00017F67
		private static string StripVerticalName(string familyName)
		{
			if (familyName != null && familyName.Length > 1 && familyName[0] == '@')
			{
				return familyName.Substring(1);
			}
			return familyName;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00019D8C File Offset: 0x00017F8C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}: Name={1}, Size={2}, Units={3}, GdiCharSet={4}, GdiVerticalFont={5}]", new object[]
			{
				base.GetType().Name,
				this.FontFamily.Name,
				this.fontSize,
				(int)this.fontUnit,
				this.gdiCharSet,
				this.gdiVerticalFont
			});
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00019E04 File Offset: 0x00018004
		public void ToLogFont(object logFont)
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(dc);
				try
				{
					this.ToLogFont(logFont, graphics);
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00019E64 File Offset: 0x00018064
		public void ToLogFont(object logFont, Graphics graphics)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			int num;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				num = SafeNativeMethods.Gdip.GdipGetLogFontA(new HandleRef(this, this.NativeFont), new HandleRef(graphics, graphics.NativeGraphics), logFont);
			}
			else
			{
				num = SafeNativeMethods.Gdip.GdipGetLogFontW(new HandleRef(this, this.NativeFont), new HandleRef(graphics, graphics.NativeGraphics), logFont);
			}
			if (this.gdiVerticalFont)
			{
				if (Marshal.SystemDefaultCharSize == 1)
				{
					for (int i = 30; i >= 0; i--)
					{
						Marshal.WriteByte(logFont, 28 + i + 1, Marshal.ReadByte(logFont, 28 + i));
					}
					Marshal.WriteByte(logFont, 28, 64);
				}
				else
				{
					for (int j = 60; j >= 0; j -= 2)
					{
						Marshal.WriteInt16(logFont, 28 + j + 2, Marshal.ReadInt16(logFont, 28 + j));
					}
					Marshal.WriteInt16(logFont, 28, 64);
				}
			}
			if (Marshal.ReadByte(logFont, 23) == 0)
			{
				Marshal.WriteByte(logFont, 23, this.gdiCharSet);
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00019F60 File Offset: 0x00018160
		public IntPtr ToHfont()
		{
			SafeNativeMethods.LOGFONT logfont = new SafeNativeMethods.LOGFONT();
			IntSecurity.ObjectFromWin32Handle.Assert();
			try
			{
				this.ToLogFont(logfont);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			IntPtr intPtr = IntUnsafeNativeMethods.IntCreateFontIndirect(logfont);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			return intPtr;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00019FB8 File Offset: 0x000181B8
		public float GetHeight(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			float result;
			int num = SafeNativeMethods.Gdip.GdipGetFontHeight(new HandleRef(this, this.NativeFont), new HandleRef(graphics, graphics.NativeGraphics), out result);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001A000 File Offset: 0x00018200
		public float GetHeight()
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			float result = 0f;
			try
			{
				using (Graphics graphics = Graphics.FromHdcInternal(dc))
				{
					result = this.GetHeight(graphics);
				}
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
			return result;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A06C File Offset: 0x0001826C
		public float GetHeight(float dpi)
		{
			float result;
			int num = SafeNativeMethods.Gdip.GdipGetFontHeightGivenDPI(new HandleRef(this, this.NativeFont), dpi, out result);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001A099 File Offset: 0x00018299
		[Browsable(false)]
		public FontStyle Style
		{
			get
			{
				return this.fontStyle;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001A0A1 File Offset: 0x000182A1
		public float Size
		{
			get
			{
				return this.fontSize;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001A0AC File Offset: 0x000182AC
		[Browsable(false)]
		public float SizeInPoints
		{
			get
			{
				if (this.Unit == GraphicsUnit.Point)
				{
					return this.Size;
				}
				IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				float result;
				try
				{
					using (Graphics graphics = Graphics.FromHdcInternal(dc))
					{
						float num = (float)((double)graphics.DpiY / 72.0);
						float height = this.GetHeight(graphics);
						float num2 = height * (float)this.FontFamily.GetEmHeight(this.Style) / (float)this.FontFamily.GetLineSpacing(this.Style);
						result = num2 / num;
					}
				}
				finally
				{
					UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
				}
				return result;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001A164 File Offset: 0x00018364
		[TypeConverter(typeof(FontConverter.FontUnitConverter))]
		public GraphicsUnit Unit
		{
			get
			{
				return this.fontUnit;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001A16C File Offset: 0x0001836C
		[Browsable(false)]
		public int Height
		{
			get
			{
				return (int)Math.Ceiling((double)this.GetHeight());
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001A17B File Offset: 0x0001837B
		[Browsable(false)]
		public bool IsSystemFont
		{
			get
			{
				return !string.IsNullOrEmpty(this.systemFontName);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001A18B File Offset: 0x0001838B
		[Browsable(false)]
		public string SystemFontName
		{
			get
			{
				return this.systemFontName;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001A193 File Offset: 0x00018393
		internal void SetSystemFontName(string systemFontName)
		{
			this.systemFontName = systemFontName;
		}

		// Token: 0x04000339 RID: 825
		private const int LogFontCharSetOffset = 23;

		// Token: 0x0400033A RID: 826
		private const int LogFontNameOffset = 28;

		// Token: 0x0400033B RID: 827
		private IntPtr nativeFont;

		// Token: 0x0400033C RID: 828
		private float fontSize;

		// Token: 0x0400033D RID: 829
		private FontStyle fontStyle;

		// Token: 0x0400033E RID: 830
		private FontFamily fontFamily;

		// Token: 0x0400033F RID: 831
		private GraphicsUnit fontUnit;

		// Token: 0x04000340 RID: 832
		private byte gdiCharSet = 1;

		// Token: 0x04000341 RID: 833
		private bool gdiVerticalFont;

		// Token: 0x04000342 RID: 834
		private string systemFontName = "";

		// Token: 0x04000343 RID: 835
		private string originalFontName;
	}
}
