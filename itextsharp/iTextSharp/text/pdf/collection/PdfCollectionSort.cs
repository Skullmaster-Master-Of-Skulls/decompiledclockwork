using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.collection
{
	// Token: 0x020004A6 RID: 1190
	public class PdfCollectionSort : PdfDictionary
	{
		// Token: 0x06002840 RID: 10304 RVA: 0x000F2D53 File Offset: 0x000F1D53
		public PdfCollectionSort(string key) : base(PdfName.COLLECTIONSORT)
		{
			base.Put(PdfName.S, new PdfName(key));
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000F2D74 File Offset: 0x000F1D74
		public PdfCollectionSort(string[] keys) : base(PdfName.COLLECTIONSORT)
		{
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < keys.Length; i++)
			{
				pdfArray.Add(new PdfName(keys[i]));
			}
			base.Put(PdfName.S, pdfArray);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000F2DBC File Offset: 0x000F1DBC
		public void SetSortOrder(bool ascending)
		{
			PdfObject pdfObject = base.Get(PdfName.S);
			if (pdfObject is PdfName)
			{
				base.Put(PdfName.A, new PdfBoolean(ascending));
				return;
			}
			throw new InvalidOperationException(MessageLocalization.GetComposedMessage("you.have.to.define.a.bool.array.for.this.collection.sort.dictionary"));
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000F2E00 File Offset: 0x000F1E00
		public void SetSortOrder(bool[] ascending)
		{
			PdfObject pdfObject = base.Get(PdfName.S);
			if (!(pdfObject is PdfArray))
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("you.need.a.single.bool.for.this.collection.sort.dictionary"));
			}
			if (((PdfArray)pdfObject).Size != ascending.Length)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.number.of.booleans.in.this.array.doesn.t.correspond.with.the.number.of.fields"));
			}
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < ascending.Length; i++)
			{
				pdfArray.Add(new PdfBoolean(ascending[i]));
			}
			base.Put(PdfName.A, pdfArray);
		}
	}
}
