using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001607 RID: 5639
	public class Jbig2Filter : IFilter
	{
		// Token: 0x0600DBAC RID: 56236 RVA: 0x00300A5D File Offset: 0x002FEC5D
		public Jbig2Filter()
		{
			throw new UnsupportedFilterException("JBIG2Decode");
		}

		// Token: 0x17004347 RID: 17223
		// (get) Token: 0x0600DBAD RID: 56237 RVA: 0x00300A6F File Offset: 0x002FEC6F
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.JBIG2Decode;
			}
		}

		// Token: 0x17004348 RID: 17224
		// (get) Token: 0x0600DBAE RID: 56238 RVA: 0x00300A76 File Offset: 0x002FEC76
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x17004349 RID: 17225
		// (get) Token: 0x0600DBAF RID: 56239 RVA: 0x00300A7D File Offset: 0x002FEC7D
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DBB0 RID: 56240 RVA: 0x00300A80 File Offset: 0x002FEC80
		public byte[] Encode(byte[] data)
		{
			return data;
		}
	}
}
