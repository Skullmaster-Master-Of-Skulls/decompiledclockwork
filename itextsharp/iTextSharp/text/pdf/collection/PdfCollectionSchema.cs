using System;

namespace iTextSharp.text.pdf.collection
{
	// Token: 0x0200005C RID: 92
	public class PdfCollectionSchema : PdfDictionary
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000CF0E File Offset: 0x0000BF0E
		public PdfCollectionSchema() : base(PdfName.COLLECTIONSCHEMA)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CF1B File Offset: 0x0000BF1B
		public void AddField(string name, PdfCollectionField field)
		{
			base.Put(new PdfName(name), field);
		}
	}
}
