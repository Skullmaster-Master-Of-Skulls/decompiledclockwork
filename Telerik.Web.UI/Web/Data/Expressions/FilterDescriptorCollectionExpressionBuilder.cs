using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB0 RID: 7088
	internal class FilterDescriptorCollectionExpressionBuilder : FilterExpressionBuilder
	{
		// Token: 0x06011235 RID: 70197 RVA: 0x003C781B File Offset: 0x003C5A1B
		public FilterDescriptorCollectionExpressionBuilder(ParameterExpression parameterExpression, IEnumerable<IFilterDescriptor> filterDescriptors) : this(parameterExpression, filterDescriptors, FilterCompositionLogicalOperator.And)
		{
		}

		// Token: 0x06011236 RID: 70198 RVA: 0x003C7826 File Offset: 0x003C5A26
		public FilterDescriptorCollectionExpressionBuilder(ParameterExpression parameterExpression, IEnumerable<IFilterDescriptor> filterDescriptors, FilterCompositionLogicalOperator logicalOperator) : base(parameterExpression)
		{
			this.filterDescriptors = filterDescriptors;
			this.logicalOperator = logicalOperator;
		}

		// Token: 0x06011237 RID: 70199 RVA: 0x003C7840 File Offset: 0x003C5A40
		public override Expression CreateBodyExpression()
		{
			Expression expression = null;
			foreach (IFilterDescriptor filterDescriptor in this.filterDescriptors)
			{
				Expression expression2 = filterDescriptor.CreateFilterExpression(base.ParameterExpression);
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = FilterDescriptorCollectionExpressionBuilder.ComposeExpressions(expression, expression2, this.logicalOperator);
				}
			}
			if (expression == null)
			{
				return ExpressionParser.TrueLiteral;
			}
			return expression;
		}

		// Token: 0x06011238 RID: 70200 RVA: 0x003C78B4 File Offset: 0x003C5AB4
		private static Expression ComposeExpressions(Expression left, Expression right, FilterCompositionLogicalOperator logicalOperator)
		{
			switch (logicalOperator)
			{
			case FilterCompositionLogicalOperator.Or:
				return Expression.OrElse(left, right);
			}
			return Expression.AndAlso(left, right);
		}

		// Token: 0x04004CB8 RID: 19640
		private readonly IEnumerable<IFilterDescriptor> filterDescriptors;

		// Token: 0x04004CB9 RID: 19641
		private readonly FilterCompositionLogicalOperator logicalOperator;
	}
}
