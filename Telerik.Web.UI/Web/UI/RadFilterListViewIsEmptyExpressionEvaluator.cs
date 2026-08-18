using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D6 RID: 6358
	internal class RadFilterListViewIsEmptyExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5B5 RID: 62901 RVA: 0x0037C618 File Offset: 0x0037A818
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			return base.CreateListViewExpression(typeof(RadListViewIsEmptyFilterExpression), expression);
		}
	}
}
