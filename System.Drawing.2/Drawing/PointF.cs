using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000040 RID: 64
	[ComVisible(true)]
	[Serializable]
	public struct PointF
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0001A99E File Offset: 0x00018B9E
		public PointF(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001A9AE File Offset: 0x00018BAE
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0f && this.y == 0f;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001A9CC File Offset: 0x00018BCC
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		public float X
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

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001A9DD File Offset: 0x00018BDD
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0001A9E5 File Offset: 0x00018BE5
		public float Y
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

		// Token: 0x0600065C RID: 1628 RVA: 0x0001A9EE File Offset: 0x00018BEE
		public static PointF operator +(PointF pt, Size sz)
		{
			return PointF.Add(pt, sz);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A9F7 File Offset: 0x00018BF7
		public static PointF operator -(PointF pt, Size sz)
		{
			return PointF.Subtract(pt, sz);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001AA00 File Offset: 0x00018C00
		public static PointF operator +(PointF pt, SizeF sz)
		{
			return PointF.Add(pt, sz);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001AA09 File Offset: 0x00018C09
		public static PointF operator -(PointF pt, SizeF sz)
		{
			return PointF.Subtract(pt, sz);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001AA12 File Offset: 0x00018C12
		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001AA36 File Offset: 0x00018C36
		public static bool operator !=(PointF left, PointF right)
		{
			return !(left == right);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001AA42 File Offset: 0x00018C42
		public static PointF Add(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001AA69 File Offset: 0x00018C69
		public static PointF Subtract(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001AA90 File Offset: 0x00018C90
		public static PointF Add(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001AAB5 File Offset: 0x00018CB5
		public static PointF Subtract(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001AADC File Offset: 0x00018CDC
		public override bool Equals(object obj)
		{
			if (!(obj is PointF))
			{
				return false;
			}
			PointF pointF = (PointF)obj;
			return pointF.X == this.X && pointF.Y == this.Y && pointF.GetType().Equals(base.GetType());
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001AB3A File Offset: 0x00018D3A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001AB4C File Offset: 0x00018D4C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", new object[]
			{
				this.x,
				this.y
			});
		}

		// Token: 0x0400056E RID: 1390
		public static readonly PointF Empty;

		// Token: 0x0400056F RID: 1391
		private float x;

		// Token: 0x04000570 RID: 1392
		private float y;
	}
}
