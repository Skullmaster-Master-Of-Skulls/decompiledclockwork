using System;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.codec;

namespace iTextSharp.text
{
	// Token: 0x02000173 RID: 371
	public class ImgCCITT : Image
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x0005305D File Offset: 0x0005205D
		public ImgCCITT(Image image) : base(image)
		{
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00053068 File Offset: 0x00052068
		public ImgCCITT(int width, int height, bool reverseBits, int typeCCITT, int parameters, byte[] data) : base(null)
		{
			if (typeCCITT != 256 && typeCCITT != 257 && typeCCITT != 258)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("the.ccitt.compression.type.must.be.ccittg4.ccittg3.1d.or.ccittg3.2d"));
			}
			if (reverseBits)
			{
				TIFFFaxDecoder.ReverseBits(data);
			}
			this.type = 34;
			this.scaledHeight = (float)height;
			this.Top = this.scaledHeight;
			this.scaledWidth = (float)width;
			this.Right = this.scaledWidth;
			this.colorspace = parameters;
			this.bpc = typeCCITT;
			this.rawData = data;
			this.plainWidth = this.Width;
			this.plainHeight = base.Height;
		}
	}
}
