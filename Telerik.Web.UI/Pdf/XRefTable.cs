using System;

namespace Telerik.Pdf
{
	// Token: 0x0200167E RID: 5758
	public class XRefTable
	{
		// Token: 0x0600DE95 RID: 56981 RVA: 0x00309DA7 File Offset: 0x00307FA7
		public void Add(PdfObjectId objectId, long offset)
		{
			this.section.Add(objectId, offset);
		}

		// Token: 0x0600DE96 RID: 56982 RVA: 0x00309DB6 File Offset: 0x00307FB6
		public void Write(PdfWriter writer)
		{
			this.section.Write(writer);
		}

		// Token: 0x04004005 RID: 16389
		private XRefSection section = new XRefSection();
	}
}
