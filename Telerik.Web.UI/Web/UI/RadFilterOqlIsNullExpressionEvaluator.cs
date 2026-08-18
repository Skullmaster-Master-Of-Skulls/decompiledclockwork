using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B7 RID: 6327
	internal class RadFilterOqlIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F4A7 RID: 62631 RVA: 0x003790F8 File Offset: 0x003772F8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "{0} == nil";
			return base.PrepareExpression(expressionFormat, expression);
		}
	}
}
