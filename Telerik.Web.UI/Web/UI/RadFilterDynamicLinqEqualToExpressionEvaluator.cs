using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200185C RID: 6236
	internal class RadFilterDynamicLinqEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F296 RID: 62102 RVA: 0x00374558 File Offset: 0x00372758
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} = {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
