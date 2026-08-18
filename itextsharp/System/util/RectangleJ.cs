using System;

namespace System.util
{
	// Token: 0x02000598 RID: 1432
	public class RectangleJ
	{
		// Token: 0x06003108 RID: 12552 RVA: 0x0012FD5C File Offset: 0x0012ED5C
		public RectangleJ(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x0012FD81 File Offset: 0x0012ED81
		// (set) Token: 0x0600310A RID: 12554 RVA: 0x0012FD89 File Offset: 0x0012ED89
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

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x0012FD92 File Offset: 0x0012ED92
		// (set) Token: 0x0600310C RID: 12556 RVA: 0x0012FD9A File Offset: 0x0012ED9A
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

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x0012FDA3 File Offset: 0x0012EDA3
		// (set) Token: 0x0600310E RID: 12558 RVA: 0x0012FDAB File Offset: 0x0012EDAB
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x0012FDB4 File Offset: 0x0012EDB4
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x0012FDBC File Offset: 0x0012EDBC
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0012FDC8 File Offset: 0x0012EDC8
		public void Add(RectangleJ rect)
		{
			float num = Math.Min(Math.Min(this.x, this.x + this.width), Math.Min(rect.x, this.x + rect.width));
			float num2 = Math.Max(Math.Max(this.x, this.x + this.width), Math.Max(rect.x, this.x + rect.width));
			float num3 = Math.Min(Math.Min(this.y, this.y + this.height), Math.Min(rect.y, rect.y + rect.height));
			float num4 = Math.Max(Math.Max(this.y, this.y + this.height), Math.Max(rect.y, rect.y + rect.height));
			this.x = num;
			this.y = num3;
			this.width = num2 - num;
			this.height = num4 - num3;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x0012FED0 File Offset: 0x0012EED0
		public int Outcode(double x, double y)
		{
			int num = 0;
			if (this.width <= 0f)
			{
				num |= 5;
			}
			else if (x < (double)this.x)
			{
				num |= 1;
			}
			else if (x > (double)this.x + (double)this.width)
			{
				num |= 4;
			}
			if (this.height <= 0f)
			{
				num |= 10;
			}
			else if (y < (double)this.y)
			{
				num |= 2;
			}
			else if (y > (double)this.y + (double)this.height)
			{
				num |= 8;
			}
			return num;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0012FF54 File Offset: 0x0012EF54
		public bool IntersectsLine(double x1, double y1, double x2, double y2)
		{
			int num;
			if ((num = this.Outcode(x2, y2)) == 0)
			{
				return true;
			}
			int num2;
			while ((num2 = this.Outcode(x1, y1)) != 0)
			{
				if ((num2 & num) != 0)
				{
					return false;
				}
				if ((num2 & 5) != 0)
				{
					double num3 = (double)this.X;
					if ((num2 & 4) != 0)
					{
						num3 += (double)this.Width;
					}
					y1 += (num3 - x1) * (y2 - y1) / (x2 - x1);
					x1 = num3;
				}
				else
				{
					double num4 = (double)this.Y;
					if ((num2 & 8) != 0)
					{
						num4 += (double)this.Height;
					}
					x1 += (num4 - y1) * (x2 - x1) / (y2 - y1);
					y1 = num4;
				}
			}
			return true;
		}

		// Token: 0x040021BE RID: 8638
		public const int OUT_LEFT = 1;

		// Token: 0x040021BF RID: 8639
		public const int OUT_TOP = 2;

		// Token: 0x040021C0 RID: 8640
		public const int OUT_RIGHT = 4;

		// Token: 0x040021C1 RID: 8641
		public const int OUT_BOTTOM = 8;

		// Token: 0x040021C2 RID: 8642
		private float x;

		// Token: 0x040021C3 RID: 8643
		private float y;

		// Token: 0x040021C4 RID: 8644
		private float width;

		// Token: 0x040021C5 RID: 8645
		private float height;
	}
}
