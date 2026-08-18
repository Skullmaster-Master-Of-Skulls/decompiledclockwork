using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018EF RID: 6383
	internal class RadFilterSqlLessThanExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F601 RID: 62977 RVA: 0x0037D2C4 File Offset: 0x0037B4C4
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "[{0}] < '{1}'";
			}
			else
			{
				expressionFormat = "[{0}] < {1}";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
