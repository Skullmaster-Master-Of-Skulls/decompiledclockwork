using System;
using System.Collections;
using Telerik.Pdf;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001685 RID: 5765
	internal abstract class CIDFont : Font
	{
		// Token: 0x1700442A RID: 17450
		// (get) Token: 0x0600DED3 RID: 57043
		public abstract string CidBaseFont { get; }

		// Token: 0x1700442B RID: 17451
		// (get) Token: 0x0600DED4 RID: 57044
		public abstract PdfWArray WArray { get; }

		// Token: 0x1700442C RID: 17452
		// (get) Token: 0x0600DED5 RID: 57045
		public abstract IDictionary CMapEntries { get; }

		// Token: 0x1700442D RID: 17453
		// (get) Token: 0x0600DED6 RID: 57046 RVA: 0x0030E0F1 File Offset: 0x0030C2F1
		public override PdfFontTypeEnum Type
		{
			get
			{
				return PdfFontTypeEnum.CIDFont;
			}
		}

		// Token: 0x1700442E RID: 17454
		// (get) Token: 0x0600DED7 RID: 57047 RVA: 0x0030E0F4 File Offset: 0x0030C2F4
		public virtual string Registry
		{
			get
			{
				return "Adobe";
			}
		}

		// Token: 0x1700442F RID: 17455
		// (get) Token: 0x0600DED8 RID: 57048 RVA: 0x0030E0FB File Offset: 0x0030C2FB
		public virtual string Ordering
		{
			get
			{
				return "Identity";
			}
		}

		// Token: 0x17004430 RID: 17456
		// (get) Token: 0x0600DED9 RID: 57049 RVA: 0x0030E102 File Offset: 0x0030C302
		public virtual int Supplement
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17004431 RID: 17457
		// (get) Token: 0x0600DEDA RID: 57050 RVA: 0x0030E105 File Offset: 0x0030C305
		public virtual int DefaultWidth
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x0400402C RID: 16428
		public const int DefaultWidthConst = 1000;
	}
}
