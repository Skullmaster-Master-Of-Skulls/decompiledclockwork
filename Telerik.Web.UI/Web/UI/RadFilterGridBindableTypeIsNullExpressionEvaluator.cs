using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200186A RID: 6250
	internal class RadFilterGridBindableTypeIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2B2 RID: 62130 RVA: 0x003747F0 File Offset: 0x003729F0
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it == null";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
