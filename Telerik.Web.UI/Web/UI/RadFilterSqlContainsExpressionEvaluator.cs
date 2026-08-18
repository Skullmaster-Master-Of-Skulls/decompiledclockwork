using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018E5 RID: 6373
	internal class RadFilterSqlContainsExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5ED RID: 62957 RVA: 0x0037D058 File Offset: 0x0037B258
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] LIKE '%{1}%'";
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
