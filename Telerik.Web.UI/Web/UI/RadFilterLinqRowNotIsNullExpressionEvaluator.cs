using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001867 RID: 6247
	internal class RadFilterLinqRowNotIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2AC RID: 62124 RVA: 0x00374748 File Offset: 0x00372948
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it[\"{0}\"] != Convert.DBNull";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
