using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000056 RID: 86
	public class MarkedContentInfo
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000CA5B File Offset: 0x0000BA5B
		public MarkedContentInfo(PdfName tag, PdfDictionary dictionary)
		{
			this.tag = tag;
			this.dictionary = ((dictionary != null) ? dictionary : new PdfDictionary());
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000CA7B File Offset: 0x0000BA7B
		public PdfName GetTag()
		{
			return this.tag;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000CA83 File Offset: 0x0000BA83
		public bool HasMcid()
		{
			return this.dictionary.Contains(PdfName.MCID);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000CA98 File Offset: 0x0000BA98
		public int GetMcid()
		{
			PdfNumber asNumber = this.dictionary.GetAsNumber(PdfName.MCID);
			if (asNumber == null)
			{
				throw new InvalidOperationException("MarkedContentInfo does not contain MCID");
			}
			return asNumber.IntValue;
		}

		// Token: 0x0400013D RID: 317
		private PdfName tag;

		// Token: 0x0400013E RID: 318
		private PdfDictionary dictionary;
	}
}
