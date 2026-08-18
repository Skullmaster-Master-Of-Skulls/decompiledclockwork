using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200187D RID: 6269
	internal class RadFilterEntitySqlEndsWithToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2EA RID: 62186 RVA: 0x003750E4 File Offset: 0x003732E4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F2EB RID: 62187 RVA: 0x003750FF File Offset: 0x003732FF
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			return RadFilterEntitySqlExpressionEvaluator.PrepareEndWithValue(base.FormatEvaluationData(evaluationData));
		}
	}
}
