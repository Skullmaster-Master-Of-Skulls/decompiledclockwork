using System;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000CA RID: 202
	public sealed class Matrix : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x00028A08 File Offset: 0x00026C08
		public Matrix()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateMatrix(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.nativeMatrix = zero;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00028A3C File Offset: 0x00026C3C
		public Matrix(float m11, float m12, float m21, float m22, float dx, float dy)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateMatrix2(m11, m12, m21, m22, dx, dy, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.nativeMatrix = zero;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00028A78 File Offset: 0x00026C78
		public Matrix(RectangleF rect, PointF[] plgpts)
		{
			if (plgpts == null)
			{
				throw new ArgumentNullException("plgpts");
			}
			if (plgpts.Length != 3)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(plgpts);
			try
			{
				IntPtr zero = IntPtr.Zero;
				GPRECTF gprectf = new GPRECTF(rect);
				int num = SafeNativeMethods.Gdip.GdipCreateMatrix3(ref gprectf, new HandleRef(null, intPtr), out zero);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				this.nativeMatrix = zero;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00028AF8 File Offset: 0x00026CF8
		public Matrix(Rectangle rect, Point[] plgpts)
		{
			if (plgpts == null)
			{
				throw new ArgumentNullException("plgpts");
			}
			if (plgpts.Length != 3)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(plgpts);
			try
			{
				IntPtr zero = IntPtr.Zero;
				GPRECT gprect = new GPRECT(rect);
				int num = SafeNativeMethods.Gdip.GdipCreateMatrix3I(ref gprect, new HandleRef(null, intPtr), out zero);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				this.nativeMatrix = zero;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00028B78 File Offset: 0x00026D78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00028B87 File Offset: 0x00026D87
		private void Dispose(bool disposing)
		{
			if (this.nativeMatrix != IntPtr.Zero)
			{
				SafeNativeMethods.Gdip.GdipDeleteMatrix(new HandleRef(this, this.nativeMatrix));
				this.nativeMatrix = IntPtr.Zero;
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00028BB8 File Offset: 0x00026DB8
		~Matrix()
		{
			this.Dispose(false);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00028BE8 File Offset: 0x00026DE8
		public Matrix Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneMatrix(new HandleRef(this, this.nativeMatrix), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Matrix(zero);
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00028C20 File Offset: 0x00026E20
		public float[] Elements
		{
			get
			{
				IntPtr intPtr = Marshal.AllocHGlobal(48);
				float[] array;
				try
				{
					int num = SafeNativeMethods.Gdip.GdipGetMatrixElements(new HandleRef(this, this.nativeMatrix), intPtr);
					if (num != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num);
					}
					array = new float[6];
					Marshal.Copy(intPtr, array, 0, 6);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00028C7C File Offset: 0x00026E7C
		public float OffsetX
		{
			get
			{
				return this.Elements[4];
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00028C86 File Offset: 0x00026E86
		public float OffsetY
		{
			get
			{
				return this.Elements[5];
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00028C90 File Offset: 0x00026E90
		public void Reset()
		{
			int num = SafeNativeMethods.Gdip.GdipSetMatrixElements(new HandleRef(this, this.nativeMatrix), 1f, 0f, 0f, 1f, 0f, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00028CD7 File Offset: 0x00026ED7
		public void Multiply(Matrix matrix)
		{
			this.Multiply(matrix, MatrixOrder.Prepend);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00028CE4 File Offset: 0x00026EE4
		public void Multiply(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipMultiplyMatrix(new HandleRef(this, this.nativeMatrix), new HandleRef(matrix, matrix.nativeMatrix), order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00028D28 File Offset: 0x00026F28
		public void Translate(float offsetX, float offsetY)
		{
			this.Translate(offsetX, offsetY, MatrixOrder.Prepend);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00028D34 File Offset: 0x00026F34
		public void Translate(float offsetX, float offsetY, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef(this, this.nativeMatrix), offsetX, offsetY, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00028D60 File Offset: 0x00026F60
		public void Scale(float scaleX, float scaleY)
		{
			this.Scale(scaleX, scaleY, MatrixOrder.Prepend);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00028D6C File Offset: 0x00026F6C
		public void Scale(float scaleX, float scaleY, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipScaleMatrix(new HandleRef(this, this.nativeMatrix), scaleX, scaleY, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00028D98 File Offset: 0x00026F98
		public void Rotate(float angle)
		{
			this.Rotate(angle, MatrixOrder.Prepend);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00028DA4 File Offset: 0x00026FA4
		public void Rotate(float angle, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipRotateMatrix(new HandleRef(this, this.nativeMatrix), angle, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00028DCF File Offset: 0x00026FCF
		public void RotateAt(float angle, PointF point)
		{
			this.RotateAt(angle, point, MatrixOrder.Prepend);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00028DDC File Offset: 0x00026FDC
		public void RotateAt(float angle, PointF point, MatrixOrder order)
		{
			int num;
			if (order == MatrixOrder.Prepend)
			{
				num = SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef(this, this.nativeMatrix), point.X, point.Y, order);
				num |= SafeNativeMethods.Gdip.GdipRotateMatrix(new HandleRef(this, this.nativeMatrix), angle, order);
				num |= SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef(this, this.nativeMatrix), -point.X, -point.Y, order);
			}
			else
			{
				num = SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef(this, this.nativeMatrix), -point.X, -point.Y, order);
				num |= SafeNativeMethods.Gdip.GdipRotateMatrix(new HandleRef(this, this.nativeMatrix), angle, order);
				num |= SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef(this, this.nativeMatrix), point.X, point.Y, order);
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00028EB0 File Offset: 0x000270B0
		public void Shear(float shearX, float shearY)
		{
			int num = SafeNativeMethods.Gdip.GdipShearMatrix(new HandleRef(this, this.nativeMatrix), shearX, shearY, MatrixOrder.Prepend);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00028EDC File Offset: 0x000270DC
		public void Shear(float shearX, float shearY, MatrixOrder order)
		{
			int num = SafeNativeMethods.Gdip.GdipShearMatrix(new HandleRef(this, this.nativeMatrix), shearX, shearY, order);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00028F08 File Offset: 0x00027108
		public void Invert()
		{
			int num = SafeNativeMethods.Gdip.GdipInvertMatrix(new HandleRef(this, this.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00028F34 File Offset: 0x00027134
		public void TransformPoints(PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipTransformMatrixPoints(new HandleRef(this, this.nativeMatrix), new HandleRef(null, intPtr), pts.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				PointF[] array = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr, pts.Length);
				for (int i = 0; i < pts.Length; i++)
				{
					pts[i] = array[i];
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00028FBC File Offset: 0x000271BC
		public void TransformPoints(Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipTransformMatrixPointsI(new HandleRef(this, this.nativeMatrix), new HandleRef(null, intPtr), pts.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				Point[] array = SafeNativeMethods.Gdip.ConvertGPPOINTArray(intPtr, pts.Length);
				for (int i = 0; i < pts.Length; i++)
				{
					pts[i] = array[i];
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00029044 File Offset: 0x00027244
		public void TransformVectors(PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipVectorTransformMatrixPoints(new HandleRef(this, this.nativeMatrix), new HandleRef(null, intPtr), pts.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				PointF[] array = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr, pts.Length);
				for (int i = 0; i < pts.Length; i++)
				{
					pts[i] = array[i];
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000290CC File Offset: 0x000272CC
		public void VectorTransformPoints(Point[] pts)
		{
			this.TransformVectors(pts);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000290D8 File Offset: 0x000272D8
		public void TransformVectors(Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipVectorTransformMatrixPointsI(new HandleRef(this, this.nativeMatrix), new HandleRef(null, intPtr), pts.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				Point[] array = SafeNativeMethods.Gdip.ConvertGPPOINTArray(intPtr, pts.Length);
				for (int i = 0; i < pts.Length; i++)
				{
					pts[i] = array[i];
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00029160 File Offset: 0x00027360
		public bool IsInvertible
		{
			get
			{
				int num2;
				int num = SafeNativeMethods.Gdip.GdipIsMatrixInvertible(new HandleRef(this, this.nativeMatrix), out num2);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return num2 != 0;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00029190 File Offset: 0x00027390
		public bool IsIdentity
		{
			get
			{
				int num2;
				int num = SafeNativeMethods.Gdip.GdipIsMatrixIdentity(new HandleRef(this, this.nativeMatrix), out num2);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return num2 != 0;
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000291C0 File Offset: 0x000273C0
		public override bool Equals(object obj)
		{
			Matrix matrix = obj as Matrix;
			if (matrix == null)
			{
				return false;
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsMatrixEqual(new HandleRef(this, this.nativeMatrix), new HandleRef(matrix, matrix.nativeMatrix), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00029207 File Offset: 0x00027407
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002920F File Offset: 0x0002740F
		internal Matrix(IntPtr nativeMatrix)
		{
			this.SetNativeMatrix(nativeMatrix);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002921E File Offset: 0x0002741E
		internal void SetNativeMatrix(IntPtr nativeMatrix)
		{
			this.nativeMatrix = nativeMatrix;
		}

		// Token: 0x040009EE RID: 2542
		internal IntPtr nativeMatrix;
	}
}
