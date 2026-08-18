using System;
using System.Linq;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A5 RID: 1189
	internal static class QueryableHelpers
	{
		// Token: 0x06003B95 RID: 15253 RVA: 0x000C1944 File Offset: 0x000BFB44
		public static IQueryable<T> SortandPageHelper<T>(IQueryable<T> queryable, int? startRowIndex, int? maxRowIndex, string sortExpression) where T : class
		{
			if (queryable == null)
			{
				throw new ArgumentNullException("queryable");
			}
			if (!string.IsNullOrEmpty(sortExpression))
			{
				queryable = queryable.SortBy(sortExpression);
			}
			if (startRowIndex != null && maxRowIndex != null)
			{
				queryable = queryable.Skip(startRowIndex.Value).Take(maxRowIndex.Value);
			}
			return queryable;
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000C199E File Offset: 0x000BFB9E
		public static int CountHelper<T>(IQueryable<T> queryable) where T : class
		{
			if (queryable == null)
			{
				throw new ArgumentNullException("queryable");
			}
			return queryable.Count<T>();
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000C19B4 File Offset: 0x000BFBB4
		public static bool IsOrderingMethodFound<T>(IQueryable<T> queryable) where T : class
		{
			return OrderingMethodFinder.OrderMethodExists(queryable.Expression);
		}
	}
}
