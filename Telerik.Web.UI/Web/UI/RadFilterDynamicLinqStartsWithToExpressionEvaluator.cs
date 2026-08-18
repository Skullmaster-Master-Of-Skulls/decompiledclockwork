using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200186F RID: 6255
	internal class RadFilterDynamicLinqStartsWithToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2C0 RID: 62144 RVA: 0x00374968 File Offset: 0x00372B68
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0}.StartsWith({1})";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
