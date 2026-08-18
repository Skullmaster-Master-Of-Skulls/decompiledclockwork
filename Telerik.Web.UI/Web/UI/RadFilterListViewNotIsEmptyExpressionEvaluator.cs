using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018DB RID: 6363
	internal class RadFilterListViewNotIsEmptyExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5BF RID: 62911 RVA: 0x0037C6EC File Offset: 0x0037A8EC
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			return base.CreateListViewExpression(typeof(RadListViewIsNotEmptyFilterExpression), expression);
		}
	}
}
