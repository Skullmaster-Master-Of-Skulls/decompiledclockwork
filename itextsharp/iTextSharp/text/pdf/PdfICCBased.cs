using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200063B RID: 1595
	public class PdfICCBased : PdfStream
	{
		// Token: 0x060035FB RID: 13819 RVA: 0x0014F4E0 File Offset: 0x0014E4E0
		public PdfICCBased(ICC_Profile profile) : this(profile, -1)
		{
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x0014F4EC File Offset: 0x0014E4EC
		public PdfICCBased(ICC_Profile profile, int compressionLevel)
		{
			int numComponents = profile.NumComponents;
			switch (numComponents)
			{
			case 1:
				base.Put(PdfName.ALTERNATE, PdfName.DEVICEGRAY);
				goto IL_75;
			case 3:
				base.Put(PdfName.ALTERNATE, PdfName.DEVICERGB);
				goto IL_75;
			case 4:
				base.Put(PdfName.ALTERNATE, PdfName.DEVICECMYK);
				goto IL_75;
			}
			throw new PdfException(MessageLocalization.GetComposedMessage("1.component.s.is.not.supported", numComponents));
			IL_75:
			base.Put(PdfName.N, new PdfNumber(numComponents));
			this.bytes = profile.Data;
			base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
			base.FlateCompress(compressionLevel);
		}
	}
}
