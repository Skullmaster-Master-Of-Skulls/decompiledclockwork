using System;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000831 RID: 2097
	internal interface ISchedulerRenderer
	{
		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06004DB1 RID: 19889
		bool ShouldRenderFooter { get; }

		// Token: 0x06004DB2 RID: 19890
		Control GetContent();

		// Token: 0x06004DB3 RID: 19891
		Control GetInnerContent();
	}
}
