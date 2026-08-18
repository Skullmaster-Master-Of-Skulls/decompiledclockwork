using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D3 RID: 6355
	internal class RadFilterListViewEqualToExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5AF RID: 62895 RVA: 0x0037C57C File Offset: 0x0037A77C
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewEqualToFilterExpression<>), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
