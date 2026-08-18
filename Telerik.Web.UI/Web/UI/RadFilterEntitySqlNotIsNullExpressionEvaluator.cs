using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F72 RID: 3954
	internal class RadFilterEntitySqlNotIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600977C RID: 38780 RVA: 0x0021F7A8 File Offset: 0x0021D9A8
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "NOT (it.{0} IS null)";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
