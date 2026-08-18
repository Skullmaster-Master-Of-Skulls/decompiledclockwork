using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000328 RID: 808
	public interface ITaskBase
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001AEF RID: 6895
		// (set) Token: 0x06001AF0 RID: 6896
		string Title { get; set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001AF1 RID: 6897
		// (set) Token: 0x06001AF2 RID: 6898
		object ID { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001AF3 RID: 6899
		// (set) Token: 0x06001AF4 RID: 6900
		object ParentID { get; set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001AF5 RID: 6901
		// (set) Token: 0x06001AF6 RID: 6902
		object OrderID { get; set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001AF7 RID: 6903
		// (set) Token: 0x06001AF8 RID: 6904
		bool Summary { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001AF9 RID: 6905
		// (set) Token: 0x06001AFA RID: 6906
		bool Expanded { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001AFB RID: 6907
		// (set) Token: 0x06001AFC RID: 6908
		decimal PercentComplete { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001AFD RID: 6909
		// (set) Token: 0x06001AFE RID: 6910
		DateTime Start { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001AFF RID: 6911
		// (set) Token: 0x06001B00 RID: 6912
		DateTime? PlannedStart { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001B01 RID: 6913
		// (set) Token: 0x06001B02 RID: 6914
		DateTime End { get; set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001B03 RID: 6915
		// (set) Token: 0x06001B04 RID: 6916
		DateTime? PlannedEnd { get; set; }
	}
}
