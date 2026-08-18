using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.PerformanceTesting
{
	// Token: 0x02000256 RID: 598
	public class SearchForPersonPerformanceTestResult
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x000187B3 File Offset: 0x000169B3
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x000187BB File Offset: 0x000169BB
		public PerformanceTestResult TestResult { get; set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000187C4 File Offset: 0x000169C4
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x000187CC File Offset: 0x000169CC
		public IList<UserGroupObject> FoundPersons { get; set; }
	}
}
