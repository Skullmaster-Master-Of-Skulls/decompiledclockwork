using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200041A RID: 1050
	public class PdfBorderDictionary : PdfDictionary
	{
		// Token: 0x060023C4 RID: 9156 RVA: 0x000DAA40 File Offset: 0x000D9A40
		public PdfBorderDictionary(float borderWidth, int borderStyle, PdfDashPattern dashes)
		{
			base.Put(PdfName.W, new PdfNumber(borderWidth));
			switch (borderStyle)
			{
			case 0:
				base.Put(PdfName.S, PdfName.S);
				return;
			case 1:
				if (dashes != null)
				{
					base.Put(PdfName.D, dashes);
				}
				base.Put(PdfName.S, PdfName.D);
				return;
			case 2:
				base.Put(PdfName.S, PdfName.B);
				return;
			case 3:
				base.Put(PdfName.S, PdfName.I);
				return;
			case 4:
				base.Put(PdfName.S, PdfName.U);
				return;
			default:
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.border.style"));
			}
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000DAAF5 File Offset: 0x000D9AF5
		public PdfBorderDictionary(float borderWidth, int borderStyle) : this(borderWidth, borderStyle, null)
		{
		}

		// Token: 0x04001897 RID: 6295
		public const int STYLE_SOLID = 0;

		// Token: 0x04001898 RID: 6296
		public const int STYLE_DASHED = 1;

		// Token: 0x04001899 RID: 6297
		public const int STYLE_BEVELED = 2;

		// Token: 0x0400189A RID: 6298
		public const int STYLE_INSET = 3;

		// Token: 0x0400189B RID: 6299
		public const int STYLE_UNDERLINE = 4;
	}
}
