using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001863 RID: 6243
	internal class RadFilterDynamicLinqNotIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2A4 RID: 62116 RVA: 0x00374668 File Offset: 0x00372868
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it.{0} <> \"\"";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
