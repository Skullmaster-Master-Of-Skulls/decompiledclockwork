using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200030F RID: 783
	public interface IAssignmentsDataBindings
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001A7C RID: 6780
		// (set) Token: 0x06001A7D RID: 6781
		string IdField { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001A7E RID: 6782
		// (set) Token: 0x06001A7F RID: 6783
		string TaskIdField { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001A80 RID: 6784
		// (set) Token: 0x06001A81 RID: 6785
		string ResourceIdField { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001A82 RID: 6786
		// (set) Token: 0x06001A83 RID: 6787
		string UnitsField { get; set; }
	}
}
