using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200186D RID: 6253
	internal class RadFilterGridBindableTypeNotIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2B8 RID: 62136 RVA: 0x00374898 File Offset: 0x00372A98
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it != \"\"";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
