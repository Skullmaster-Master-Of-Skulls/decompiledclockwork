using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001860 RID: 6240
	internal class RadFilterDynamicLinqLessThanExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F29E RID: 62110 RVA: 0x003745E8 File Offset: 0x003727E8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} < {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
