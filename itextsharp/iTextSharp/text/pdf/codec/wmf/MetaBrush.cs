using System;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x020005F5 RID: 1525
	public class MetaBrush : MetaObject
	{
		// Token: 0x060033E0 RID: 13280 RVA: 0x00140E29 File Offset: 0x0013FE29
		public MetaBrush()
		{
			this.type = 2;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x00140E43 File Offset: 0x0013FE43
		public void Init(InputMeta meta)
		{
			this.style = meta.ReadWord();
			this.color = meta.ReadColor();
			this.hatch = meta.ReadWord();
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x00140E69 File Offset: 0x0013FE69
		public int Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x00140E71 File Offset: 0x0013FE71
		public int Hatch
		{
			get
			{
				return this.hatch;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x00140E79 File Offset: 0x0013FE79
		public BaseColor Color
		{
			get
			{
				return this.color;
			}
		}

		// Token: 0x040022F8 RID: 8952
		public const int BS_SOLID = 0;

		// Token: 0x040022F9 RID: 8953
		public const int BS_NULL = 1;

		// Token: 0x040022FA RID: 8954
		public const int BS_HATCHED = 2;

		// Token: 0x040022FB RID: 8955
		public const int BS_PATTERN = 3;

		// Token: 0x040022FC RID: 8956
		public const int BS_DIBPATTERN = 5;

		// Token: 0x040022FD RID: 8957
		public const int HS_HORIZONTAL = 0;

		// Token: 0x040022FE RID: 8958
		public const int HS_VERTICAL = 1;

		// Token: 0x040022FF RID: 8959
		public const int HS_FDIAGONAL = 2;

		// Token: 0x04002300 RID: 8960
		public const int HS_BDIAGONAL = 3;

		// Token: 0x04002301 RID: 8961
		public const int HS_CROSS = 4;

		// Token: 0x04002302 RID: 8962
		public const int HS_DIAGCROSS = 5;

		// Token: 0x04002303 RID: 8963
		private int style;

		// Token: 0x04002304 RID: 8964
		private int hatch;

		// Token: 0x04002305 RID: 8965
		private BaseColor color = BaseColor.WHITE;
	}
}
