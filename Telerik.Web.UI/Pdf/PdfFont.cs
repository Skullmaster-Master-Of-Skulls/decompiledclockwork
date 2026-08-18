using System;

namespace Telerik.Pdf
{
	// Token: 0x02001654 RID: 5716
	public abstract class PdfFont : PdfDictionary
	{
		// Token: 0x0600DDA5 RID: 56741 RVA: 0x00306DBB File Offset: 0x00304FBB
		public PdfFont(string fontname, PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Font;
			base[PdfName.Names.Name] = new PdfName(fontname);
		}

		// Token: 0x170043D5 RID: 17365
		// (get) Token: 0x0600DDA6 RID: 56742 RVA: 0x00306DE5 File Offset: 0x00304FE5
		public PdfName Name
		{
			get
			{
				return (PdfName)base[PdfName.Names.Name];
			}
		}
	}
}
