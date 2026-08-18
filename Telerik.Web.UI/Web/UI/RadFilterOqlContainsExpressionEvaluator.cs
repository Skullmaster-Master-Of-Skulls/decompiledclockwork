using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018AC RID: 6316
	internal class RadFilterOqlContainsExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F48D RID: 62605 RVA: 0x00378E0C File Offset: 0x0037700C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F48E RID: 62606 RVA: 0x00378E28 File Offset: 0x00377028
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			string expression = RadFilterOqlExpressionEvaluator.PrepareStartWithValue(base.FormatEvaluationData(evaluationData));
			return RadFilterOqlExpressionEvaluator.PrepareEndWithValue(expression);
		}
	}
}
