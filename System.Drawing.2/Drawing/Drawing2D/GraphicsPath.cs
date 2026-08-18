using System;
using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000C0 RID: 192
	public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00025EF1 File Offset: 0x000240F1
		public GraphicsPath() : this(FillMode.Alternate)
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00025EFC File Offset: 0x000240FC
		public GraphicsPath(FillMode fillMode)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreatePath((int)fillMode, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.nativePath = zero;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00025F2F File Offset: 0x0002412F
		public GraphicsPath(PointF[] pts, byte[] types) : this(pts, types, FillMode.Alternate)
		{
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00025F3C File Offset: 0x0002413C
		public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr zero = IntPtr.Zero;
			if (pts.Length != types.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = types.Length;
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(types, 0, intPtr2, num);
				int num2 = SafeNativeMethods.Gdip.GdipCreatePath2(new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num, (int)fillMode, out zero);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
			this.nativePath = zero;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00025FD8 File Offset: 0x000241D8
		public GraphicsPath(Point[] pts, byte[] types) : this(pts, types, FillMode.Alternate)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00025FE4 File Offset: 0x000241E4
		public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr zero = IntPtr.Zero;
			if (pts.Length != types.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = types.Length;
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(types, 0, intPtr2, num);
				int num2 = SafeNativeMethods.Gdip.GdipCreatePath2I(new HandleRef(null, intPtr), new HandleRef(null, intPtr2), num, (int)fillMode, out zero);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
			this.nativePath = zero;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00026080 File Offset: 0x00024280
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipClonePath(new HandleRef(this, this.nativePath), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new GraphicsPath(zero, 0);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000260B8 File Offset: 0x000242B8
		private GraphicsPath(IntPtr nativePath, int extra)
		{
			if (nativePath == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativePath");
			}
			this.nativePath = nativePath;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000260DF File Offset: 0x000242DF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000260F0 File Offset: 0x000242F0
		private void Dispose(bool disposing)
		{
			if (this.nativePath != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeletePath(new HandleRef(this, this.nativePath));
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
					this.nativePath = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00026158 File Offset: 0x00024358
		~GraphicsPath()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00026188 File Offset: 0x00024388
		public void Reset()
		{
			int num = SafeNativeMethods.Gdip.GdipResetPath(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000261B4 File Offset: 0x000243B4
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x000261E4 File Offset: 0x000243E4
		public FillMode FillMode
		{
			get
			{
				int result = 0;
				int num = SafeNativeMethods.Gdip.GdipGetPathFillMode(new HandleRef(this, this.nativePath), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return (FillMode)result;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FillMode));
				}
				int num = SafeNativeMethods.Gdip.GdipSetPathFillMode(new HandleRef(this, this.nativePath), (int)value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00026234 File Offset: 0x00024434
		private PathData _GetPathData()
		{
			int num = Marshal.SizeOf(typeof(GPPOINTF));
			int pointCount = this.PointCount;
			PathData pathData = new PathData();
			pathData.Types = new byte[pointCount];
			IntPtr intPtr = Marshal.AllocHGlobal(3 * IntPtr.Size);
			IntPtr intPtr2 = Marshal.AllocHGlobal(checked(num * pointCount));
			try
			{
				GCHandle gchandle = GCHandle.Alloc(pathData.Types, GCHandleType.Pinned);
				try
				{
					IntPtr intPtr3 = gchandle.AddrOfPinnedObject();
					Marshal.StructureToPtr(pointCount, intPtr, false);
					Marshal.StructureToPtr(intPtr2, (IntPtr)((long)intPtr + (long)IntPtr.Size), false);
					Marshal.StructureToPtr(intPtr3, (IntPtr)((long)intPtr + (long)(2 * IntPtr.Size)), false);
					int num2 = SafeNativeMethods.Gdip.GdipGetPathData(new HandleRef(this, this.nativePath), intPtr);
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
					pathData.Points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr2, pointCount);
				}
				finally
				{
					gchandle.Free();
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
			return pathData;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00026348 File Offset: 0x00024548
		public PathData PathData
		{
			get
			{
				return this._GetPathData();
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00026350 File Offset: 0x00024550
		public void StartFigure()
		{
			int num = SafeNativeMethods.Gdip.GdipStartPathFigure(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002637C File Offset: 0x0002457C
		public void CloseFigure()
		{
			int num = SafeNativeMethods.Gdip.GdipClosePathFigure(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000263A8 File Offset: 0x000245A8
		public void CloseAllFigures()
		{
			int num = SafeNativeMethods.Gdip.GdipClosePathFigures(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000263D4 File Offset: 0x000245D4
		public void SetMarkers()
		{
			int num = SafeNativeMethods.Gdip.GdipSetPathMarker(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00026400 File Offset: 0x00024600
		public void ClearMarkers()
		{
			int num = SafeNativeMethods.Gdip.GdipClearPathMarkers(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002642C File Offset: 0x0002462C
		public void Reverse()
		{
			int num = SafeNativeMethods.Gdip.GdipReversePath(new HandleRef(this, this.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00026458 File Offset: 0x00024658
		public PointF GetLastPoint()
		{
			GPPOINTF gppointf = new GPPOINTF();
			int num = SafeNativeMethods.Gdip.GdipGetPathLastPoint(new HandleRef(this, this.nativePath), gppointf);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gppointf.ToPoint();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002648E File Offset: 0x0002468E
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(new PointF(x, y), null);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002649E File Offset: 0x0002469E
		public bool IsVisible(PointF point)
		{
			return this.IsVisible(point, null);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000264A8 File Offset: 0x000246A8
		public bool IsVisible(float x, float y, Graphics graphics)
		{
			return this.IsVisible(new PointF(x, y), graphics);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000264B8 File Offset: 0x000246B8
		public bool IsVisible(PointF pt, Graphics graphics)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsVisiblePathPoint(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002650B File Offset: 0x0002470B
		public bool IsVisible(int x, int y)
		{
			return this.IsVisible(new Point(x, y), null);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002651B File Offset: 0x0002471B
		public bool IsVisible(Point point)
		{
			return this.IsVisible(point, null);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00026525 File Offset: 0x00024725
		public bool IsVisible(int x, int y, Graphics graphics)
		{
			return this.IsVisible(new Point(x, y), graphics);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00026538 File Offset: 0x00024738
		public bool IsVisible(Point pt, Graphics graphics)
		{
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsVisiblePathPointI(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002658B File Offset: 0x0002478B
		public bool IsOutlineVisible(float x, float y, Pen pen)
		{
			return this.IsOutlineVisible(new PointF(x, y), pen, null);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002659C File Offset: 0x0002479C
		public bool IsOutlineVisible(PointF point, Pen pen)
		{
			return this.IsOutlineVisible(point, pen, null);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000265A7 File Offset: 0x000247A7
		public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(new PointF(x, y), pen, graphics);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000265BC File Offset: 0x000247BC
		public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPoint(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(pen, pen.NativePen), new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00026629 File Offset: 0x00024829
		public bool IsOutlineVisible(int x, int y, Pen pen)
		{
			return this.IsOutlineVisible(new Point(x, y), pen, null);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002663A File Offset: 0x0002483A
		public bool IsOutlineVisible(Point point, Pen pen)
		{
			return this.IsOutlineVisible(point, pen, null);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00026645 File Offset: 0x00024845
		public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(new Point(x, y), pen, graphics);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00026658 File Offset: 0x00024858
		public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			int num2;
			int num = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPointI(new HandleRef(this, this.nativePath), pt.X, pt.Y, new HandleRef(pen, pen.NativePen), new HandleRef(graphics, (graphics != null) ? graphics.NativeGraphics : IntPtr.Zero), out num2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return num2 != 0;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000266C5 File Offset: 0x000248C5
		public void AddLine(PointF pt1, PointF pt2)
		{
			this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000266EC File Offset: 0x000248EC
		public void AddLine(float x1, float y1, float x2, float y2)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathLine(new HandleRef(this, this.nativePath), x1, y1, x2, y2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002671C File Offset: 0x0002491C
		public void AddLines(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathLine2(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00026780 File Offset: 0x00024980
		public void AddLine(Point pt1, Point pt2)
		{
			this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000267A4 File Offset: 0x000249A4
		public void AddLine(int x1, int y1, int x2, int y2)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathLineI(new HandleRef(this, this.nativePath), x1, y1, x2, y2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000267D4 File Offset: 0x000249D4
		public void AddLines(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathLine2I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00026838 File Offset: 0x00024A38
		public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
		{
			this.AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00026860 File Offset: 0x00024A60
		public void AddArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathArc(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00026892 File Offset: 0x00024A92
		public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
		{
			this.AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000268B8 File Offset: 0x00024AB8
		public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathArcI(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000268EC File Offset: 0x00024AEC
		public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			this.AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00026938 File Offset: 0x00024B38
		public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathBezier(new HandleRef(this, this.nativePath), x1, y1, x2, y2, x3, y3, x4, y4);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00026970 File Offset: 0x00024B70
		public void AddBeziers(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathBeziers(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000269D4 File Offset: 0x00024BD4
		public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
		{
			this.AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00026A20 File Offset: 0x00024C20
		public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathBezierI(new HandleRef(this, this.nativePath), x1, y1, x2, y2, x3, y3, x4, y4);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00026A58 File Offset: 0x00024C58
		public void AddBeziers(params Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathBeziersI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00026ABC File Offset: 0x00024CBC
		public void AddCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00026B20 File Offset: 0x00024D20
		public void AddCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve2(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00026B84 File Offset: 0x00024D84
		public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve3(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, offset, numberOfSegments, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00026BEC File Offset: 0x00024DEC
		public void AddCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurveI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00026C50 File Offset: 0x00024E50
		public void AddCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve2I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00026CB4 File Offset: 0x00024EB4
		public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathCurve3I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, offset, numberOfSegments, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00026D1C File Offset: 0x00024F1C
		public void AddClosedCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurve(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00026D80 File Offset: 0x00024F80
		public void AddClosedCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00026DE4 File Offset: 0x00024FE4
		public void AddClosedCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurveI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00026E48 File Offset: 0x00025048
		public void AddClosedCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2I(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length, tension);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00026EAC File Offset: 0x000250AC
		public void AddRectangle(RectangleF rect)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathRectangle(new HandleRef(this, this.nativePath), rect.X, rect.Y, rect.Width, rect.Height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00026EF4 File Offset: 0x000250F4
		public void AddRectangles(RectangleF[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathRectangles(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), rects.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00026F58 File Offset: 0x00025158
		public void AddRectangle(Rectangle rect)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathRectangleI(new HandleRef(this, this.nativePath), rect.X, rect.Y, rect.Width, rect.Height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00026FA0 File Offset: 0x000251A0
		public void AddRectangles(Rectangle[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathRectanglesI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), rects.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00027004 File Offset: 0x00025204
		public void AddEllipse(RectangleF rect)
		{
			this.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00027028 File Offset: 0x00025228
		public void AddEllipse(float x, float y, float width, float height)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathEllipse(new HandleRef(this, this.nativePath), x, y, width, height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00027056 File Offset: 0x00025256
		public void AddEllipse(Rectangle rect)
		{
			this.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002707C File Offset: 0x0002527C
		public void AddEllipse(int x, int y, int width, int height)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathEllipseI(new HandleRef(this, this.nativePath), x, y, width, height);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000270AA File Offset: 0x000252AA
		public void AddPie(Rectangle rect, float startAngle, float sweepAngle)
		{
			this.AddPie(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000270D0 File Offset: 0x000252D0
		public void AddPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathPie(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00027104 File Offset: 0x00025304
		public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			int num = SafeNativeMethods.Gdip.GdipAddPathPieI(new HandleRef(this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00027138 File Offset: 0x00025338
		public void AddPolygon(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathPolygon(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002719C File Offset: 0x0002539C
		public void AddPolygon(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipAddPathPolygonI(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), points.Length);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00027200 File Offset: 0x00025400
		public void AddPath(GraphicsPath addingPath, bool connect)
		{
			if (addingPath == null)
			{
				throw new ArgumentNullException("addingPath");
			}
			int num = SafeNativeMethods.Gdip.GdipAddPathPath(new HandleRef(this, this.nativePath), new HandleRef(addingPath, addingPath.nativePath), connect);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00027244 File Offset: 0x00025444
		public void AddString(string s, FontFamily family, int style, float emSize, PointF origin, StringFormat format)
		{
			GPRECTF gprectf = new GPRECTF(origin.X, origin.Y, 0f, 0f);
			int num = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprectf, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000272C8 File Offset: 0x000254C8
		public void AddString(string s, FontFamily family, int style, float emSize, Point origin, StringFormat format)
		{
			GPRECT gprect = new GPRECT(origin.X, origin.Y, 0, 0);
			int num = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprect, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00027344 File Offset: 0x00025544
		public void AddString(string s, FontFamily family, int style, float emSize, RectangleF layoutRect, StringFormat format)
		{
			GPRECTF gprectf = new GPRECTF(layoutRect);
			int num = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprectf, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000273B4 File Offset: 0x000255B4
		public void AddString(string s, FontFamily family, int style, float emSize, Rectangle layoutRect, StringFormat format)
		{
			GPRECT gprect = new GPRECT(layoutRect);
			int num = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef(this, this.nativePath), s, s.Length, new HandleRef(family, (family != null) ? family.NativeFamily : IntPtr.Zero), style, emSize, ref gprect, new HandleRef(format, (format != null) ? format.nativeFormat : IntPtr.Zero));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00027424 File Offset: 0x00025624
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			if (matrix.nativeMatrix == IntPtr.Zero)
			{
				return;
			}
			int num = SafeNativeMethods.Gdip.GdipTransformPath(new HandleRef(this, this.nativePath), new HandleRef(matrix, matrix.nativeMatrix));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002747A File Offset: 0x0002567A
		public RectangleF GetBounds()
		{
			return this.GetBounds(null);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00027483 File Offset: 0x00025683
		public RectangleF GetBounds(Matrix matrix)
		{
			return this.GetBounds(matrix, null);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00027490 File Offset: 0x00025690
		public RectangleF GetBounds(Matrix matrix, Pen pen)
		{
			GPRECTF gprectf = default(GPRECTF);
			IntPtr handle = IntPtr.Zero;
			IntPtr handle2 = IntPtr.Zero;
			if (matrix != null)
			{
				handle = matrix.nativeMatrix;
			}
			if (pen != null)
			{
				handle2 = pen.NativePen;
			}
			int num = SafeNativeMethods.Gdip.GdipGetPathWorldBounds(new HandleRef(this, this.nativePath), ref gprectf, new HandleRef(matrix, handle), new HandleRef(pen, handle2));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return gprectf.ToRectangleF();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000274F8 File Offset: 0x000256F8
		public void Flatten()
		{
			this.Flatten(null);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00027501 File Offset: 0x00025701
		public void Flatten(Matrix matrix)
		{
			this.Flatten(matrix, 0.25f);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00027510 File Offset: 0x00025710
		public void Flatten(Matrix matrix, float flatness)
		{
			int num = SafeNativeMethods.Gdip.GdipFlattenPath(new HandleRef(this, this.nativePath), new HandleRef(matrix, (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix), flatness);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00027550 File Offset: 0x00025750
		public void Widen(Pen pen)
		{
			float flatness = 0.6666667f;
			this.Widen(pen, null, flatness);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002756C File Offset: 0x0002576C
		public void Widen(Pen pen, Matrix matrix)
		{
			float flatness = 0.6666667f;
			this.Widen(pen, matrix, flatness);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00027588 File Offset: 0x00025788
		public void Widen(Pen pen, Matrix matrix, float flatness)
		{
			IntPtr handle;
			if (matrix == null)
			{
				handle = IntPtr.Zero;
			}
			else
			{
				handle = matrix.nativeMatrix;
			}
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			int num;
			SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef(this, this.nativePath), out num);
			if (num == 0)
			{
				return;
			}
			int num2 = SafeNativeMethods.Gdip.GdipWidenPath(new HandleRef(this, this.nativePath), new HandleRef(pen, pen.NativePen), new HandleRef(matrix, handle), flatness);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000275FD File Offset: 0x000257FD
		public void Warp(PointF[] destPoints, RectangleF srcRect)
		{
			this.Warp(destPoints, srcRect, null);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00027608 File Offset: 0x00025808
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix)
		{
			this.Warp(destPoints, srcRect, matrix, WarpMode.Perspective);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00027614 File Offset: 0x00025814
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode)
		{
			this.Warp(destPoints, srcRect, matrix, warpMode, 0.25f);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00027628 File Offset: 0x00025828
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode, float flatness)
		{
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			IntPtr intPtr = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
			try
			{
				int num = SafeNativeMethods.Gdip.GdipWarpPath(new HandleRef(this, this.nativePath), new HandleRef(matrix, (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix), new HandleRef(null, intPtr), destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, warpMode, flatness);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000276C0 File Offset: 0x000258C0
		public int PointCount
		{
			get
			{
				int result = 0;
				int num = SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef(this, this.nativePath), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x000276F0 File Offset: 0x000258F0
		public byte[] PathTypes
		{
			get
			{
				int pointCount = this.PointCount;
				byte[] array = new byte[pointCount];
				int num = SafeNativeMethods.Gdip.GdipGetPathTypes(new HandleRef(this, this.nativePath), array, pointCount);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return array;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002772C File Offset: 0x0002592C
		public PointF[] PathPoints
		{
			get
			{
				int pointCount = this.PointCount;
				int num = Marshal.SizeOf(typeof(GPPOINTF));
				IntPtr intPtr = Marshal.AllocHGlobal(checked(pointCount * num));
				PointF[] result;
				try
				{
					int num2 = SafeNativeMethods.Gdip.GdipGetPathPoints(new HandleRef(this, this.nativePath), new HandleRef(null, intPtr), pointCount);
					if (num2 != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num2);
					}
					PointF[] array = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr, pointCount);
					result = array;
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return result;
			}
		}

		// Token: 0x04000991 RID: 2449
		internal IntPtr nativePath;
	}
}
