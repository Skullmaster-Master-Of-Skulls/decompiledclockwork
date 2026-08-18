using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001879 RID: 6265
	internal class RadFilterEntitySqlDoesNotContainExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2DD RID: 62173 RVA: 0x00374F7C File Offset: 0x0037317C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} NOT LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F2DE RID: 62174 RVA: 0x00374F98 File Offset: 0x00373198
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			string expression = RadFilterEntitySqlExpressionEvaluator.PrepareStartWithValue(base.FormatEvaluationData(evaluationData));
			return RadFilterEntitySqlExpressionEvaluator.PrepareEndWithValue(expression);
		}
	}
}
