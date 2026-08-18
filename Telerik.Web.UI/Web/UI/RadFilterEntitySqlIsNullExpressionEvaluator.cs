using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F71 RID: 3953
	internal class RadFilterEntitySqlIsNullExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600977A RID: 38778 RVA: 0x0021F770 File Offset: 0x0021D970
		public override RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			string expressionFormat = "it.{0} IS null";
			return new RadFilterEvaluationData(expression, new ArrayList(1)
			{
				expression.FieldName
			}, expressionFormat);
		}
	}
}
