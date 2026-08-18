using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018E9 RID: 6377
	internal class RadFilterSqlGreaterThanExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5F5 RID: 62965 RVA: 0x0037D14C File Offset: 0x0037B34C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "[{0}] > '{1}'";
			}
			else
			{
				expressionFormat = "[{0}] > {1}";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
