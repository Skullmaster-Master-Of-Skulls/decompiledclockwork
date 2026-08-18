using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B3 RID: 6323
	internal class RadFilterOqlLessThanExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F49F RID: 62623 RVA: 0x00379068 File Offset: 0x00377268
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} < {1}";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
