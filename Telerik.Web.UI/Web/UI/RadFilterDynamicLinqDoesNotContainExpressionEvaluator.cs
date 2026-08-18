using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200185B RID: 6235
	internal class RadFilterDynamicLinqDoesNotContainExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F294 RID: 62100 RVA: 0x00374534 File Offset: 0x00372734
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "!{0}.Contains({1})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
