using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018F4 RID: 6388
	internal class RadFilterSqlStartsWithExpressionEvaluator : RadFilterSqlExpressionEvaluator
	{
		// Token: 0x0600F60F RID: 62991 RVA: 0x0037D490 File Offset: 0x0037B690
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "[{0}] LIKE '{1}%'";
			ArrayList values = this.ExtractPlaceHolders((IRadFilterValueExpression)expression, expression.FieldName);
			return new RadFilterEvaluationData(expression, values, expressionFormat);
		}
	}
}
