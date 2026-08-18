using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B4 RID: 6324
	internal class RadFilterOqlLessThanOrEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4A1 RID: 62625 RVA: 0x0037908C File Offset: 0x0037728C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} <= {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
