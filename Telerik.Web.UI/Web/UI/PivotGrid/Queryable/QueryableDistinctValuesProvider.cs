using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200073A RID: 1850
	internal class QueryableDistinctValuesProvider : DistinctValuesProvider
	{
		// Token: 0x060041D2 RID: 16850 RVA: 0x000CE8F6 File Offset: 0x000CCAF6
		public QueryableDistinctValuesProvider(IQueryable queryable, QueryableFilterDescription filterDescription)
		{
			if (queryable == null)
			{
				throw new ArgumentNullException("queryable");
			}
			if (filterDescription == null)
			{
				throw new ArgumentNullException("filterDescription");
			}
			this.queryable = queryable;
			this.description = filterDescription;
			this.disctinctValues = new List<object>();
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x000CE933 File Offset: 0x000CCB33
		public override IEnumerable<object> DisctinctValues
		{
			get
			{
				return this.disctinctValues;
			}
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000CE93C File Offset: 0x000CCB3C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We should not catch general exceptions, however the IQueryable Source can vary.")]
		public override void Refresh()
		{
			if (this.description.PropertyName == null)
			{
				this.disctinctValues = Enumerable.Empty<object>();
				return;
			}
			Type elementType = this.queryable.ElementType;
			ParameterExpression parameterExpression = Expression.Parameter(elementType);
			Expression body = QueryableExpressionHelper.MakeMemberAccess(parameterExpression, this.description.PropertyName);
			LambdaExpression lambda = Expression.Lambda(body, new ParameterExpression[]
			{
				parameterExpression
			});
			try
			{
				IQueryable queryable = PivotQueryableExtensions.SelectDistinct(this.queryable, lambda);
				List<object> list = new List<object>();
				foreach (object obj in queryable)
				{
					if (obj != null)
					{
						list.Add(obj);
					}
				}
				this.disctinctValues = QueryableDistinctValuesProvider.GetSortedDistincsValues(list);
			}
			catch (Exception ex)
			{
				this.description.ThrowExceptionOnDataProvider(ex);
			}
			base.OnUpdated();
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000CEA38 File Offset: 0x000CCC38
		private static Type GetDistinctType(IEnumerable<object> objects)
		{
			foreach (object obj in objects)
			{
				if (obj != null)
				{
					return obj.GetType();
				}
			}
			return null;
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000CEA88 File Offset: 0x000CCC88
		private static IEnumerable<object> GetSortedDistincsValues(IEnumerable<object> uniqueItems)
		{
			Type distinctType = QueryableDistinctValuesProvider.GetDistinctType(uniqueItems);
			if (distinctType == null)
			{
				return uniqueItems;
			}
			try
			{
				bool flag = PivotTypeExtensions.CanSort(distinctType);
				if (flag)
				{
					object[] array = uniqueItems.ToArray<object>();
					Array.Sort<object>(array);
					uniqueItems = array;
				}
			}
			catch (InvalidOperationException)
			{
			}
			return uniqueItems;
		}

		// Token: 0x04001168 RID: 4456
		private readonly IQueryable queryable;

		// Token: 0x04001169 RID: 4457
		private readonly QueryableFilterDescription description;

		// Token: 0x0400116A RID: 4458
		private IEnumerable<object> disctinctValues;
	}
}
