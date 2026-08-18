using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001870 RID: 6256
	internal class RadFilterDynamicLinqEndsWithToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2C2 RID: 62146 RVA: 0x0037498C File Offset: 0x00372B8C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0}.EndsWith({1})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
