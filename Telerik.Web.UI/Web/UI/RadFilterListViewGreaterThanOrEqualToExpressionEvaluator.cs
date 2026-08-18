using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D5 RID: 6357
	internal class RadFilterListViewGreaterThanOrEqualToExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5B3 RID: 62899 RVA: 0x0037C5E4 File Offset: 0x0037A7E4
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewGreaterThenOrEqualToFilterExpression<>), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
