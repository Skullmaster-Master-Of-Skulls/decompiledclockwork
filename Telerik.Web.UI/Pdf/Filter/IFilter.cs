using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001600 RID: 5632
	public interface IFilter
	{
		// Token: 0x17004335 RID: 17205
		// (get) Token: 0x0600DB8C RID: 56204
		PdfObject Name { get; }

		// Token: 0x17004336 RID: 17206
		// (get) Token: 0x0600DB8D RID: 56205
		PdfObject DecodeParms { get; }

		// Token: 0x17004337 RID: 17207
		// (get) Token: 0x0600DB8E RID: 56206
		bool HasDecodeParams { get; }

		// Token: 0x0600DB8F RID: 56207
		byte[] Encode(byte[] data);
	}
}
