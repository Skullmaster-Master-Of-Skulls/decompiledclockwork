using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Telerik.Charting
{
	// Token: 0x020016DA RID: 5850
	internal class ChartGraphics : IDisposable
	{
		// Token: 0x0600E1E3 RID: 57827 RVA: 0x0032379E File Offset: 0x0032199E
		internal void TranslateTransformDefault()
		{
			this.chartGraphicsGraphics.TranslateTransform(this.translateTransformDefaultX, this.translateTransformDefaultY, this.translateTransformDefaultOrder);
		}

		// Token: 0x0600E1E4 RID: 57828 RVA: 0x003237BD File Offset: 0x003219BD
		internal void DropTranslateTransformDefault()
		{
			this.chartGraphicsGraphics.TranslateTransform(-this.translateTransformDefaultX, -this.translateTransformDefaultY, this.translateTransformDefaultOrder);
		}

		// Token: 0x17004541 RID: 17729
		// (get) Token: 0x0600E1E5 RID: 57829 RVA: 0x003237DE File Offset: 0x003219DE
		// (set) Token: 0x0600E1E6 RID: 57830 RVA: 0x003237E6 File Offset: 0x003219E6
		internal Graphics Graphics
		{
			get
			{
				return this.chartGraphicsGraphics;
			}
			set
			{
				this.chartGraphicsGraphics = value;
			}
		}

		// Token: 0x0600E1E7 RID: 57831 RVA: 0x003237EF File Offset: 0x003219EF
		internal ChartGraphics(Graphics graphics)
		{
			this.chartGraphicsGraphics = graphics;
			this.translateTransformDefaultX = 0f;
			this.translateTransformDefaultY = 0f;
			this.translateTransformDefaultOrder = MatrixOrder.Prepend;
		}

		// Token: 0x0600E1E8 RID: 57832 RVA: 0x0032381B File Offset: 0x00321A1B
		internal void TranslateTransformDefault(float dx, float dy)
		{
			this.translateTransformDefaultX = dx;
			this.translateTransformDefaultY = dy;
			this.TranslateTransformDefault();
		}

		// Token: 0x0600E1E9 RID: 57833 RVA: 0x00323831 File Offset: 0x00321A31
		internal void TranslateTransformDefault(float dx, float dy, MatrixOrder order)
		{
			this.translateTransformDefaultX = dx;
			this.translateTransformDefaultY = dy;
			this.translateTransformDefaultOrder = order;
			this.TranslateTransformDefault();
		}

		// Token: 0x0600E1EA RID: 57834 RVA: 0x0032384E File Offset: 0x00321A4E
		internal void TranslateTransformDefaultAdd(float dx, float dy)
		{
			this.translateTransformDefaultX += dx;
			this.translateTransformDefaultY += dy;
			this.ResetTransform();
		}

		// Token: 0x17004542 RID: 17730
		// (get) Token: 0x0600E1EB RID: 57835 RVA: 0x00323872 File Offset: 0x00321A72
		// (set) Token: 0x0600E1EC RID: 57836 RVA: 0x0032387F File Offset: 0x00321A7F
		public Region Clip
		{
			get
			{
				return this.chartGraphicsGraphics.Clip;
			}
			set
			{
				this.chartGraphicsGraphics.Clip = value;
			}
		}

		// Token: 0x17004543 RID: 17731
		// (get) Token: 0x0600E1ED RID: 57837 RVA: 0x0032388D File Offset: 0x00321A8D
		public RectangleF ClipBounds
		{
			get
			{
				return this.chartGraphicsGraphics.ClipBounds;
			}
		}

		// Token: 0x17004544 RID: 17732
		// (get) Token: 0x0600E1EE RID: 57838 RVA: 0x0032389A File Offset: 0x00321A9A
		// (set) Token: 0x0600E1EF RID: 57839 RVA: 0x003238A7 File Offset: 0x00321AA7
		public CompositingMode CompositingMode
		{
			get
			{
				return this.chartGraphicsGraphics.CompositingMode;
			}
			set
			{
				this.chartGraphicsGraphics.CompositingMode = value;
			}
		}

		// Token: 0x17004545 RID: 17733
		// (get) Token: 0x0600E1F0 RID: 57840 RVA: 0x003238B5 File Offset: 0x00321AB5
		// (set) Token: 0x0600E1F1 RID: 57841 RVA: 0x003238C2 File Offset: 0x00321AC2
		public CompositingQuality CompositingQuality
		{
			get
			{
				return this.chartGraphicsGraphics.CompositingQuality;
			}
			set
			{
				this.chartGraphicsGraphics.CompositingQuality = value;
			}
		}

		// Token: 0x17004546 RID: 17734
		// (get) Token: 0x0600E1F2 RID: 57842 RVA: 0x003238D0 File Offset: 0x00321AD0
		public float DpiX
		{
			get
			{
				return this.chartGraphicsGraphics.DpiX;
			}
		}

		// Token: 0x17004547 RID: 17735
		// (get) Token: 0x0600E1F3 RID: 57843 RVA: 0x003238DD File Offset: 0x00321ADD
		public float DpiY
		{
			get
			{
				return this.chartGraphicsGraphics.DpiY;
			}
		}

		// Token: 0x17004548 RID: 17736
		// (get) Token: 0x0600E1F4 RID: 57844 RVA: 0x003238EA File Offset: 0x00321AEA
		// (set) Token: 0x0600E1F5 RID: 57845 RVA: 0x003238F7 File Offset: 0x00321AF7
		public InterpolationMode InterpolationMode
		{
			get
			{
				return this.chartGraphicsGraphics.InterpolationMode;
			}
			set
			{
				this.chartGraphicsGraphics.InterpolationMode = value;
			}
		}

		// Token: 0x17004549 RID: 17737
		// (get) Token: 0x0600E1F6 RID: 57846 RVA: 0x00323905 File Offset: 0x00321B05
		public bool IsClipEmpty
		{
			get
			{
				return this.chartGraphicsGraphics.IsClipEmpty;
			}
		}

		// Token: 0x1700454A RID: 17738
		// (get) Token: 0x0600E1F7 RID: 57847 RVA: 0x00323912 File Offset: 0x00321B12
		public bool IsVisibleClipEmpty
		{
			get
			{
				return this.chartGraphicsGraphics.IsVisibleClipEmpty;
			}
		}

		// Token: 0x1700454B RID: 17739
		// (get) Token: 0x0600E1F8 RID: 57848 RVA: 0x0032391F File Offset: 0x00321B1F
		// (set) Token: 0x0600E1F9 RID: 57849 RVA: 0x0032392C File Offset: 0x00321B2C
		public float PageScale
		{
			get
			{
				return this.chartGraphicsGraphics.PageScale;
			}
			set
			{
				this.chartGraphicsGraphics.PageScale = value;
			}
		}

		// Token: 0x1700454C RID: 17740
		// (get) Token: 0x0600E1FA RID: 57850 RVA: 0x0032393A File Offset: 0x00321B3A
		// (set) Token: 0x0600E1FB RID: 57851 RVA: 0x00323947 File Offset: 0x00321B47
		public GraphicsUnit PageUnit
		{
			get
			{
				return this.chartGraphicsGraphics.PageUnit;
			}
			set
			{
				this.chartGraphicsGraphics.PageUnit = value;
			}
		}

		// Token: 0x1700454D RID: 17741
		// (get) Token: 0x0600E1FC RID: 57852 RVA: 0x00323955 File Offset: 0x00321B55
		// (set) Token: 0x0600E1FD RID: 57853 RVA: 0x00323962 File Offset: 0x00321B62
		public PixelOffsetMode PixelOffsetMode
		{
			get
			{
				return this.chartGraphicsGraphics.PixelOffsetMode;
			}
			set
			{
				this.chartGraphicsGraphics.PixelOffsetMode = value;
			}
		}

		// Token: 0x1700454E RID: 17742
		// (get) Token: 0x0600E1FE RID: 57854 RVA: 0x00323970 File Offset: 0x00321B70
		// (set) Token: 0x0600E1FF RID: 57855 RVA: 0x0032397D File Offset: 0x00321B7D
		public Point RenderingOrigin
		{
			get
			{
				return this.chartGraphicsGraphics.RenderingOrigin;
			}
			set
			{
				this.chartGraphicsGraphics.RenderingOrigin = value;
			}
		}

		// Token: 0x1700454F RID: 17743
		// (get) Token: 0x0600E200 RID: 57856 RVA: 0x0032398B File Offset: 0x00321B8B
		// (set) Token: 0x0600E201 RID: 57857 RVA: 0x00323998 File Offset: 0x00321B98
		public SmoothingMode SmoothingMode
		{
			get
			{
				return this.chartGraphicsGraphics.SmoothingMode;
			}
			set
			{
				this.chartGraphicsGraphics.SmoothingMode = value;
			}
		}

		// Token: 0x17004550 RID: 17744
		// (get) Token: 0x0600E202 RID: 57858 RVA: 0x003239A6 File Offset: 0x00321BA6
		// (set) Token: 0x0600E203 RID: 57859 RVA: 0x003239B3 File Offset: 0x00321BB3
		public int TextContrast
		{
			get
			{
				return this.chartGraphicsGraphics.TextContrast;
			}
			set
			{
				this.chartGraphicsGraphics.TextContrast = value;
			}
		}

		// Token: 0x17004551 RID: 17745
		// (get) Token: 0x0600E204 RID: 57860 RVA: 0x003239C1 File Offset: 0x00321BC1
		// (set) Token: 0x0600E205 RID: 57861 RVA: 0x003239CE File Offset: 0x00321BCE
		public TextRenderingHint TextRenderingHint
		{
			get
			{
				return this.chartGraphicsGraphics.TextRenderingHint;
			}
			set
			{
				this.chartGraphicsGraphics.TextRenderingHint = value;
			}
		}

		// Token: 0x17004552 RID: 17746
		// (get) Token: 0x0600E206 RID: 57862 RVA: 0x003239DC File Offset: 0x00321BDC
		// (set) Token: 0x0600E207 RID: 57863 RVA: 0x003239E9 File Offset: 0x00321BE9
		public Matrix Transform
		{
			get
			{
				return this.chartGraphicsGraphics.Transform;
			}
			set
			{
				this.chartGraphicsGraphics.Transform = value;
			}
		}

		// Token: 0x17004553 RID: 17747
		// (get) Token: 0x0600E208 RID: 57864 RVA: 0x003239F7 File Offset: 0x00321BF7
		public RectangleF VisibleClipBounds
		{
			get
			{
				return this.chartGraphicsGraphics.VisibleClipBounds;
			}
		}

		// Token: 0x0600E209 RID: 57865 RVA: 0x00323A04 File Offset: 0x00321C04
		public void AddMetafileComment(byte[] data)
		{
			this.chartGraphicsGraphics.AddMetafileComment(data);
		}

		// Token: 0x0600E20A RID: 57866 RVA: 0x00323A12 File Offset: 0x00321C12
		public GraphicsContainer BeginContainer()
		{
			return this.chartGraphicsGraphics.BeginContainer();
		}

		// Token: 0x0600E20B RID: 57867 RVA: 0x00323A1F File Offset: 0x00321C1F
		public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
		{
			return this.chartGraphicsGraphics.BeginContainer(dstrect, srcrect, unit);
		}

		// Token: 0x0600E20C RID: 57868 RVA: 0x00323A2F File Offset: 0x00321C2F
		public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
		{
			return this.chartGraphicsGraphics.BeginContainer(dstrect, srcrect, unit);
		}

		// Token: 0x0600E20D RID: 57869 RVA: 0x00323A3F File Offset: 0x00321C3F
		public void Clear(Color color)
		{
			this.chartGraphicsGraphics.Clear(color);
		}

		// Token: 0x0600E20E RID: 57870 RVA: 0x00323A4D File Offset: 0x00321C4D
		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
		{
			this.chartGraphicsGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize);
		}

		// Token: 0x0600E20F RID: 57871 RVA: 0x00323A5D File Offset: 0x00321C5D
		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			this.chartGraphicsGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize, copyPixelOperation);
		}

		// Token: 0x0600E210 RID: 57872 RVA: 0x00323A6F File Offset: 0x00321C6F
		public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
		{
			this.chartGraphicsGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize);
		}

		// Token: 0x0600E211 RID: 57873 RVA: 0x00323A83 File Offset: 0x00321C83
		public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			this.chartGraphicsGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
		}

		// Token: 0x0600E212 RID: 57874 RVA: 0x00323A99 File Offset: 0x00321C99
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E213 RID: 57875 RVA: 0x00323AA8 File Offset: 0x00321CA8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.chartGraphicsGraphics != null)
			{
				this.chartGraphicsGraphics.Dispose();
			}
		}

		// Token: 0x0600E214 RID: 57876 RVA: 0x00323AC0 File Offset: 0x00321CC0
		public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.DrawArc(pen, rect, startAngle, sweepAngle);
		}

		// Token: 0x0600E215 RID: 57877 RVA: 0x00323AD2 File Offset: 0x00321CD2
		public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.DrawArc(pen, rect, startAngle, sweepAngle);
		}

		// Token: 0x0600E216 RID: 57878 RVA: 0x00323AE4 File Offset: 0x00321CE4
		public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
		}

		// Token: 0x0600E217 RID: 57879 RVA: 0x00323AFC File Offset: 0x00321CFC
		public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this.chartGraphicsGraphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
		}

		// Token: 0x0600E218 RID: 57880 RVA: 0x00323B14 File Offset: 0x00321D14
		public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
		{
			this.chartGraphicsGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
		}

		// Token: 0x0600E219 RID: 57881 RVA: 0x00323B28 File Offset: 0x00321D28
		public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			this.chartGraphicsGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
		}

		// Token: 0x0600E21A RID: 57882 RVA: 0x00323B3C File Offset: 0x00321D3C
		public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			this.chartGraphicsGraphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
		}

		// Token: 0x0600E21B RID: 57883 RVA: 0x00323B63 File Offset: 0x00321D63
		public void DrawBeziers(Pen pen, Point[] points)
		{
			this.chartGraphicsGraphics.DrawBeziers(pen, points);
		}

		// Token: 0x0600E21C RID: 57884 RVA: 0x00323B72 File Offset: 0x00321D72
		public void DrawBeziers(Pen pen, PointF[] points)
		{
			this.chartGraphicsGraphics.DrawBeziers(pen, points);
		}

		// Token: 0x0600E21D RID: 57885 RVA: 0x00323B81 File Offset: 0x00321D81
		public void DrawClosedCurve(Pen pen, Point[] points)
		{
			this.chartGraphicsGraphics.DrawClosedCurve(pen, points);
		}

		// Token: 0x0600E21E RID: 57886 RVA: 0x00323B90 File Offset: 0x00321D90
		public void DrawClosedCurve(Pen pen, PointF[] points)
		{
			this.chartGraphicsGraphics.DrawClosedCurve(pen, points);
		}

		// Token: 0x0600E21F RID: 57887 RVA: 0x00323B9F File Offset: 0x00321D9F
		public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
		{
			this.chartGraphicsGraphics.DrawClosedCurve(pen, points, tension, fillmode);
		}

		// Token: 0x0600E220 RID: 57888 RVA: 0x00323BB1 File Offset: 0x00321DB1
		public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
		{
			this.chartGraphicsGraphics.DrawClosedCurve(pen, points, tension, fillmode);
		}

		// Token: 0x0600E221 RID: 57889 RVA: 0x00323BC3 File Offset: 0x00321DC3
		public void DrawCurve(Pen pen, Point[] points)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points);
		}

		// Token: 0x0600E222 RID: 57890 RVA: 0x00323BD2 File Offset: 0x00321DD2
		public void DrawCurve(Pen pen, PointF[] points)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points);
		}

		// Token: 0x0600E223 RID: 57891 RVA: 0x00323BE1 File Offset: 0x00321DE1
		public void DrawCurve(Pen pen, Point[] points, float tension)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points, tension);
		}

		// Token: 0x0600E224 RID: 57892 RVA: 0x00323BF1 File Offset: 0x00321DF1
		public void DrawCurve(Pen pen, PointF[] points, float tension)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points, tension);
		}

		// Token: 0x0600E225 RID: 57893 RVA: 0x00323C01 File Offset: 0x00321E01
		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points, offset, numberOfSegments);
		}

		// Token: 0x0600E226 RID: 57894 RVA: 0x00323C13 File Offset: 0x00321E13
		public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
		}

		// Token: 0x0600E227 RID: 57895 RVA: 0x00323C27 File Offset: 0x00321E27
		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
		{
			this.chartGraphicsGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
		}

		// Token: 0x0600E228 RID: 57896 RVA: 0x00323C3B File Offset: 0x00321E3B
		public void DrawEllipse(Pen pen, Rectangle rect)
		{
			this.chartGraphicsGraphics.DrawEllipse(pen, rect);
		}

		// Token: 0x0600E229 RID: 57897 RVA: 0x00323C4A File Offset: 0x00321E4A
		public void DrawEllipse(Pen pen, RectangleF rect)
		{
			this.chartGraphicsGraphics.DrawEllipse(pen, rect);
		}

		// Token: 0x0600E22A RID: 57898 RVA: 0x00323C59 File Offset: 0x00321E59
		public void DrawEllipse(Pen pen, float x, float y, float width, float height)
		{
			this.chartGraphicsGraphics.DrawEllipse(pen, x, y, width, height);
		}

		// Token: 0x0600E22B RID: 57899 RVA: 0x00323C6D File Offset: 0x00321E6D
		public void DrawEllipse(Pen pen, int x, int y, int width, int height)
		{
			this.chartGraphicsGraphics.DrawEllipse(pen, x, y, width, height);
		}

		// Token: 0x0600E22C RID: 57900 RVA: 0x00323C81 File Offset: 0x00321E81
		public void DrawIcon(Icon icon, Rectangle targetRect)
		{
			this.chartGraphicsGraphics.DrawIcon(icon, targetRect);
		}

		// Token: 0x0600E22D RID: 57901 RVA: 0x00323C90 File Offset: 0x00321E90
		public void DrawIcon(Icon icon, int x, int y)
		{
			this.chartGraphicsGraphics.DrawIcon(icon, x, y);
		}

		// Token: 0x0600E22E RID: 57902 RVA: 0x00323CA0 File Offset: 0x00321EA0
		public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
		{
			this.chartGraphicsGraphics.DrawIconUnstretched(icon, targetRect);
		}

		// Token: 0x0600E22F RID: 57903 RVA: 0x00323CAF File Offset: 0x00321EAF
		public void DrawImage(Image image, Point point)
		{
			this.chartGraphicsGraphics.DrawImage(image, point);
		}

		// Token: 0x0600E230 RID: 57904 RVA: 0x00323CBE File Offset: 0x00321EBE
		public void DrawImage(Image image, Point[] destPoints)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints);
		}

		// Token: 0x0600E231 RID: 57905 RVA: 0x00323CCD File Offset: 0x00321ECD
		public void DrawImage(Image image, PointF point)
		{
			this.chartGraphicsGraphics.DrawImage(image, point);
		}

		// Token: 0x0600E232 RID: 57906 RVA: 0x00323CDC File Offset: 0x00321EDC
		public void DrawImage(Image image, PointF[] destPoints)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints);
		}

		// Token: 0x0600E233 RID: 57907 RVA: 0x00323CEB File Offset: 0x00321EEB
		public void DrawImage(Image image, Rectangle rect)
		{
			this.chartGraphicsGraphics.DrawImage(image, rect);
		}

		// Token: 0x0600E234 RID: 57908 RVA: 0x00323CFA File Offset: 0x00321EFA
		public void DrawImage(Image image, RectangleF rect)
		{
			this.chartGraphicsGraphics.DrawImage(image, rect);
		}

		// Token: 0x0600E235 RID: 57909 RVA: 0x00323D09 File Offset: 0x00321F09
		public void DrawImage(Image image, float x, float y)
		{
			this.chartGraphicsGraphics.DrawImage(image, x, y);
		}

		// Token: 0x0600E236 RID: 57910 RVA: 0x00323D19 File Offset: 0x00321F19
		public void DrawImage(Image image, int x, int y)
		{
			this.chartGraphicsGraphics.DrawImage(image, x, y);
		}

		// Token: 0x0600E237 RID: 57911 RVA: 0x00323D29 File Offset: 0x00321F29
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		// Token: 0x0600E238 RID: 57912 RVA: 0x00323D3B File Offset: 0x00321F3B
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		// Token: 0x0600E239 RID: 57913 RVA: 0x00323D4D File Offset: 0x00321F4D
		public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcRect, srcUnit);
		}

		// Token: 0x0600E23A RID: 57914 RVA: 0x00323D5F File Offset: 0x00321F5F
		public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcRect, srcUnit);
		}

		// Token: 0x0600E23B RID: 57915 RVA: 0x00323D71 File Offset: 0x00321F71
		public void DrawImage(Image image, float x, float y, float width, float height)
		{
			this.chartGraphicsGraphics.DrawImage(image, x, y, width, height);
		}

		// Token: 0x0600E23C RID: 57916 RVA: 0x00323D85 File Offset: 0x00321F85
		public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, x, y, srcRect, srcUnit);
		}

		// Token: 0x0600E23D RID: 57917 RVA: 0x00323D99 File Offset: 0x00321F99
		public void DrawImage(Image image, int x, int y, int width, int height)
		{
			this.chartGraphicsGraphics.DrawImage(image, x, y, width, height);
		}

		// Token: 0x0600E23E RID: 57918 RVA: 0x00323DAD File Offset: 0x00321FAD
		public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, x, y, srcRect, srcUnit);
		}

		// Token: 0x0600E23F RID: 57919 RVA: 0x00323DC1 File Offset: 0x00321FC1
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
		}

		// Token: 0x0600E240 RID: 57920 RVA: 0x00323DD5 File Offset: 0x00321FD5
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
		}

		// Token: 0x0600E241 RID: 57921 RVA: 0x00323DE9 File Offset: 0x00321FE9
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback);
		}

		// Token: 0x0600E242 RID: 57922 RVA: 0x00323DFF File Offset: 0x00321FFF
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback);
		}

		// Token: 0x0600E243 RID: 57923 RVA: 0x00323E15 File Offset: 0x00322015
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, callbackData);
		}

		// Token: 0x0600E244 RID: 57924 RVA: 0x00323E2D File Offset: 0x0032202D
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
		{
			this.chartGraphicsGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, callbackData);
		}

		// Token: 0x0600E245 RID: 57925 RVA: 0x00323E45 File Offset: 0x00322045
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
		}

		// Token: 0x0600E246 RID: 57926 RVA: 0x00323E5D File Offset: 0x0032205D
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
		}

		// Token: 0x0600E247 RID: 57927 RVA: 0x00323E78 File Offset: 0x00322078
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs);
		}

		// Token: 0x0600E248 RID: 57928 RVA: 0x00323EA0 File Offset: 0x003220A0
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr);
		}

		// Token: 0x0600E249 RID: 57929 RVA: 0x00323EC8 File Offset: 0x003220C8
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback);
		}

		// Token: 0x0600E24A RID: 57930 RVA: 0x00323EF0 File Offset: 0x003220F0
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, callback);
		}

		// Token: 0x0600E24B RID: 57931 RVA: 0x00323F18 File Offset: 0x00322118
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, callbackData);
		}

		// Token: 0x0600E24C RID: 57932 RVA: 0x00323F44 File Offset: 0x00322144
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, callbackData);
		}

		// Token: 0x0600E24D RID: 57933 RVA: 0x00323F6D File Offset: 0x0032216D
		public void DrawImageUnscaled(Image image, Point point)
		{
			this.chartGraphicsGraphics.DrawImageUnscaled(image, point);
		}

		// Token: 0x0600E24E RID: 57934 RVA: 0x00323F7C File Offset: 0x0032217C
		public void DrawImageUnscaled(Image image, Rectangle rect)
		{
			this.chartGraphicsGraphics.DrawImageUnscaled(image, rect);
		}

		// Token: 0x0600E24F RID: 57935 RVA: 0x00323F8B File Offset: 0x0032218B
		public void DrawImageUnscaled(Image image, int x, int y)
		{
			this.chartGraphicsGraphics.DrawImageUnscaled(image, x, y);
		}

		// Token: 0x0600E250 RID: 57936 RVA: 0x00323F9B File Offset: 0x0032219B
		public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
		{
			this.chartGraphicsGraphics.DrawImageUnscaled(image, x, y, width, height);
		}

		// Token: 0x0600E251 RID: 57937 RVA: 0x00323FAF File Offset: 0x003221AF
		public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
		{
			this.chartGraphicsGraphics.DrawImageUnscaled(image, rect);
		}

		// Token: 0x0600E252 RID: 57938 RVA: 0x00323FBE File Offset: 0x003221BE
		public void DrawLine(Pen pen, Point pt1, Point pt2)
		{
			this.chartGraphicsGraphics.DrawLine(pen, pt1, pt2);
		}

		// Token: 0x0600E253 RID: 57939 RVA: 0x00323FCE File Offset: 0x003221CE
		public void DrawLine(Pen pen, PointF pt1, PointF pt2)
		{
			this.chartGraphicsGraphics.DrawLine(pen, pt1, pt2);
		}

		// Token: 0x0600E254 RID: 57940 RVA: 0x00323FDE File Offset: 0x003221DE
		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			this.chartGraphicsGraphics.DrawLine(pen, x1, y1, x2, y2);
		}

		// Token: 0x0600E255 RID: 57941 RVA: 0x00323FF2 File Offset: 0x003221F2
		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
		{
			this.chartGraphicsGraphics.DrawLine(pen, x1, y1, x2, y2);
		}

		// Token: 0x0600E256 RID: 57942 RVA: 0x00324006 File Offset: 0x00322206
		public void DrawLines(Pen pen, Point[] points)
		{
			this.chartGraphicsGraphics.DrawLines(pen, points);
		}

		// Token: 0x0600E257 RID: 57943 RVA: 0x00324015 File Offset: 0x00322215
		public void DrawLines(Pen pen, PointF[] points)
		{
			this.chartGraphicsGraphics.DrawLines(pen, points);
		}

		// Token: 0x0600E258 RID: 57944 RVA: 0x00324024 File Offset: 0x00322224
		public void DrawPath(Pen pen, GraphicsPath path)
		{
			this.chartGraphicsGraphics.DrawPath(pen, path);
		}

		// Token: 0x0600E259 RID: 57945 RVA: 0x00324033 File Offset: 0x00322233
		public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.DrawPie(pen, rect, startAngle, sweepAngle);
		}

		// Token: 0x0600E25A RID: 57946 RVA: 0x00324045 File Offset: 0x00322245
		public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.DrawPie(pen, rect, startAngle, sweepAngle);
		}

		// Token: 0x0600E25B RID: 57947 RVA: 0x00324057 File Offset: 0x00322257
		public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
		}

		// Token: 0x0600E25C RID: 57948 RVA: 0x0032406F File Offset: 0x0032226F
		public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this.chartGraphicsGraphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
		}

		// Token: 0x0600E25D RID: 57949 RVA: 0x00324087 File Offset: 0x00322287
		public void DrawPolygon(Pen pen, Point[] points)
		{
			this.chartGraphicsGraphics.DrawPolygon(pen, points);
		}

		// Token: 0x0600E25E RID: 57950 RVA: 0x00324096 File Offset: 0x00322296
		public void DrawPolygon(Pen pen, PointF[] points)
		{
			this.chartGraphicsGraphics.DrawPolygon(pen, points);
		}

		// Token: 0x0600E25F RID: 57951 RVA: 0x003240A5 File Offset: 0x003222A5
		public void DrawRectangle(Pen pen, Rectangle rect)
		{
			this.chartGraphicsGraphics.DrawRectangle(pen, rect);
		}

		// Token: 0x0600E260 RID: 57952 RVA: 0x003240B4 File Offset: 0x003222B4
		public void DrawRectangle(Pen pen, RectangleF rect)
		{
			this.chartGraphicsGraphics.DrawRectangle(pen, Rectangle.Round(rect));
		}

		// Token: 0x0600E261 RID: 57953 RVA: 0x003240C8 File Offset: 0x003222C8
		public void DrawRectangle(Pen pen, float x, float y, float width, float height)
		{
			this.chartGraphicsGraphics.DrawRectangle(pen, x, y, width, height);
		}

		// Token: 0x0600E262 RID: 57954 RVA: 0x003240DC File Offset: 0x003222DC
		public void DrawRectangle(Pen pen, int x, int y, int width, int height)
		{
			this.chartGraphicsGraphics.DrawRectangle(pen, x, y, width, height);
		}

		// Token: 0x0600E263 RID: 57955 RVA: 0x003240F0 File Offset: 0x003222F0
		public void DrawRectangles(Pen pen, Rectangle[] rects)
		{
			this.chartGraphicsGraphics.DrawRectangles(pen, rects);
		}

		// Token: 0x0600E264 RID: 57956 RVA: 0x003240FF File Offset: 0x003222FF
		public void DrawRectangles(Pen pen, RectangleF[] rects)
		{
			this.chartGraphicsGraphics.DrawRectangles(pen, rects);
		}

		// Token: 0x0600E265 RID: 57957 RVA: 0x0032410E File Offset: 0x0032230E
		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this.chartGraphicsGraphics.DrawString(s, font, brush, point);
		}

		// Token: 0x0600E266 RID: 57958 RVA: 0x00324120 File Offset: 0x00322320
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this.chartGraphicsGraphics.DrawString(s, font, brush, layoutRectangle);
		}

		// Token: 0x0600E267 RID: 57959 RVA: 0x00324132 File Offset: 0x00322332
		public void DrawString(string s, Font font, Brush brush, float x, float y)
		{
			this.chartGraphicsGraphics.DrawString(s, font, brush, x, y);
		}

		// Token: 0x0600E268 RID: 57960 RVA: 0x00324146 File Offset: 0x00322346
		public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
		{
			this.chartGraphicsGraphics.DrawString(s, font, brush, point, format);
		}

		// Token: 0x0600E269 RID: 57961 RVA: 0x0032415A File Offset: 0x0032235A
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			this.chartGraphicsGraphics.DrawString(s, font, brush, layoutRectangle, format);
		}

		// Token: 0x0600E26A RID: 57962 RVA: 0x0032416E File Offset: 0x0032236E
		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this.chartGraphicsGraphics.DrawString(s, font, brush, x, y, format);
		}

		// Token: 0x0600E26B RID: 57963 RVA: 0x00324184 File Offset: 0x00322384
		public void EndContainer(GraphicsContainer container)
		{
			this.chartGraphicsGraphics.EndContainer(container);
		}

		// Token: 0x0600E26C RID: 57964 RVA: 0x00324192 File Offset: 0x00322392
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, callback);
		}

		// Token: 0x0600E26D RID: 57965 RVA: 0x003241A2 File Offset: 0x003223A2
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, callback);
		}

		// Token: 0x0600E26E RID: 57966 RVA: 0x003241B2 File Offset: 0x003223B2
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, callback);
		}

		// Token: 0x0600E26F RID: 57967 RVA: 0x003241C2 File Offset: 0x003223C2
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, callback);
		}

		// Token: 0x0600E270 RID: 57968 RVA: 0x003241D2 File Offset: 0x003223D2
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, callback);
		}

		// Token: 0x0600E271 RID: 57969 RVA: 0x003241E2 File Offset: 0x003223E2
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, callback);
		}

		// Token: 0x0600E272 RID: 57970 RVA: 0x003241F2 File Offset: 0x003223F2
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData);
		}

		// Token: 0x0600E273 RID: 57971 RVA: 0x00324204 File Offset: 0x00322404
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData);
		}

		// Token: 0x0600E274 RID: 57972 RVA: 0x00324216 File Offset: 0x00322416
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData);
		}

		// Token: 0x0600E275 RID: 57973 RVA: 0x00324228 File Offset: 0x00322428
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData);
		}

		// Token: 0x0600E276 RID: 57974 RVA: 0x0032423A File Offset: 0x0032243A
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData);
		}

		// Token: 0x0600E277 RID: 57975 RVA: 0x0032424C File Offset: 0x0032244C
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData);
		}

		// Token: 0x0600E278 RID: 57976 RVA: 0x0032425E File Offset: 0x0032245E
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E279 RID: 57977 RVA: 0x00324272 File Offset: 0x00322472
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback);
		}

		// Token: 0x0600E27A RID: 57978 RVA: 0x00324286 File Offset: 0x00322486
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E27B RID: 57979 RVA: 0x0032429A File Offset: 0x0032249A
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback);
		}

		// Token: 0x0600E27C RID: 57980 RVA: 0x003242AE File Offset: 0x003224AE
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E27D RID: 57981 RVA: 0x003242C2 File Offset: 0x003224C2
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback);
		}

		// Token: 0x0600E27E RID: 57982 RVA: 0x003242D6 File Offset: 0x003224D6
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E27F RID: 57983 RVA: 0x003242EA File Offset: 0x003224EA
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback);
		}

		// Token: 0x0600E280 RID: 57984 RVA: 0x003242FE File Offset: 0x003224FE
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E281 RID: 57985 RVA: 0x00324312 File Offset: 0x00322512
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback);
		}

		// Token: 0x0600E282 RID: 57986 RVA: 0x00324326 File Offset: 0x00322526
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E283 RID: 57987 RVA: 0x0032433A File Offset: 0x0032253A
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback);
		}

		// Token: 0x0600E284 RID: 57988 RVA: 0x0032434E File Offset: 0x0032254E
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData);
		}

		// Token: 0x0600E285 RID: 57989 RVA: 0x00324364 File Offset: 0x00322564
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData);
		}

		// Token: 0x0600E286 RID: 57990 RVA: 0x0032437A File Offset: 0x0032257A
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData);
		}

		// Token: 0x0600E287 RID: 57991 RVA: 0x00324390 File Offset: 0x00322590
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData);
		}

		// Token: 0x0600E288 RID: 57992 RVA: 0x003243A6 File Offset: 0x003225A6
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData);
		}

		// Token: 0x0600E289 RID: 57993 RVA: 0x003243BC File Offset: 0x003225BC
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData);
		}

		// Token: 0x0600E28A RID: 57994 RVA: 0x003243D2 File Offset: 0x003225D2
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, srcRect, unit, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E28B RID: 57995 RVA: 0x003243EA File Offset: 0x003225EA
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, srcRect, unit, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E28C RID: 57996 RVA: 0x00324402 File Offset: 0x00322602
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoint, srcRect, unit, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E28D RID: 57997 RVA: 0x0032441A File Offset: 0x0032261A
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destPoints, srcRect, unit, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E28E RID: 57998 RVA: 0x00324432 File Offset: 0x00322632
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, srcRect, unit, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E28F RID: 57999 RVA: 0x0032444A File Offset: 0x0032264A
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			this.chartGraphicsGraphics.EnumerateMetafile(metafile, destRect, srcRect, unit, callback, callbackData, imageAttr);
		}

		// Token: 0x0600E290 RID: 58000 RVA: 0x00324462 File Offset: 0x00322662
		public void ExcludeClip(Rectangle rect)
		{
			this.chartGraphicsGraphics.ExcludeClip(rect);
		}

		// Token: 0x0600E291 RID: 58001 RVA: 0x00324470 File Offset: 0x00322670
		public void ExcludeClip(Region region)
		{
			this.chartGraphicsGraphics.ExcludeClip(region);
		}

		// Token: 0x0600E292 RID: 58002 RVA: 0x0032447E File Offset: 0x0032267E
		public void FillClosedCurve(Brush brush, Point[] points)
		{
			this.chartGraphicsGraphics.FillClosedCurve(brush, points);
		}

		// Token: 0x0600E293 RID: 58003 RVA: 0x0032448D File Offset: 0x0032268D
		public void FillClosedCurve(Brush brush, PointF[] points)
		{
			this.chartGraphicsGraphics.FillClosedCurve(brush, points);
		}

		// Token: 0x0600E294 RID: 58004 RVA: 0x0032449C File Offset: 0x0032269C
		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
		{
			this.chartGraphicsGraphics.FillClosedCurve(brush, points, fillmode);
		}

		// Token: 0x0600E295 RID: 58005 RVA: 0x003244AC File Offset: 0x003226AC
		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
		{
			this.chartGraphicsGraphics.FillClosedCurve(brush, points, fillmode);
		}

		// Token: 0x0600E296 RID: 58006 RVA: 0x003244BC File Offset: 0x003226BC
		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
		{
			this.chartGraphicsGraphics.FillClosedCurve(brush, points, fillmode, tension);
		}

		// Token: 0x0600E297 RID: 58007 RVA: 0x003244CE File Offset: 0x003226CE
		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
		{
			this.chartGraphicsGraphics.FillClosedCurve(brush, points, fillmode, tension);
		}

		// Token: 0x0600E298 RID: 58008 RVA: 0x003244E0 File Offset: 0x003226E0
		public void FillEllipse(Brush brush, Rectangle rect)
		{
			this.chartGraphicsGraphics.FillEllipse(brush, rect);
		}

		// Token: 0x0600E299 RID: 58009 RVA: 0x003244EF File Offset: 0x003226EF
		public void FillEllipse(Brush brush, RectangleF rect)
		{
			this.chartGraphicsGraphics.FillEllipse(brush, rect);
		}

		// Token: 0x0600E29A RID: 58010 RVA: 0x003244FE File Offset: 0x003226FE
		public void FillEllipse(Brush brush, float x, float y, float width, float height)
		{
			this.chartGraphicsGraphics.FillEllipse(brush, x, y, width, height);
		}

		// Token: 0x0600E29B RID: 58011 RVA: 0x00324512 File Offset: 0x00322712
		public void FillEllipse(Brush brush, int x, int y, int width, int height)
		{
			this.chartGraphicsGraphics.FillEllipse(brush, x, y, width, height);
		}

		// Token: 0x0600E29C RID: 58012 RVA: 0x00324526 File Offset: 0x00322726
		public void FillPath(Brush brush, GraphicsPath path)
		{
			this.chartGraphicsGraphics.FillPath(brush, path);
		}

		// Token: 0x0600E29D RID: 58013 RVA: 0x00324535 File Offset: 0x00322735
		public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.FillPie(brush, rect, startAngle, sweepAngle);
		}

		// Token: 0x0600E29E RID: 58014 RVA: 0x00324547 File Offset: 0x00322747
		public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			this.chartGraphicsGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
		}

		// Token: 0x0600E29F RID: 58015 RVA: 0x0032455F File Offset: 0x0032275F
		public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this.chartGraphicsGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
		}

		// Token: 0x0600E2A0 RID: 58016 RVA: 0x00324577 File Offset: 0x00322777
		public void FillPolygon(Brush brush, Point[] points)
		{
			this.chartGraphicsGraphics.FillPolygon(brush, points);
		}

		// Token: 0x0600E2A1 RID: 58017 RVA: 0x00324586 File Offset: 0x00322786
		public void FillPolygon(Brush brush, PointF[] points)
		{
			this.chartGraphicsGraphics.FillPolygon(brush, points);
		}

		// Token: 0x0600E2A2 RID: 58018 RVA: 0x00324595 File Offset: 0x00322795
		public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
		{
			this.chartGraphicsGraphics.FillPolygon(brush, points, fillMode);
		}

		// Token: 0x0600E2A3 RID: 58019 RVA: 0x003245A5 File Offset: 0x003227A5
		public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
		{
			this.chartGraphicsGraphics.FillPolygon(brush, points, fillMode);
		}

		// Token: 0x0600E2A4 RID: 58020 RVA: 0x003245B5 File Offset: 0x003227B5
		public void FillRectangle(Brush brush, Rectangle rect)
		{
			this.chartGraphicsGraphics.FillRectangle(brush, rect);
		}

		// Token: 0x0600E2A5 RID: 58021 RVA: 0x003245C4 File Offset: 0x003227C4
		public void FillRectangle(Brush brush, RectangleF rect)
		{
			this.chartGraphicsGraphics.FillRectangle(brush, rect);
		}

		// Token: 0x0600E2A6 RID: 58022 RVA: 0x003245D3 File Offset: 0x003227D3
		public void FillRectangle(Brush brush, float x, float y, float width, float height)
		{
			this.chartGraphicsGraphics.FillRectangle(brush, x, y, width, height);
		}

		// Token: 0x0600E2A7 RID: 58023 RVA: 0x003245E7 File Offset: 0x003227E7
		public void FillRectangle(Brush brush, int x, int y, int width, int height)
		{
			this.chartGraphicsGraphics.FillRectangle(brush, x, y, width, height);
		}

		// Token: 0x0600E2A8 RID: 58024 RVA: 0x003245FB File Offset: 0x003227FB
		public void FillRectangles(Brush brush, Rectangle[] rects)
		{
			this.chartGraphicsGraphics.FillRectangles(brush, rects);
		}

		// Token: 0x0600E2A9 RID: 58025 RVA: 0x0032460A File Offset: 0x0032280A
		public void FillRectangles(Brush brush, RectangleF[] rects)
		{
			this.chartGraphicsGraphics.FillRectangles(brush, rects);
		}

		// Token: 0x0600E2AA RID: 58026 RVA: 0x00324619 File Offset: 0x00322819
		public void FillRegion(Brush brush, Region region)
		{
			this.chartGraphicsGraphics.FillRegion(brush, region);
		}

		// Token: 0x0600E2AB RID: 58027 RVA: 0x00324628 File Offset: 0x00322828
		public void Flush()
		{
			this.chartGraphicsGraphics.Flush();
		}

		// Token: 0x0600E2AC RID: 58028 RVA: 0x00324635 File Offset: 0x00322835
		public void Flush(FlushIntention intention)
		{
			this.chartGraphicsGraphics.Flush(intention);
		}

		// Token: 0x0600E2AD RID: 58029 RVA: 0x00324643 File Offset: 0x00322843
		public object GetContextInfo()
		{
			return this.chartGraphicsGraphics.GetContextInfo();
		}

		// Token: 0x0600E2AE RID: 58030 RVA: 0x00324650 File Offset: 0x00322850
		public IntPtr GetHdc()
		{
			return this.chartGraphicsGraphics.GetHdc();
		}

		// Token: 0x0600E2AF RID: 58031 RVA: 0x0032465D File Offset: 0x0032285D
		public Color GetNearestColor(Color color)
		{
			return this.chartGraphicsGraphics.GetNearestColor(color);
		}

		// Token: 0x0600E2B0 RID: 58032 RVA: 0x0032466B File Offset: 0x0032286B
		public void IntersectClip(Rectangle rect)
		{
			this.chartGraphicsGraphics.IntersectClip(rect);
		}

		// Token: 0x0600E2B1 RID: 58033 RVA: 0x00324679 File Offset: 0x00322879
		public void IntersectClip(RectangleF rect)
		{
			this.chartGraphicsGraphics.IntersectClip(rect);
		}

		// Token: 0x0600E2B2 RID: 58034 RVA: 0x00324687 File Offset: 0x00322887
		public void IntersectClip(Region region)
		{
			this.chartGraphicsGraphics.IntersectClip(region);
		}

		// Token: 0x0600E2B3 RID: 58035 RVA: 0x00324695 File Offset: 0x00322895
		public bool IsVisible(Point point)
		{
			return this.chartGraphicsGraphics.IsVisible(point);
		}

		// Token: 0x0600E2B4 RID: 58036 RVA: 0x003246A3 File Offset: 0x003228A3
		public bool IsVisible(PointF point)
		{
			return this.chartGraphicsGraphics.IsVisible(point);
		}

		// Token: 0x0600E2B5 RID: 58037 RVA: 0x003246B1 File Offset: 0x003228B1
		public bool IsVisible(Rectangle rect)
		{
			return this.chartGraphicsGraphics.IsVisible(rect);
		}

		// Token: 0x0600E2B6 RID: 58038 RVA: 0x003246BF File Offset: 0x003228BF
		public bool IsVisible(RectangleF rect)
		{
			return this.chartGraphicsGraphics.IsVisible(rect);
		}

		// Token: 0x0600E2B7 RID: 58039 RVA: 0x003246CD File Offset: 0x003228CD
		public bool IsVisible(float x, float y)
		{
			return this.chartGraphicsGraphics.IsVisible(x, y);
		}

		// Token: 0x0600E2B8 RID: 58040 RVA: 0x003246DC File Offset: 0x003228DC
		public bool IsVisible(int x, int y)
		{
			return this.chartGraphicsGraphics.IsVisible(x, y);
		}

		// Token: 0x0600E2B9 RID: 58041 RVA: 0x003246EB File Offset: 0x003228EB
		public bool IsVisible(float x, float y, float width, float height)
		{
			return this.chartGraphicsGraphics.IsVisible(x, y, width, height);
		}

		// Token: 0x0600E2BA RID: 58042 RVA: 0x003246FD File Offset: 0x003228FD
		public bool IsVisible(int x, int y, int width, int height)
		{
			return this.chartGraphicsGraphics.IsVisible(x, y, width, height);
		}

		// Token: 0x0600E2BB RID: 58043 RVA: 0x0032470F File Offset: 0x0032290F
		public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
		{
			return this.chartGraphicsGraphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
		}

		// Token: 0x0600E2BC RID: 58044 RVA: 0x00324721 File Offset: 0x00322921
		public SizeF MeasureString(string text, Font font)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font);
		}

		// Token: 0x0600E2BD RID: 58045 RVA: 0x00324730 File Offset: 0x00322930
		public SizeF MeasureString(string text, Font font, int width)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font, width);
		}

		// Token: 0x0600E2BE RID: 58046 RVA: 0x00324740 File Offset: 0x00322940
		public SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font, layoutArea);
		}

		// Token: 0x0600E2BF RID: 58047 RVA: 0x00324750 File Offset: 0x00322950
		public SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font, width, format);
		}

		// Token: 0x0600E2C0 RID: 58048 RVA: 0x00324762 File Offset: 0x00322962
		public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font, origin, stringFormat);
		}

		// Token: 0x0600E2C1 RID: 58049 RVA: 0x00324774 File Offset: 0x00322974
		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font, layoutArea, stringFormat);
		}

		// Token: 0x0600E2C2 RID: 58050 RVA: 0x00324786 File Offset: 0x00322986
		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
		{
			return this.chartGraphicsGraphics.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
		}

		// Token: 0x0600E2C3 RID: 58051 RVA: 0x0032479C File Offset: 0x0032299C
		public void MultiplyTransform(Matrix matrix)
		{
			this.chartGraphicsGraphics.MultiplyTransform(matrix);
		}

		// Token: 0x0600E2C4 RID: 58052 RVA: 0x003247AA File Offset: 0x003229AA
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			this.chartGraphicsGraphics.MultiplyTransform(matrix, order);
		}

		// Token: 0x0600E2C5 RID: 58053 RVA: 0x003247B9 File Offset: 0x003229B9
		public void ReleaseHdc()
		{
			this.chartGraphicsGraphics.ReleaseHdc();
		}

		// Token: 0x0600E2C6 RID: 58054 RVA: 0x003247C6 File Offset: 0x003229C6
		public void ReleaseHdc(IntPtr hdc)
		{
			this.chartGraphicsGraphics.ReleaseHdc(hdc);
		}

		// Token: 0x0600E2C7 RID: 58055 RVA: 0x003247D4 File Offset: 0x003229D4
		public void ReleaseHdcInternal(IntPtr hdc)
		{
			this.chartGraphicsGraphics.ReleaseHdcInternal(hdc);
		}

		// Token: 0x0600E2C8 RID: 58056 RVA: 0x003247E2 File Offset: 0x003229E2
		public void ResetClip()
		{
			this.chartGraphicsGraphics.ResetClip();
		}

		// Token: 0x0600E2C9 RID: 58057 RVA: 0x003247EF File Offset: 0x003229EF
		public void ResetTransform()
		{
			this.chartGraphicsGraphics.ResetTransform();
			this.TranslateTransformDefault();
		}

		// Token: 0x0600E2CA RID: 58058 RVA: 0x00324802 File Offset: 0x00322A02
		public void Restore(GraphicsState gstate)
		{
			this.chartGraphicsGraphics.Restore(gstate);
		}

		// Token: 0x0600E2CB RID: 58059 RVA: 0x00324810 File Offset: 0x00322A10
		public void RotateTransform(float angle)
		{
			this.chartGraphicsGraphics.RotateTransform(angle);
		}

		// Token: 0x0600E2CC RID: 58060 RVA: 0x0032481E File Offset: 0x00322A1E
		public void RotateTransform(float angle, MatrixOrder order)
		{
			this.chartGraphicsGraphics.RotateTransform(angle, order);
		}

		// Token: 0x0600E2CD RID: 58061 RVA: 0x0032482D File Offset: 0x00322A2D
		public GraphicsState Save()
		{
			return this.chartGraphicsGraphics.Save();
		}

		// Token: 0x0600E2CE RID: 58062 RVA: 0x0032483A File Offset: 0x00322A3A
		public void ScaleTransform(float sx, float sy)
		{
			this.chartGraphicsGraphics.ScaleTransform(sx, sy);
		}

		// Token: 0x0600E2CF RID: 58063 RVA: 0x00324849 File Offset: 0x00322A49
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			this.chartGraphicsGraphics.ScaleTransform(sx, sy, order);
		}

		// Token: 0x0600E2D0 RID: 58064 RVA: 0x00324859 File Offset: 0x00322A59
		public void SetClip(Graphics g)
		{
			this.chartGraphicsGraphics.SetClip(g);
		}

		// Token: 0x0600E2D1 RID: 58065 RVA: 0x00324867 File Offset: 0x00322A67
		public void SetClip(GraphicsPath path)
		{
			this.chartGraphicsGraphics.SetClip(path);
		}

		// Token: 0x0600E2D2 RID: 58066 RVA: 0x00324875 File Offset: 0x00322A75
		public void SetClip(Rectangle rect)
		{
			this.chartGraphicsGraphics.SetClip(rect);
		}

		// Token: 0x0600E2D3 RID: 58067 RVA: 0x00324883 File Offset: 0x00322A83
		public void SetClip(RectangleF rect)
		{
			this.chartGraphicsGraphics.SetClip(rect);
		}

		// Token: 0x0600E2D4 RID: 58068 RVA: 0x00324891 File Offset: 0x00322A91
		public void SetClip(Graphics g, CombineMode combineMode)
		{
			this.chartGraphicsGraphics.SetClip(g, combineMode);
		}

		// Token: 0x0600E2D5 RID: 58069 RVA: 0x003248A0 File Offset: 0x00322AA0
		public void SetClip(GraphicsPath path, CombineMode combineMode)
		{
			this.chartGraphicsGraphics.SetClip(path, combineMode);
		}

		// Token: 0x0600E2D6 RID: 58070 RVA: 0x003248AF File Offset: 0x00322AAF
		public void SetClip(Rectangle rect, CombineMode combineMode)
		{
			this.chartGraphicsGraphics.SetClip(rect, combineMode);
		}

		// Token: 0x0600E2D7 RID: 58071 RVA: 0x003248BE File Offset: 0x00322ABE
		public void SetClip(RectangleF rect, CombineMode combineMode)
		{
			this.chartGraphicsGraphics.SetClip(rect, combineMode);
		}

		// Token: 0x0600E2D8 RID: 58072 RVA: 0x003248CD File Offset: 0x00322ACD
		public void SetClip(Region region, CombineMode combineMode)
		{
			this.chartGraphicsGraphics.SetClip(region, combineMode);
		}

		// Token: 0x0600E2D9 RID: 58073 RVA: 0x003248DC File Offset: 0x00322ADC
		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
		{
			this.chartGraphicsGraphics.TransformPoints(destSpace, srcSpace, pts);
		}

		// Token: 0x0600E2DA RID: 58074 RVA: 0x003248EC File Offset: 0x00322AEC
		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
		{
			this.chartGraphicsGraphics.TransformPoints(destSpace, srcSpace, pts);
		}

		// Token: 0x0600E2DB RID: 58075 RVA: 0x003248FC File Offset: 0x00322AFC
		public void TranslateClip(float dx, float dy)
		{
			this.chartGraphicsGraphics.TranslateClip(dx, dy);
		}

		// Token: 0x0600E2DC RID: 58076 RVA: 0x0032490B File Offset: 0x00322B0B
		public void TranslateClip(int dx, int dy)
		{
			this.chartGraphicsGraphics.TranslateClip(dx, dy);
		}

		// Token: 0x0600E2DD RID: 58077 RVA: 0x0032491A File Offset: 0x00322B1A
		public void TranslateTransform(float dx, float dy)
		{
			this.chartGraphicsGraphics.TranslateTransform(dx, dy);
		}

		// Token: 0x0600E2DE RID: 58078 RVA: 0x00324929 File Offset: 0x00322B29
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			this.chartGraphicsGraphics.TranslateTransform(dx, dy, order);
		}

		// Token: 0x04004173 RID: 16755
		private Graphics chartGraphicsGraphics;

		// Token: 0x04004174 RID: 16756
		private float translateTransformDefaultX;

		// Token: 0x04004175 RID: 16757
		private float translateTransformDefaultY;

		// Token: 0x04004176 RID: 16758
		private MatrixOrder translateTransformDefaultOrder;
	}
}
