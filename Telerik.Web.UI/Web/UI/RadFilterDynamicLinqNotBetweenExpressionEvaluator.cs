using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001859 RID: 6233
	internal class RadFilterDynamicLinqNotBetweenExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F290 RID: 62096 RVA: 0x003744EC File Offset: 0x003726EC
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "({0} < {1}) OR ({0} > {2})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
