using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D2 RID: 6354
	internal class RadFilterListViewContainsExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5AD RID: 62893 RVA: 0x0037C548 File Offset: 0x0037A748
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewContainsFilterExpression), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
