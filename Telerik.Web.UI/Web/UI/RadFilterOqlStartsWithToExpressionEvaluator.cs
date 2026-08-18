using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018BA RID: 6330
	internal class RadFilterOqlStartsWithToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4AE RID: 62638 RVA: 0x00379180 File Offset: 0x00377380
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} LIKE {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}

		// Token: 0x0600F4AF RID: 62639 RVA: 0x0037919B File Offset: 0x0037739B
		protected override string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			return RadFilterOqlExpressionEvaluator.PrepareStartWithValue(base.FormatEvaluationData(evaluationData));
		}
	}
}
