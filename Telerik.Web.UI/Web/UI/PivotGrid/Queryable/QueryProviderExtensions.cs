using System;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200073C RID: 1852
	internal static class QueryProviderExtensions
	{
		// Token: 0x060041E5 RID: 16869 RVA: 0x000CEDBF File Offset: 0x000CCFBF
		public static bool IsEntityFrameworkProvider(this IQueryProvider provider)
		{
			return provider.GetType().FullName == "System.Data.Objects.ELinq.ObjectQueryProvider";
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000CEDD6 File Offset: 0x000CCFD6
		public static bool IsLinqToObjectsProvider(this IQueryProvider provider)
		{
			return provider.GetType().FullName.Contains("EnumerableQuery");
		}
	}
}
