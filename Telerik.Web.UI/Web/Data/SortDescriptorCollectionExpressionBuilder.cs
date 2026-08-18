using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA8 RID: 7080
	internal class SortDescriptorCollectionExpressionBuilder
	{
		// Token: 0x060111F4 RID: 70132 RVA: 0x003C686A File Offset: 0x003C4A6A
		public SortDescriptorCollectionExpressionBuilder(IQueryable queryable, IEnumerable<SortDescriptor> sortDescriptors)
		{
			this.queryable = queryable;
			this.sortDescriptors = sortDescriptors;
		}

		// Token: 0x060111F5 RID: 70133 RVA: 0x003C6880 File Offset: 0x003C4A80
		public IQueryable Sort()
		{
			IQueryable queryable = this.queryable;
			bool flag = true;
			foreach (SortDescriptor sortDescriptor in this.sortDescriptors)
			{
				Type typeFromHandle = typeof(object);
				MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = ExpressionBuilderFactory.MemberAccess(this.queryable, typeFromHandle, sortDescriptor.Member);
				LambdaExpression lambdaExpression = memberAccessExpressionBuilderBase.CreateLambdaExpression();
				string methodName;
				if (flag)
				{
					methodName = ((sortDescriptor.SortDirection == ListSortDirection.Ascending) ? "OrderBy" : "OrderByDescending");
					flag = false;
				}
				else
				{
					methodName = ((sortDescriptor.SortDirection == ListSortDirection.Ascending) ? "ThenBy" : "ThenByDescending");
				}
				queryable = queryable.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, new Type[]
				{
					queryable.ElementType,
					lambdaExpression.Body.Type
				}, new Expression[]
				{
					queryable.Expression,
					Expression.Quote(lambdaExpression)
				}));
			}
			return queryable;
		}

		// Token: 0x04004CAD RID: 19629
		private readonly IEnumerable<SortDescriptor> sortDescriptors;

		// Token: 0x04004CAE RID: 19630
		private readonly IQueryable queryable;
	}
}
