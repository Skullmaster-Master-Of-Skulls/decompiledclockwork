using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001861 RID: 6241
	internal class RadFilterDynamicLinqLessThanOrEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2A0 RID: 62112 RVA: 0x0037460C File Offset: 0x0037280C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} <= {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
