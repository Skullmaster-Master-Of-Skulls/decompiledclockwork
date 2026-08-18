using System;

namespace Telerik.Pdf
{
	// Token: 0x0200167B RID: 5755
	internal class XRefSection
	{
		// Token: 0x0600DE8D RID: 56973 RVA: 0x00309BC3 File Offset: 0x00307DC3
		internal void Add(PdfObjectId objectId, long offset)
		{
			this.subsection.Add(objectId, offset);
		}

		// Token: 0x0600DE8E RID: 56974 RVA: 0x00309BD2 File Offset: 0x00307DD2
		internal void Write(PdfWriter writer)
		{
			writer.WriteKeywordLine(Keyword.XRef);
			this.subsection.Write(writer);
		}

		// Token: 0x04004001 RID: 16385
		private XRefSubSection subsection = new XRefSubSection();
	}
}
