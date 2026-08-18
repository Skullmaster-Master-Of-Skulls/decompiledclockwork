using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D8 RID: 6360
	internal class RadFilterListViewLessThanExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5B9 RID: 62905 RVA: 0x0037C650 File Offset: 0x0037A850
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewLessThanFilterExpression<>), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
