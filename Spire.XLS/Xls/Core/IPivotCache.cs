using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000228 RID: 552
	public interface IPivotCache
	{
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002193 RID: 8595
		int Index { get; }

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002194 RID: 8596
		DataSourceType SourceType { get; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002195 RID: 8597
		IXLSRange SourceRange { get; }
	}
}
