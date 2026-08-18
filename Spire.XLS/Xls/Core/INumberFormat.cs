using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000244 RID: 580
	public interface INumberFormat : IExcelApplication
	{
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600232E RID: 9006
		int Index { get; }

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600232F RID: 9007
		string FormatString { get; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06002330 RID: 9008
		CellFormatType FormatType { get; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06002331 RID: 9009
		bool IsFraction { get; }

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002332 RID: 9010
		bool IsScientific { get; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002333 RID: 9011
		bool IsThousandSeparator { get; }

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002334 RID: 9012
		int DecimalPlaces { get; }
	}
}
