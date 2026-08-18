using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018E4 RID: 6372
	internal class RadFilterSqlBetweenExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5EB RID: 62955 RVA: 0x0037D00C File Offset: 0x0037B20C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "([{0}] >= '{1}') AND ([{0}] <= '{2}')";
			}
			else
			{
				expressionFormat = "([{0}] >= {1}) AND ([{0}] <= {2})";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
