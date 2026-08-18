using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200187B RID: 6267
	public class RadFilterEntitySqlQueryProvider : RadFilterDynamicLinqQueryProvider
	{
		// Token: 0x0600F2E4 RID: 62180 RVA: 0x00375071 File Offset: 0x00373271
		public RadFilterEntitySqlQueryProvider()
		{
		}

		// Token: 0x0600F2E5 RID: 62181 RVA: 0x00375079 File Offset: 0x00373279
		public RadFilterEntitySqlQueryProvider(IList<RadFilterFunction> supportedFilterFunctions, IList<RadFilterGroupOperation> supportedGroupOperations) : base(supportedFilterFunctions, supportedGroupOperations)
		{
		}

		// Token: 0x0600F2E6 RID: 62182 RVA: 0x00375084 File Offset: 0x00373284
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			RadFilterDynamicLinqExpressionEvaluator evaluator = RadFilterEntitySqlExpressionEvaluator.GetEvaluator(expression.FilterFunction);
			evaluator.OnExpressionEvaluated = base.OnExpressionEvaluated;
			return evaluator.Evaluate(expression);
		}
	}
}
