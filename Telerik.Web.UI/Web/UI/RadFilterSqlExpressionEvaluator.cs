using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020018E3 RID: 6371
	public abstract class RadFilterSqlExpressionEvaluator : RadFilterExpressionEvaluatorBase
	{
		// Token: 0x0600F5E6 RID: 62950 RVA: 0x0037CE1C File Offset: 0x0037B01C
		public static RadFilterSqlExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterSqlExpressionEvaluator result;
			switch (function)
			{
			case RadFilterFunction.Contains:
				result = new RadFilterSqlContainsExpressionEvaluator();
				break;
			case RadFilterFunction.DoesNotContain:
				result = new RadFilterSqlDoesNotContainExpressionEvaluator();
				break;
			case RadFilterFunction.StartsWith:
				result = new RadFilterSqlStartsWithExpressionEvaluator();
				break;
			case RadFilterFunction.EndsWith:
				result = new RadFilterSqlEndsWithExpressionEvaluator();
				break;
			case RadFilterFunction.EqualTo:
				result = new RadFilterSqlEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.NotEqualTo:
				result = new RadFilterSqlNotEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.GreaterThan:
				result = new RadFilterSqlGreaterThanExpressionEvaluator();
				break;
			case RadFilterFunction.LessThan:
				result = new RadFilterSqlLessThanExpressionEvaluator();
				break;
			case RadFilterFunction.GreaterThanOrEqualTo:
				result = new RadFilterSqlGreaterThanOrEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.LessThanOrEqualTo:
				result = new RadFilterSqlLessThanOrEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.Between:
				result = new RadFilterSqlBetweenExpressionEvaluator();
				break;
			case RadFilterFunction.NotBetween:
				result = new RadFilterSqlNotBetweenExpressionEvaluator();
				break;
			case RadFilterFunction.IsEmpty:
				result = new RadFilterSqlIsEmptyExpressionEvaluator();
				break;
			case RadFilterFunction.NotIsEmpty:
				result = new RadFilterSqlNotIsEmptyExpressionEvaluator();
				break;
			case RadFilterFunction.IsNull:
				result = new RadFilterSqlIsNullExpressionEvaluator();
				break;
			case RadFilterFunction.NotIsNull:
				result = new RadFilterSqlNotIsNullExpressionEvaluator();
				break;
			default:
				result = new RadFilterSqlEqualToExpressionEvaluator();
				break;
			}
			return result;
		}

		// Token: 0x0600F5E7 RID: 62951 RVA: 0x0037CF00 File Offset: 0x0037B100
		protected virtual ArrayList ExtractPlaceHolders(IRadFilterValueExpression valueExpression, string fieldName)
		{
			ArrayList arrayList = this.EnsureQoutes(valueExpression.Values);
			arrayList.Insert(0, fieldName);
			return arrayList;
		}

		// Token: 0x0600F5E8 RID: 62952 RVA: 0x0037CF24 File Offset: 0x0037B124
		protected bool ShouldAddQoutes(Type valueType)
		{
			return valueType == typeof(string) || valueType == typeof(char) || valueType == typeof(Guid) || valueType == typeof(DateTime) || valueType == typeof(TimeSpan) || valueType == typeof(bool) || valueType == typeof(object);
		}

		// Token: 0x0600F5E9 RID: 62953 RVA: 0x0037CFB4 File Offset: 0x0037B1B4
		protected ArrayList EnsureQoutes(ArrayList sourceList)
		{
			int i = 0;
			int count = sourceList.Count;
			while (i < count)
			{
				object obj = sourceList[i];
				if (obj is string)
				{
					sourceList[i] = obj.ToString().Replace("'", "''");
				}
				i++;
			}
			return sourceList;
		}
	}
}
