using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B6 RID: 6326
	internal class RadFilterOqlNotIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4A5 RID: 62629 RVA: 0x003790D4 File Offset: 0x003772D4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} <> ''";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
