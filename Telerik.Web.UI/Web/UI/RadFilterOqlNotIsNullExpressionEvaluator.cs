using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B8 RID: 6328
	internal class RadFilterOqlNotIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4A9 RID: 62633 RVA: 0x0037911C File Offset: 0x0037731C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} = ''";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
