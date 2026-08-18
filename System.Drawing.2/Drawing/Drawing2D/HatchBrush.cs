using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000C3 RID: 195
	public sealed class HatchBrush : Brush
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00027C1F File Offset: 0x00025E1F
		public HatchBrush(HatchStyle hatchstyle, Color foreColor) : this(hatchstyle, foreColor, Color.FromArgb(-16777216))
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00027C34 File Offset: 0x00025E34
		public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateHatchBrush((int)hatchstyle, foreColor.ToArgb(), backColor.ToArgb(), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		internal HatchBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00027C78 File Offset: 0x00025E78
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new HatchBrush(zero);
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00027CB0 File Offset: 0x00025EB0
		public HatchStyle HatchStyle
		{
			get
			{
				int result = 0;
				int num = SafeNativeMethods.Gdip.GdipGetHatchStyle(new HandleRef(this, base.NativeBrush), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return (HatchStyle)result;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00027CE0 File Offset: 0x00025EE0
		public Color ForegroundColor
		{
			get
			{
				int argb;
				int num = SafeNativeMethods.Gdip.GdipGetHatchForegroundColor(new HandleRef(this, base.NativeBrush), out argb);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return Color.FromArgb(argb);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00027D14 File Offset: 0x00025F14
		public Color BackgroundColor
		{
			get
			{
				int argb;
				int num = SafeNativeMethods.Gdip.GdipGetHatchBackgroundColor(new HandleRef(this, base.NativeBrush), out argb);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return Color.FromArgb(argb);
			}
		}
	}
}
