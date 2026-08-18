using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018E7 RID: 6375
	internal class RadFilterSqlEndsWithExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5F1 RID: 62961 RVA: 0x0037D0C8 File Offset: 0x0037B2C8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] LIKE '%{1}'";
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
