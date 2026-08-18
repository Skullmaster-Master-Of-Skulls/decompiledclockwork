using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001865 RID: 6245
	internal class RadFilterDynamicLinqNotIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2A8 RID: 62120 RVA: 0x003746D8 File Offset: 0x003728D8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it.{0} != null";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
