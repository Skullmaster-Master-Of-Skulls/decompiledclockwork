using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200186C RID: 6252
	internal class RadFilterGridBindableTypeIsEmptyExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2B6 RID: 62134 RVA: 0x00374860 File Offset: 0x00372A60
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it == \"\"";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
