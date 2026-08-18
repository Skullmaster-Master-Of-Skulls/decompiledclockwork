using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018EC RID: 6380
	internal class RadFilterSqlNotIsEmptyExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5FB RID: 62971 RVA: 0x0037D21C File Offset: 0x0037B41C
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] <> ''";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
