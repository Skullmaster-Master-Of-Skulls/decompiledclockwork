using System;

namespace System.Web.Http.Routing
{
	// Token: 0x0200010F RID: 271
	internal sealed class PathLiteralSubsegment : PathSubsegment
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00015DDC File Offset: 0x00013FDC
		public PathLiteralSubsegment(string literal)
		{
			this.Literal = literal;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00015DEB File Offset: 0x00013FEB
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00015DF3 File Offset: 0x00013FF3
		public string Literal { get; private set; }
	}
}
