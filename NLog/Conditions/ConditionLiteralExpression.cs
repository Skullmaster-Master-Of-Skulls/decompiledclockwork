using System;
using System.Globalization;

namespace NLog.Conditions
{
	// Token: 0x0200002E RID: 46
	internal sealed class ConditionLiteralExpression : ConditionExpression
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00003583 File Offset: 0x00001783
		public ConditionLiteralExpression(object literalValue)
		{
			this.LiteralValue = literalValue;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003592 File Offset: 0x00001792
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000359A File Offset: 0x0000179A
		public object LiteralValue { get; private set; }

		// Token: 0x060000CA RID: 202 RVA: 0x000035A3 File Offset: 0x000017A3
		public override string ToString()
		{
			if (this.LiteralValue == null)
			{
				return "null";
			}
			return Convert.ToString(this.LiteralValue, CultureInfo.InvariantCulture);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000035C3 File Offset: 0x000017C3
		protected override object EvaluateNode(LogEventInfo context)
		{
			return this.LiteralValue;
		}
	}
}
