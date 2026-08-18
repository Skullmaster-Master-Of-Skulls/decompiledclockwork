using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200187C RID: 6268
	internal class RadFilterEntitySqlStartsWithToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2E7 RID: 62183 RVA: 0x003750B0 File Offset: 0x003732B0
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F2E8 RID: 62184 RVA: 0x003750CB File Offset: 0x003732CB
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			return RadFilterEntitySqlExpressionEvaluator.PrepareStartWithValue(base.FormatEvaluationData(evaluationData));
		}
	}
}
