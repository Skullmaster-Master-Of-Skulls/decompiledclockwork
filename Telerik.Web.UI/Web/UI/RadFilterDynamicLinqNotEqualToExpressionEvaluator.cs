using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200185D RID: 6237
	internal class RadFilterDynamicLinqNotEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F298 RID: 62104 RVA: 0x0037457C File Offset: 0x0037277C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} <> {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
