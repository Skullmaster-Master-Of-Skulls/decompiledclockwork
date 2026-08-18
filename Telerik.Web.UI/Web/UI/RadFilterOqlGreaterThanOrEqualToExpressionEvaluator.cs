using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B2 RID: 6322
	internal class RadFilterOqlGreaterThanOrEqualToExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F49D RID: 62621 RVA: 0x00379044 File Offset: 0x00377244
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} >= {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
