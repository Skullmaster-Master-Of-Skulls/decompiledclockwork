using System;

namespace NLog.Conditions
{
	// Token: 0x0200002D RID: 45
	internal sealed class ConditionLevelExpression : ConditionExpression
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x0000356C File Offset: 0x0000176C
		public override string ToString()
		{
			return "level";
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003573 File Offset: 0x00001773
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.Level;
		}
	}
}
