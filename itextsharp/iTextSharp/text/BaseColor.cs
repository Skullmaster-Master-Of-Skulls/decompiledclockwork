using System;
using System.Drawing;

namespace iTextSharp.text
{
	// Token: 0x020001CB RID: 459
	public class BaseColor
	{
		// Token: 0x060011F2 RID: 4594 RVA: 0x0006779E File Offset: 0x0006679E
		public BaseColor(int red, int green, int blue)
		{
			this.color = Color.FromArgb(red, green, blue);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000677B4 File Offset: 0x000667B4
		public BaseColor(int red, int green, int blue, int alpha)
		{
			this.color = Color.FromArgb(alpha, red, green, blue);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000677CC File Offset: 0x000667CC
		public BaseColor(float red, float green, float blue)
		{
			this.color = Color.FromArgb((int)((double)(red * 255f) + 0.5), (int)((double)(green * 255f) + 0.5), (int)((double)(blue * 255f) + 0.5));
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00067824 File Offset: 0x00066824
		public BaseColor(float red, float green, float blue, float alpha)
		{
			this.color = Color.FromArgb((int)((double)(alpha * 255f) + 0.5), (int)((double)(red * 255f) + 0.5), (int)((double)(green * 255f) + 0.5), (int)((double)(blue * 255f) + 0.5));
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0006788F File Offset: 0x0006688F
		public BaseColor(int argb)
		{
			this.color = Color.FromArgb(argb);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000678A3 File Offset: 0x000668A3
		public BaseColor(Color color)
		{
			this.color = color;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000678B2 File Offset: 0x000668B2
		public int R
		{
			get
			{
				return (int)this.color.R;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000678BF File Offset: 0x000668BF
		public int G
		{
			get
			{
				return (int)this.color.G;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000678CC File Offset: 0x000668CC
		public int B
		{
			get
			{
				return (int)this.color.B;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000678DC File Offset: 0x000668DC
		public BaseColor Brighter()
		{
			int num = (int)this.color.R;
			int num2 = (int)this.color.G;
			int num3 = (int)this.color.B;
			int num4 = 3;
			if (num == 0 && num2 == 0 && num3 == 0)
			{
				return new BaseColor(num4, num4, num4);
			}
			if (num > 0 && num < num4)
			{
				num = num4;
			}
			if (num2 > 0 && num2 < num4)
			{
				num2 = num4;
			}
			if (num3 > 0 && num3 < num4)
			{
				num3 = num4;
			}
			return new BaseColor(Math.Min((int)((double)num / 0.7), 255), Math.Min((int)((double)num2 / 0.7), 255), Math.Min((int)((double)num3 / 0.7), 255));
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0006798C File Offset: 0x0006698C
		public BaseColor Darker()
		{
			return new BaseColor(Math.Max((int)((double)this.color.R * 0.7), 0), Math.Max((int)((double)this.color.G * 0.7), 0), Math.Max((int)((double)this.color.B * 0.7), 0));
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x000679F5 File Offset: 0x000669F5
		public override bool Equals(object obj)
		{
			return obj is BaseColor && this.color.Equals(((BaseColor)obj).color);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00067A22 File Offset: 0x00066A22
		public override int GetHashCode()
		{
			return this.color.GetHashCode();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00067A35 File Offset: 0x00066A35
		public int ToArgb()
		{
			return this.color.ToArgb();
		}

		// Token: 0x04000C99 RID: 3225
		private const double FACTOR = 0.7;

		// Token: 0x04000C9A RID: 3226
		public static readonly BaseColor WHITE = new BaseColor(255, 255, 255);

		// Token: 0x04000C9B RID: 3227
		public static readonly BaseColor LIGHT_GRAY = new BaseColor(192, 192, 192);

		// Token: 0x04000C9C RID: 3228
		public static readonly BaseColor GRAY = new BaseColor(128, 128, 128);

		// Token: 0x04000C9D RID: 3229
		public static readonly BaseColor DARK_GRAY = new BaseColor(64, 64, 64);

		// Token: 0x04000C9E RID: 3230
		public static readonly BaseColor BLACK = new BaseColor(0, 0, 0);

		// Token: 0x04000C9F RID: 3231
		public static readonly BaseColor RED = new BaseColor(255, 0, 0);

		// Token: 0x04000CA0 RID: 3232
		public static readonly BaseColor PINK = new BaseColor(255, 175, 175);

		// Token: 0x04000CA1 RID: 3233
		public static readonly BaseColor ORANGE = new BaseColor(255, 200, 0);

		// Token: 0x04000CA2 RID: 3234
		public static readonly BaseColor YELLOW = new BaseColor(255, 255, 0);

		// Token: 0x04000CA3 RID: 3235
		public static readonly BaseColor GREEN = new BaseColor(0, 255, 0);

		// Token: 0x04000CA4 RID: 3236
		public static readonly BaseColor MAGENTA = new BaseColor(255, 0, 255);

		// Token: 0x04000CA5 RID: 3237
		public static readonly BaseColor CYAN = new BaseColor(0, 255, 255);

		// Token: 0x04000CA6 RID: 3238
		public static readonly BaseColor BLUE = new BaseColor(0, 0, 255);

		// Token: 0x04000CA7 RID: 3239
		private Color color;
	}
}
