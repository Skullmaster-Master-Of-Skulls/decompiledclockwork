using System;
using NLog.Layouts;

namespace NLog.Conditions
{
	// Token: 0x0200002C RID: 44
	internal sealed class ConditionLayoutExpression : ConditionExpression
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003531 File Offset: 0x00001731
		public ConditionLayoutExpression(Layout layout)
		{
			this.Layout = layout;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003540 File Offset: 0x00001740
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003548 File Offset: 0x00001748
		public Layout Layout { get; private set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x00003551 File Offset: 0x00001751
		public override string ToString()
		{
			return this.Layout.ToString();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000355E File Offset: 0x0000175E
		protected override object EvaluateNode(LogEventInfo context)
		{
			return this.Layout.Render(context);
		}
	}
}
