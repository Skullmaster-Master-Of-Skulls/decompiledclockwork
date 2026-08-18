using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018BB RID: 6331
	internal class RadFilterOqlEndsWithToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4B1 RID: 62641 RVA: 0x003791B4 File Offset: 0x003773B4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F4B2 RID: 62642 RVA: 0x003791CF File Offset: 0x003773CF
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			return RadFilterOqlExpressionEvaluator.PrepareEndWithValue(base.FormatEvaluationData(evaluationData));
		}
	}
}
