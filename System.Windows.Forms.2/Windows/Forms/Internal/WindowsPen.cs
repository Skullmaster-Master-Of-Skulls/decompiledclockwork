using System;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EA RID: 1258
	internal sealed class WindowsPen : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06005219 RID: 21017 RVA: 0x001551B2 File Offset: 0x001533B2
		public WindowsPen(DeviceContext dc) : this(dc, WindowsPenStyle.Solid, 1, Color.Black)
		{
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x001551C2 File Offset: 0x001533C2
		public WindowsPen(DeviceContext dc, Color color) : this(dc, WindowsPenStyle.Solid, 1, color)
		{
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x001551CE File Offset: 0x001533CE
		public WindowsPen(DeviceContext dc, WindowsBrush windowsBrush) : this(dc, WindowsPenStyle.Solid, 1, windowsBrush)
		{
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x001551DA File Offset: 0x001533DA
		public WindowsPen(DeviceContext dc, WindowsPenStyle style, int width, Color color)
		{
			this.style = style;
			this.width = width;
			this.color = color;
			this.dc = dc;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x001551FF File Offset: 0x001533FF
		public WindowsPen(DeviceContext dc, WindowsPenStyle style, int width, WindowsBrush windowsBrush)
		{
			this.style = style;
			this.wndBrush = (WindowsBrush)windowsBrush.Clone();
			this.width = width;
			this.color = windowsBrush.Color;
			this.dc = dc;
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0015523C File Offset: 0x0015343C
		private void CreatePen()
		{
			if (this.width > 1)
			{
				this.style |= WindowsPenStyle.Geometric;
			}
			if (this.wndBrush == null)
			{
				this.nativeHandle = IntSafeNativeMethods.CreatePen((int)this.style, this.width, ColorTranslator.ToWin32(this.color));
				return;
			}
			IntNativeMethods.LOGBRUSH logbrush = new IntNativeMethods.LOGBRUSH();
			logbrush.lbColor = ColorTranslator.ToWin32(this.wndBrush.Color);
			logbrush.lbStyle = 0;
			logbrush.lbHatch = 0;
			this.nativeHandle = IntSafeNativeMethods.ExtCreatePen((int)this.style, this.width, logbrush, 0, null);
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x001552D4 File Offset: 0x001534D4
		public object Clone()
		{
			if (this.wndBrush == null)
			{
				return new WindowsPen(this.dc, this.style, this.width, this.color);
			}
			return new WindowsPen(this.dc, this.style, this.width, (WindowsBrush)this.wndBrush.Clone());
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00155330 File Offset: 0x00153530
		~WindowsPen()
		{
			this.Dispose(false);
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00155360 File Offset: 0x00153560
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0015536C File Offset: 0x0015356C
		private void Dispose(bool disposing)
		{
			if (this.nativeHandle != IntPtr.Zero && this.dc != null)
			{
				this.dc.DeleteObject(this.nativeHandle, GdiObjectType.Pen);
				this.nativeHandle = IntPtr.Zero;
			}
			if (this.wndBrush != null)
			{
				this.wndBrush.Dispose();
				this.wndBrush = null;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06005223 RID: 21027 RVA: 0x001553D3 File Offset: 0x001535D3
		public IntPtr HPen
		{
			get
			{
				if (this.nativeHandle == IntPtr.Zero)
				{
					this.CreatePen();
				}
				return this.nativeHandle;
			}
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x001553F4 File Offset: 0x001535F4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: Style={1}, Color={2}, Width={3}, Brush={4}", new object[]
			{
				base.GetType().Name,
				this.style,
				this.color,
				this.width,
				(this.wndBrush != null) ? this.wndBrush.ToString() : "null"
			});
		}

		// Token: 0x040035FD RID: 13821
		private IntPtr nativeHandle;

		// Token: 0x040035FE RID: 13822
		private const int dashStyleMask = 15;

		// Token: 0x040035FF RID: 13823
		private const int endCapMask = 3840;

		// Token: 0x04003600 RID: 13824
		private const int joinMask = 61440;

		// Token: 0x04003601 RID: 13825
		private DeviceContext dc;

		// Token: 0x04003602 RID: 13826
		private WindowsBrush wndBrush;

		// Token: 0x04003603 RID: 13827
		private WindowsPenStyle style;

		// Token: 0x04003604 RID: 13828
		private Color color;

		// Token: 0x04003605 RID: 13829
		private int width;

		// Token: 0x04003606 RID: 13830
		private const int cosmeticPenWidth = 1;
	}
}
