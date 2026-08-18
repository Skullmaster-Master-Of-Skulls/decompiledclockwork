using System;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000050 RID: 80
	internal sealed class PathLiteralSubsegment : PathSubsegment
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public PathLiteralSubsegment(string literal)
		{
			this.Literal = literal;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00007B03 File Offset: 0x00005D03
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00007B0B File Offset: 0x00005D0B
		public string Literal { get; private set; }
	}
}
