using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D7 RID: 6359
	internal class RadFilterListViewIsNullExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5B7 RID: 62903 RVA: 0x0037C633 File Offset: 0x0037A833
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			return base.CreateListViewExpression(typeof(RadListViewIsNullFilterExpression), expression);
		}
	}
}
