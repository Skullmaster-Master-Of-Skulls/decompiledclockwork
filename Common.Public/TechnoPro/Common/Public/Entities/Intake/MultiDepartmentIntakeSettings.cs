using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x0200032A RID: 810
	public class MultiDepartmentIntakeSettings
	{
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0001DD5A File Offset: 0x0001BF5A
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x0001DD62 File Offset: 0x0001BF62
		public bool IsEnabled { get; set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0001DD6B File Offset: 0x0001BF6B
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x0001DD73 File Offset: 0x0001BF73
		public int DepartmentChooserControlId { get; set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0001DD7C File Offset: 0x0001BF7C
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x0001DD84 File Offset: 0x0001BF84
		public IDictionary<int, int> LookupIdToUserGroupMapping { get; set; }
	}
}
