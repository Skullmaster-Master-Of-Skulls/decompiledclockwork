using System;

namespace Telerik.Pdf
{
	// Token: 0x02001656 RID: 5718
	public class PdfFontDescriptor : PdfDictionary
	{
		// Token: 0x0600DDAF RID: 56751 RVA: 0x0030718B File Offset: 0x0030538B
		public PdfFontDescriptor(string fontName, PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Font;
			base[PdfName.Names.FontName] = new PdfName(fontName);
		}

		// Token: 0x170043D6 RID: 17366
		// (set) Token: 0x0600DDB0 RID: 56752 RVA: 0x003071B5 File Offset: 0x003053B5
		public PdfNumeric Flags
		{
			set
			{
				base[PdfName.Names.Flags] = value;
			}
		}

		// Token: 0x170043D7 RID: 17367
		// (set) Token: 0x0600DDB1 RID: 56753 RVA: 0x003071C3 File Offset: 0x003053C3
		public PdfArray FontBBox
		{
			set
			{
				base[PdfName.Names.FontBBox] = value;
			}
		}

		// Token: 0x170043D8 RID: 17368
		// (set) Token: 0x0600DDB2 RID: 56754 RVA: 0x003071D1 File Offset: 0x003053D1
		public PdfNumeric ItalicAngle
		{
			set
			{
				base[PdfName.Names.ItalicAngle] = value;
			}
		}

		// Token: 0x170043D9 RID: 17369
		// (set) Token: 0x0600DDB3 RID: 56755 RVA: 0x003071DF File Offset: 0x003053DF
		public PdfNumeric Ascent
		{
			set
			{
				base[PdfName.Names.Ascent] = value;
			}
		}

		// Token: 0x170043DA RID: 17370
		// (set) Token: 0x0600DDB4 RID: 56756 RVA: 0x003071ED File Offset: 0x003053ED
		public PdfNumeric Descent
		{
			set
			{
				base[PdfName.Names.Descent] = value;
			}
		}

		// Token: 0x170043DB RID: 17371
		// (set) Token: 0x0600DDB5 RID: 56757 RVA: 0x003071FB File Offset: 0x003053FB
		public PdfNumeric CapHeight
		{
			set
			{
				base[PdfName.Names.CapHeight] = value;
			}
		}

		// Token: 0x170043DC RID: 17372
		// (set) Token: 0x0600DDB6 RID: 56758 RVA: 0x00307209 File Offset: 0x00305409
		public PdfNumeric StemV
		{
			set
			{
				base[PdfName.Names.StemV] = value;
			}
		}

		// Token: 0x170043DD RID: 17373
		// (set) Token: 0x0600DDB7 RID: 56759 RVA: 0x00307217 File Offset: 0x00305417
		public PdfFontFile FontFile2
		{
			set
			{
				base[PdfName.Names.FontFile2] = value.GetReference();
			}
		}
	}
}
