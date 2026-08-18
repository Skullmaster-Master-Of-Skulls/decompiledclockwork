using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001878 RID: 6264
	internal class RadFilterEntitySqlContainsExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2DA RID: 62170 RVA: 0x00374F38 File Offset: 0x00373138
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F2DB RID: 62171 RVA: 0x00374F54 File Offset: 0x00373154
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			string expression = RadFilterEntitySqlExpressionEvaluator.PrepareStartWithValue(base.FormatEvaluationData(evaluationData));
			return RadFilterEntitySqlExpressionEvaluator.PrepareEndWithValue(expression);
		}
	}
}
