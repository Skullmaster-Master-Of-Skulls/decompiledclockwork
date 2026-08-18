using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002D0 RID: 720
	internal class PdfColor : PdfArray
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x0009ED54 File Offset: 0x0009DD54
		internal PdfColor(int red, int green, int blue) : base(new PdfNumber((double)(red & 255) / 255.0))
		{
			this.Add(new PdfNumber((double)(green & 255) / 255.0));
			this.Add(new PdfNumber((double)(blue & 255) / 255.0));
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0009EDBA File Offset: 0x0009DDBA
		internal PdfColor(BaseColor color) : this(color.R, color.G, color.B)
		{
		}
	}
}
