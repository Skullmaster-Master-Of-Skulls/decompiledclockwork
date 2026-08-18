using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000675 RID: 1653
	public static class AggregateValueExtensions
	{
		// Token: 0x06003C69 RID: 15465 RVA: 0x000C3BE0 File Offset: 0x000C1DE0
		public static bool TryConvertValue<T>(this AggregateValue aggregateValue, out T value)
		{
			IConvertibleAggregateValue<T> convertibleAggregateValue = aggregateValue as IConvertibleAggregateValue<T>;
			if (convertibleAggregateValue != null)
			{
				return convertibleAggregateValue.TryConvertValue(out value);
			}
			value = default(T);
			return false;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000C3C08 File Offset: 0x000C1E08
		public static T ConvertOrDefault<T>(this AggregateValue aggregateValue)
		{
			T result;
			aggregateValue.TryConvertValue(out result);
			return result;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000C3C20 File Offset: 0x000C1E20
		public static bool ContainsError(this IEnumerable<AggregateValue> aggregateValues)
		{
			foreach (AggregateValue aggregateValue in aggregateValues)
			{
				if (aggregateValue.IsError())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000C3C70 File Offset: 0x000C1E70
		public static bool IsError(this AggregateValue aggregateValue)
		{
			return aggregateValue != null && aggregateValue.GetValue() is AggregateError;
		}
	}
}
