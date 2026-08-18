using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000218 RID: 536
	public interface IRenderListener
	{
		// Token: 0x060014EC RID: 5356
		void BeginTextBlock();

		// Token: 0x060014ED RID: 5357
		void RenderText(TextRenderInfo renderInfo);

		// Token: 0x060014EE RID: 5358
		void EndTextBlock();

		// Token: 0x060014EF RID: 5359
		void RenderImage(ImageRenderInfo renderInfo);
	}
}
