using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018AF RID: 6319
	internal class RadFilterOqlNotEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F495 RID: 62613 RVA: 0x00378EC4 File Offset: 0x003770C4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} <> {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
