using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F70 RID: 3952
	public abstract class RadFilterDynamicLinqExpressionEvaluator : RadFilterExpressionEvaluatorBase
	{
		// Token: 0x17002FD8 RID: 12248
		// (get) Token: 0x06009775 RID: 38773 RVA: 0x0021F5F0 File Offset: 0x0021D7F0
		// (set) Token: 0x06009774 RID: 38772 RVA: 0x0021F5E7 File Offset: 0x0021D7E7
		internal virtual IRadFilterExpressionFormatter Formatter
		{
			get
			{
				if (this._formatter == null)
				{
					this._formatter = new RadFilterDLinqExpressionFormatter();
				}
				return this._formatter;
			}
			set
			{
				this._formatter = value;
			}
		}

		// Token: 0x06009776 RID: 38774 RVA: 0x0021F60C File Offset: 0x0021D80C
		public static RadFilterDynamicLinqExpressionEvaluator GetEvaluator(RadFilterFunction function)
		{
			RadFilterDynamicLinqExpressionEvaluator result;
			switch (function)
			{
			case RadFilterFunction.Contains:
				result = new RadFilterDynamicLinqContainsExpressionEvaluator();
				break;
			case RadFilterFunction.DoesNotContain:
				result = new RadFilterDynamicLinqDoesNotContainExpressionEvaluator();
				break;
			case RadFilterFunction.StartsWith:
				result = new RadFilterDynamicLinqStartsWithToExpressionEvaluator();
				break;
			case RadFilterFunction.EndsWith:
				result = new RadFilterDynamicLinqEndsWithToExpressionEvaluator();
				break;
			case RadFilterFunction.EqualTo:
				result = new RadFilterDynamicLinqEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.NotEqualTo:
				result = new RadFilterDynamicLinqNotEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.GreaterThan:
				result = new RadFilterDynamicLinqGreaterThanExpressionEvaluator();
				break;
			case RadFilterFunction.LessThan:
				result = new RadFilterDynamicLinqLessThanExpressionEvaluator();
				break;
			case RadFilterFunction.GreaterThanOrEqualTo:
				result = new RadFilterDynamicLinqGreaterThanOrEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.LessThanOrEqualTo:
				result = new RadFilterDynamicLinqLessThanOrEqualToExpressionEvaluator();
				break;
			case RadFilterFunction.Between:
				result = new RadFilterDynamicLinqBetweenExpressionEvaluator();
				break;
			case RadFilterFunction.NotBetween:
				result = new RadFilterDynamicLinqNotBetweenExpressionEvaluator();
				break;
			case RadFilterFunction.IsEmpty:
				result = new RadFilterDynamicLinqIsEmptyExpressionEvaluator();
				break;
			case RadFilterFunction.NotIsEmpty:
				result = new RadFilterDynamicLinqNotIsEmptyExpressionEvaluator();
				break;
			case RadFilterFunction.IsNull:
				result = new RadFilterDynamicLinqIsNullExpressionEvaluator();
				break;
			case RadFilterFunction.NotIsNull:
				result = new RadFilterDynamicLinqNotIsNullExpressionEvaluator();
				break;
			default:
				result = new RadFilterDynamicLinqEqualToExpressionEvaluator();
				break;
			}
			return result;
		}

		// Token: 0x06009777 RID: 38775 RVA: 0x0021F6EF File Offset: 0x0021D8EF
		protected string PrepareFieldName(RadFilterNonGroupExpression expression)
		{
			return this.Formatter.FormatFieldName(expression.FieldName, expression.FieldType, base.IsCaseSensitive);
		}

		// Token: 0x06009778 RID: 38776 RVA: 0x0021F710 File Offset: 0x0021D910
		protected RadFilterEvaluationData PrepareExpression(string expressionFormat, RadFilterNonGroupExpression expression)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.PrepareFieldName(expression));
			IRadFilterValueExpression radFilterValueExpression = expression as IRadFilterValueExpression;
			if (radFilterValueExpression != null)
			{
				arrayList.AddRange(this.Formatter.FormatFieldValue(radFilterValueExpression.Values, expression.FieldType, base.IsCaseSensitive));
			}
			return new RadFilterEvaluationData(expression, arrayList, expressionFormat);
		}

		// Token: 0x04002B4C RID: 11084
		private IRadFilterExpressionFormatter _formatter;
	}
}
