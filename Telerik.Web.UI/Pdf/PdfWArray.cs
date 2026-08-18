using System;

namespace Telerik.Pdf
{
	// Token: 0x02001674 RID: 5748
	public class PdfWArray : PdfObject
	{
		// Token: 0x0600DE3E RID: 56894 RVA: 0x003090BF File Offset: 0x003072BF
		public PdfWArray(int startCID)
		{
			this.startCID = startCID;
		}

		// Token: 0x0600DE3F RID: 56895 RVA: 0x003090D9 File Offset: 0x003072D9
		public void AddEntry(int[] widths)
		{
			this.array.AddArray(widths);
		}

		// Token: 0x0600DE40 RID: 56896 RVA: 0x003090E7 File Offset: 0x003072E7
		protected internal override void Write(PdfWriter writer)
		{
			writer.WriteKeyword(Keyword.ArrayBegin);
			writer.WriteSpace();
			writer.Write(this.startCID);
			writer.WriteSpace();
			this.array.Write(writer);
			writer.WriteKeyword(Keyword.ArrayEnd);
		}

		// Token: 0x04003FE8 RID: 16360
		private int startCID;

		// Token: 0x04003FE9 RID: 16361
		private PdfArray array = new PdfArray();
	}
}
