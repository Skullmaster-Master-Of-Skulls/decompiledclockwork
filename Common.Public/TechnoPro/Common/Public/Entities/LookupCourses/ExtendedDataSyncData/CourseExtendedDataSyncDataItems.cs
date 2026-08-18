using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.LookupCourses.ExtendedDataSyncData
{
	// Token: 0x020002F5 RID: 757
	public class CourseExtendedDataSyncDataItems : BusinessBase<int>
	{
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0001C1C3 File Offset: 0x0001A3C3
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x0001C1CB File Offset: 0x0001A3CB
		public IDictionary<string, object> DataItems { get; set; }
	}
}
