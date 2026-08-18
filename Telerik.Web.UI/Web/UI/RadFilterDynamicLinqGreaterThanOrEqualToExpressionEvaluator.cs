using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200185F RID: 6239
	internal class RadFilterDynamicLinqGreaterThanOrEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F29C RID: 62108 RVA: 0x003745C4 File Offset: 0x003727C4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} >= {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
