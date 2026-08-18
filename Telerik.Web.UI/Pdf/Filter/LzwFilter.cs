using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001608 RID: 5640
	public class LzwFilter : IFilter
	{
		// Token: 0x0600DBB1 RID: 56241 RVA: 0x00300A83 File Offset: 0x002FEC83
		public LzwFilter()
		{
			throw new UnsupportedFilterException("LZWDecode");
		}

		// Token: 0x1700434A RID: 17226
		// (get) Token: 0x0600DBB2 RID: 56242 RVA: 0x00300A95 File Offset: 0x002FEC95
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.LZWDecode;
			}
		}

		// Token: 0x1700434B RID: 17227
		// (get) Token: 0x0600DBB3 RID: 56243 RVA: 0x00300A9C File Offset: 0x002FEC9C
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x1700434C RID: 17228
		// (get) Token: 0x0600DBB4 RID: 56244 RVA: 0x00300AA3 File Offset: 0x002FECA3
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DBB5 RID: 56245 RVA: 0x00300AA6 File Offset: 0x002FECA6
		public byte[] Encode(byte[] data)
		{
			return data;
		}
	}
}
