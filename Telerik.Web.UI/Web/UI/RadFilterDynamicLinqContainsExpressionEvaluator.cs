using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200185A RID: 6234
	internal class RadFilterDynamicLinqContainsExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F292 RID: 62098 RVA: 0x00374510 File Offset: 0x00372710
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0}.Contains({1})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
