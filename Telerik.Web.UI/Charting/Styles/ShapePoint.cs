using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001771 RID: 6001
	internal class ShapePoint : ShapePointBase
	{
		// Token: 0x170046FC RID: 18172
		// (get) Token: 0x0600EA2B RID: 59947 RVA: 0x00355600 File Offset: 0x00353800
		// (set) Token: 0x0600EA2C RID: 59948 RVA: 0x00355608 File Offset: 0x00353808
		[Description("The bezier curve control point 1")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ShapePointBase ControlPoint1
		{
			get
			{
				return this.controlPoint1;
			}
			set
			{
				this.controlPoint1 = value;
			}
		}

		// Token: 0x170046FD RID: 18173
		// (get) Token: 0x0600EA2D RID: 59949 RVA: 0x00355611 File Offset: 0x00353811
		// (set) Token: 0x0600EA2E RID: 59950 RVA: 0x00355619 File Offset: 0x00353819
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The bezier curve control point 2")]
		public ShapePointBase ControlPoint2
		{
			get
			{
				return this.controlPoint2;
			}
			set
			{
				this.controlPoint2 = value;
			}
		}

		// Token: 0x170046FE RID: 18174
		// (get) Token: 0x0600EA2F RID: 59951 RVA: 0x00355622 File Offset: 0x00353822
		// (set) Token: 0x0600EA30 RID: 59952 RVA: 0x0035562A File Offset: 0x0035382A
		[DefaultValue(false)]
		[Description("Determines if this point marks the begin of a bezier curve")]
		public bool Bezier
		{
			get
			{
				return this.bezier;
			}
			set
			{
				this.bezier = value;
			}
		}

		// Token: 0x0600EA31 RID: 59953 RVA: 0x00355633 File Offset: 0x00353833
		public ShapePoint()
		{
		}

		// Token: 0x0600EA32 RID: 59954 RVA: 0x00355651 File Offset: 0x00353851
		public ShapePoint(int x, int y) : base((float)x, (float)y)
		{
		}

		// Token: 0x0600EA33 RID: 59955 RVA: 0x00355673 File Offset: 0x00353873
		public ShapePoint(Point point) : base(point)
		{
		}

		// Token: 0x0600EA34 RID: 59956 RVA: 0x00355694 File Offset: 0x00353894
		public ShapePoint(ShapePoint point) : base(point)
		{
			this.ControlPoint1 = new ShapePointBase(point.ControlPoint1);
			this.ControlPoint2 = new ShapePointBase(point.ControlPoint2);
			this.Bezier = point.bezier;
		}

		// Token: 0x0600EA35 RID: 59957 RVA: 0x003556EC File Offset: 0x003538EC
		internal void CreateBezier(ShapePointBase nextPoint)
		{
			this.ControlPoint1.Set(base.X + 10f, base.Y);
			this.ControlPoint2.Set(nextPoint.X - 10f, nextPoint.Y);
		}

		// Token: 0x0600EA36 RID: 59958 RVA: 0x00355728 File Offset: 0x00353928
		internal Point[] GetCurve(ShapePoint nextPoint)
		{
			double num = (double)this.shapePointBaseX;
			double num2 = (double)nextPoint.X;
			double num3 = (double)this.controlPoint1.X;
			double num4 = (double)this.controlPoint2.X;
			double num5 = (double)this.shapePointBaseY;
			double num6 = (double)nextPoint.Y;
			double num7 = (double)this.controlPoint1.Y;
			double num8 = (double)this.controlPoint2.Y;
			double num9 = 3.0 * (num3 - num);
			double num10 = 3.0 * (num4 - num3) - num9;
			double num11 = num2 - num - num9 - num10;
			double num12 = 3.0 * (num7 - num5);
			double num13 = 3.0 * (num8 - num7) - num12;
			double num14 = num6 - num5 - num12 - num13;
			Point[] array = new Point[10];
			for (int i = 0; i < 10; i++)
			{
				double num15 = (double)i / 9.0;
				double num16 = num15 * num15;
				double num17 = num16 * num15;
				array[i] = new Point((int)(num11 * num17 + num10 * num16 + num9 * num15 + num), (int)(num14 * num17 + num13 * num16 + num12 * num15 + num5));
			}
			return array;
		}

		// Token: 0x0600EA37 RID: 59959 RVA: 0x00355860 File Offset: 0x00353A60
		internal bool IsVisible(ShapePoint nextPoint, Point pt, int width)
		{
			if (this.bezier)
			{
				return ShapePoint.IsCurveVisible(this.GetCurve(nextPoint), pt, (double)width);
			}
			return ShapePoint.IsLineVisible(base.GetPoint(), nextPoint.GetPoint(), pt, (double)width);
		}

		// Token: 0x0600EA38 RID: 59960 RVA: 0x00355890 File Offset: 0x00353A90
		private static bool IsLineVisible(Point pt1, Point pt2, Point pt, double radius)
		{
			double num = (double)(pt1.Y - pt2.Y);
			double num2 = (double)(pt2.X - pt1.X);
			double num3 = (double)(pt1.X * pt2.Y - pt2.X * pt1.Y);
			double value = (num * (double)pt.X + num2 * (double)pt.Y + num3) / Math.Sqrt(num * num + num2 * num2);
			if (Math.Abs(value) < radius)
			{
				double num4 = (double)Math.Min(pt1.X, pt2.X) - radius;
				double num5 = (double)Math.Max(pt1.X, pt2.X) + radius;
				double num6 = (double)Math.Min(pt1.Y, pt2.Y) - radius;
				double num7 = (double)Math.Max(pt1.Y, pt2.Y) + radius;
				if (num4 <= (double)pt.X && (double)pt.X <= num5 && num6 <= (double)pt.Y && (double)pt.Y <= num7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600EA39 RID: 59961 RVA: 0x003559A4 File Offset: 0x00353BA4
		private static bool IsCurveVisible(Point[] points, Point pt, double radius)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				Point pt2 = points[i];
				Point pt3 = points[i + 1];
				if (ShapePoint.IsLineVisible(pt2, pt3, pt, radius))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004372 RID: 17266
		private ShapePointBase controlPoint1 = new ShapePointBase();

		// Token: 0x04004373 RID: 17267
		private ShapePointBase controlPoint2 = new ShapePointBase();

		// Token: 0x04004374 RID: 17268
		private bool bezier;

		// Token: 0x02001772 RID: 6002
		internal enum LineDirections
		{
			// Token: 0x04004376 RID: 17270
			South,
			// Token: 0x04004377 RID: 17271
			Nord,
			// Token: 0x04004378 RID: 17272
			East,
			// Token: 0x04004379 RID: 17273
			West,
			// Token: 0x0400437A RID: 17274
			SouthEast,
			// Token: 0x0400437B RID: 17275
			SouthWest,
			// Token: 0x0400437C RID: 17276
			NordEast,
			// Token: 0x0400437D RID: 17277
			NordWest
		}

		// Token: 0x02001773 RID: 6003
		internal enum LinePositions
		{
			// Token: 0x0400437F RID: 17279
			Horizontal,
			// Token: 0x04004380 RID: 17280
			Vertical
		}
	}
}
