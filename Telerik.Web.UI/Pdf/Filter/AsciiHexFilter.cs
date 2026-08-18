using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001602 RID: 5634
	public class AsciiHexFilter : IFilter
	{
		// Token: 0x1700433B RID: 17211
		// (get) Token: 0x0600DB96 RID: 56214 RVA: 0x0030088D File Offset: 0x002FEA8D
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.ASCIIHexDecode;
			}
		}

		// Token: 0x1700433C RID: 17212
		// (get) Token: 0x0600DB97 RID: 56215 RVA: 0x00300894 File Offset: 0x002FEA94
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x1700433D RID: 17213
		// (get) Token: 0x0600DB98 RID: 56216 RVA: 0x0030089B File Offset: 0x002FEA9B
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DB99 RID: 56217 RVA: 0x003008A0 File Offset: 0x002FEAA0
		public byte[] Encode(byte[] data)
		{
			byte[] array = new byte[data.Length * 2 + 1];
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				array[num++] = AsciiHexFilter.HexDigits[data[i] >> 4];
				array[num++] = AsciiHexFilter.HexDigits[(int)(data[i] & 15)];
			}
			array[num++] = 62;
			return array;
		}

		// Token: 0x04003D66 RID: 15718
		private static readonly byte[] HexDigits = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};
	}
}
