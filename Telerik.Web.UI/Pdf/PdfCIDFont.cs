using System;

namespace Telerik.Pdf
{
	// Token: 0x0200164A RID: 5706
	public class PdfCIDFont : PdfDictionary
	{
		// Token: 0x0600DD3A RID: 56634 RVA: 0x003055E8 File Offset: 0x003037E8
		public PdfCIDFont(PdfObjectId objectId, PdfFontSubTypeEnum subType, string baseFont) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Font;
			base[PdfName.Names.Subtype] = new PdfName(subType.ToString());
			base[PdfName.Names.BaseFont] = new PdfName(baseFont);
			base[PdfName.Names.DW] = new PdfNumeric(1000m);
			base[PdfName.Names.CIDToGIDMap] = PdfName.Names.Identity;
		}

		// Token: 0x170043B7 RID: 17335
		// (set) Token: 0x0600DD3B RID: 56635 RVA: 0x00305662 File Offset: 0x00303862
		public PdfCIDSystemInfo SystemInfo
		{
			set
			{
				base[PdfName.Names.CIDSystemInfo] = value;
			}
		}

		// Token: 0x170043B8 RID: 17336
		// (set) Token: 0x0600DD3C RID: 56636 RVA: 0x00305670 File Offset: 0x00303870
		public PdfFontDescriptor Descriptor
		{
			set
			{
				base[PdfName.Names.FontDescriptor] = value.GetReference();
			}
		}

		// Token: 0x170043B9 RID: 17337
		// (set) Token: 0x0600DD3D RID: 56637 RVA: 0x00305683 File Offset: 0x00303883
		public PdfNumeric DefaultWidth
		{
			set
			{
				base[PdfName.Names.DW] = value;
			}
		}

		// Token: 0x170043BA RID: 17338
		// (set) Token: 0x0600DD3E RID: 56638 RVA: 0x00305691 File Offset: 0x00303891
		public PdfWArray Widths
		{
			set
			{
				base[PdfName.Names.W] = value;
			}
		}
	}
}
