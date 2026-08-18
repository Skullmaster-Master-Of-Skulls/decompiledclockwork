using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018EE RID: 6382
	internal class RadFilterSqlNotIsNullExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5FF RID: 62975 RVA: 0x0037D28C File Offset: 0x0037B48C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "NOT([{0}] IS NULL)";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
