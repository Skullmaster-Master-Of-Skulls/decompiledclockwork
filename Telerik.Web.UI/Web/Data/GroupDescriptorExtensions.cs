using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA3 RID: 7075
	internal static class GroupDescriptorExtensions
	{
		// Token: 0x060111E8 RID: 70120 RVA: 0x003C67B4 File Offset: 0x003C49B4
		public static IEnumerable<AggregateFunction> GetAggregateFunctions(this IGroupDescriptor groupDescriptor)
		{
			IAggregateFunctionsProvider aggregateFunctionsProvider = groupDescriptor as IAggregateFunctionsProvider;
			if (aggregateFunctionsProvider != null)
			{
				return aggregateFunctionsProvider.AggregateFunctions;
			}
			return Enumerable.Empty<AggregateFunction>();
		}
	}
}
