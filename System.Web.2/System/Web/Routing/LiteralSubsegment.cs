using System;

namespace System.Web.Routing
{
	// Token: 0x02000142 RID: 322
	internal sealed class LiteralSubsegment : PathSubsegment
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x000369B8 File Offset: 0x00034BB8
		public LiteralSubsegment(string literal)
		{
			this.Literal = literal;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000369C7 File Offset: 0x00034BC7
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x000369CF File Offset: 0x00034BCF
		public string Literal { get; private set; }
	}
}
