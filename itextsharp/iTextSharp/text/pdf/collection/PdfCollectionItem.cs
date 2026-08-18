using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.collection
{
	// Token: 0x02000461 RID: 1121
	public class PdfCollectionItem : PdfDictionary
	{
		// Token: 0x06002622 RID: 9762 RVA: 0x000E6460 File Offset: 0x000E5460
		public PdfCollectionItem(PdfCollectionSchema schema) : base(PdfName.COLLECTIONITEM)
		{
			this.schema = schema;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000E6474 File Offset: 0x000E5474
		public void AddItem(string key, string value)
		{
			PdfName key2 = new PdfName(key);
			PdfCollectionField pdfCollectionField = (PdfCollectionField)this.schema.Get(key2);
			base.Put(key2, pdfCollectionField.GetValue(value));
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000E64A8 File Offset: 0x000E54A8
		public void AddItem(string key, PdfString value)
		{
			PdfName key2 = new PdfName(key);
			PdfCollectionField pdfCollectionField = (PdfCollectionField)this.schema.Get(key2);
			if (pdfCollectionField.fieldType == 0)
			{
				base.Put(key2, value);
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000E64E0 File Offset: 0x000E54E0
		public void AddItem(string key, PdfDate d)
		{
			PdfName key2 = new PdfName(key);
			PdfCollectionField pdfCollectionField = (PdfCollectionField)this.schema.Get(key2);
			if (pdfCollectionField.fieldType == 1)
			{
				base.Put(key2, d);
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000E6518 File Offset: 0x000E5518
		public void AddItem(string key, PdfNumber n)
		{
			PdfName key2 = new PdfName(key);
			PdfCollectionField pdfCollectionField = (PdfCollectionField)this.schema.Get(key2);
			if (pdfCollectionField.fieldType == 2)
			{
				base.Put(key2, n);
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000E654F File Offset: 0x000E554F
		public void AddItem(string key, DateTime c)
		{
			this.AddItem(key, new PdfDate(c));
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000E655E File Offset: 0x000E555E
		public void AddItem(string key, int i)
		{
			this.AddItem(key, new PdfNumber(i));
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000E656D File Offset: 0x000E556D
		public void AddItem(string key, float f)
		{
			this.AddItem(key, new PdfNumber(f));
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000E657C File Offset: 0x000E557C
		public void AddItem(string key, double d)
		{
			this.AddItem(key, new PdfNumber(d));
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000E658C File Offset: 0x000E558C
		public void SetPrefix(string key, string prefix)
		{
			PdfName key2 = new PdfName(key);
			PdfObject pdfObject = base.Get(key2);
			if (pdfObject == null)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("you.must.set.a.value.before.adding.a.prefix"));
			}
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.COLLECTIONSUBITEM);
			pdfDictionary.Put(PdfName.D, pdfObject);
			pdfDictionary.Put(PdfName.P, new PdfString(prefix, "UnicodeBig"));
			base.Put(key2, pdfDictionary);
		}

		// Token: 0x04001A7A RID: 6778
		internal PdfCollectionSchema schema;
	}
}
