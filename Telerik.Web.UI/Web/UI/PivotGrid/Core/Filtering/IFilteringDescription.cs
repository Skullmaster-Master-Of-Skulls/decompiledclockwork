using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C2 RID: 1730
	internal interface IFilteringDescription : IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06003E0B RID: 15883
		Type FilteringType { get; }

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06003E0C RID: 15884
		bool PrefersDistinct { get; }
	}
}
