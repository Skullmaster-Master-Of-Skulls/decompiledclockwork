using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200015B RID: 347
	public sealed class Type3Glyph : PdfContentByte
	{
		// Token: 0x06000D00 RID: 3328 RVA: 0x00047C6E File Offset: 0x00046C6E
		private Type3Glyph() : base(null)
		{
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00047C78 File Offset: 0x00046C78
		internal Type3Glyph(PdfWriter writer, PageResources pageResources, float wx, float llx, float lly, float urx, float ury, bool colorized) : base(writer)
		{
			this.pageResources = pageResources;
			this.colorized = colorized;
			if (colorized)
			{
				this.content.Append(wx).Append(" 0 d0\n");
				return;
			}
			this.content.Append(wx).Append(" 0 ").Append(llx).Append(' ').Append(lly).Append(' ').Append(urx).Append(' ').Append(ury).Append(" d1\n");
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00047D09 File Offset: 0x00046D09
		internal override PageResources PageResources
		{
			get
			{
				return this.pageResources;
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00047D14 File Offset: 0x00046D14
		public override void AddImage(Image image, float a, float b, float c, float d, float e, float f, bool inlineImage)
		{
			if (!this.colorized && (!image.IsMask() || (image.Bpc != 1 && image.Bpc <= 255)))
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("not.colorized.typed3.fonts.only.accept.mask.images"));
			}
			base.AddImage(image, a, b, c, d, e, f, inlineImage);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00047D6C File Offset: 0x00046D6C
		public PdfContentByte GetDuplicate()
		{
			return new Type3Glyph
			{
				writer = this.writer,
				pdf = this.pdf,
				pageResources = this.pageResources,
				colorized = this.colorized
			};
		}

		// Token: 0x040009C7 RID: 2503
		private PageResources pageResources;

		// Token: 0x040009C8 RID: 2504
		private bool colorized;
	}
}
