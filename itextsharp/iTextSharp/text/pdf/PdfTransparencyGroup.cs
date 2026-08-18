using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005CF RID: 1487
	public class PdfTransparencyGroup : PdfDictionary
	{
		// Token: 0x0600333A RID: 13114 RVA: 0x0013E641 File Offset: 0x0013D641
		public PdfTransparencyGroup()
		{
			base.Put(PdfName.S, PdfName.TRANSPARENCY);
		}

		// Token: 0x170008DE RID: 2270
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x0013E659 File Offset: 0x0013D659
		public bool Isolated
		{
			set
			{
				if (value)
				{
					base.Put(PdfName.I, PdfBoolean.PDFTRUE);
					return;
				}
				base.Remove(PdfName.I);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x0013E67A File Offset: 0x0013D67A
		public bool Knockout
		{
			set
			{
				if (value)
				{
					base.Put(PdfName.K, PdfBoolean.PDFTRUE);
					return;
				}
				base.Remove(PdfName.K);
			}
		}
	}
}
