using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000351 RID: 849
	public interface IDependenciesDataBinding
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001D5F RID: 7519
		// (set) Token: 0x06001D60 RID: 7520
		string IdField { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001D61 RID: 7521
		// (set) Token: 0x06001D62 RID: 7522
		string SuccessorIdField { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001D63 RID: 7523
		// (set) Token: 0x06001D64 RID: 7524
		string PredecessorIdField { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001D65 RID: 7525
		// (set) Token: 0x06001D66 RID: 7526
		string TypeField { get; set; }
	}
}
