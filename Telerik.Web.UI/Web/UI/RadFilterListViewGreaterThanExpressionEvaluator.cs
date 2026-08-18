using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D4 RID: 6356
	internal class RadFilterListViewGreaterThanExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5B1 RID: 62897 RVA: 0x0037C5B0 File Offset: 0x0037A7B0
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewGreaterThanFilterExpression<>), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
