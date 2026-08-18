using System;
using System.Drawing.Drawing2D;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200002E RID: 46
	public sealed class Region : MarshalByRefObject, IDisposable
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00015FD0 File Offset: 0x000141D0
		public Region()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegion(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016004 File Offset: 0x00014204
		public Region(RectangleF rect)
		{
			IntPtr zero = IntPtr.Zero;
			GPRECTF gprectf = rect.ToGPRECTF();
			int num = SafeNativeMethods.Gdip.GdipCreateRegionRect(ref gprectf, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016040 File Offset: 0x00014240
		public Region(Rectangle rect)
		{
			IntPtr zero = IntPtr.Zero;
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCreateRegionRectI(ref gprect, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001607C File Offset: 0x0001427C
		public Region(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegionPath(new HandleRef(path, path.nativePath), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000160C8 File Offset: 0x000142C8
		public Region(RegionData rgnData)
		{
			if (rgnData == null)
			{
				throw new ArgumentNullException("rgnData");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegionRgnData(rgnData.Data, rgnData.Data.Length, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeRegion(zero);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016116 File Offset: 0x00014316
		internal Region(IntPtr nativeRegion)
		{
			this.SetNativeRegion(nativeRegion);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016128 File Offset: 0x00014328
		public static Region FromHrgn(IntPtr hrgn)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateRegionHrgn(new HandleRef(null, hrgn), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Region(zero);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016164 File Offset: 0x00014364
		private void SetNativeRegion(IntPtr nativeRegion)
		{
			if (nativeRegion == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeRegion");
			}
			this.nativeRegion = nativeRegion;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00016188 File Offset: 0x00014388
		public Region Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneRegion(new HandleRef(this, this.nativeRegion), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new Region(zero);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000161BF File Offset: 0x000143BF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000161D0 File Offset: 0x000143D0
		private void Dispose(bool disposing)
		{
			if (this.nativeRegion != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeleteRegion(new HandleRef(this, this.nativeRegion));
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
					this.nativeRegion = IntPtr.Zero;
				}
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00016238 File Offset: 0x00014438
		~Region()
		{
			this.Dispose(false);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00016268 File Offset: 0x00014468
		public void MakeInfinite()
		{
			int num = SafeNativeMethods.Gdip.GdipSetInfinite(new HandleRef(this, this.nativeRegion));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016294 File Offset: 0x00014494
		public void MakeEmpty()
		{
			int num = SafeNativeMethods.Gdip.GdipSetEmpty(new HandleRef(this, this.nativeRegion));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000162C0 File Offset: 0x000144C0
		public void Intersect(RectangleF rect)
		{
			GPRECTF gprectf = rect.ToGPRECTF();
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000162F4 File Offset: 0x000144F4
		public void Intersect(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00016328 File Offset: 0x00014528
		public void Intersect(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001636C File Offset: 0x0001456C
		public void Intersect(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Intersect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000163B0 File Offset: 0x000145B0
		public void ReleaseHrgn(IntPtr regionHandle)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			if (regionHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("regionHandle");
			}
			SafeNativeMethods.IntDeleteObject(new HandleRef(this, regionHandle));
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000163E4 File Offset: 0x000145E4
		public void Union(RectangleF rect)
		{
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00016418 File Offset: 0x00014618
		public void Union(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001644C File Offset: 0x0001464C
		public void Union(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016490 File Offset: 0x00014690
		public void Union(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Union);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000164D4 File Offset: 0x000146D4
		public void Xor(RectangleF rect)
		{
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00016508 File Offset: 0x00014708
		public void Xor(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001653C File Offset: 0x0001473C
		public void Xor(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00016580 File Offset: 0x00014780
		public void Xor(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Xor);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000165C4 File Offset: 0x000147C4
		public void Exclude(RectangleF rect)
		{
			GPRECTF gprectf = new GPRECTF(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000165F8 File Offset: 0x000147F8
		public void Exclude(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001662C File Offset: 0x0001482C
		public void Exclude(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00016670 File Offset: 0x00014870
		public void Exclude(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Exclude);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000166B4 File Offset: 0x000148B4
		public void Complement(RectangleF rect)
		{
			GPRECTF gprectf = rect.ToGPRECTF();
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef(this, this.nativeRegion), ref gprectf, CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000166E8 File Offset: 0x000148E8
		public void Complement(Rectangle rect)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef(this, this.nativeRegion), ref gprect, CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001671C File Offset: 0x0001491C
		public void Complement(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef(this, this.nativeRegion), new HandleRef(path, path.nativePath), CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00016760 File Offset: 0x00014960
		public void Complement(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num = SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), CombineMode.Complement);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000167A4 File Offset: 0x000149A4
		public void Translate(float dx, float dy)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslateRegion(new HandleRef(this, this.nativeRegion), dx, dy);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000167D0 File Offset: 0x000149D0
		public void Translate(int dx, int dy)
		{
			int num = SafeNativeMethods.Gdip.GdipTranslateRegionI(new HandleRef(this, this.nativeRegion), dx, dy);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000167FC File Offset: 0x000149FC
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = SafeNativeMethods.Gdip.GdipTransformRegion(new HandleRef(this, this.nativeRegion), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00016840 File Offset: 0x00014A40
		public RectangleF GetBounds(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			GPRECTF gprectf = default(GPRECTF);
			int num = SafeNativeMethods.Gdip.GdipGetRegionBounds(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), ref gprectf);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gprectf.ToRectangleF();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00016894 File Offset: 0x00014A94
		public IntPtr GetHrgn(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipGetRegionHRgn(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000168E0 File Offset: 0x00014AE0
		public bool IsEmpty(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsEmptyRegion(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001692C File Offset: 0x00014B2C
		public bool IsInfinite(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsInfiniteRegion(new HandleRef(this, this.nativeRegion), new HandleRef(g, g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00016978 File Offset: 0x00014B78
		public bool Equals(Region region, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsEqualRegion(new HandleRef(this, this.nativeRegion), new HandleRef(region, region.nativeRegion), new HandleRef(g, g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000169DC File Offset: 0x00014BDC
		public RegionData GetRegionData()
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetRegionDataSize(new HandleRef(this, this.nativeRegion), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			if (num == 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			num2 = SafeNativeMethods.Gdip.GdipGetRegionData(new HandleRef(this, this.nativeRegion), array, num, out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return new RegionData(array);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00016A3B File Offset: 0x00014C3B
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(new PointF(x, y), null);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00016A4B File Offset: 0x00014C4B
		public bool IsVisible(PointF point)
		{
			return this.IsVisible(point, null);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00016A55 File Offset: 0x00014C55
		public bool IsVisible(float x, float y, Graphics g)
		{
			return this.IsVisible(new PointF(x, y), g);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00016A68 File Offset: 0x00014C68
		public bool IsVisible(PointF point, Graphics g)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsVisibleRegionPoint(new HandleRef(this, this.nativeRegion), point.X, point.Y, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00016ABB File Offset: 0x00014CBB
		public bool IsVisible(float x, float y, float width, float height)
		{
			return this.IsVisible(new RectangleF(x, y, width, height), null);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00016ACE File Offset: 0x00014CCE
		public bool IsVisible(RectangleF rect)
		{
			return this.IsVisible(rect, null);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public bool IsVisible(float x, float y, float width, float height, Graphics g)
		{
			return this.IsVisible(new RectangleF(x, y, width, height), g);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00016AEC File Offset: 0x00014CEC
		public bool IsVisible(RectangleF rect, Graphics g)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipIsVisibleRegionRect(new HandleRef(this, this.nativeRegion), rect.X, rect.Y, rect.Width, rect.Height, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num != 0;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00016B4F File Offset: 0x00014D4F
		public bool IsVisible(int x, int y, Graphics g)
		{
			return this.IsVisible(new Point(x, y), g);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00016B5F File Offset: 0x00014D5F
		public bool IsVisible(Point point)
		{
			return this.IsVisible(point, null);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00016B6C File Offset: 0x00014D6C
		public bool IsVisible(Point point, Graphics g)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipIsVisibleRegionPointI(new HandleRef(this, this.nativeRegion), point.X, point.Y, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num != 0;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00016BC1 File Offset: 0x00014DC1
		public bool IsVisible(int x, int y, int width, int height)
		{
			return this.IsVisible(new Rectangle(x, y, width, height), null);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016BD4 File Offset: 0x00014DD4
		public bool IsVisible(Rectangle rect)
		{
			return this.IsVisible(rect, null);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00016BDE File Offset: 0x00014DDE
		public bool IsVisible(int x, int y, int width, int height, Graphics g)
		{
			return this.IsVisible(new Rectangle(x, y, width, height), g);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public bool IsVisible(Rectangle rect, Graphics g)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipIsVisibleRegionRectI(new HandleRef(this, this.nativeRegion), rect.X, rect.Y, rect.Width, rect.Height, new HandleRef(g, (g == null) ? IntPtr.Zero : g.NativeGraphics), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return num != 0;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00016C58 File Offset: 0x00014E58
		public RectangleF[] GetRegionScans(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetRegionScansCount(new HandleRef(this, this.nativeRegion), out num, new HandleRef(matrix, matrix.nativeMatrix));
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			int num3 = Marshal.SizeOf(typeof(GPRECTF));
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num3 * num));
			RectangleF[] array;
			try
			{
				num2 = SafeNativeMethods.Gdip.GdipGetRegionScans(new HandleRef(this, this.nativeRegion), intPtr, out num, new HandleRef(matrix, matrix.nativeMatrix));
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				GPRECTF gprectf = default(GPRECTF);
				array = new RectangleF[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ((GPRECTF)UnsafeNativeMethods.PtrToStructure((IntPtr)(checked((long)intPtr + unchecked((long)(checked(num3 * i))))), typeof(GPRECTF))).ToRectangleF();
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return array;
		}

		// Token: 0x04000305 RID: 773
		internal IntPtr nativeRegion;
	}
}
