using System;
using System.util;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020000E2 RID: 226
	public class LineSegment
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0002B770 File Offset: 0x0002A770
		public LineSegment(Vector startPoint, Vector endPoint)
		{
			this.startPoint = startPoint;
			this.endPoint = endPoint;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002B786 File Offset: 0x0002A786
		public Vector GetStartPoint()
		{
			return this.startPoint;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002B78E File Offset: 0x0002A78E
		public Vector GetEndPoint()
		{
			return this.endPoint;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002B796 File Offset: 0x0002A796
		public float GetLength()
		{
			return this.endPoint.Subtract(this.startPoint).Length;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002B7B0 File Offset: 0x0002A7B0
		public RectangleJ GetBoundingRectange()
		{
			float num = this.GetStartPoint()[0];
			float num2 = this.GetStartPoint()[1];
			float num3 = this.GetEndPoint()[0];
			float num4 = this.GetEndPoint()[1];
			return new RectangleJ(Math.Min(num, num3), Math.Min(num2, num4), Math.Abs(num3 - num), Math.Abs(num4 - num2));
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002B814 File Offset: 0x0002A814
		public LineSegment TransformBy(Matrix m)
		{
			Vector vector = this.startPoint.Cross(m);
			Vector vector2 = this.endPoint.Cross(m);
			return new LineSegment(vector, vector2);
		}

		// Token: 0x040006E9 RID: 1769
		private Vector startPoint;

		// Token: 0x040006EA RID: 1770
		private Vector endPoint;
	}
}
