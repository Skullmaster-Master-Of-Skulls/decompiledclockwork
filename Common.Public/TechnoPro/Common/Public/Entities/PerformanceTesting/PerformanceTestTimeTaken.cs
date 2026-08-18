using System;

namespace TechnoPro.Common.Public.Entities.PerformanceTesting
{
	// Token: 0x02000255 RID: 597
	public class PerformanceTestTimeTaken
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00018791 File Offset: 0x00016991
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00018799 File Offset: 0x00016999
		public DateTime EntryPoint { get; set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000187A2 File Offset: 0x000169A2
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000187AA File Offset: 0x000169AA
		public TimeSpan TimeElapsed { get; set; }
	}
}
