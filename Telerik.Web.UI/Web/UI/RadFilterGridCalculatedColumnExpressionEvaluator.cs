using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018D0 RID: 6352
	public abstract class RadFilterGridCalculatedColumnExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F5A6 RID: 62886 RVA: 0x0037C3E4 File Offset: 0x0037A5E4
		public new static RadFilterDynamicLinqExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterDynamicLinqExpressionEvaluator evaluator = RadFilterDynamicLinqExpressionEvaluator.GetEvaluator(function);
			evaluator.Formatter = new RadFilterGridCalculatedColumnExpressionFormatter();
			return evaluator;
		}
	}
}
