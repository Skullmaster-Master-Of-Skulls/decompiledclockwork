using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x0200068B RID: 1675
	public interface IConvertibleAggregateValue<T>
	{
		// Token: 0x06003CE3 RID: 15587
		bool TryConvertValue(out T value);
	}
}
