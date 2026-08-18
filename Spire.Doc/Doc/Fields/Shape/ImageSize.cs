using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000039 RID: 57
	public class ImageSize
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000069A0 File Offset: 0x000059A0
		public ImageSize(int widthPixels, int heightPixels) : this(widthPixels, heightPixels, 96.0, 96.0)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000069C8 File Offset: 0x000059C8
		public ImageSize(int widthPixels, int heightPixels, double horizontalResolution, double verticalResolution)
		{
			this.ᜀ = widthPixels;
			this.ᜁ = heightPixels;
			this.ᜂ = horizontalResolution;
			this.ᜃ = verticalResolution;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000069F8 File Offset: 0x000059F8
		internal ImageSize(spr\u2481 A_0)
		{
			this.ᜀ = A_0.ᜁ();
			this.ᜁ = A_0.ᜎ();
			this.ᜂ = A_0.ᜄ();
			this.ᜃ = A_0.ᜊ();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00006A3C File Offset: 0x00005A3C
		internal bool IsValid
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.WidthPixels > 0)
					{
						return this.HeightPixels > 0;
					}
					break;
				}
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00006A90 File Offset: 0x00005A90
		public int WidthPixels
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00006AD4 File Offset: 0x00005AD4
		public int HeightPixels
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00006B18 File Offset: 0x00005B18
		public double HorizontalResolution
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜂ;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00006B5C File Offset: 0x00005B5C
		public double VerticalResolution
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜃ;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00006BA0 File Offset: 0x00005BA0
		internal int WidthTwips
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return spr\u23C4.ᜃ((double)this.ᜀ, this.ᜂ);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00006BF0 File Offset: 0x00005BF0
		internal int HeightTwips
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return spr\u23C4.ᜃ((double)this.ᜁ, this.ᜃ);
			}
		}

		// Token: 0x040002B9 RID: 697
		private readonly int ᜀ;

		// Token: 0x040002BA RID: 698
		private string[] \u25D9ª\u00AD\u0099;

		// Token: 0x040002BB RID: 699
		private byte \u25D8\u008A\u008C\u0082;

		// Token: 0x040002BC RID: 700
		private bool \u2460\u00A6\u00AF\u0087;

		// Token: 0x040002BD RID: 701
		private readonly int ᜁ;

		// Token: 0x040002BE RID: 702
		private bool[] \u2593\u009A\u009A\u0094;

		// Token: 0x040002BF RID: 703
		private float \u2460\u00AF\u00A3\u009D;

		// Token: 0x040002C0 RID: 704
		private readonly double ᜂ;

		// Token: 0x040002C1 RID: 705
		private long \u2460\u0094\u0088\u00A4;

		// Token: 0x040002C2 RID: 706
		private readonly double ᜃ;
	}
}
