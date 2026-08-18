using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018DC RID: 6364
	internal class RadFilterListViewNotIsNullExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5C1 RID: 62913 RVA: 0x0037C707 File Offset: 0x0037A907
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			return base.CreateListViewExpression(typeof(RadListViewIsNotNullFilterExpression), expression);
		}
	}
}
