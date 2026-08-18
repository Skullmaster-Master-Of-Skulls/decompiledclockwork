using System;

namespace Telerik.Pdf
{
	// Token: 0x0200164B RID: 5707
	public class PdfCIDSystemInfo : PdfDictionary
	{
		// Token: 0x0600DD3F RID: 56639 RVA: 0x003056A0 File Offset: 0x003038A0
		public PdfCIDSystemInfo()
		{
			base[PdfName.Names.Registry] = new PdfString("Adobe");
			base[PdfName.Names.Ordering] = new PdfString("Identity");
			base[PdfName.Names.Supplement] = new PdfNumeric(0m);
		}

		// Token: 0x0600DD40 RID: 56640 RVA: 0x003056F3 File Offset: 0x003038F3
		public PdfCIDSystemInfo(string registry, string ordering, int supplement)
		{
			base[PdfName.Names.Registry] = new PdfString(registry);
			base[PdfName.Names.Ordering] = new PdfString(ordering);
			base[PdfName.Names.Supplement] = new PdfNumeric(supplement);
		}

		// Token: 0x04003EEF RID: 16111
		public const string DefaultRegistry = "Adobe";

		// Token: 0x04003EF0 RID: 16112
		public const string DefaultOrdering = "Identity";

		// Token: 0x04003EF1 RID: 16113
		public const int DefaultSupplement = 0;
	}
}
