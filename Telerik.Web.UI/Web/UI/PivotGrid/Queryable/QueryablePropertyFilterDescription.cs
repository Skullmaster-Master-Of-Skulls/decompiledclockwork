using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200072D RID: 1837
	[DataContract]
	public sealed class QueryablePropertyFilterDescription : QueryableFilterDescription
	{
		// Token: 0x0600414C RID: 16716 RVA: 0x000CD5CC File Offset: 0x000CB7CC
		internal override IEnumerable<Expression> CreateFilterKeyValuesExpressions(ParameterExpression itemExpression)
		{
			if (itemExpression == null)
			{
				throw new ArgumentNullException("itemExpression");
			}
			if (string.IsNullOrEmpty(base.PropertyName))
			{
				return new ParameterExpression[]
				{
					itemExpression
				};
			}
			Expression memberAccess = QueryableExpressionHelper.MakeMemberAccess(itemExpression, base.PropertyName);
			Expression expression = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess);
			Expression expression2 = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess, Expression.Constant(true, typeof(bool)), Expression.Constant(false, typeof(bool)));
			return new Expression[]
			{
				expression,
				expression2
			};
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000CD65C File Offset: 0x000CB85C
		internal override Expression CreateFilterKeyExpression(IEnumerable<Expression> valueExpressions)
		{
			if (valueExpressions == null)
			{
				throw new ArgumentNullException("valueExpressions");
			}
			if (valueExpressions.Count<Expression>() == 0)
			{
				throw new InvalidOperationException("There should be at least one value expression");
			}
			if (base.Condition == null || !base.Condition.IsActive)
			{
				return null;
			}
			Expression expression;
			Expression left;
			using (IEnumerator<Expression> enumerator = valueExpressions.GetEnumerator())
			{
				enumerator.MoveNext();
				expression = enumerator.Current;
				enumerator.MoveNext();
				left = enumerator.Current;
			}
			expression = base.Condition.GetExpression(expression);
			if (expression == null)
			{
				return null;
			}
			return Expression.And(left, expression);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000CD6FC File Offset: 0x000CB8FC
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryablePropertyFilterDescription();
		}
	}
}
