using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018CF RID: 6351
	public abstract class RadFilterGridBindableTypeExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F5A4 RID: 62884 RVA: 0x0037C388 File Offset: 0x0037A588
		public new static RadFilterDynamicLinqExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterDynamicLinqExpressionEvaluator radFilterDynamicLinqExpressionEvaluator = RadFilterDynamicLinqExpressionEvaluator.GetEvaluator(function);
			radFilterDynamicLinqExpressionEvaluator.Formatter = new RadFilterGridBindableTypeExpressionFormatter();
			if (function == RadFilterFunction.IsNull)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterGridBindableTypeIsNullExpressionEvaluator();
			}
			else if (function == RadFilterFunction.NotIsNull)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterGridBindableTypeNotIsNullExpressionEvaluator();
			}
			else if (function == RadFilterFunction.IsEmpty)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterGridBindableTypeIsEmptyExpressionEvaluator();
			}
			else if (function == RadFilterFunction.NotIsEmpty)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterGridBindableTypeNotIsEmptyExpressionEvaluator();
			}
			return radFilterDynamicLinqExpressionEvaluator;
		}
	}
}
