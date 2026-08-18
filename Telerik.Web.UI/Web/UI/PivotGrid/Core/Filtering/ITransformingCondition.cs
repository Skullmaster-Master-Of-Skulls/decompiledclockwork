using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C9 RID: 1737
	internal interface ITransformingCondition
	{
		// Token: 0x06003E43 RID: 15939
		object TransformConditionValueToDistinctItem(object item);

		// Token: 0x06003E44 RID: 15940
		object TransformDistinctItemToConditionValue(object item);

		// Token: 0x06003E45 RID: 15941
		object GetDistinctItemFromValue(object value, IEnumerable<object> distinctItems);
	}
}
