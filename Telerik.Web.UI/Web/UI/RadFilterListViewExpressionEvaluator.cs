using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020018D1 RID: 6353
	public abstract class RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5A8 RID: 62888 RVA: 0x0037C40C File Offset: 0x0037A60C
		public static RadFilterListViewExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			switch (function)
			{
			case RadFilterFunction.Contains:
				return new RadFilterListViewContainsExpressionEvaluator();
			case RadFilterFunction.StartsWith:
				return new RadFilterListViewStartsWithExpressionEvaluator();
			case RadFilterFunction.EndsWith:
				return new RadFilterListViewEndsWithExpressionEvaluator();
			case RadFilterFunction.EqualTo:
				return new RadFilterListViewEqualToExpressionEvaluator();
			case RadFilterFunction.NotEqualTo:
				return new RadFilterListViewNotEqualToExpressionEvaluator();
			case RadFilterFunction.GreaterThan:
				return new RadFilterListViewGreaterThanExpressionEvaluator();
			case RadFilterFunction.LessThan:
				return new RadFilterListViewLessThanExpressionEvaluator();
			case RadFilterFunction.GreaterThanOrEqualTo:
				return new RadFilterListViewGreaterThanOrEqualToExpressionEvaluator();
			case RadFilterFunction.LessThanOrEqualTo:
				return new RadFilterListViewLessThanOrEqualToExpressionEvaluator();
			case RadFilterFunction.IsEmpty:
				return new RadFilterListViewIsEmptyExpressionEvaluator();
			case RadFilterFunction.NotIsEmpty:
				return new RadFilterListViewNotIsEmptyExpressionEvaluator();
			case RadFilterFunction.IsNull:
				return new RadFilterListViewIsNullExpressionEvaluator();
			case RadFilterFunction.NotIsNull:
				return new RadFilterListViewNotIsNullExpressionEvaluator();
			}
			throw new ArgumentOutOfRangeException("function");
		}

		// Token: 0x0600F5A9 RID: 62889
		public abstract RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression);

		// Token: 0x0600F5AA RID: 62890 RVA: 0x0037C4DC File Offset: 0x0037A6DC
		protected RadListViewFilterExpression HandleSingleValueExpressionValues(RadListViewFilterExpression listViewExpression, IRadFilterValueExpression filterValueExpression)
		{
			IRadListViewSingleValueExpression radListViewSingleValueExpression = listViewExpression as IRadListViewSingleValueExpression;
			if (radListViewSingleValueExpression != null)
			{
				radListViewSingleValueExpression.CurrentValue = filterValueExpression.Values[0];
			}
			return listViewExpression;
		}

		// Token: 0x0600F5AB RID: 62891 RVA: 0x0037C508 File Offset: 0x0037A708
		protected RadListViewFilterExpression CreateListViewExpression(Type expressionType, RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression radListViewFilterExpression = RadListViewFilterExpression.CreateExpressionFromTypeName(expressionType.Name, expression.FieldType.FullName);
			((IStateManager)radListViewFilterExpression).TrackViewState();
			radListViewFilterExpression.FieldName = expression.FieldName;
			return radListViewFilterExpression;
		}
	}
}
