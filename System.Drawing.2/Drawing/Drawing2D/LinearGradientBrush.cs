using System;
using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000C6 RID: 198
	public sealed class LinearGradientBrush : Brush
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x00027D48 File Offset: 0x00025F48
		public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateLineBrush(new GPPOINTF(point1), new GPPOINTF(point2), color1.ToArgb(), color2.ToArgb(), 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00027D98 File Offset: 0x00025F98
		public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateLineBrushI(new GPPOINT(point1), new GPPOINT(point2), color1.ToArgb(), color2.ToArgb(), 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00027DE8 File Offset: 0x00025FE8
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (!ClientUtils.IsEnumValid(linearGradientMode, (int)linearGradientMode, 0, 3))
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if ((double)rect.Width == 0.0 || (double)rect.Height == 0.0)
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", new object[]
				{
					rect.ToString()
				}));
			}
			IntPtr zero = IntPtr.Zero;
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRect(ref gprectf, color1.ToArgb(), color2.ToArgb(), (int)linearGradientMode, 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00027EAC File Offset: 0x000260AC
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (!ClientUtils.IsEnumValid(linearGradientMode, (int)linearGradientMode, 0, 3))
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", new object[]
				{
					rect.ToString()
				}));
			}
			IntPtr zero = IntPtr.Zero;
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRectI(ref gprect, color1.ToArgb(), color2.ToArgb(), (int)linearGradientMode, 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00027F5A File Offset: 0x0002615A
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle) : this(rect, color1, color2, angle, false)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00027F68 File Offset: 0x00026168
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			IntPtr zero = IntPtr.Zero;
			if ((double)rect.Width == 0.0 || (double)rect.Height == 0.0)
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", new object[]
				{
					rect.ToString()
				}));
			}
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRectWithAngle(ref gprectf, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00028003 File Offset: 0x00026203
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle) : this(rect, color1, color2, angle, false)
		{
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00028014 File Offset: 0x00026214
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			IntPtr zero = IntPtr.Zero;
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", new object[]
				{
					rect.ToString()
				}));
			}
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRectWithAngleI(ref gprect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, 0, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		internal LinearGradientBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002809C File Offset: 0x0002629C
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new LinearGradientBrush(zero);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000280D4 File Offset: 0x000262D4
		private void _SetLinearColors(Color color1, Color color2)
		{
			int num = SafeNativeMethods.Gdip.GdipSetLineColors(new HandleRef(this, base.NativeBrush), color1.ToArgb(), color2.ToArgb());
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002810C File Offset: 0x0002630C
		private Color[] _GetLinearColors()
		{
			int[] array = new int[2];
			int num = SafeNativeMethods.Gdip.GdipGetLineColors(new HandleRef(this, base.NativeBrush), array);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Color[]
			{
				Color.FromArgb(array[0]),
				Color.FromArgb(array[1])
			};
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00028163 File Offset: 0x00026363
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0002816B File Offset: 0x0002636B
		public Color[] LinearColors
		{
			get
			{
				return this._GetLinearColors();
			}
			set
			{
				this._SetLinearColors(value[0], value[1]);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00028184 File Offset: 0x00026384
		private RectangleF _GetRectangle()
		{
			GPRECTF gprectf = default(GPRECTF);
			int num = SafeNativeMethods.Gdip.GdipGetLineRect(new HandleRef(this, base.NativeBrush), ref gprectf);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gprectf.ToRectangleF();
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000281BE File Offset: 0x000263BE
		public RectangleF Rectangle
		{
			get
			{
				return this._GetRectangle();
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000281C8 File Offset: 0x000263C8
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x000281F4 File Offset: 0x000263F4
		public bool GammaCorrection
		{
			get
			{
				bool result;
				int num = SafeNativeMethods.Gdip.GdipGetLineGammaCorrection(new HandleRef(this, base.NativeBrush), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				int num = SafeNativeMethods.Gdip.GdipSetLineGammaCorrection(new HandleRef(this, base.NativeBrush), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00028220 File Offset: 0x00026420
		private Blend _GetBlend()
		{
			if (this.interpolationColorsWasSet)
			{
				return null;
			}
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetLineBlendCount(new HandleRef(this, base.NativeBrush), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			if (num <= 0)
			{
				return null;
			}
			int num3 = num;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			Blend blend;
			try
			{
				int cb = checked(4 * num3);
				intPtr = Marshal.AllocHGlobal(cb);
				intPtr2 = Marshal.AllocHGlobal(cb);
				num2 = SafeNativeMethods.Gdip.GdipGetLineBlend(new HandleRef(this, base.NativeBrush), intPtr, intPtr2, num3);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				blend = new Blend(num3);
				Marshal.Copy(intPtr, blend.Factors, 0, num3);
				Marshal.Copy(intPtr2, blend.Positions, 0, num3);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			return blend;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00028308 File Offset: 0x00026508
		private void _SetBlend(Blend blend)
		{
			int num = blend.Factors.Length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				int cb = checked(4 * num);
				intPtr = Marshal.AllocHGlobal(cb);
				intPtr2 = Marshal.AllocHGlobal(cb);
				Marshal.Copy(blend.Factors, 0, intPtr, num);
				Marshal.Copy(blend.Positions, 0, intPtr2, num);
				int num2 = SafeNativeMethods.Gdip.GdipSetLineBlend(new HandleRef(this, base.NativeBrush), new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000283C0 File Offset: 0x000265C0
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x000283C8 File Offset: 0x000265C8
		public Blend Blend
		{
			get
			{
				return this._GetBlend();
			}
			set
			{
				this._SetBlend(value);
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000283D1 File Offset: 0x000265D1
		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000283E0 File Offset: 0x000265E0
		public void SetSigmaBellShape(float focus, float scale)
		{
			int num = SafeNativeMethods.Gdip.GdipSetLineSigmaBlend(new HandleRef(this, base.NativeBrush), focus, scale);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002840B File Offset: 0x0002660B
		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002841C File Offset: 0x0002661C
		public void SetBlendTriangularShape(float focus, float scale)
		{
			int num = SafeNativeMethods.Gdip.GdipSetLineLinearBlend(new HandleRef(this, base.NativeBrush), focus, scale);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00028448 File Offset: 0x00026648
		private ColorBlend _GetInterpolationColors()
		{
			if (!this.interpolationColorsWasSet)
			{
				throw new ArgumentException(SR.GetString("InterpolationColorsCommon", new object[]
				{
					SR.GetString("InterpolationColorsColorBlendNotSet"),
					""
				}));
			}
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetLinePresetBlendCount(new HandleRef(this, base.NativeBrush), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			int num3 = num;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			ColorBlend colorBlend;
			try
			{
				int cb = checked(4 * num3);
				intPtr = Marshal.AllocHGlobal(cb);
				intPtr2 = Marshal.AllocHGlobal(cb);
				num2 = SafeNativeMethods.Gdip.GdipGetLinePresetBlend(new HandleRef(this, base.NativeBrush), intPtr, intPtr2, num3);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				colorBlend = new ColorBlend(num3);
				int[] array = new int[num3];
				Marshal.Copy(intPtr, array, 0, num3);
				Marshal.Copy(intPtr2, colorBlend.Positions, 0, num3);
				colorBlend.Colors = new Color[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					colorBlend.Colors[i] = Color.FromArgb(array[i]);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			return colorBlend;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00028590 File Offset: 0x00026790
		private void _SetInterpolationColors(ColorBlend blend)
		{
			this.interpolationColorsWasSet = true;
			if (blend == null)
			{
				throw new ArgumentException(SR.GetString("InterpolationColorsCommon", new object[]
				{
					SR.GetString("InterpolationColorsInvalidColorBlendObject"),
					""
				}));
			}
			if (blend.Colors.Length < 2)
			{
				throw new ArgumentException(SR.GetString("InterpolationColorsCommon", new object[]
				{
					SR.GetString("InterpolationColorsInvalidColorBlendObject"),
					SR.GetString("InterpolationColorsLength")
				}));
			}
			if (blend.Colors.Length != blend.Positions.Length)
			{
				throw new ArgumentException(SR.GetString("InterpolationColorsCommon", new object[]
				{
					SR.GetString("InterpolationColorsInvalidColorBlendObject"),
					SR.GetString("InterpolationColorsLengthsDiffer")
				}));
			}
			if (blend.Positions[0] != 0f)
			{
				throw new ArgumentException(SR.GetString("InterpolationColorsCommon", new object[]
				{
					SR.GetString("InterpolationColorsInvalidColorBlendObject"),
					SR.GetString("InterpolationColorsInvalidStartPosition")
				}));
			}
			if (blend.Positions[blend.Positions.Length - 1] != 1f)
			{
				throw new ArgumentException(SR.GetString("InterpolationColorsCommon", new object[]
				{
					SR.GetString("InterpolationColorsInvalidColorBlendObject"),
					SR.GetString("InterpolationColorsInvalidEndPosition")
				}));
			}
			int num = blend.Colors.Length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				int cb = checked(4 * num);
				intPtr = Marshal.AllocHGlobal(cb);
				intPtr2 = Marshal.AllocHGlobal(cb);
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = blend.Colors[i].ToArgb();
				}
				Marshal.Copy(array, 0, intPtr, num);
				Marshal.Copy(blend.Positions, 0, intPtr2, num);
				int num2 = SafeNativeMethods.Gdip.GdipSetLinePresetBlend(new HandleRef(this, base.NativeBrush), new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000287AC File Offset: 0x000269AC
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x000287B4 File Offset: 0x000269B4
		public ColorBlend InterpolationColors
		{
			get
			{
				return this._GetInterpolationColors();
			}
			set
			{
				this._SetInterpolationColors(value);
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000287C0 File Offset: 0x000269C0
		private void _SetWrapMode(WrapMode wrapMode)
		{
			int num = SafeNativeMethods.Gdip.GdipSetLineWrapMode(new HandleRef(this, base.NativeBrush), (int)wrapMode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000287EC File Offset: 0x000269EC
		private WrapMode _GetWrapMode()
		{
			int result = 0;
			int num = SafeNativeMethods.Gdip.GdipGetLineWrapMode(new HandleRef(this, base.NativeBrush), out result);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return (WrapMode)result;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002881A File Offset: 0x00026A1A
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00028822 File Offset: 0x00026A22
		public WrapMode WrapMode
		{
			get
			{
				return this._GetWrapMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(WrapMode));
				}
				this._SetWrapMode(value);
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00028854 File Offset: 0x00026A54
		private void _SetTransform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipSetLineTransform(new HandleRef(this, base.NativeBrush), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00028898 File Offset: 0x00026A98
		private Matrix _GetTransform()
		{
			Matrix matrix = new Matrix();
			int num = SafeNativeMethods.Gdip.GdipGetLineTransform(new HandleRef(this, base.NativeBrush), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return matrix;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x000288D4 File Offset: 0x00026AD4
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x000288DC File Offset: 0x00026ADC
		public Matrix Transform
		{
			get
			{
				return this._GetTransform();
			}
			set
			{
				this._SetTransform(value);
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000288E8 File Offset: 0x00026AE8
		public void ResetTransform()
		{
			int num = SafeNativeMethods.Gdip.GdipResetLineTransform(new HandleRef(this, base.NativeBrush));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00028911 File Offset: 0x00026B11
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002891C File Offset: 0x00026B1C
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipMultiplyLineTransform(new HandleRef(this, base.NativeBrush), new HandleRef(matrix, matrix.nativeMatrix), order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00028960 File Offset: 0x00026B60
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002896C File Offset: 0x00026B6C
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslateLineTransform(new HandleRef(this, base.NativeBrush), dx, dy, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00028998 File Offset: 0x00026B98
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000289A4 File Offset: 0x00026BA4
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipScaleLineTransform(new HandleRef(this, base.NativeBrush), sx, sy, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000289D0 File Offset: 0x00026BD0
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000289DC File Offset: 0x00026BDC
		public void RotateTransform(float angle, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipRotateLineTransform(new HandleRef(this, base.NativeBrush), angle, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x040009D7 RID: 2519
		private bool interpolationColorsWasSet;
	}
}
