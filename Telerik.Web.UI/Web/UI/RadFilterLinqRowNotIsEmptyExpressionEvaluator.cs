using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001869 RID: 6249
	internal class RadFilterLinqRowNotIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2B0 RID: 62128 RVA: 0x003747B8 File Offset: 0x003729B8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it[\"{0}\"] != \"\"";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
