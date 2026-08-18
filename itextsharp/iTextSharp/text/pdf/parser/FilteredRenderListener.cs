using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020004E4 RID: 1252
	public class FilteredRenderListener : IRenderListener
	{
		// Token: 0x06002AD6 RID: 10966 RVA: 0x0010458E File Offset: 0x0010358E
		public FilteredRenderListener(IRenderListener deleg, RenderFilter[] filters)
		{
			this.deleg = deleg;
			this.filters = filters;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x001045A4 File Offset: 0x001035A4
		public void RenderText(TextRenderInfo renderInfo)
		{
			foreach (RenderFilter renderFilter in this.filters)
			{
				if (!renderFilter.AllowText(renderInfo))
				{
					return;
				}
			}
			this.deleg.RenderText(renderInfo);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x001045E1 File Offset: 0x001035E1
		public void BeginTextBlock()
		{
			this.deleg.BeginTextBlock();
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x001045EE File Offset: 0x001035EE
		public void EndTextBlock()
		{
			this.deleg.EndTextBlock();
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x001045FC File Offset: 0x001035FC
		public void RenderImage(ImageRenderInfo renderInfo)
		{
			foreach (RenderFilter renderFilter in this.filters)
			{
				if (!renderFilter.AllowImage(renderInfo))
				{
					return;
				}
			}
			this.deleg.RenderImage(renderInfo);
		}

		// Token: 0x04001DA2 RID: 7586
		private IRenderListener deleg;

		// Token: 0x04001DA3 RID: 7587
		private RenderFilter[] filters;
	}
}
