using System;

namespace NLog.Conditions
{
	// Token: 0x02000036 RID: 54
	internal sealed class ConditionNotExpression : ConditionExpression
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003939 File Offset: 0x00001B39
		public ConditionNotExpression(ConditionExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003948 File Offset: 0x00001B48
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003950 File Offset: 0x00001B50
		public ConditionExpression Expression { get; private set; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00003959 File Offset: 0x00001B59
		public override string ToString()
		{
			return "(not " + this.Expression + ")";
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003970 File Offset: 0x00001B70
		protected override object EvaluateNode(LogEventInfo context)
		{
			return !(bool)this.Expression.Evaluate(context);
		}
	}
}
