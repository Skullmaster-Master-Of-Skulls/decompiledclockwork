using System;
using System.Collections.Generic;

namespace BarcodeLib
{
	// Token: 0x02000009 RID: 9
	internal interface IBarcode
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91
		string Encoded_Value { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005C RID: 92
		string RawData { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005D RID: 93
		List<string> Errors { get; }
	}
}
