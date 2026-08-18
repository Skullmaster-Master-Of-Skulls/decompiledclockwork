using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018CE RID: 6350
	public abstract class RadFilterLinqRowExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F5A2 RID: 62882 RVA: 0x0037C32C File Offset: 0x0037A52C
		public new static RadFilterDynamicLinqExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterDynamicLinqExpressionEvaluator radFilterDynamicLinqExpressionEvaluator = RadFilterDynamicLinqExpressionEvaluator.GetEvaluator(function);
			radFilterDynamicLinqExpressionEvaluator.Formatter = new RadFilterLinqRowExpressionFormatter();
			if (function == RadFilterFunction.IsNull)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterLinqRowIsNullExpressionEvaluator();
			}
			else if (function == RadFilterFunction.NotIsNull)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterLinqRowNotIsNullExpressionEvaluator();
			}
			else if (function == RadFilterFunction.IsEmpty)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterLinqRowIsEmptyExpressionEvaluator();
			}
			else if (function == RadFilterFunction.NotIsEmpty)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterLinqRowNotIsEmptyExpressionEvaluator();
			}
			return radFilterDynamicLinqExpressionEvaluator;
		}
	}
}
