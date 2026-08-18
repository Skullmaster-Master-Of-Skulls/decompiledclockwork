using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x02000225 RID: 549
	public class ImgRaw : Image
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x00079D3F File Offset: 0x00078D3F
		public ImgRaw(Image image) : base(image)
		{
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00079D48 File Offset: 0x00078D48
		public ImgRaw(int width, int height, int components, int bpc, byte[] data) : base(null)
		{
			this.type = 34;
			this.scaledHeight = (float)height;
			this.Top = this.scaledHeight;
			this.scaledWidth = (float)width;
			this.Right = this.scaledWidth;
			if (components != 1 && components != 3 && components != 4)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("components.must.be.1.3.or.4"));
			}
			if (bpc != 1 && bpc != 2 && bpc != 4 && bpc != 8)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("bits.per.component.must.be.1.2.4.or.8"));
			}
			this.colorspace = components;
			this.bpc = bpc;
			this.rawData = data;
			this.plainWidth = this.Width;
			this.plainHeight = base.Height;
		}
	}
}
