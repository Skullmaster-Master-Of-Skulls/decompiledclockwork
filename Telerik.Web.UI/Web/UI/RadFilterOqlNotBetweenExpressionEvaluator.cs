using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018AB RID: 6315
	internal class RadFilterOqlNotBetweenExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F48B RID: 62603 RVA: 0x00378DE8 File Offset: 0x00376FE8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "({0} < {1}) OR ({0} > {2})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
