using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001864 RID: 6244
	internal class RadFilterDynamicLinqIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2A6 RID: 62118 RVA: 0x003746A0 File Offset: 0x003728A0
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it.{0} == null";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
