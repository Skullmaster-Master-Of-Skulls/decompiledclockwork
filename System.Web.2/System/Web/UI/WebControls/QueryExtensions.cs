using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A6 RID: 1190
	public static class QueryExtensions
	{
		// Token: 0x06003B98 RID: 15256 RVA: 0x000C19C4 File Offset: 0x000BFBC4
		public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string sortExpression) where T : class
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (string.IsNullOrWhiteSpace(sortExpression))
			{
				return source;
			}
			sortExpression = sortExpression.Trim();
			bool flag = false;
			if (sortExpression.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
				int length = sortExpression.Length - " DESC".Length;
				sortExpression = sortExpression.Substring(0, length).Trim();
			}
			if (string.IsNullOrEmpty(sortExpression))
			{
				return source;
			}
			ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
			MemberExpression memberExpression = null;
			string[] array = sortExpression.Split(new char[]
			{
				'.'
			});
			foreach (string propertyName in array)
			{
				if (memberExpression == null)
				{
					memberExpression = Expression.Property(parameterExpression, propertyName);
				}
				else
				{
					memberExpression = Expression.Property(memberExpression, propertyName);
				}
			}
			LambdaExpression expression = Expression.Lambda(memberExpression, new ParameterExpression[]
			{
				parameterExpression
			});
			string methodName = flag ? "OrderByDescending" : "OrderBy";
			Expression expression2 = Expression.Call(typeof(Queryable), methodName, new Type[]
			{
				source.ElementType,
				memberExpression.Type
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(expression)
			});
			return (IQueryable<T>)source.Provider.CreateQuery(expression2);
		}

		// Token: 0x04002349 RID: 9033
		private const string SORT_DIRECTION_DESC = " DESC";
	}
}
