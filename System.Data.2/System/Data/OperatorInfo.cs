using System;

namespace System.Data
{
	// Token: 0x020000EF RID: 239
	internal sealed class OperatorInfo
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x0007E2F4 File Offset: 0x0007D6F4
		internal OperatorInfo(Nodes type, int op, int pri)
		{
			this.type = type;
			this.op = op;
			this.priority = pri;
		}

		// Token: 0x040004DD RID: 1245
		internal Nodes type;

		// Token: 0x040004DE RID: 1246
		internal int op;

		// Token: 0x040004DF RID: 1247
		internal int priority;
	}
}
