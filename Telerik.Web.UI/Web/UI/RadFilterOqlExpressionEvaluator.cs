using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018B0 RID: 6320
	public abstract class RadFilterOqlExpressionEvaluator : RadFilterDynamicLinqExpressionEvaluator
	{
		// Token: 0x0600F497 RID: 62615 RVA: 0x00378EE8 File Offset: 0x003770E8
		public new static RadFilterDynamicLinqExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterDynamicLinqExpressionEvaluator radFilterDynamicLinqExpressionEvaluator;
			switch (function)
			{
			case RadFilterFunction.Contains:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlContainsExpressionEvaluator();
				break;
			case RadFilterFunction.DoesNotContain:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlDoesNotContainExpressionEvaluator();
				break;
			case RadFilterFunction.StartsWith:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlStartsWithToExpressionEvaluator();
				break;
			case RadFilterFunction.EndsWith:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlEndsWithToExpressionEvaluator();
				break;
			case RadFilterFunction.EqualTo:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.NotEqualTo:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlNotEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.GreaterThan:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlGreaterThanExpressionEvaluator();
				break;
			case RadFilterFunction.LessThan:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlLessThanExpressionEvaluator();
				break;
			case RadFilterFunction.GreaterThanOrEqualTo:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlGreaterThanOrEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.LessThanOrEqualTo:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlLessThanOrEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.Between:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlBetweenExpressionEvaluator();
				break;
			case RadFilterFunction.NotBetween:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlNotBetweenExpressionEvaluator();
				break;
			case RadFilterFunction.IsEmpty:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlIsEmptyExpressionEvaluator();
				break;
			case RadFilterFunction.NotIsEmpty:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlNotIsEmptyExpressionEvaluator();
				break;
			case RadFilterFunction.IsNull:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlIsNullExpressionEvaluator();
				break;
			case RadFilterFunction.NotIsNull:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlNotIsNullExpressionEvaluator();
				break;
			default:
				radFilterDynamicLinqExpressionEvaluator = new RadFilterOqlEqualToExpressionEvaluator();
				break;
			}
			radFilterDynamicLinqExpressionEvaluator.Formatter = new RadFilterOqlExpressionFormatter();
			return radFilterDynamicLinqExpressionEvaluator;
		}

		// Token: 0x0600F498 RID: 62616 RVA: 0x00378FD8 File Offset: 0x003771D8
		internal static string PrepareStartWithValue(string expression)
		{
			int length = expression.LastIndexOf("\"");
			return expression.Substring(0, length) + "*\"";
		}

		// Token: 0x0600F499 RID: 62617 RVA: 0x00379003 File Offset: 0x00377203
		internal static string PrepareEndWithValue(string expression)
		{
			return expression.Replace("LIKE \"", "LIKE \"*");
		}
	}
}
