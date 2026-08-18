using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018E6 RID: 6374
	internal class RadFilterSqlDoesNotContainExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F5EF RID: 62959 RVA: 0x0037D090 File Offset: 0x0037B290
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] NOT LIKE '%{1}%'";
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
