using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018DA RID: 6362
	internal class RadFilterListViewNotEqualToExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5BD RID: 62909 RVA: 0x0037C6B8 File Offset: 0x0037A8B8
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewNotEqualToFilterExpression<>), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
