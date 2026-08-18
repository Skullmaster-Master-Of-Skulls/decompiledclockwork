using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200186B RID: 6251
	internal class RadFilterGridBindableTypeNotIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2B4 RID: 62132 RVA: 0x00374828 File Offset: 0x00372A28
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it != null";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
