using System;

namespace iTextSharp.text.pdf.draw
{
	// Token: 0x0200016B RID: 363
	public interface IDrawInterface
	{
		// Token: 0x06000DB6 RID: 3510
		void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y);
	}
}
