using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018F1 RID: 6385
	internal class RadFilterSqlNotBetweenExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F605 RID: 62981 RVA: 0x0037D35C File Offset: 0x0037B55C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "([{0}] < '{1}') OR ([{0}] > '{2}')";
			}
			else
			{
				expressionFormat = "([{0}] < {1}) OR ([{0}] > {2})";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
