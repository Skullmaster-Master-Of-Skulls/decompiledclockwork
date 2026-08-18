using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200004F RID: 79
	public class PdfSpotColor
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000B14F File Offset: 0x0000A14F
		public PdfSpotColor(string name, BaseColor altcs)
		{
			this.name = new PdfName(name);
			this.altcs = altcs;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000B16A File Offset: 0x0000A16A
		public BaseColor AlternativeCS
		{
			get
			{
				return this.altcs;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B184 File Offset: 0x0000A184
		protected internal virtual PdfObject GetSpotObject(PdfWriter writer)
		{
			PdfArray pdfArray = new PdfArray(PdfName.SEPARATION);
			pdfArray.Add(this.name);
			PdfFunction pdfFunction;
			if (this.altcs is ExtendedColor)
			{
				switch (((ExtendedColor)this.altcs).Type)
				{
				case 1:
				{
					pdfArray.Add(PdfName.DEVICEGRAY);
					float[] domain = new float[]
					{
						0f,
						1f
					};
					float[] range = null;
					float[] c = new float[1];
					pdfFunction = PdfFunction.Type2(writer, domain, range, c, new float[]
					{
						((GrayColor)this.altcs).Gray
					}, 1f);
					break;
				}
				case 2:
				{
					pdfArray.Add(PdfName.DEVICECMYK);
					CMYKColor cmykcolor = (CMYKColor)this.altcs;
					float[] domain2 = new float[]
					{
						0f,
						1f
					};
					float[] range2 = null;
					float[] c2 = new float[4];
					pdfFunction = PdfFunction.Type2(writer, domain2, range2, c2, new float[]
					{
						cmykcolor.Cyan,
						cmykcolor.Magenta,
						cmykcolor.Yellow,
						cmykcolor.Black
					}, 1f);
					break;
				}
				default:
					throw new Exception(MessageLocalization.GetComposedMessage("only.rgb.gray.and.cmyk.are.supported.as.alternative.color.spaces"));
				}
			}
			else
			{
				pdfArray.Add(PdfName.DEVICERGB);
				pdfFunction = PdfFunction.Type2(writer, new float[]
				{
					0f,
					1f
				}, null, new float[]
				{
					1f,
					1f,
					1f
				}, new float[]
				{
					(float)this.altcs.R / 255f,
					(float)this.altcs.G / 255f,
					(float)this.altcs.B / 255f
				}, 1f);
			}
			pdfArray.Add(pdfFunction.Reference);
			return pdfArray;
		}

		// Token: 0x04000101 RID: 257
		public PdfName name;

		// Token: 0x04000102 RID: 258
		public BaseColor altcs;
	}
}
