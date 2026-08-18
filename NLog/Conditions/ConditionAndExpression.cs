using System;

namespace NLog.Conditions
{
	// Token: 0x0200002A RID: 42
	internal sealed class ConditionAndExpression : ConditionExpression
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00003430 File Offset: 0x00001630
		public ConditionAndExpression(ConditionExpression left, ConditionExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003446 File Offset: 0x00001646
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000344E File Offset: 0x0000164E
		public ConditionExpression Left { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003457 File Offset: 0x00001657
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000345F File Offset: 0x0000165F
		public ConditionExpression Right { get; private set; }

		// Token: 0x060000B8 RID: 184 RVA: 0x00003468 File Offset: 0x00001668
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.Left,
				" and ",
				this.Right,
				")"
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000034AC File Offset: 0x000016AC
		protected override object EvaluateNode(LogEventInfo context)
		{
			if (!(bool)this.Left.Evaluate(context))
			{
				return ConditionAndExpression.boxedFalse;
			}
			if (!(bool)this.Right.Evaluate(context))
			{
				return ConditionAndExpression.boxedFalse;
			}
			return ConditionAndExpression.boxedTrue;
		}

		// Token: 0x0400002E RID: 46
		private static readonly object boxedFalse = false;

		// Token: 0x0400002F RID: 47
		private static readonly object boxedTrue = true;
	}
}
