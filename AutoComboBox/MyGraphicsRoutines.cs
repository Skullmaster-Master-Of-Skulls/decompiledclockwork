using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AutoComboBox
{
	// Token: 0x02000014 RID: 20
	public class MyGraphicsRoutines
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00005074 File Offset: 0x00004074
		public static GraphicsPath GetRoundedRectanglePath(Rectangle rectangle, int radius)
		{
			int num = radius * 2;
			GraphicsPath graphicsPath = new GraphicsPath();
			if (num >= rectangle.Width || num >= rectangle.Height)
			{
				graphicsPath.AddRectangle(rectangle);
			}
			else
			{
				Point point = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
				Point point2 = new Point(rectangle.X, rectangle.Y + radius);
				Point point3 = new Point(rectangle.X + radius, rectangle.Y);
				Point point4 = new Point(point.X - radius, rectangle.Y);
				Point point5 = new Point(point.X, point2.Y);
				Point point6 = new Point(point.X, point.Y - radius);
				Point point7 = new Point(point4.X, point.Y);
				Point point8 = new Point(point3.X, point.Y);
				Point point9 = new Point(rectangle.X, point6.Y);
				graphicsPath.AddArc(rectangle.X, rectangle.Y, num, num, 180f, 90f);
				graphicsPath.AddLine(point3.X, point3.Y, point4.X, point4.Y);
				graphicsPath.AddArc(point.X - num, rectangle.Y, num, num, 270f, 90f);
				graphicsPath.AddLine(point5.X, point5.Y, point6.X, point6.Y);
				graphicsPath.AddArc(point.X - num, point.Y - num, num, num, 0f, 90f);
				graphicsPath.AddLine(point7.X, point7.Y, point8.X, point8.Y);
				graphicsPath.AddArc(rectangle.X, point.Y - num, num, num, 90f, 90f);
				graphicsPath.AddLine(point9.X, point9.Y, point2.X, point2.Y);
			}
			return graphicsPath;
		}
	}
}
