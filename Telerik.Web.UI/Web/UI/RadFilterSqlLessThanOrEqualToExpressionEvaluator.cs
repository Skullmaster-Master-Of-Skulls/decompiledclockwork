using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018F0 RID: 6384
	internal class RadFilterSqlLessThanOrEqualToExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F603 RID: 62979 RVA: 0x0037D310 File Offset: 0x0037B510
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "[{0}] <= '{1}'";
			}
			else
			{
				expressionFormat = "[{0}] <= {1}";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
