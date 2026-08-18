using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018F2 RID: 6386
	internal class RadFilterSqlNotEqualToExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F607 RID: 62983 RVA: 0x0037D3A8 File Offset: 0x0037B5A8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat;
			if (base.ShouldAddQoutes(expression.FieldType))
			{
				expressionFormat = "[{0}] <> '{1}'";
			}
			else
			{
				expressionFormat = "[{0}] <> {1}";
			}
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
