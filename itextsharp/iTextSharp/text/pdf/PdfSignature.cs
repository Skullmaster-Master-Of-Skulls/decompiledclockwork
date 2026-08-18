using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000C0 RID: 192
	public class PdfSignature : PdfDictionary
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0001EEA8 File Offset: 0x0001DEA8
		public PdfSignature(PdfName filter, PdfName subFilter) : base(PdfName.SIG)
		{
			base.Put(PdfName.FILTER, filter);
			base.Put(PdfName.SUBFILTER, subFilter);
		}

		// Token: 0x17000121 RID: 289
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0001EED0 File Offset: 0x0001DED0
		public int[] ByteRange
		{
			set
			{
				PdfArray pdfArray = new PdfArray();
				for (int i = 0; i < value.Length; i++)
				{
					pdfArray.Add(new PdfNumber(value[i]));
				}
				base.Put(PdfName.BYTERANGE, pdfArray);
			}
		}

		// Token: 0x17000122 RID: 290
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001EF0C File Offset: 0x0001DF0C
		public byte[] Contents
		{
			set
			{
				base.Put(PdfName.CONTENTS, new PdfString(value).SetHexWriting(true));
			}
		}

		// Token: 0x17000123 RID: 291
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0001EF25 File Offset: 0x0001DF25
		public byte[] Cert
		{
			set
			{
				base.Put(PdfName.CERT, new PdfString(value));
			}
		}

		// Token: 0x17000124 RID: 292
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0001EF38 File Offset: 0x0001DF38
		public string Name
		{
			set
			{
				base.Put(PdfName.NAME, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000125 RID: 293
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001EF50 File Offset: 0x0001DF50
		public PdfDate Date
		{
			set
			{
				base.Put(PdfName.M, value);
			}
		}

		// Token: 0x17000126 RID: 294
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0001EF5E File Offset: 0x0001DF5E
		public string Location
		{
			set
			{
				base.Put(PdfName.LOCATION, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000127 RID: 295
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001EF76 File Offset: 0x0001DF76
		public string Reason
		{
			set
			{
				base.Put(PdfName.REASON, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000128 RID: 296
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0001EF8E File Offset: 0x0001DF8E
		public string Contact
		{
			set
			{
				base.Put(PdfName.CONTACTINFO, new PdfString(value, "UnicodeBig"));
			}
		}
	}
}
