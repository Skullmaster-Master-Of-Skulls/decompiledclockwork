using System;
using System.Globalization;

namespace NLog.Conditions
{
	// Token: 0x0200003A RID: 58
	internal sealed class ConditionRelationalExpression : ConditionExpression
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004074 File Offset: 0x00002274
		public ConditionRelationalExpression(ConditionExpression leftExpression, ConditionExpression rightExpression, ConditionRelationalOperator relationalOperator)
		{
			this.LeftExpression = leftExpression;
			this.RightExpression = rightExpression;
			this.RelationalOperator = relationalOperator;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004091 File Offset: 0x00002291
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004099 File Offset: 0x00002299
		public ConditionExpression LeftExpression { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000040A2 File Offset: 0x000022A2
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000040AA File Offset: 0x000022AA
		public ConditionExpression RightExpression { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000040B3 File Offset: 0x000022B3
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000040BB File Offset: 0x000022BB
		public ConditionRelationalOperator RelationalOperator { get; private set; }

		// Token: 0x06000108 RID: 264 RVA: 0x000040C4 File Offset: 0x000022C4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.LeftExpression,
				" ",
				this.GetOperatorString(),
				" ",
				this.RightExpression,
				")"
			});
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000411C File Offset: 0x0000231C
		protected override object EvaluateNode(LogEventInfo context)
		{
			object leftValue = this.LeftExpression.Evaluate(context);
			object rightValue = this.RightExpression.Evaluate(context);
			return ConditionRelationalExpression.Compare(leftValue, rightValue, this.RelationalOperator);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004150 File Offset: 0x00002350
		private static object Compare(object leftValue, object rightValue, ConditionRelationalOperator relationalOperator)
		{
			StringComparer invariantCulture = StringComparer.InvariantCulture;
			ConditionRelationalExpression.PromoteTypes(ref leftValue, ref rightValue);
			switch (relationalOperator)
			{
			case ConditionRelationalOperator.Equal:
				return invariantCulture.Compare(leftValue, rightValue) == 0;
			case ConditionRelationalOperator.NotEqual:
				return invariantCulture.Compare(leftValue, rightValue) != 0;
			case ConditionRelationalOperator.Less:
				return invariantCulture.Compare(leftValue, rightValue) < 0;
			case ConditionRelationalOperator.Greater:
				return invariantCulture.Compare(leftValue, rightValue) > 0;
			case ConditionRelationalOperator.LessOrEqual:
				return invariantCulture.Compare(leftValue, rightValue) <= 0;
			case ConditionRelationalOperator.GreaterOrEqual:
				return invariantCulture.Compare(leftValue, rightValue) >= 0;
			default:
				throw new NotSupportedException("Relational operator " + relationalOperator + " is not supported.");
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004218 File Offset: 0x00002418
		private static void PromoteTypes(ref object val1, ref object val2)
		{
			if (val1 == null || val2 == null)
			{
				return;
			}
			if (val1.GetType() == val2.GetType())
			{
				return;
			}
			if (val1 is DateTime || val2 is DateTime)
			{
				val1 = Convert.ToDateTime(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToDateTime(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is string || val2 is string)
			{
				val1 = Convert.ToString(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToString(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is double || val2 is double)
			{
				val1 = Convert.ToDouble(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToDouble(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is float || val2 is float)
			{
				val1 = Convert.ToSingle(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToSingle(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is decimal || val2 is decimal)
			{
				val1 = Convert.ToDecimal(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToDecimal(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is long || val2 is long)
			{
				val1 = Convert.ToInt64(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToInt64(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is int || val2 is int)
			{
				val1 = Convert.ToInt32(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToInt32(val2, CultureInfo.InvariantCulture);
				return;
			}
			if (val1 is bool || val2 is bool)
			{
				val1 = Convert.ToBoolean(val1, CultureInfo.InvariantCulture);
				val2 = Convert.ToBoolean(val2, CultureInfo.InvariantCulture);
				return;
			}
			throw new ConditionEvaluationException(string.Concat(new string[]
			{
				"Cannot find common type for '",
				val1.GetType().Name,
				"' and '",
				val2.GetType().Name,
				"'."
			}));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000444C File Offset: 0x0000264C
		private string GetOperatorString()
		{
			switch (this.RelationalOperator)
			{
			case ConditionRelationalOperator.Equal:
				return "==";
			case ConditionRelationalOperator.NotEqual:
				return "!=";
			case ConditionRelationalOperator.Less:
				return "<";
			case ConditionRelationalOperator.Greater:
				return ">";
			case ConditionRelationalOperator.LessOrEqual:
				return "<=";
			case ConditionRelationalOperator.GreaterOrEqual:
				return ">=";
			default:
				throw new NotSupportedException("Relational operator " + this.RelationalOperator + " is not supported.");
			}
		}
	}
}
