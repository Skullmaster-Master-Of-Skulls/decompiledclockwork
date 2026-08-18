using System;
using System.Linq;
using System.Linq.Expressions;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000D6C RID: 3436
	internal static class PivotQueryableExtensions
	{
		// Token: 0x0600800D RID: 32781 RVA: 0x001D46A0 File Offset: 0x001D28A0
		private static IQueryable CallQueryableMethod(this IQueryable source, string methodName, LambdaExpression selector)
		{
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, new Type[]
			{
				source.ElementType,
				selector.Body.Type
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x001D4704 File Offset: 0x001D2904
		public static IQueryable Where(this IQueryable source, LambdaExpression filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(filter)
			}));
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x001D476B File Offset: 0x001D296B
		public static IQueryable Select(this IQueryable source, LambdaExpression selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return source.CallQueryableMethod("Select", selector);
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x001D4787 File Offset: 0x001D2987
		public static IQueryable GroupBy(this IQueryable source, LambdaExpression keySelector)
		{
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return source.CallQueryableMethod("GroupBy", keySelector);
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x001D47A4 File Offset: 0x001D29A4
		public static IQueryable SelectDistinct(IQueryable source, LambdaExpression lambda)
		{
			IQueryable queryable = source.Select(lambda);
			return queryable.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Distinct", new Type[]
			{
				lambda.Body.Type
			}, new Expression[]
			{
				queryable.Expression
			}));
		}
	}
}
