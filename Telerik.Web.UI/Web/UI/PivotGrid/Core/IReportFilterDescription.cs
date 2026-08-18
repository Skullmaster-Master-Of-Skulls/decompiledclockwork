using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006ED RID: 1773
	internal interface IReportFilterDescription : IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06003F09 RID: 16137
		// (set) Token: 0x06003F0A RID: 16138
		Condition Condition { get; set; }
	}
}
