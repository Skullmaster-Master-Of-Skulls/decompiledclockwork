using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018AA RID: 6314
	internal class RadFilterOqlBetweenExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F489 RID: 62601 RVA: 0x00378DC4 File Offset: 0x00376FC4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "({0} >= {1}) AND ({0} <= {2})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
