using System;

namespace NLog.Conditions
{
	// Token: 0x02000030 RID: 48
	internal sealed class ConditionMessageExpression : ConditionExpression
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000035E2 File Offset: 0x000017E2
		public override string ToString()
		{
			return "message";
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000035E9 File Offset: 0x000017E9
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.FormattedMessage;
		}
	}
}
