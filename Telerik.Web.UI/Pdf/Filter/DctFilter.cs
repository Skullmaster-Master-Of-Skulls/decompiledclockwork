using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001604 RID: 5636
	public class DctFilter : IFilter
	{
		// Token: 0x17004341 RID: 17217
		// (get) Token: 0x0600DBA1 RID: 56225 RVA: 0x00300957 File Offset: 0x002FEB57
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.DCTDecode;
			}
		}

		// Token: 0x17004342 RID: 17218
		// (get) Token: 0x0600DBA2 RID: 56226 RVA: 0x0030095E File Offset: 0x002FEB5E
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x17004343 RID: 17219
		// (get) Token: 0x0600DBA3 RID: 56227 RVA: 0x00300965 File Offset: 0x002FEB65
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DBA4 RID: 56228 RVA: 0x00300968 File Offset: 0x002FEB68
		public byte[] Encode(byte[] data)
		{
			return data;
		}
	}
}
