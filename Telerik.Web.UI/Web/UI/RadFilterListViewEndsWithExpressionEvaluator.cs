using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018DE RID: 6366
	internal class RadFilterListViewEndsWithExpressionEvaluator : RadFilterListViewExpressionEvaluator
	{
		// Token: 0x0600F5C5 RID: 62917 RVA: 0x0037C758 File Offset: 0x0037A958
		public override RadListViewFilterExpression Evaluate(RadFilterNonGroupExpression expression)
		{
			RadListViewFilterExpression listViewExpression = base.CreateListViewExpression(typeof(RadListViewEndsWithFilterExpression), expression);
			return base.HandleSingleValueExpressionValues(listViewExpression, (IRadFilterValueExpression)expression);
		}
	}
}
