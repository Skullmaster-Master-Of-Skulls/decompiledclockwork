using System;

namespace NLog.Conditions
{
	// Token: 0x0200002F RID: 47
	internal sealed class ConditionLoggerNameExpression : ConditionExpression
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000035CB File Offset: 0x000017CB
		public override string ToString()
		{
			return "logger";
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000035D2 File Offset: 0x000017D2
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.LoggerName;
		}
	}
}
