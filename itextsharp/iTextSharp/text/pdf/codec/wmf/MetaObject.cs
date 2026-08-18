using System;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x020000EE RID: 238
	public class MetaObject
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0002FF86 File Offset: 0x0002EF86
		public MetaObject()
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002FF8E File Offset: 0x0002EF8E
		public MetaObject(int type)
		{
			this.type = type;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002FF9D File Offset: 0x0002EF9D
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400078D RID: 1933
		public const int META_NOT_SUPPORTED = 0;

		// Token: 0x0400078E RID: 1934
		public const int META_PEN = 1;

		// Token: 0x0400078F RID: 1935
		public const int META_BRUSH = 2;

		// Token: 0x04000790 RID: 1936
		public const int META_FONT = 3;

		// Token: 0x04000791 RID: 1937
		public int type;
	}
}
