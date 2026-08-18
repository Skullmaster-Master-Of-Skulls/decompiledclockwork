using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001785 RID: 6021
	public struct DefaultFigures
	{
		// Token: 0x0600EAD2 RID: 60114 RVA: 0x00357BE8 File Offset: 0x00355DE8
		public static bool Contains(string name)
		{
			return DefaultFigures.FiguresList.Contains(name);
		}

		// Token: 0x0600EAD3 RID: 60115 RVA: 0x00357BF8 File Offset: 0x00355DF8
		internal static GraphicsPath GetPath(string name)
		{
			Rectangle rect = new Rectangle(0, 0, 10, 10);
			GraphicsPath graphicsPath = null;
			switch (name)
			{
			case "Rectangle":
				graphicsPath = new GraphicsPath();
				graphicsPath.AddRectangle(rect);
				break;
			case "Ellipse":
			case "Circle":
				graphicsPath = new GraphicsPath();
				graphicsPath.AddEllipse(rect);
				break;
			case "Triangle":
			{
				Point[] array = new Point[3];
				array[0].X = rect.Left + rect.Width / 2;
				array[0].Y = rect.Top;
				array[1].X = rect.Right;
				array[1].Y = rect.Bottom;
				array[2].X = rect.Left;
				array[2].Y = rect.Bottom;
				graphicsPath = new GraphicsPath(array, DefaultFigures.InitByteArray(3, 1));
				break;
			}
			case "Diamond":
			{
				Point[] array2 = new Point[4];
				array2[0].X = rect.Left + rect.Width / 2;
				array2[0].Y = rect.Top;
				array2[1].X = rect.Right;
				array2[1].Y = rect.Top + rect.Height / 2;
				array2[2].X = rect.Left + rect.Width / 2;
				array2[2].Y = rect.Bottom;
				array2[3].X = rect.Left;
				array2[3].Y = rect.Top + rect.Height / 2;
				graphicsPath = new GraphicsPath(array2, DefaultFigures.InitByteArray(4, 1));
				break;
			}
			case "Cross":
			{
				Point[] array3 = new Point[12];
				array3[0].X = rect.Left + rect.Width / 4;
				array3[0].Y = rect.Top;
				array3[1].X = rect.Left + rect.Width / 2;
				array3[1].Y = rect.Top + rect.Height / 5 * 2;
				array3[2].X = rect.Right - rect.Width / 4;
				array3[2].Y = rect.Top;
				array3[3].X = rect.Right;
				array3[3].Y = rect.Top + rect.Height / 4;
				array3[4].X = rect.Right - rect.Width / 5 * 2;
				array3[4].Y = rect.Top + rect.Height / 2;
				array3[5].X = rect.Right;
				array3[5].Y = rect.Bottom - rect.Height / 4;
				array3[6].X = rect.Right - rect.Width / 4;
				array3[6].Y = rect.Bottom;
				array3[7].X = rect.Left + rect.Width / 2;
				array3[7].Y = rect.Bottom - rect.Height / 5 * 2;
				array3[8].X = rect.Left + rect.Width / 4;
				array3[8].Y = rect.Bottom;
				array3[9].X = rect.Left;
				array3[9].Y = rect.Bottom - rect.Height / 4;
				array3[10].X = rect.Left + rect.Width / 5 * 2;
				array3[10].Y = rect.Top + rect.Height / 2;
				array3[11].X = rect.Left;
				array3[11].Y = rect.Top + rect.Height / 4;
				graphicsPath = new GraphicsPath(array3, DefaultFigures.InitByteArray(12, 1));
				break;
			}
			case "Star3":
				graphicsPath = DefaultFigures.CreateStarPath(6, rect, 6f);
				break;
			case "Star4":
				graphicsPath = DefaultFigures.CreateStarPath(8, rect, 4f);
				break;
			case "Star5":
				graphicsPath = DefaultFigures.CreateStarPath(10, rect, 4f);
				break;
			case "Star6":
				graphicsPath = DefaultFigures.CreateStarPath(12, rect, 4f);
				break;
			case "Star7":
				graphicsPath = DefaultFigures.CreateStarPath(14, rect, 4f);
				break;
			}
			if (graphicsPath != null)
			{
				graphicsPath.CloseAllFigures();
			}
			return graphicsPath;
		}

		// Token: 0x0600EAD4 RID: 60116 RVA: 0x003581A0 File Offset: 0x003563A0
		internal static GraphicsPath CreateStarPath(int pointsCount, Rectangle rect, float widthRatio)
		{
			PointF[] array = new PointF[pointsCount];
			int num = rect.Left + rect.Width / 2;
			int num2 = rect.Top + rect.Height / 2;
			float num3 = (float)rect.Width / 2f;
			float num4 = (float)rect.Width / widthRatio;
			int num5 = 360 / pointsCount;
			for (int i = 0; i < pointsCount; i++)
			{
				if (i % 2 == ((pointsCount > 10) ? 1 : 0))
				{
					array[i].X = (float)((double)num + (double)num3 * Math.Cos(3.141592653589793 * (double)i * (double)num5 / 180.0));
					array[i].Y = (float)((double)num2 + (double)num3 * Math.Sin(3.141592653589793 * (double)i * (double)num5 / 180.0));
				}
				else
				{
					array[i].X = (float)((double)num + (double)num4 * Math.Cos(3.141592653589793 * (double)i * (double)num5 / 180.0));
					array[i].Y = (float)((double)num2 + (double)num4 * Math.Sin(3.141592653589793 * (double)i * (double)num5 / 180.0));
				}
			}
			return new GraphicsPath(array, DefaultFigures.InitByteArray(pointsCount, 1));
		}

		// Token: 0x0600EAD5 RID: 60117 RVA: 0x00358304 File Offset: 0x00356504
		internal static byte[] InitByteArray(int itemsCount, byte value)
		{
			byte[] array = new byte[itemsCount];
			for (int i = 0; i < itemsCount; i++)
			{
				array[i] = value;
			}
			return array;
		}

		// Token: 0x040043D8 RID: 17368
		public const string Cross = "Cross";

		// Token: 0x040043D9 RID: 17369
		public const string Diamond = "Diamond";

		// Token: 0x040043DA RID: 17370
		public const string Ellipse = "Ellipse";

		// Token: 0x040043DB RID: 17371
		public const string Circle = "Circle";

		// Token: 0x040043DC RID: 17372
		public const string Rectangle = "Rectangle";

		// Token: 0x040043DD RID: 17373
		public const string Star3 = "Star3";

		// Token: 0x040043DE RID: 17374
		public const string Star4 = "Star4";

		// Token: 0x040043DF RID: 17375
		public const string Star5 = "Star5";

		// Token: 0x040043E0 RID: 17376
		public const string Star6 = "Star6";

		// Token: 0x040043E1 RID: 17377
		public const string Star7 = "Star7";

		// Token: 0x040043E2 RID: 17378
		public const string Triangle = "Triangle";

		// Token: 0x040043E3 RID: 17379
		public static List<string> FiguresList = new List<string>(new string[]
		{
			"Cross",
			"Diamond",
			"Ellipse",
			"Circle",
			"Rectangle",
			"Star3",
			"Star4",
			"Star5",
			"Star6",
			"Star7",
			"Triangle"
		});
	}
}
