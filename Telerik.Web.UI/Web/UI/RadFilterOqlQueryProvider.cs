using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020018B9 RID: 6329
	public class RadFilterOqlQueryProvider : RadFilterDynamicLinqQueryProvider
	{
		// Token: 0x0600F4AB RID: 62635 RVA: 0x0037913F File Offset: 0x0037733F
		public RadFilterOqlQueryProvider()
		{
		}

		// Token: 0x0600F4AC RID: 62636 RVA: 0x00379147 File Offset: 0x00377347
		public RadFilterOqlQueryProvider(IList<RadFilterFunction> supportedFilterFunctions, IList<RadFilterGroupOperation> supportedGroupOperations) : base(supportedFilterFunctions, supportedGroupOperations)
		{
		}

		// Token: 0x0600F4AD RID: 62637 RVA: 0x00379154 File Offset: 0x00377354
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			RadFilterDynamicLinqExpressionEvaluator evaluator = RadFilterOqlExpressionEvaluator.GetEvaluator(expression.FilterFunction);
			evaluator.OnExpressionEvaluated = base.OnExpressionEvaluated;
			return evaluator.Evaluate(expression);
		}
	}
}
