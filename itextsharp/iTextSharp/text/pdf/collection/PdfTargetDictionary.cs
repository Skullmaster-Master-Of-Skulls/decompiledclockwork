using System;

namespace iTextSharp.text.pdf.collection
{
	// Token: 0x02000330 RID: 816
	public class PdfTargetDictionary : PdfDictionary
	{
		// Token: 0x06001D82 RID: 7554 RVA: 0x000B1286 File Offset: 0x000B0286
		public PdfTargetDictionary(PdfTargetDictionary nested)
		{
			base.Put(PdfName.R, PdfName.P);
			if (nested != null)
			{
				this.AdditionalPath = nested;
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000B12A8 File Offset: 0x000B02A8
		public PdfTargetDictionary(bool child)
		{
			if (child)
			{
				base.Put(PdfName.R, PdfName.C);
				return;
			}
			base.Put(PdfName.R, PdfName.P);
		}

		// Token: 0x1700052C RID: 1324
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x000B12D4 File Offset: 0x000B02D4
		public string EmbeddedFileName
		{
			set
			{
				base.Put(PdfName.N, new PdfString(value, null));
			}
		}

		// Token: 0x1700052D RID: 1325
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x000B12E8 File Offset: 0x000B02E8
		public string FileAttachmentPagename
		{
			set
			{
				base.Put(PdfName.P, new PdfString(value, null));
			}
		}

		// Token: 0x1700052E RID: 1326
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x000B12FC File Offset: 0x000B02FC
		public int FileAttachmentPage
		{
			set
			{
				base.Put(PdfName.P, new PdfNumber(value));
			}
		}

		// Token: 0x1700052F RID: 1327
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x000B130F File Offset: 0x000B030F
		public string FileAttachmentName
		{
			set
			{
				base.Put(PdfName.A, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000530 RID: 1328
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x000B1327 File Offset: 0x000B0327
		public int FileAttachmentIndex
		{
			set
			{
				base.Put(PdfName.A, new PdfNumber(value));
			}
		}

		// Token: 0x17000531 RID: 1329
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x000B133A File Offset: 0x000B033A
		public PdfTargetDictionary AdditionalPath
		{
			set
			{
				base.Put(PdfName.T, value);
			}
		}
	}
}
