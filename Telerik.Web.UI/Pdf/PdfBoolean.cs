using System;

namespace Telerik.Pdf
{
	// Token: 0x02001647 RID: 5703
	public sealed class PdfBoolean : PdfObject
	{
		// Token: 0x0600DD25 RID: 56613 RVA: 0x003053A1 File Offset: 0x003035A1
		public PdfBoolean(bool val)
		{
			this.val = val;
		}

		// Token: 0x0600DD26 RID: 56614 RVA: 0x003053B0 File Offset: 0x003035B0
		public PdfBoolean(bool val, PdfObjectId objectId) : base(objectId)
		{
			this.val = val;
		}

		// Token: 0x0600DD27 RID: 56615 RVA: 0x003053C0 File Offset: 0x003035C0
		protected internal override void Write(PdfWriter writer)
		{
			writer.Write(this.val ? KeywordEntries.GetKeyword(Keyword.True) : KeywordEntries.GetKeyword(Keyword.False));
		}

		// Token: 0x04003EED RID: 16109
		private bool val;
	}
}
