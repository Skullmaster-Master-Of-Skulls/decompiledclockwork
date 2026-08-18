using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x0200021A RID: 538
	public abstract class RenderFilter
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x000760EC File Offset: 0x000750EC
		public virtual bool AllowText(TextRenderInfo renderInfo)
		{
			return true;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000760EF File Offset: 0x000750EF
		public virtual bool AllowImage(ImageRenderInfo renderInfo)
		{
			return true;
		}
	}
}
