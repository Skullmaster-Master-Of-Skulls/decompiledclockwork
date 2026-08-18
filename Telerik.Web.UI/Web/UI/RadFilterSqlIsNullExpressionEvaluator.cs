using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018ED RID: 6381
	internal class RadFilterSqlIsNullExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5FD RID: 62973 RVA: 0x0037D254 File Offset: 0x0037B454
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] IS NULL";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
