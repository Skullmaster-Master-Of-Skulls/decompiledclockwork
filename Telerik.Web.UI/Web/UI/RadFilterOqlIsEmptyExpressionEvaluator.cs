using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B5 RID: 6325
	internal class RadFilterOqlIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4A3 RID: 62627 RVA: 0x003790B0 File Offset: 0x003772B0
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} = ''";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
