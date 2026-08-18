using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x0200047E RID: 1150
	public class AvailabilityScheduleItemsForContext
	{
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x00026894 File Offset: 0x00024A94
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x0002689C File Offset: 0x00024A9C
		public AvailabilityScheduleContext Context { get; set; }

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x000268A5 File Offset: 0x00024AA5
		// (set) Token: 0x060022B9 RID: 8889 RVA: 0x000268AD File Offset: 0x00024AAD
		public IList<AvailabilityScheduleItemInfo> AvailabilityScheduleItems { get; set; }
	}
}
