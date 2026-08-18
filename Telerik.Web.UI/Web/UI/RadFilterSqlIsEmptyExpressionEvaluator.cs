using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018EB RID: 6379
	internal class RadFilterSqlIsEmptyExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5F9 RID: 62969 RVA: 0x0037D1E4 File Offset: 0x0037B3E4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] = ''";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
