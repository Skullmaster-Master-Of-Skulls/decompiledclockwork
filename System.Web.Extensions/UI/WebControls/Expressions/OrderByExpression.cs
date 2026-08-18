using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Resources;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000D0 RID: 208
	[PersistChildren(false)]
	[ParseChildren(true, "ThenByExpressions")]
	public class OrderByExpression : DataSourceExpression
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000269A9 File Offset: 0x00024BA9
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000269C9 File Offset: 0x00024BC9
		public string DataField
		{
			get
			{
				return ((string)base.ViewState["DataField"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000269DC File Offset: 0x00024BDC
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00026A05 File Offset: 0x00024C05
		public SortDirection Direction
		{
			get
			{
				object obj = base.ViewState["Direction"];
				if (obj == null)
				{
					return SortDirection.Ascending;
				}
				return (SortDirection)obj;
			}
			set
			{
				base.ViewState["Direction"] = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00026A1D File Offset: 0x00024C1D
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public Collection<ThenBy> ThenByExpressions
		{
			get
			{
				if (this._thenByExpressions == null)
				{
					this._thenByExpressions = new Collection<ThenBy>();
				}
				return this._thenByExpressions;
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00026A38 File Offset: 0x00024C38
		public override IQueryable GetQueryable(IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.DataField))
			{
				throw new InvalidOperationException(AtlasWeb.Expressions_DataFieldRequired);
			}
			ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
			source = OrderByExpression.CreateSortQueryable(source, parameterExpression, this.Direction, this.DataField, false);
			foreach (ThenBy thenBy in this.ThenByExpressions)
			{
				source = OrderByExpression.CreateSortQueryable(source, parameterExpression, thenBy.Direction, thenBy.DataField, true);
			}
			return source;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00026ADC File Offset: 0x00024CDC
		private static IQueryable CreateSortQueryable(IQueryable source, ParameterExpression parameterExpression, SortDirection direction, string dataField, bool isThenBy)
		{
			string queryableMethod = isThenBy ? OrderByExpression.GetThenBySortMethod(direction) : OrderByExpression.GetSortMethod(direction);
			Expression expression = ExpressionHelper.CreatePropertyExpression(parameterExpression, dataField);
			return source.Call(queryableMethod, Expression.Lambda(expression, new ParameterExpression[]
			{
				parameterExpression
			}), new Type[]
			{
				source.ElementType,
				expression.Type
			});
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00026B33 File Offset: 0x00024D33
		private static string GetSortMethod(SortDirection direction)
		{
			if (direction == SortDirection.Ascending)
			{
				return "OrderBy";
			}
			if (direction != SortDirection.Descending)
			{
				return "OrderBy";
			}
			return "OrderByDescending";
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00026B4F File Offset: 0x00024D4F
		private static string GetThenBySortMethod(SortDirection direction)
		{
			if (direction == SortDirection.Ascending)
			{
				return "ThenBy";
			}
			if (direction != SortDirection.Descending)
			{
				return null;
			}
			return "ThenByDescending";
		}

		// Token: 0x04000352 RID: 850
		private const string OrderByMethod = "OrderBy";

		// Token: 0x04000353 RID: 851
		private const string ThenByMethod = "ThenBy";

		// Token: 0x04000354 RID: 852
		private const string OrderDescendingByMethod = "OrderByDescending";

		// Token: 0x04000355 RID: 853
		private const string ThenDescendingByMethod = "ThenByDescending";

		// Token: 0x04000356 RID: 854
		private Collection<ThenBy> _thenByExpressions;
	}
}
