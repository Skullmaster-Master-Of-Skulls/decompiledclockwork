using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000759 RID: 1881
	internal interface IPivotFilter
	{
		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06004282 RID: 17026
		// (set) Token: 0x06004283 RID: 17027
		string FieldName { get; set; }

		// Token: 0x06004284 RID: 17028
		GroupFilter GetDataEngineFilter();
	}
}
