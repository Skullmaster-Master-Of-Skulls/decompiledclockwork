using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000685 RID: 1669
	internal interface IInitializeDescription
	{
		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06003CCC RID: 15564
		bool Initialized { get; }

		// Token: 0x06003CCD RID: 15565
		void Initialize(IDataProvider provider);
	}
}
