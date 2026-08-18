using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001603 RID: 5635
	public class CcittFaxFilter : IFilter
	{
		// Token: 0x0600DB9B RID: 56219 RVA: 0x00300929 File Offset: 0x002FEB29
		public CcittFaxFilter()
		{
			throw new UnsupportedFilterException("CCITTFaxDecode");
		}

		// Token: 0x1700433E RID: 17214
		// (get) Token: 0x0600DB9C RID: 56220 RVA: 0x0030093B File Offset: 0x002FEB3B
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.CCITTFaxDecode;
			}
		}

		// Token: 0x1700433F RID: 17215
		// (get) Token: 0x0600DB9D RID: 56221 RVA: 0x00300942 File Offset: 0x002FEB42
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x17004340 RID: 17216
		// (get) Token: 0x0600DB9E RID: 56222 RVA: 0x00300949 File Offset: 0x002FEB49
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DB9F RID: 56223 RVA: 0x0030094C File Offset: 0x002FEB4C
		public byte[] Encode(byte[] data)
		{
			return data;
		}
	}
}
