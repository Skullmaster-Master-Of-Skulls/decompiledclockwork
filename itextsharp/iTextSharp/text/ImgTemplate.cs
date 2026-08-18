using System;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x020000F3 RID: 243
	public class ImgTemplate : Image
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x00031F52 File Offset: 0x00030F52
		public ImgTemplate(Image image) : base(image)
		{
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00031F5C File Offset: 0x00030F5C
		public ImgTemplate(PdfTemplate template) : base(null)
		{
			if (template == null)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("the.template.can.not.be.null"));
			}
			if (template.Type == 3)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("a.pattern.can.not.be.used.as.a.template.to.create.an.image"));
			}
			this.type = 35;
			this.scaledHeight = template.Height;
			this.Top = this.scaledHeight;
			this.scaledWidth = template.Width;
			this.Right = this.scaledWidth;
			base.TemplateData = template;
			this.plainWidth = this.Width;
			this.plainHeight = base.Height;
		}
	}
}
