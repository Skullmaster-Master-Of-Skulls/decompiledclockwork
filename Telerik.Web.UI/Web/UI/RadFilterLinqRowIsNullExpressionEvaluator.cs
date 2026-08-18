using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001866 RID: 6246
	internal class RadFilterLinqRowIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2AA RID: 62122 RVA: 0x00374710 File Offset: 0x00372910
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it[\"{0}\"] == Convert.DBNull";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
