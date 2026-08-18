using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200187A RID: 6266
	public abstract class RadFilterEntitySqlExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F2E0 RID: 62176 RVA: 0x00374FC0 File Offset: 0x003731C0
		public new static RadFilterDynamicLinqExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterDynamicLinqExpressionEvaluator radFilterDynamicLinqExpressionEvaluator;
			if (function == RadFilterFunction.Contains)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterEntitySqlContainsExpressionEvaluator();
			}
			else if (function == RadFilterFunction.DoesNotContain)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterEntitySqlDoesNotContainExpressionEvaluator();
			}
			else if (function == RadFilterFunction.StartsWith)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterEntitySqlStartsWithToExpressionEvaluator();
			}
			else if (function == RadFilterFunction.EndsWith)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterEntitySqlEndsWithToExpressionEvaluator();
			}
			else if (function == RadFilterFunction.IsNull)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterEntitySqlIsNullExpressionEvaluator();
			}
			else if (function == RadFilterFunction.NotIsNull)
			{
				radFilterDynamicLinqExpressionEvaluator = new RadFilterEntitySqlNotIsNullExpressionEvaluator();
			}
			else
			{
				radFilterDynamicLinqExpressionEvaluator = RadFilterDynamicLinqExpressionEvaluator.GetEvaluator(function);
			}
			radFilterDynamicLinqExpressionEvaluator.Formatter = new RadFilterEntitySqlExpressionFormatter();
			return radFilterDynamicLinqExpressionEvaluator;
		}

		// Token: 0x0600F2E1 RID: 62177 RVA: 0x0037502C File Offset: 0x0037322C
		internal static string PrepareStartWithValue(string expression)
		{
			int length = expression.LastIndexOf("\"");
			return expression.Substring(0, length) + "%\"";
		}

		// Token: 0x0600F2E2 RID: 62178 RVA: 0x00375057 File Offset: 0x00373257
		internal static string PrepareEndWithValue(string expression)
		{
			return expression.Replace("LIKE \"", "LIKE \"%");
		}
	}
}
