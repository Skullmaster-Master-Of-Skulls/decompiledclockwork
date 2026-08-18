using System;

namespace Telerik.Pdf
{
	// Token: 0x02001649 RID: 5705
	public sealed class PdfCatalog : PdfDictionary
	{
		// Token: 0x0600DD37 RID: 56631 RVA: 0x00305598 File Offset: 0x00303798
		public PdfCatalog(PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Catalog;
		}

		// Token: 0x170043B5 RID: 17333
		// (set) Token: 0x0600DD38 RID: 56632 RVA: 0x003055B1 File Offset: 0x003037B1
		public PdfObject Pages
		{
			set
			{
				base[PdfName.Names.Pages] = value.GetReference();
			}
		}

		// Token: 0x170043B6 RID: 17334
		// (set) Token: 0x0600DD39 RID: 56633 RVA: 0x003055C4 File Offset: 0x003037C4
		public PdfObject Outlines
		{
			set
			{
				base[PdfName.Names.Outlines] = value.GetReference();
				base[PdfName.Names.PageMode] = PdfName.Names.UseOutlines;
			}
		}
	}
}
