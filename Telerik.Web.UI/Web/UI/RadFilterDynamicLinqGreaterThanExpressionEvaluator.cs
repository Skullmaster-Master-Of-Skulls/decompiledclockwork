using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200185E RID: 6238
	internal class RadFilterDynamicLinqGreaterThanExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F29A RID: 62106 RVA: 0x003745A0 File Offset: 0x003727A0
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} > {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
