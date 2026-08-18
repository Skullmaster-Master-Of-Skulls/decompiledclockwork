using System;

namespace System.Data
{
	// Token: 0x020001AD RID: 429
	internal sealed class OperatorInfo
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x00256098 File Offset: 0x00255498
		internal OperatorInfo(Nodes type, int op, int pri)
		{
			this.type = type;
			this.op = op;
			this.priority = pri;
		}

		// Token: 0x04000DAE RID: 3502
		internal Nodes type;

		// Token: 0x04000DAF RID: 3503
		internal int op;

		// Token: 0x04000DB0 RID: 3504
		internal int priority;
	}
}
