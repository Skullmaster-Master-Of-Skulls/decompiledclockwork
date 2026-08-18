using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000679 RID: 1657
	internal interface IObservableServiceProvider : IServiceProvider
	{
		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06003C7C RID: 15484
		// (remove) Token: 0x06003C7D RID: 15485
		event EventHandler<EventArgs> ServicesChanged;
	}
}
