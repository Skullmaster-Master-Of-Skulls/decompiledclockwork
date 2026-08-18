using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018AD RID: 6317
	internal class RadFilterOqlDoesNotContainExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F490 RID: 62608 RVA: 0x00378E50 File Offset: 0x00377050
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "NOT ({0} LIKE {1})";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F491 RID: 62609 RVA: 0x00378E6C File Offset: 0x0037706C
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			string expression = RadFilterOqlExpressionEvaluator.PrepareStartWithValue(base.FormatEvaluationData(evaluationData));
			return RadFilterOqlExpressionEvaluator.PrepareEndWithValue(expression) + ")";
		}
	}
}
