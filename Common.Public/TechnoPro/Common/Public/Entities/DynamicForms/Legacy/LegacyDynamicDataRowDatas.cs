using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Legacy
{
	// Token: 0x02000376 RID: 886
	public class LegacyDynamicDataRowDatas
	{
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0001F52E File Offset: 0x0001D72E
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x0001F536 File Offset: 0x0001D736
		public IList<LegacyDynamicDataRowData> RowDatas { get; set; }

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0001F53F File Offset: 0x0001D73F
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0001F547 File Offset: 0x0001D747
		public eLegacyDynamicDataType ControlValueType { get; set; }
	}
}
