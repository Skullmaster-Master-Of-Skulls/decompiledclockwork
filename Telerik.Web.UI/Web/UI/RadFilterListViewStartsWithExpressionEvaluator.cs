using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018DD RID: 6365
	internal class RadFilterListViewStartsWithExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5C3 RID: 62915 RVA: 0x0037C724 File Offset: 0x0037A924
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewStartsWithFilterExpression), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
