using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001858 RID: 6232
	internal class RadFilterDynamicLinqBetweenExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F28E RID: 62094 RVA: 0x003744C8 File Offset: 0x003726C8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "({0} >= {1}) AND ({0} <= {2})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
