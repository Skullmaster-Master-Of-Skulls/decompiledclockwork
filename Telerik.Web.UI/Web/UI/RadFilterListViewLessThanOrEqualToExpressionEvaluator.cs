using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D9 RID: 6361
	internal class RadFilterListViewLessThanOrEqualToExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5BB RID: 62907 RVA: 0x0037C684 File Offset: 0x0037A884
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewLessThanOrEqualToFilterExpression<>), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
