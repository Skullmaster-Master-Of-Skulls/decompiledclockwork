using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B1 RID: 6321
	internal class RadFilterOqlGreaterThanExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F49B RID: 62619 RVA: 0x00379020 File Offset: 0x00377220
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} > {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
