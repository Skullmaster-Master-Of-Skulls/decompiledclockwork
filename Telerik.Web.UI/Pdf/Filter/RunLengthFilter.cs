using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001609 RID: 5641
	public class RunLengthFilter : IFilter
	{
		// Token: 0x0600DBB6 RID: 56246 RVA: 0x00300AA9 File Offset: 0x002FECA9
		public RunLengthFilter()
		{
			throw new UnsupportedFilterException("RunLengthDecode");
		}

		// Token: 0x1700434D RID: 17229
		// (get) Token: 0x0600DBB7 RID: 56247 RVA: 0x00300ABB File Offset: 0x002FECBB
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.RunLengthDecode;
			}
		}

		// Token: 0x1700434E RID: 17230
		// (get) Token: 0x0600DBB8 RID: 56248 RVA: 0x00300AC2 File Offset: 0x002FECC2
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x1700434F RID: 17231
		// (get) Token: 0x0600DBB9 RID: 56249 RVA: 0x00300AC9 File Offset: 0x002FECC9
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DBBA RID: 56250 RVA: 0x00300ACC File Offset: 0x002FECCC
		public byte[] Encode(byte[] data)
		{
			return data;
		}
	}
}
