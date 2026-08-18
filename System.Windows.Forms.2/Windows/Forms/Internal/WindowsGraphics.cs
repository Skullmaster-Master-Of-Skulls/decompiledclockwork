using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E9 RID: 1257
	internal sealed class WindowsGraphics : MarshalByRefObject, IDisposable, IDeviceContext
	{
		// Token: 0x060051EF RID: 20975 RVA: 0x001545AC File Offset: 0x001527AC
		public WindowsGraphics(DeviceContext dc)
		{
			this.dc = dc;
			this.dc.SaveHdc();
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x001545C8 File Offset: 0x001527C8
		public static WindowsGraphics CreateMeasurementWindowsGraphics()
		{
			DeviceContext deviceContext = DeviceContext.FromCompatibleDC(IntPtr.Zero);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x001545F0 File Offset: 0x001527F0
		public static WindowsGraphics CreateMeasurementWindowsGraphics(IntPtr screenDC)
		{
			DeviceContext deviceContext = DeviceContext.FromCompatibleDC(screenDC);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00154614 File Offset: 0x00152814
		public static WindowsGraphics FromHwnd(IntPtr hWnd)
		{
			DeviceContext deviceContext = DeviceContext.FromHwnd(hWnd);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00154638 File Offset: 0x00152838
		public static WindowsGraphics FromHdc(IntPtr hDc)
		{
			DeviceContext deviceContext = DeviceContext.FromHdc(hDc);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0015465C File Offset: 0x0015285C
		public static WindowsGraphics FromGraphics(Graphics g)
		{
			ApplyGraphicsProperties properties = ApplyGraphicsProperties.All;
			return WindowsGraphics.FromGraphics(g, properties);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00154674 File Offset: 0x00152874
		public static WindowsGraphics FromGraphics(Graphics g, ApplyGraphicsProperties properties)
		{
			WindowsRegion windowsRegion = null;
			float[] array = null;
			Region region = null;
			Matrix matrix = null;
			if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None || (properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None)
			{
				object[] array2 = g.GetContextInfo() as object[];
				if (array2 != null && array2.Length == 2)
				{
					region = (array2[0] as Region);
					matrix = (array2[1] as Matrix);
				}
				if (matrix != null)
				{
					if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None)
					{
						array = matrix.Elements;
					}
					matrix.Dispose();
				}
				if (region != null)
				{
					if ((properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None && !region.IsInfinite(g))
					{
						windowsRegion = WindowsRegion.FromRegion(region, g);
					}
					region.Dispose();
				}
			}
			WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(g.GetHdc());
			windowsGraphics.graphics = g;
			if (windowsRegion != null)
			{
				using (windowsRegion)
				{
					windowsGraphics.DeviceContext.IntersectClip(windowsRegion);
				}
			}
			if (array != null)
			{
				windowsGraphics.DeviceContext.TranslateTransform((int)array[4], (int)array[5]);
			}
			return windowsGraphics;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00154754 File Offset: 0x00152954
		~WindowsGraphics()
		{
			this.Dispose(false);
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060051F7 RID: 20983 RVA: 0x00154784 File Offset: 0x00152984
		public DeviceContext DeviceContext
		{
			get
			{
				return this.dc;
			}
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0015478C File Offset: 0x0015298C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x0015479C File Offset: 0x0015299C
		internal void Dispose(bool disposing)
		{
			if (this.dc != null)
			{
				try
				{
					this.dc.RestoreHdc();
					if (this.disposeDc)
					{
						this.dc.Dispose(disposing);
					}
					if (this.graphics != null)
					{
						this.graphics.ReleaseHdcInternal(this.dc.Hdc);
						this.graphics = null;
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.dc = null;
				}
			}
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x00154828 File Offset: 0x00152A28
		public IntPtr GetHdc()
		{
			return this.dc.Hdc;
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x00154835 File Offset: 0x00152A35
		public void ReleaseHdc()
		{
			this.dc.Dispose();
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x00154842 File Offset: 0x00152A42
		// (set) Token: 0x060051FD RID: 20989 RVA: 0x0015484A File Offset: 0x00152A4A
		public TextPaddingOptions TextPadding
		{
			get
			{
				return this.paddingFlags;
			}
			set
			{
				if (this.paddingFlags != value)
				{
					this.paddingFlags = value;
				}
			}
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0015485C File Offset: 0x00152A5C
		public void DrawPie(WindowsPen pen, Rectangle bounds, float startAngle, float sweepAngle)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (pen != null)
			{
				IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(pen, pen.HPen));
			}
			int num = Math.Min(bounds.Width, bounds.Height);
			Point point = new Point(bounds.X + num / 2, bounds.Y + num / 2);
			int radius = num / 2;
			IntUnsafeNativeMethods.BeginPath(handleRef);
			IntUnsafeNativeMethods.MoveToEx(handleRef, point.X, point.Y, null);
			IntUnsafeNativeMethods.AngleArc(handleRef, point.X, point.Y, radius, startAngle, sweepAngle);
			IntUnsafeNativeMethods.LineTo(handleRef, point.X, point.Y);
			IntUnsafeNativeMethods.EndPath(handleRef);
			IntUnsafeNativeMethods.StrokePath(handleRef);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x00154928 File Offset: 0x00152B28
		private void DrawEllipse(WindowsPen pen, WindowsBrush brush, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (pen != null)
			{
				IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(pen, pen.HPen));
			}
			if (brush != null)
			{
				IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(brush, brush.HBrush));
			}
			IntUnsafeNativeMethods.Ellipse(handleRef, nLeftRect, nTopRect, nRightRect, nBottomRect);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00154987 File Offset: 0x00152B87
		public void DrawAndFillEllipse(WindowsPen pen, WindowsBrush brush, Rectangle bounds)
		{
			this.DrawEllipse(pen, brush, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x001549AD File Offset: 0x00152BAD
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor)
		{
			this.DrawText(text, font, pt, foreColor, Color.Empty, IntTextFormatFlags.Default);
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x001549C0 File Offset: 0x00152BC0
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor, Color backColor)
		{
			this.DrawText(text, font, pt, foreColor, backColor, IntTextFormatFlags.Default);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x001549D0 File Offset: 0x00152BD0
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor, IntTextFormatFlags flags)
		{
			this.DrawText(text, font, pt, foreColor, Color.Empty, flags);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x001549E4 File Offset: 0x00152BE4
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor, Color backColor, IntTextFormatFlags flags)
		{
			Rectangle bounds = new Rectangle(pt.X, pt.Y, int.MaxValue, int.MaxValue);
			this.DrawText(text, font, bounds, foreColor, backColor, flags);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00154A1F File Offset: 0x00152C1F
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color foreColor)
		{
			this.DrawText(text, font, bounds, foreColor, Color.Empty);
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00154A31 File Offset: 0x00152C31
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color foreColor, Color backColor)
		{
			this.DrawText(text, font, bounds, foreColor, backColor, IntTextFormatFlags.HorizontalCenter | IntTextFormatFlags.VerticalCenter);
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00154A41 File Offset: 0x00152C41
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color color, IntTextFormatFlags flags)
		{
			this.DrawText(text, font, bounds, color, Color.Empty, flags);
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x00154A58 File Offset: 0x00152C58
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color foreColor, Color backColor, IntTextFormatFlags flags)
		{
			if (string.IsNullOrEmpty(text) || foreColor == Color.Transparent)
			{
				return;
			}
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (this.dc.TextAlignment != DeviceContextTextAlignment.Top)
			{
				this.dc.SetTextAlignment(DeviceContextTextAlignment.Top);
			}
			if (!foreColor.IsEmpty && foreColor != this.dc.TextColor)
			{
				this.dc.SetTextColor(foreColor);
			}
			if (font != null)
			{
				this.dc.SelectFont(font);
			}
			DeviceContextBackgroundMode deviceContextBackgroundMode = (backColor.IsEmpty || backColor == Color.Transparent) ? DeviceContextBackgroundMode.Transparent : DeviceContextBackgroundMode.Opaque;
			if (this.dc.BackgroundMode != deviceContextBackgroundMode)
			{
				this.dc.SetBackgroundMode(deviceContextBackgroundMode);
			}
			if (deviceContextBackgroundMode != DeviceContextBackgroundMode.Transparent && backColor != this.dc.BackgroundColor)
			{
				this.dc.SetBackgroundColor(backColor);
			}
			IntNativeMethods.DRAWTEXTPARAMS textMargins = this.GetTextMargins(font);
			bounds = WindowsGraphics.AdjustForVerticalAlignment(handleRef, text, bounds, flags, textMargins);
			if (bounds.Width == WindowsGraphics.MaxSize.Width)
			{
				bounds.Width -= bounds.X;
			}
			if (bounds.Height == WindowsGraphics.MaxSize.Height)
			{
				bounds.Height -= bounds.Y;
			}
			IntNativeMethods.RECT rect = new IntNativeMethods.RECT(bounds);
			IntUnsafeNativeMethods.DrawTextEx(handleRef, text, ref rect, (int)flags, textMargins);
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00154BCC File Offset: 0x00152DCC
		public Color GetNearestColor(Color color)
		{
			HandleRef hDC = new HandleRef(null, this.dc.Hdc);
			int nearestColor = IntUnsafeNativeMethods.GetNearestColor(hDC, ColorTranslator.ToWin32(color));
			return ColorTranslator.FromWin32(nearestColor);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00154C00 File Offset: 0x00152E00
		public float GetOverhangPadding(WindowsFont font)
		{
			WindowsFont windowsFont = font;
			if (windowsFont == null)
			{
				windowsFont = this.dc.Font;
			}
			float result = (float)windowsFont.Height / 6f;
			if (windowsFont != font)
			{
				windowsFont.Dispose();
			}
			return result;
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00154C38 File Offset: 0x00152E38
		public IntNativeMethods.DRAWTEXTPARAMS GetTextMargins(WindowsFont font)
		{
			int leftMargin = 0;
			int rightMargin = 0;
			switch (this.TextPadding)
			{
			case TextPaddingOptions.GlyphOverhangPadding:
			{
				float overhangPadding = this.GetOverhangPadding(font);
				leftMargin = (int)Math.Ceiling((double)overhangPadding);
				rightMargin = (int)Math.Ceiling((double)(overhangPadding * 1.5f));
				break;
			}
			case TextPaddingOptions.LeftAndRightPadding:
			{
				float overhangPadding = this.GetOverhangPadding(font);
				leftMargin = (int)Math.Ceiling((double)(2f * overhangPadding));
				rightMargin = (int)Math.Ceiling((double)(overhangPadding * 2.5f));
				break;
			}
			}
			return new IntNativeMethods.DRAWTEXTPARAMS(leftMargin, rightMargin);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00154CBC File Offset: 0x00152EBC
		public Size GetTextExtent(string text, WindowsFont font)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
			HandleRef hDC = new HandleRef(null, this.dc.Hdc);
			if (font != null)
			{
				this.dc.SelectFont(font);
			}
			IntUnsafeNativeMethods.GetTextExtentPoint32(hDC, text, size);
			if (font != null && !MeasurementDCInfo.IsMeasurementDC(this.dc))
			{
				this.dc.ResetFont();
			}
			return new Size(size.cx, size.cy);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x00154D35 File Offset: 0x00152F35
		public Size MeasureText(string text, WindowsFont font)
		{
			return this.MeasureText(text, font, WindowsGraphics.MaxSize, IntTextFormatFlags.Default);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00154D45 File Offset: 0x00152F45
		public Size MeasureText(string text, WindowsFont font, Size proposedSize)
		{
			return this.MeasureText(text, font, proposedSize, IntTextFormatFlags.Default);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00154D54 File Offset: 0x00152F54
		public Size MeasureText(string text, WindowsFont font, Size proposedSize, IntTextFormatFlags flags)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			IntNativeMethods.DRAWTEXTPARAMS drawtextparams = null;
			if (MeasurementDCInfo.IsMeasurementDC(this.DeviceContext))
			{
				drawtextparams = MeasurementDCInfo.GetTextMargins(this, font);
			}
			if (drawtextparams == null)
			{
				drawtextparams = this.GetTextMargins(font);
			}
			int num = 1 + drawtextparams.iLeftMargin + drawtextparams.iRightMargin;
			if (proposedSize.Width <= num)
			{
				proposedSize.Width = num;
			}
			if (proposedSize.Height <= 0)
			{
				proposedSize.Height = 1;
			}
			IntNativeMethods.RECT rect = IntNativeMethods.RECT.FromXYWH(0, 0, proposedSize.Width, proposedSize.Height);
			HandleRef hDC = new HandleRef(null, this.dc.Hdc);
			if (font != null)
			{
				this.dc.SelectFont(font);
			}
			if (proposedSize.Height >= WindowsGraphics.MaxSize.Height && (flags & IntTextFormatFlags.SingleLine) != IntTextFormatFlags.Default)
			{
				flags &= ~(IntTextFormatFlags.Bottom | IntTextFormatFlags.VerticalCenter);
			}
			if (proposedSize.Width == WindowsGraphics.MaxSize.Width)
			{
				flags &= ~IntTextFormatFlags.WordBreak;
			}
			flags |= IntTextFormatFlags.CalculateRectangle;
			IntUnsafeNativeMethods.DrawTextEx(hDC, text, ref rect, (int)flags, drawtextparams);
			return rect.Size;
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00154E60 File Offset: 0x00153060
		public static Rectangle AdjustForVerticalAlignment(HandleRef hdc, string text, Rectangle bounds, IntTextFormatFlags flags, IntNativeMethods.DRAWTEXTPARAMS dtparams)
		{
			if (((flags & IntTextFormatFlags.Bottom) == IntTextFormatFlags.Default && (flags & IntTextFormatFlags.VerticalCenter) == IntTextFormatFlags.Default) || (flags & IntTextFormatFlags.SingleLine) != IntTextFormatFlags.Default || (flags & IntTextFormatFlags.CalculateRectangle) != IntTextFormatFlags.Default)
			{
				return bounds;
			}
			IntNativeMethods.RECT rect = new IntNativeMethods.RECT(bounds);
			flags |= IntTextFormatFlags.CalculateRectangle;
			int num = IntUnsafeNativeMethods.DrawTextEx(hdc, text, ref rect, (int)flags, dtparams);
			if (num > bounds.Height)
			{
				return bounds;
			}
			Rectangle result = bounds;
			if ((flags & IntTextFormatFlags.VerticalCenter) != IntTextFormatFlags.Default)
			{
				result.Y = result.Top + result.Height / 2 - num / 2;
			}
			else
			{
				result.Y = result.Bottom - num;
			}
			return result;
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00154EF0 File Offset: 0x001530F0
		public void DrawRectangle(WindowsPen pen, Rectangle rect)
		{
			this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00154F18 File Offset: 0x00153118
		public void DrawRectangle(WindowsPen pen, int x, int y, int width, int height)
		{
			HandleRef hdc = new HandleRef(this.dc, this.dc.Hdc);
			if (pen != null)
			{
				this.dc.SelectObject(pen.HPen, GdiObjectType.Pen);
			}
			DeviceContextBinaryRasterOperationFlags deviceContextBinaryRasterOperationFlags = this.dc.BinaryRasterOperation;
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				deviceContextBinaryRasterOperationFlags = this.dc.SetRasterOperation(DeviceContextBinaryRasterOperationFlags.CopyPen);
			}
			IntUnsafeNativeMethods.SelectObject(hdc, new HandleRef(null, IntUnsafeNativeMethods.GetStockObject(5)));
			IntUnsafeNativeMethods.Rectangle(hdc, x, y, x + width, y + height);
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				this.dc.SetRasterOperation(deviceContextBinaryRasterOperationFlags);
			}
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00154FA8 File Offset: 0x001531A8
		public void FillRectangle(WindowsBrush brush, Rectangle rect)
		{
			this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00154FD0 File Offset: 0x001531D0
		public void FillRectangle(WindowsBrush brush, int x, int y, int width, int height)
		{
			HandleRef hDC = new HandleRef(this.dc, this.dc.Hdc);
			IntPtr hbrush = brush.HBrush;
			IntNativeMethods.RECT rect = new IntNativeMethods.RECT(x, y, x + width, y + height);
			IntUnsafeNativeMethods.FillRect(hDC, ref rect, new HandleRef(brush, hbrush));
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0015501D File Offset: 0x0015321D
		public void DrawLine(WindowsPen pen, Point p1, Point p2)
		{
			this.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00155044 File Offset: 0x00153244
		public void DrawLine(WindowsPen pen, int x1, int y1, int x2, int y2)
		{
			HandleRef hdc = new HandleRef(this.dc, this.dc.Hdc);
			DeviceContextBinaryRasterOperationFlags deviceContextBinaryRasterOperationFlags = this.dc.BinaryRasterOperation;
			DeviceContextBackgroundMode deviceContextBackgroundMode = this.dc.BackgroundMode;
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				deviceContextBinaryRasterOperationFlags = this.dc.SetRasterOperation(DeviceContextBinaryRasterOperationFlags.CopyPen);
			}
			if (deviceContextBackgroundMode != DeviceContextBackgroundMode.Transparent)
			{
				deviceContextBackgroundMode = this.dc.SetBackgroundMode(DeviceContextBackgroundMode.Transparent);
			}
			if (pen != null)
			{
				this.dc.SelectObject(pen.HPen, GdiObjectType.Pen);
			}
			IntNativeMethods.POINT point = new IntNativeMethods.POINT();
			IntUnsafeNativeMethods.MoveToEx(hdc, x1, y1, point);
			IntUnsafeNativeMethods.LineTo(hdc, x2, y2);
			if (deviceContextBackgroundMode != DeviceContextBackgroundMode.Transparent)
			{
				this.dc.SetBackgroundMode(deviceContextBackgroundMode);
			}
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				this.dc.SetRasterOperation(deviceContextBinaryRasterOperationFlags);
			}
			IntUnsafeNativeMethods.MoveToEx(hdc, point.x, point.y, null);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00155110 File Offset: 0x00153310
		public IntNativeMethods.TEXTMETRIC GetTextMetrics()
		{
			IntNativeMethods.TEXTMETRIC result = default(IntNativeMethods.TEXTMETRIC);
			HandleRef hDC = new HandleRef(this.dc, this.dc.Hdc);
			DeviceContextMapMode deviceContextMapMode = this.dc.MapMode;
			bool flag = deviceContextMapMode != DeviceContextMapMode.Text;
			if (flag)
			{
				this.dc.SaveHdc();
			}
			try
			{
				if (flag)
				{
					deviceContextMapMode = this.dc.SetMapMode(DeviceContextMapMode.Text);
				}
				IntUnsafeNativeMethods.GetTextMetrics(hDC, ref result);
			}
			finally
			{
				if (flag)
				{
					this.dc.RestoreHdc();
				}
			}
			return result;
		}

		// Token: 0x040035F6 RID: 13814
		private DeviceContext dc;

		// Token: 0x040035F7 RID: 13815
		private bool disposeDc;

		// Token: 0x040035F8 RID: 13816
		private Graphics graphics;

		// Token: 0x040035F9 RID: 13817
		public const int GdiUnsupportedFlagMask = -16777216;

		// Token: 0x040035FA RID: 13818
		public static readonly Size MaxSize = new Size(int.MaxValue, int.MaxValue);

		// Token: 0x040035FB RID: 13819
		private const float ItalicPaddingFactor = 0.5f;

		// Token: 0x040035FC RID: 13820
		private TextPaddingOptions paddingFlags;
	}
}
