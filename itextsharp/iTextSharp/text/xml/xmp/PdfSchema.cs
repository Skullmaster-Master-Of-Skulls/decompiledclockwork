using System;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x02000415 RID: 1045
	public class PdfSchema : XmpSchema
	{
		// Token: 0x06002387 RID: 9095 RVA: 0x000D9B67 File Offset: 0x000D8B67
		public PdfSchema() : base("xmlns:pdf=\"http://ns.adobe.com/pdf/1.3/\"")
		{
			this.AddProducer(Document.Version);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000D9B7F File Offset: 0x000D8B7F
		public void AddKeywords(string keywords)
		{
			this["pdf:keywords"] = keywords;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000D9B8D File Offset: 0x000D8B8D
		public void AddProducer(string producer)
		{
			this["pdf:Producer"] = producer;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000D9B9B File Offset: 0x000D8B9B
		public void AddVersion(string version)
		{
			this["pdf:PDFVersion"] = version;
		}

		// Token: 0x04001886 RID: 6278
		public const string DEFAULT_XPATH_ID = "pdf";

		// Token: 0x04001887 RID: 6279
		public const string DEFAULT_XPATH_URI = "http://ns.adobe.com/pdf/1.3/";

		// Token: 0x04001888 RID: 6280
		public const string KEYWORDS = "pdf:keywords";

		// Token: 0x04001889 RID: 6281
		public const string VERSION = "pdf:PDFVersion";

		// Token: 0x0400188A RID: 6282
		public const string PRODUCER = "pdf:Producer";
	}
}
