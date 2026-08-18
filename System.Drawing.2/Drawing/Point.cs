using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200002A RID: 42
	[TypeConverter(typeof(PointConverter))]
	[ComVisible(true)]
	[Serializable]
	public struct Point
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00014FCF File Offset: 0x000131CF
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00014FDF File Offset: 0x000131DF
		public Point(Size sz)
		{
			this.x = sz.Width;
			this.y = sz.Height;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00014FFB File Offset: 0x000131FB
		public Point(int dw)
		{
			this.x = (int)((short)Point.LOWORD(dw));
			this.y = (int)((short)Point.HIWORD(dw));
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00015017 File Offset: 0x00013217
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0 && this.y == 0;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001502C File Offset: 0x0001322C
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00015034 File Offset: 0x00013234
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001503D File Offset: 0x0001323D
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00015045 File Offset: 0x00013245
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001504E File Offset: 0x0001324E
		public static implicit operator PointF(Point p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00015065 File Offset: 0x00013265
		public static explicit operator Size(Point p)
		{
			return new Size(p.X, p.Y);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001507A File Offset: 0x0001327A
		public static Point operator +(Point pt, Size sz)
		{
			return Point.Add(pt, sz);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00015083 File Offset: 0x00013283
		public static Point operator -(Point pt, Size sz)
		{
			return Point.Subtract(pt, sz);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001508C File Offset: 0x0001328C
		public static bool operator ==(Point left, Point right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000150B0 File Offset: 0x000132B0
		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000150BC File Offset: 0x000132BC
		public static Point Add(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000150E1 File Offset: 0x000132E1
		public static Point Subtract(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00015106 File Offset: 0x00013306
		public static Point Ceiling(PointF value)
		{
			return new Point((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y));
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00015129 File Offset: 0x00013329
		public static Point Truncate(PointF value)
		{
			return new Point((int)value.X, (int)value.Y);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00015140 File Offset: 0x00013340
		public static Point Round(PointF value)
		{
			return new Point((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y));
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015164 File Offset: 0x00013364
		public override bool Equals(object obj)
		{
			if (!(obj is Point))
			{
				return false;
			}
			Point point = (Point)obj;
			return point.X == this.X && point.Y == this.Y;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000151A2 File Offset: 0x000133A2
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000151B1 File Offset: 0x000133B1
		public void Offset(int dx, int dy)
		{
			this.X += dx;
			this.Y += dy;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000151CF File Offset: 0x000133CF
		public void Offset(Point p)
		{
			this.Offset(p.X, p.Y);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000151E8 File Offset: 0x000133E8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(CultureInfo.CurrentCulture),
				",Y=",
				this.Y.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00015244 File Offset: 0x00013444
		private static int HIWORD(int n)
		{
			return n >> 16 & 65535;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00015250 File Offset: 0x00013450
		private static int LOWORD(int n)
		{
			return n & 65535;
		}

		// Token: 0x040002FD RID: 765
		public static readonly Point Empty;

		// Token: 0x040002FE RID: 766
		private int x;

		// Token: 0x040002FF RID: 767
		private int y;
	}
}
