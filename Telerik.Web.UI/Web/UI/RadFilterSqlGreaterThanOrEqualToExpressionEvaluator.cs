using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018EA RID: 6378
	internal class RadFilterSqlGreaterThanOrEqualToExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5F7 RID: 62967 RVA: 0x0037D198 File Offset: 0x0037B398
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "[{0}] >= '{1}'";
			}
			else
			{
				expressionFormat = "[{0}] >= {1}";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
