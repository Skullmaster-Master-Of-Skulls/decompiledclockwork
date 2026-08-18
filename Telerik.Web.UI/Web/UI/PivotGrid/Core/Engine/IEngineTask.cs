using System;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006A6 RID: 1702
	internal interface IEngineTask
	{
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06003D60 RID: 15712
		// (remove) Token: 0x06003D61 RID: 15713
		event EventHandler<EngineTaskCompletedEventArgs> Completed;

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06003D62 RID: 15714
		object Result { get; }

		// Token: 0x06003D63 RID: 15715
		void Run(object input);

		// Token: 0x06003D64 RID: 15716
		void Cancel();
	}
}
