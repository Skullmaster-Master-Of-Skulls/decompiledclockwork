using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001868 RID: 6248
	internal class RadFilterLinqRowIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2AE RID: 62126 RVA: 0x00374780 File Offset: 0x00372980
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it[\"{0}\"] == \"\"";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
