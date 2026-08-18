using System;

namespace NLog.Conditions
{
	// Token: 0x02000037 RID: 55
	internal sealed class ConditionOrExpression : ConditionExpression
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000398B File Offset: 0x00001B8B
		public ConditionOrExpression(ConditionExpression left, ConditionExpression right)
		{
			this.LeftExpression = left;
			this.RightExpression = right;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000039A1 File Offset: 0x00001BA1
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000039A9 File Offset: 0x00001BA9
		public ConditionExpression LeftExpression { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000039B2 File Offset: 0x00001BB2
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000039BA File Offset: 0x00001BBA
		public ConditionExpression RightExpression { get; private set; }

		// Token: 0x060000EE RID: 238 RVA: 0x000039C4 File Offset: 0x00001BC4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.LeftExpression,
				" or ",
				this.RightExpression,
				")"
			});
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003A08 File Offset: 0x00001C08
		protected override object EvaluateNode(LogEventInfo context)
		{
			bool flag = (bool)this.LeftExpression.Evaluate(context);
			if (flag)
			{
				return ConditionOrExpression.boxedTrue;
			}
			bool flag2 = (bool)this.RightExpression.Evaluate(context);
			if (flag2)
			{
				return ConditionOrExpression.boxedTrue;
			}
			return ConditionOrExpression.boxedFalse;
		}

		// Token: 0x0400003A RID: 58
		private static readonly object boxedFalse = false;

		// Token: 0x0400003B RID: 59
		private static readonly object boxedTrue = true;
	}
}
