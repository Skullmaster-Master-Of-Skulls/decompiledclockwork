using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018AE RID: 6318
	internal class RadFilterOqlEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F493 RID: 62611 RVA: 0x00378EA0 File Offset: 0x003770A0
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} = {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
