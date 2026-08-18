using System;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x020000EF RID: 239
	public class MetaPen : MetaObject
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0002FFA5 File Offset: 0x0002EFA5
		public MetaPen()
		{
			this.type = 1;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002FFC6 File Offset: 0x0002EFC6
		public void Init(InputMeta meta)
		{
			this.style = meta.ReadWord();
			this.penWidth = meta.ReadShort();
			meta.ReadWord();
			this.color = meta.ReadColor();
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0002FFF3 File Offset: 0x0002EFF3
		public int Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002FFFB File Offset: 0x0002EFFB
		public int PenWidth
		{
			get
			{
				return this.penWidth;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00030003 File Offset: 0x0002F003
		public BaseColor Color
		{
			get
			{
				return this.color;
			}
		}

		// Token: 0x04000792 RID: 1938
		public const int PS_SOLID = 0;

		// Token: 0x04000793 RID: 1939
		public const int PS_DASH = 1;

		// Token: 0x04000794 RID: 1940
		public const int PS_DOT = 2;

		// Token: 0x04000795 RID: 1941
		public const int PS_DASHDOT = 3;

		// Token: 0x04000796 RID: 1942
		public const int PS_DASHDOTDOT = 4;

		// Token: 0x04000797 RID: 1943
		public const int PS_NULL = 5;

		// Token: 0x04000798 RID: 1944
		public const int PS_INSIDEFRAME = 6;

		// Token: 0x04000799 RID: 1945
		private int style;

		// Token: 0x0400079A RID: 1946
		private int penWidth = 1;

		// Token: 0x0400079B RID: 1947
		private BaseColor color = BaseColor.BLACK;
	}
}
