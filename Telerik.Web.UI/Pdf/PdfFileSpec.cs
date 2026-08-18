using System;

namespace Telerik.Pdf
{
	// Token: 0x02001652 RID: 5714
	public sealed class PdfFileSpec : PdfDictionary
	{
		// Token: 0x0600DD94 RID: 56724 RVA: 0x00306C80 File Offset: 0x00304E80
		public PdfFileSpec(PdfObjectId objectId, string filename) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.FileSpec;
			base[PdfName.Names.F] = new PdfString(filename);
		}
	}
}
