using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006BF RID: 1727
	internal interface IConditionFactory
	{
		// Token: 0x06003E08 RID: 15880
		Condition CreateCondition(Type conditionType);
	}
}
