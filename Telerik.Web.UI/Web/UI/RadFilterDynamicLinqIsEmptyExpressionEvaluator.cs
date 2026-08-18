using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001862 RID: 6242
	internal class RadFilterDynamicLinqIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2A2 RID: 62114 RVA: 0x00374630 File Offset: 0x00372830
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it.{0} = \"\"";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
