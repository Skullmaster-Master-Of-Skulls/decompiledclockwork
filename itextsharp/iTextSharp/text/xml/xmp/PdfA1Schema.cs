using System;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x020000BD RID: 189
	public class PdfA1Schema : XmpSchema
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001E731 File Offset: 0x0001D731
		public PdfA1Schema() : base("xmlns:pdfaid=\"http://www.aiim.org/pdfa/ns/id/\"")
		{
			this.AddPart("1");
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001E749 File Offset: 0x0001D749
		public void AddPart(string part)
		{
			this["pdfaid:part"] = part;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001E757 File Offset: 0x0001D757
		public void AddConformance(string conformance)
		{
			this["pdfaid:conformance"] = conformance;
		}

		// Token: 0x040002D2 RID: 722
		public const string DEFAULT_XPATH_ID = "pdfaid";

		// Token: 0x040002D3 RID: 723
		public const string DEFAULT_XPATH_URI = "http://www.aiim.org/pdfa/ns/id/";

		// Token: 0x040002D4 RID: 724
		public const string PART = "pdfaid:part";

		// Token: 0x040002D5 RID: 725
		public const string CONFORMANCE = "pdfaid:conformance";
	}
}
