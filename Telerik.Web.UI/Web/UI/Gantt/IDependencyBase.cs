using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000324 RID: 804
	public interface IDependencyBase
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001ADA RID: 6874
		// (set) Token: 0x06001ADB RID: 6875
		object ID { get; set; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001ADC RID: 6876
		// (set) Token: 0x06001ADD RID: 6877
		object SuccessorID { get; set; }

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001ADE RID: 6878
		// (set) Token: 0x06001ADF RID: 6879
		object PredecessorID { get; set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001AE0 RID: 6880
		// (set) Token: 0x06001AE1 RID: 6881
		DependencyType Type { get; set; }
	}
}
